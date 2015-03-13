using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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
        
        

        public MainWindow()
        {
          var hostsfromdb = new DBController().GetHosts();
          foreach (var h in hostsfromdb)
            hosts.Add(new ObservableHost(h));
         
          InitializeComponent();
            lblStatus.Content = "I Sleep";
            backgroundWorker = (BackgroundWorker)this.FindResource("backgroundWoker");
            trwHosts.ItemsSource = hosts;
   
       }

    
       private object block = new object();
       private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
    
                while (true)
                {
                    lock (block)
                    {
                        for (int i=0; i<hosts.Count; i++)
                        {

                            var host = hosts[i];
                            if (!host.Enabled) continue;

                            for (int j=0; j<host.TestCollection.Count; j++)
                            {
                                if (!host.TestCollection[j].Enabled) continue;

                                var test = host.TestCollection[j];
                                
                                if (worker.CancellationPending)
                                    return;

                                Dispatcher.BeginInvoke(new ThreadStart(delegate { test.CheckedNow = true; }));

                                if (test.Execute())
                                {
                                    Dispatcher.BeginInvoke(
                                        new ThreadStart(delegate { lblStatus.Content = test + ".... Pass"; }));
                                    Dispatcher.BeginInvoke(new ThreadStart(delegate { test.Pass = true; }));
                                }

                                else
                                {
                                    Dispatcher.BeginInvoke(new ThreadStart(delegate { lblStatus.Content = test + ".... Fail"; }));
                                    Dispatcher.BeginInvoke(new ThreadStart(delegate { test.Pass = false; }));
                                }
                                Thread.Sleep(1000);
                                Dispatcher.BeginInvoke(new ThreadStart(delegate { test.CheckedNow = false; }));
                            }
                        }
                    }
                }
          }


        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Content = "I Sleep";
            
        }


        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private bool _started = false;
        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
           // if (_started == false)
            if (!backgroundWorker.IsBusy)
            {
                _started = true;
                backgroundWorker.RunWorkerAsync(hosts);
            }
            else
            {
                _started = false;
                
                backgroundWorker.CancelAsync();
                //lblStatus.Content = "I Sleep";
            }
        }


   
        private void btnAddHost_Click(object sender, RoutedEventArgs e)
        {
            AddHostWindow addHostWindow = new AddHostWindow();
            if (addHostWindow.ShowDialog() == true)
            {
                Host host = addHostWindow.obHost;
                hosts.Add(new ObservableHost(host));
                DBController dbController = new DBController();
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
                DBController dbController = new DBController();
                dbController.RemoveHost(host);
            }
            else
            {
                var test = currentItem as TestFactory;
                host = hosts.First(x => x.TestCollection.Contains(test));
                int index = hosts.IndexOf(host);
                hosts[index].TestCollection.Remove(test);
                DBController dbController = new DBController();
                
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
                var test = currentItem as TestFactory;
                host = hosts.First(x => x.TestCollection.Contains(test));
            }

            int index = hosts.IndexOf(host);

            AddHostWindow addHostWindow = new AddHostWindow(host);

            if (addHostWindow.ShowDialog() == true)
            {
              hosts[index] = new ObservableHost(addHostWindow.obHost); //Из за этой строчки сворачивается дерево?
                //hosts[index].Description
                DBController dbController = new DBController();
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
                var test = currentItem as TestFactory;
                if (test != null)
                {
                    test.Enabled = !test.Enabled;
                    host = hosts.First(x => x.TestCollection.Contains(test));
                }
            }

            DBController dbController = new DBController();
            dbController.AddOrUpdateHost(host);
        }
      

    }
}
