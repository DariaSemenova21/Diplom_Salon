using Salon.Models;
using Salon.Tools;
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
    /// Логика взаимодействия для WindowAdminEmployee.xaml
    /// </summary>
    public partial class WindowAdminEmployee : Window
    {
        private String _search = "";
        private String _sort = "";
        private Int32? _stars = null;

        SalonEntities1 db = new SalonEntities1();
        public WindowAdminEmployee()
        {
            InitializeComponent();
            GetData();            
        }

        private void GetData()
        {
            Employee[] employees = db.Employee.ToArray();
            Int32[] ids = employees.Select(e => e.Id_user).ToArray();
            User[] users = db.User.Where(u => ids.Contains(u.Id)).ToArray();

            employees = DataBase.Query(db, db =>
            {
                return db.Employee.AsEnumerable().Where(e => FilterEmployee(e, users)).ToArray();
            });

            if (_stars != null)
            {
                employees = employees.Where(e => e.Rating == _stars).ToArray();
            }

            if (!String.IsNullOrEmpty(_search))
            {
                String search = _search.ToLower();
                employees = employees.Where(e => e.FIO.ToLower().Contains(search) || e.phone.ToLower().Contains(search)).ToArray();
            }
          
            if (!String.IsNullOrEmpty(_sort))
            {
                if (_sort == "nameDescending") employees = employees.OrderByDescending(e => e.FIO).ToArray();
                else employees = employees.OrderBy(e => e.FIO).ToArray();
            }

           MainData.ClearValue(ListView.ItemsSourceProperty);
            MainData.ItemsSource = employees;
        }


        private Boolean FilterEmployee(Employee employee, User[] users)
        {
            User user = users.FirstOrDefault(u => u.Id == employee.Id_user);
            if (user is null || user.role != (Int32)Role.Master) return false;

            return true;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = App.ShowMessage("Вы уверены, что хотите удалить мастера?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            Int32 employeeId = Convert.ToInt32((sender as Button).Tag);
            Employee employee = db.Employee.FirstOrDefault(emp => emp.Id == employeeId);
            User user = db.User.FirstOrDefault(u => u.Id == employee.Id_user);

            db.Entry(employee).State = EntityState.Deleted;
            db.Employee.Remove(employee);

            db.SaveChanges();

            if (user != null)
            {
                db.Entry(user).State = EntityState.Deleted;
                db.User.Remove(user);
                db.SaveChanges();
            }

            GetData();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee add = new AddEmployee(null, GetData);
            add.ShowDialog();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchTextHolder.Visibility = Visibility.Hidden;
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SearchTextBox.Text)) searchTextHolder.Visibility = Visibility.Visible;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _search = (sender as TextBox).Text;
            GetData();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortTextHolder.Visibility = SortComboBox.SelectedItem == null ? Visibility.Visible : Visibility.Hidden;

            _sort = (SortComboBox.SelectedItem as ComboBoxItem).Tag.ToString();
            GetData();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem boxItem = FilterComboBox.SelectedItem as ComboBoxItem;
            Int32? tag;
            if (boxItem == null) tag = null;
            else tag = Convert.ToInt32(boxItem.Tag);
            if (tag == null || tag == -1)
            {
                _stars = null;
                FilterComboBox.SelectedIndex = -1;
                filterTextHolder.Visibility = Visibility.Visible;
            }
            else
            {
                _stars = tag;
                filterTextHolder.Visibility = Visibility.Hidden;
            }

            GetData();
        }

        private void MainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainData.SelectedItem != null)
            {
                AddEmployee add = new AddEmployee(MainData.SelectedItem as Employee, GetData);
                add.Owner = this;
                add.ShowDialog();
                MainData.SelectedItem = null;
            }
        }
    }
}
