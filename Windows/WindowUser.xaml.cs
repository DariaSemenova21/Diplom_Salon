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
using System.Windows.Threading;
using Salon.Models;
using Salon.Windows;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        SalonEntities1 db = new SalonEntities1();
        public WindowUser()
        {
            InitializeComponent();
            App.Current.Dispatcher.UnhandledException += ExceptionFilter;
        }

        private void ExceptionFilter(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            App.ShowMessage("Произошла неизвестная ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(LoginTextBox.Text) ||
               String.IsNullOrEmpty(PasswordTextBox.Password))
            {
                MessageBox.Show("Ошибка, одно или оба поле пустые");
                return;
            }

            User user = db.User.Where(x => x.login == LoginTextBox.Text && x.password == PasswordTextBox.Password).SingleOrDefault();

            if (user == null)
            {
                MessageBox.Show("Логин или пароль неверны");
                return;
            }
            Object human = null;

            switch (Enum.Parse(typeof(Role), user.role.ToString()))
            {
                case Role.Admin:
                    human = db.Employee.FirstOrDefault(emp => emp.Id_user == user.Id);
                    break;

                case Role.Customer:
                    human = db.Customer.FirstOrDefault(c => c.Id_user == user.Id);
                    break;

                case Role.Master:
                    human = db.Employee.FirstOrDefault(emp => emp.Id_user == user.Id);
                    break;
            }

            App.CurrentRole = (Role)user.role;
            App.Human = human;

            switch (Enum.Parse(typeof(Role), user.role.ToString()))
            {
                case Role.Admin:

                    human = db.Employee.FirstOrDefault(emp => emp.Id_user == user.Id);

                    WindowAdmin window = new WindowAdmin();
                    window.Show();
                    break;

                case Role.Customer:

                    human = db.Customer.FirstOrDefault(c => c.Id_user == user.Id);

                    WindowCustomer window1 = new WindowCustomer();
                    window1.Show();
                    break;

                case Role.Master:

                    human = db.Employee.FirstOrDefault(emp => emp.Id_user == user.Id);

                    WindowMaster window2 = new WindowMaster();
                    window2.Show();
                    break;
            }

            this.Close();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            WindowReg window3 = new WindowReg();
            window3.ShowDialog();
        }
    }
}
