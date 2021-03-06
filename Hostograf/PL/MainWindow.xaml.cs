﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hardcodet.Wpf.TaskbarNotification;
using Tester;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        private BackgroundWorker backgroundWorker;
        private TrulyObservableCollection<ObservableHost> hosts = new TrulyObservableCollection<ObservableHost>();
        DBController dbController = new DBController();
        TaskbarIcon notifyIcon = new TaskbarIcon();


        public MainWindow()
        {
          var hostsfromdb = new DBController().GetHosts();
          foreach (var h in hostsfromdb)
            hosts.Add(new ObservableHost(h));
         
          InitializeComponent();

          //Uncomment in the future //notifyIcon = (TaskbarIcon)FindResource("NotifyIconOk");
          notifyIcon.Icon = Properties.Resources.green_hat;
          notifyIcon.Visibility = Visibility.Visible;
          notifyIcon.ToolTipText = Properties.Resources.NotifyIconString_Default;

          lblStatus.Content = Properties.Resources.StatusString_Default;
          backgroundWorker = (BackgroundWorker)this.FindResource("backgroundWoker");
          trwHosts.ItemsSource = hosts;
   
       }

    
       private object block = new object();
       private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
           BackgroundWorker worker = sender as BackgroundWorker;
           bool testCountGreaterThanZero = true; //For first iteration must be true

            while (!worker.CancellationPending && testCountGreaterThanZero)
                {
                    testCountGreaterThanZero = false;
                    lock (block)
                    {
                        bool alertNotifyIcon = false;
                        string errorHosts = Properties.Resources.NotifyIconString_Alert;

                        // Loop by Hosts
                        for (int i=0; i<hosts.Count && !worker.CancellationPending; i++)
                        {
                            var host = hosts[i];
                            if (!host.Enabled) continue;

                            #region Loop by tests
                            for (int j = 0; j < host.TestCollection.Count && !worker.CancellationPending; j++)
                            {
                                if (!host.TestCollection[j].Enabled) continue;

                                var test = host.ObservableTestCollection[j];
                                testCountGreaterThanZero = true;
                                
                                Dispatcher.BeginInvoke(new ThreadStart(delegate { test.ObservableCheckedNow = true; }));

                                if (test.Execute()) //Run test
                                {
                                    Dispatcher.BeginInvoke(
                                        new ThreadStart(delegate { lblStatus.Content = test + Properties.Resources.StatusString_Pass; }));
                                    Dispatcher.BeginInvoke(new ThreadStart(delegate { test.ObservablePass = true; }));
                                }

                                else
                                {
                                    Dispatcher.BeginInvoke(new ThreadStart(delegate { lblStatus.Content = test + Properties.Resources.StatusString_Fail; }));
                                    Dispatcher.BeginInvoke(new ThreadStart(delegate { test.ObservablePass = false; }));
                                    alertNotifyIcon = true;
                                    errorHosts += "\n" + host;
                                }

                                Thread.Sleep(1000);
                                Dispatcher.BeginInvoke(new ThreadStart(delegate { test.ObservableCheckedNow = false; }));
                            }
                            #endregion Loop by tests
                        }

                        
                        if (alertNotifyIcon)
                        {
                            Dispatcher.BeginInvoke(new ThreadStart(delegate { notifyIcon.Icon = Properties.Resources.red_hat; ; }));
                            Dispatcher.BeginInvoke(new ThreadStart(delegate { notifyIcon.ToolTipText = errorHosts; }));    
                        }
                            
                        else
                        {
                            Dispatcher.BeginInvoke(new ThreadStart(delegate { notifyIcon.Icon = Properties.Resources.green_hat; ; }));
                            Dispatcher.BeginInvoke(new ThreadStart(delegate { notifyIcon.ToolTipText = Properties.Resources.NotifyIconString_Default; }));
                        }

                    }
                
                }
          }


        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Content = Properties.Resources.StatusString_Default;
            btnStartStop.Content = FindResource("ImageStart");
        }


        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {

            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();

            }
            else
            {
                btnStartStop.Content = FindResource("ImagePause");
                backgroundWorker.RunWorkerAsync();
            }
        }


        private void btnAddHost_Click(object sender, RoutedEventArgs e)
        {
            AddHostWindow addHostWindow = new AddHostWindow();
            if (addHostWindow.ShowDialog() == true)
            {
                Host host = addHostWindow.GetHost();
                hosts.Add(new ObservableHost(host));
                dbController.AddOrUpdateHost(host);
            }
                
        }


        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (trwHosts.SelectedItem == null) return;

            var currentItem = trwHosts.SelectedItem;

            var host = currentItem as ObservableHost;
            if (host != null)
            {
                hosts.Remove(host);
                dbController.RemoveHost(host);
            }
            else
            {
                var test = currentItem as ObservableTestFactory;
                host = hosts.First(x => x.TestCollection.Contains(test.TestFactory));
                int index = hosts.IndexOf(host);
                hosts[index].RemoveTestElement(test.TestFactory);
                dbController.AddOrUpdateHost(host);
            }
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            if (trwHosts.SelectedItem == null) return;

            var currentItem = trwHosts.SelectedItem;

            var host = currentItem as ObservableHost;
            if (host == null)
            {
                var test = currentItem as ObservableTestFactory;
                host = hosts.First(x => x.TestCollection.Contains(test.TestFactory));
            }

            int index = hosts.IndexOf(host);

            AddHostWindow addHostWindow = new AddHostWindow(host);

            if (addHostWindow.ShowDialog() == true)
            {
              hosts[index] = new ObservableHost(addHostWindow.GetHost());
              dbController.AddOrUpdateHost(hosts[index]);
            }
           
        }


        private void btnEnabled_Click(object sender, RoutedEventArgs e)
        {
            if (trwHosts.SelectedItem == null) return;
            
            var currentItem = trwHosts.SelectedItem;

            var host = currentItem as ObservableHost;

            if (host != null)
                host.ObservableEnabled = !host.ObservableEnabled;
            else
            {
                var test = currentItem as ObservableTestFactory;
                if (test != null)
                {
                    test.ObservableEnabled = !test.ObservableEnabled;
                    host = hosts.First(x => x.TestCollection.Contains(test.TestFactory));
                }
            }
            dbController.AddOrUpdateHost(host);
        }
      

    }
}
