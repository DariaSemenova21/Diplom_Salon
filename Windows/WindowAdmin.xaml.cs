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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        public WindowAdmin()
        {
            InitializeComponent();
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            WindowAdminCustomer window1 = new WindowAdminCustomer();
            window1.ShowDialog();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowAdminEmployee window2 = new WindowAdminEmployee();
            window2.ShowDialog();
        }

        private void EntriesButton_Click(object sender, RoutedEventArgs e)
        {
            WindowAdminEntries window3 = new WindowAdminEntries();
            window3.ShowDialog();
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
         WindowAdminServices window4 = new WindowAdminServices();
            window4.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            WindowUser window5 = new WindowUser();
            window5.Show();
            Close();
        }

        private void OthetButton_Click(object sender, RoutedEventArgs e)
        {
            OthetWindow window6 = new OthetWindow();
            window6.Show();
        }
    }
}
