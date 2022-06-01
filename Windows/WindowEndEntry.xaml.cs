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
    /// Логика взаимодействия для WindowEndEntry.xaml
    /// </summary>
    public partial class WindowEndEntry : Window
    {
        private readonly Action Callback;
        private readonly Int32 EntryId;
        private List<Services> _services = new List<Services>();
        public Int32 Total => _services.Select(s => s.price).Sum();

        SalonEntities1 db = new SalonEntities1();

        public WindowEndEntry(int entryId, Action callback)
        {
            InitializeComponent();
            Callback = callback;
            EntryId = entryId;

            DataContext = this;
        }

        private void RefreshServices()
        {
            ServicesData.ItemsSource = null;
            ServicesData.ItemsSource = _services;

            DataContext = null;
            DataContext = this;
        }

        private void AddService(Services service)
        {
            _services.Add(service);
        }

        private async void EndButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? time = MyTimePicker.Value;
            if (time == null)
            {
                App.ShowMessage("Выберите время окончания записи");
                return;
            }

            Entries entry = db.Entries.First(a => a.Id == EntryId);
            entry.end_datetime = time;
            entry.grand_total = Total;

            db.Entry(entry).State = EntityState.Modified;

            foreach (Services service in _services)
            {
                EntryServicesRefs itemRef = new EntryServicesRefs();
                itemRef.Id_entry = entry.Id;
                itemRef.Id_service = service.Id;

                db.EntryServicesRefs.Add(itemRef);
            }

            await db.SaveChangesAsync();

            Callback();
            this.Hide();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EndEntryAddService window = new EndEntryAddService(AddService, _services);
            window.ShowDialog();

            RefreshServices();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Services service = ServicesData.SelectedItem as Services;
            MessageBoxResult result = App.ShowMessage($"Вы уверены, что хотите удалить услугу {service.name}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            _services = _services.Where(s => s.Id != service.Id).ToList();
            RefreshServices();
        }

        private void ServicesData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Services service = ServicesData.SelectedItem as Services;
            if (service == null)
            {
                DeleteButton.IsEnabled = false;
                return;
            }

            DeleteButton.IsEnabled = true;
        }
    }
}
