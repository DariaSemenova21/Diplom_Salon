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
    /// Логика взаимодействия для WindowAdminServices.xaml
    /// </summary>
    public partial class WindowAdminServices : Window
    {
        SalonEntities1 db = new SalonEntities1();
        private String _search = "";
        public Services[] Services { get; set; }
        public WindowAdminServices()
        {
            InitializeComponent();
            GetData();

        }
        public void GetData()
        {
            Services = DataBase.Query(db, db =>
            {
                return db.Services.ToArray();
            });

            if (!String.IsNullOrEmpty(_search))
            {
                String search = _search.ToLower();
                Services = Services.Where(e => e.name.ToLower().Contains(search)).ToArray();
            }

            MainData.ClearValue(ListView.ItemsSourceProperty);
            MainData.ItemsSource = Services;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = App.ShowMessage("Вы уверены, что хотите удалить услугу?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;
            Int32 servicesId = Convert.ToInt32((sender as Button).Tag);
            Services services = db.Services.FirstOrDefault(emp => emp.Id == servicesId);

            db.Entry(services).State = EntityState.Deleted;
            db.Services.Remove(services);

            db.SaveChanges();
            GetData();
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            AddServices add = new AddServices(null, GetData);
            add.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void MainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainData.SelectedItem != null)
            {
                AddServices add = new AddServices(MainData.SelectedItem as Services, GetData);
                add.Owner = this;
                add.ShowDialog();
                MainData.SelectedItem = null;
            }
        }
    }
}
