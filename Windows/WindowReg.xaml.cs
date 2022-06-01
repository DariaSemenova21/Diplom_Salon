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
using Salon.Models;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowReg.xaml
    /// </summary>
    public partial class WindowReg : Window
    {
        SalonEntities1 db = new SalonEntities1();
        public WindowReg()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            String fio = FIOTextBox.Text;
            if (String.IsNullOrWhiteSpace(fio))
            {
                App.ShowMessage("Введите ФИО");
                return;
            }   
            
            String address = AddressTextBox.Text;
            if (String.IsNullOrWhiteSpace(address))
            {
                App.ShowMessage("Введите адрес");
                return;
            } 

            String phone = PhoneTextBox.Text;
            if (String.IsNullOrWhiteSpace(phone))
            {
                App.ShowMessage("Введите телефон");
                return;
            }

            String login = LoginTextBox.Text;
            if (String.IsNullOrWhiteSpace(login))
            {
                App.ShowMessage("Введите логин");
                return;
            }

            String password = PasswordTextBox.Password;
            if (String.IsNullOrWhiteSpace(password))
            {
                App.ShowMessage("Введите пароль");
                return;
            }

            String repeatPassword = RepeatPasswordTextBox.Password;
            if (String.IsNullOrWhiteSpace(repeatPassword))
            {
                App.ShowMessage("Введите повторение пароля");
                return;
            }
            if (!password.Equals(repeatPassword))
            {
                App.ShowMessage("Пароль не сопвадает");
                return;
            }


            User existUser = db.User.FirstOrDefault(u => u.login == login);
            if (existUser != null)
            {
                App.ShowMessage("Пользователь с таким логином уже существует");
                return;
            }

            User[] users = db.User.ToArray();
            Int32 userId = users.Last().Id + 1;

            User user = new User();
            user.login = login;
            user.password = password;
            user.role = (Int32) Role.Customer;

            db.User.Add(user);
            db.SaveChanges();

            Customer customer = new Customer();
            customer.FIO = fio;
            customer.address = address;
            customer.phone = phone;
            customer.Id_user = userId;

            db.Customer.Add(customer);
            db.SaveChanges();

            App.ShowMessage("Регистрация прошла успешно");
            this.Close();
        }
    }
}
