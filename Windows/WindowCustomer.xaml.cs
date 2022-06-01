using Salon.Models;
using Salon.Tools;
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

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowCustomer.xaml
    /// </summary>
    public partial class WindowCustomer : Window
    {
        SalonEntities1 db = new SalonEntities1();
        public Employee[] Employees { get; set; }

        public WindowCustomer()
        {
            InitializeComponent();
            GetData();
        }
        public void GetData()
        {
            Int32[] ids = db.Employee.Select(e => e.Id_user).ToArray();
            User[] users = db.User.Where(u => ids.Contains(u.Id)).ToArray();
            Employees = DataBase.Query(db, db =>
            {
                return db.Employee.AsEnumerable().Where(e => FilterEmployee(e, users)).ToArray();
            });

            MainData.ClearValue(ListView.ItemsSourceProperty);
            MainData.ItemsSource = Employees;
        }

        private Boolean FilterEmployee(Employee employee, User[] users)
        {
            User user = users.FirstOrDefault(u => u.Id == employee.Id_user);
            if (user is null || user.role != (Int32)Role.Master) return false;

            return true;
        }

        private void EntryButton_Click(object sender, RoutedEventArgs e)
        {
            Int32 masterId = (int)(sender as Button).Tag;

            WindowEnroll window2 = new WindowEnroll(masterId);
            window2.ShowDialog();
        }

        private void MainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = MainData.SelectedItem as Employee;
            if (employee is null) return;
            MainData.SelectedIndex = -1;

            WindowWorks window3 = new WindowWorks(employee.Id);
            window3.Show();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            WindowUser window1 = new WindowUser();
            window1.Show();

            Close();
        }

        private void EstimateButton_Click(object sender, RoutedEventArgs e)
        {
            Int32 employeeId = (Int32)(sender as Button).Tag;

            WindowRating window = new WindowRating(employeeId, GetData);
            window.Show();
        }
    }
}
