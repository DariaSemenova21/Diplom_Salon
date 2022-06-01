using Salon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        SalonEntities1 db = new SalonEntities1();
        Customer Customers;
        private readonly Action _callBack;

        public AddCustomer(Customer customer, Action callback)
        {
            InitializeComponent();
          _callBack = callback; 
            Customers = customer;
            
            if (Customers != null)
            {
                FIOTextBox.Text = Customers.FIO;
                PhoneTextBox.Text = Customers.phone;
                AddressTextBox.Text = Customers.address;

                LoginTextBox.Text = Customers.User.login;
                PasswordTextBox.Password = Customers.User.password;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
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

            User existUser = db.User.FirstOrDefault(u => u.login == login);
            if (Customers == null && existUser != null)
            {
                App.ShowMessage("Пользователь с таким логином уже существует");
                return;
            }

            Int32 userId;
            if (Customers == null)
            {
                User[] users = db.User.ToArray();
                userId = users.Last().Id + 1;

                User user = new User();
                user.login = login;
                user.password = password;
                user.role = (Int32)Role.Customer;

                db.User.Add(user);
                db.SaveChanges();

                Customer customers = new Customer();

                customers.FIO = fio;
                customers.address = address;
                customers.phone = phone;
                customers.Id_user = userId;

                db.Customer.Add(customers);
                db.SaveChanges();
            }
            else
            {
                User user = db.User.First(u => u.Id == Customers.User.Id);
                userId = user.Id;
                user.login = login;
                user.password = password;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                
                Customer customers = db.Customer.First(c => c.Id == Customers.Id);
                customers.FIO = fio;
                customers.address = address;
                customers.phone = phone;
                customers.Id_user = userId;

                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
            }

           
            App.ShowMessage("Сохранение прошло успешно");
            _callBack();
            Close();
        }

        private void FIOTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text[0])) e.Handled = true;
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           if (!Char.IsLetterOrDigit(e.Text[0])) e.Handled = true;
        }

        private void LoginTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text[0])) e.Handled = true;
        }

        private void PasswordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text[0])) e.Handled = true;
        }
    }
}
