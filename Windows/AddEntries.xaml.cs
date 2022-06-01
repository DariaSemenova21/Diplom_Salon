using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using Microsoft.Win32;
using Salon.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEntries.xaml
    /// </summary>
    public partial class AddEntries : Window
    {
        SalonEntities1 db = new SalonEntities1();
        private readonly Action _callBack;
        Entries Entries;

        public AddEntries(Entries entry, Action callback)
        {
            InitializeComponent();
            _callBack = callback;
            Entries = entry;
            Customer[] customers = db.Customer.ToArray();
            Employee[] masters = db.Employee.Where(e => e.User.role == (Int32)Role.Master).ToArray();
            FIOComboBox.ItemsSource = customers;
            FIOComboBox1.ItemsSource = masters;

            if (Entries != null)
            {
                Customer selectedCustomer = customers.FirstOrDefault(c => c.Id == Entries.Id_customer);
                FIOComboBox.SelectedItem = selectedCustomer;
                Employee selectedEmployee = masters.FirstOrDefault(emp => emp.Id == Entries.Id_employee);
                FIOComboBox1.SelectedItem = selectedEmployee;
                DatePicker.SelectedDate = Entries.start_datetime;
                MyTimePicker.Value = Entries.start_datetime;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = DatePicker.SelectedDate;
            if (date == null)
            {
                App.ShowMessage("Выберете дату записи");
                return;
            }

            DateTime? time = MyTimePicker.Value;
            if (time == null)
            {
                App.ShowMessage("Выберете время записи");
                return;
            }

            Customer customer = FIOComboBox.SelectedItem as Customer;
            if(customer == null)
            {
                App.ShowMessage("Выберите клиента");
                return;
            }

            Employee master = FIOComboBox1.SelectedItem as Employee;
            if (master == null)
            {
                App.ShowMessage("Выберите мастера");
                return;
            }

            Entries[] entries = db.Entries.Where(a =>
                a.start_datetime.Year == date.Value.Year
                && a.start_datetime.Month == date.Value.Month
                && a.start_datetime.Day == date.Value.Day
                && a.Id_employee == master.Id).ToArray();

            Entries[] beforeEntries = entries.Where(a => a.start_datetime.TimeOfDay <= time.Value.TimeOfDay).ToArray();
            Entries lastBeforeEntry = beforeEntries.LastOrDefault();
            if (lastBeforeEntry != null)
            {

                TimeSpan beforedifferent = time.Value.TimeOfDay.Subtract(lastBeforeEntry.start_datetime.TimeOfDay);
                if (beforedifferent.Hours < 2)
                {
                    App.ShowMessage($"Время записи занято. Выберете время после {lastBeforeEntry.start_datetime.TimeOfDay.Add(new TimeSpan(2, 0, 0)).ToString(@"hh\:mm")}");
                    return;
                }
            }

            Entries[] afterEntries = entries.Where(a => a.start_datetime.TimeOfDay > time.Value.TimeOfDay).ToArray();
            Entries firstAfterEntry = afterEntries.FirstOrDefault();
            if (firstAfterEntry != null)
            {
                TimeSpan afterDifferent = firstAfterEntry.start_datetime.TimeOfDay.Subtract(time.Value.TimeOfDay);
                if (afterDifferent.Hours < 2)
                {
                    App.ShowMessage($"Время записи недоступно. Следующая запись возвожна с: {firstAfterEntry.start_datetime.TimeOfDay.Add(new TimeSpan(2, 0, 0)).ToString(@"hh\:mm")}");
                    return;
                }
            }
            if (Entries == null)
            {
                Entries entry = new Entries();
                entry.Id_customer = customer.Id;
                entry.Id_employee = master.Id;
                entry.start_datetime = time.Value;

                db.Entries.Add(entry);
                db.SaveChanges();
            }
            else
            {
                Entries entry = db.Entries.First(c => c.Id == Entries.Id);
                entry.Id_customer = customer.Id;
                entry.Id_employee = master.Id;
                entry.start_datetime = time.Value;

                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
            }
            App.ShowMessage("Сохранение прошло успешно");
            _callBack();
            Close();

        }
    }
}
