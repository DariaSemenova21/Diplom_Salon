using Salon.Models;
using Salon.Models.ModelViews;
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
    /// Логика взаимодействия для OthetWindow.xaml
    /// </summary>
    public partial class OthetWindow : Window
    {
        SalonEntities1 db = new SalonEntities1();
        Int32 TotalRevenueSum;
        ServiceView[] ServiceViews;

        public OthetWindow()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData(DateTime? startDate = null, DateTime? endDate = null)
        {
            DataBase.Query(db, db =>
            {
                Entries[] entries = db.Entries.Where(e => e.end_datetime != null).ToArray();
                if(startDate != null)
                {
                    entries = entries.Where(e => e.start_datetime >= startDate).ToArray();
                }
                if(endDate != null)
                {
                    entries = entries.Where(e => e.start_datetime <= endDate).ToArray();
                }

                Int32[] employeeIds = entries.Select(e => e.Id_employee).ToArray();
                Employee[] employees = db.Employee.Where(emp => employeeIds.Contains(emp.Id)).ToArray();

                Int32[] entryIds = entries.Select(e => e.Id).ToArray();
                EntryServicesRefs[] refs = db.EntryServicesRefs.Where(esr => entryIds.Any(e => e == esr.Id_entry)).ToArray();
                Services[] services = db.Services.ToArray();

                ServiceView[] serviceViews = services.Select(s => GetServiceView(s, refs)).ToArray();
                MasterView[] masterViews = employees.Select(e => new MasterView(e.FIO, entries.Where(en => en.Id_employee == e.Id).Count(), CalculateServicesCount(e, entries, refs, services))).ToArray();

                MastersList.ItemsSource = masterViews;
                ServicesList.ItemsSource = serviceViews;

                TotalRevenueSum = serviceViews.Select(e => e.TotalPrice).Sum();
                ServiceViews = serviceViews;

                TotalRevenue.Text = $"Объём выручки: {TotalRevenueSum}";
                Entries.Text = $"Всего записей: {entries.Length}";
                Clients.Text = $"Всего клиентов: {entries.Select(e => e.Id_customer).Distinct().Count()}";
            });
        }

        private Int32 CalculateServicesCount(Employee employee, Entries[] entries, EntryServicesRefs[] refs, Services[] services)
        {
            Entries[] employeeEntries = entries.Where(en => en.Id_employee == employee.Id).ToArray();

            Int32[] entryIds = employeeEntries.Select(en => en.Id).ToArray();

            EntryServicesRefs[] employeeRefs = refs.Where(r => entryIds.Contains(r.Id_entry)).ToArray();
            Int32[] serviceIds = employeeRefs.Select(er => er.Id_service).ToArray();
            Services[] employeeServices = services.Where(s => serviceIds.Contains(s.Id)).ToArray();

            return employeeServices.Length;
        }

        private ServiceView GetServiceView(Services services, EntryServicesRefs[] refs)
        {
            Int32 numberOfuse = refs.Where(r => r.Id_service == services.Id).Count();
            return new ServiceView(services.name, numberOfuse, numberOfuse * services.price);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (startDate.SelectedDate != null) ClearStartDate.IsEnabled = true;
            else ClearStartDate.IsEnabled = false;
            GetData(startDate.SelectedDate);
        }

        private void endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (endDate.SelectedDate != null) ClearEndDate.IsEnabled = true;
            else ClearEndDate.IsEnabled = false;
            GetData(endDate: endDate.SelectedDate);
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow window = new ChartWindow(TotalRevenueSum, ServiceViews);
            window.ShowDialog();
        }

        private void ClearStartDate_Click(object sender, RoutedEventArgs e)
        {
            startDate.SelectedDate = null;
        }

        private void ClearEndDate_Click(object sender, RoutedEventArgs e)
        {
            endDate.SelectedDate = null;
        }
    }
}
