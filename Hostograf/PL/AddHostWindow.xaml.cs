using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tester;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddHostWindow.xaml
    /// </summary>
    public partial class AddHostWindow : Window
    {
        //public ObservableHost obHost { get; private set; }
        private ObservableHost obHost;

        public AddHostWindow()
        {
            obHost = new ObservableHost();
            InitializeComponent();
            //lstTestCollection.ItemsSource = obHost.observableSetOfTest;
        }


        public AddHostWindow(ObservableHost host)
        {

            obHost = new ObservableHost(host);
           InitializeComponent();

           txtDescription.Text = host.Description;
           foreach (ObservableTestFactory test in obHost.ObservableTestCollection)
               lstTestCollection.Items.Add(test);

        }

        void commandDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            while (lstTestCollection.SelectedItems.Count > 0)
            {
                //obHost.TestCollection.Remove(lstTestCollection.SelectedItems[0] as TestFactory);
                obHost.RemoveTestElement(lstTestCollection.SelectedItems[0] as TestFactory);
                lstTestCollection.Items.Remove(lstTestCollection.SelectedItems[0]);
            }
           
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
                MessageBox.Show("Please, enter Description", "Empty textbox", MessageBoxButton.OK);
            else
            {
                obHost.Description = txtDescription.Text;
                DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnICMP_Click(object sender, RoutedEventArgs e)
        {
            AddICMPTestWindow icmpTestWindow = new AddICMPTestWindow();
            if (icmpTestWindow.ShowDialog() == true)
            {
                var address = icmpTestWindow.txtAddress.Text;
                TestFactory test = new TestFactory_ICMP(Guid.NewGuid(), address);
                //obHost.TestCollection.Add(test);
                obHost.AddTestElement(test);
                lstTestCollection.Items.Add(test);

            }

        }

        private void btnTCP_Click(object sender, RoutedEventArgs e)
        {
            AddTCPTestWindow tcpTestWindow = new AddTCPTestWindow();
            if (tcpTestWindow.ShowDialog() == true)
            {
                var address = tcpTestWindow.txtAddress.Text;
                var port = tcpTestWindow.txtPort.Text;
                TestFactory test = new TestFactory_TCP(Guid.NewGuid(), address, port);
                //obHost.TestCollection.Add(test);
                obHost.AddTestElement(test);
                lstTestCollection.Items.Add(test);
            }
        }

        private void btnDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            commandDelete_Executed(this, null);
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    btnOK_Click(this, e);
                    break;
                case Key.Escape:
                    btnCancel_Click(this, e);
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtDescription);
        }

        internal Host GetHost()
        {
            return obHost;
        }
    }
}
