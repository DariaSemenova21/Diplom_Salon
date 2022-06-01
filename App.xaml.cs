using Salon.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Role? CurrentRole = null;
        public static Object Human = null;

        public static Customer GetCustomer() => Human as Customer;
        public static Employee GetEmployee() => Human as Employee;


        public static MessageBoxResult ShowMessage(String message, String title = "Сообщение", MessageBoxButton button  = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.Asterisk)
        {
            return MessageBox.Show(message, title, button, image );
        }
    }
}
