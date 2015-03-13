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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddICMPTestWindow.xaml
    /// </summary>
    public partial class AddICMPTestWindow : Window
    {
        public AddICMPTestWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtAddress.Text))
                MessageBox.Show("Please, enter IP Address or Hostname", "Empty textbox", MessageBoxButton.OK);
            else DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtAddress);
        }

      

        private void Window_KeyDown(object sender, KeyEventArgs e)
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

      
    }
}
