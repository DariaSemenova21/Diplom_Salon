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
using Xceed.Wpf.Toolkit;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowEnroll.xaml
    /// </summary>
    public partial class WindowEnroll : Window
    {
        public readonly Int32 MasterId;
        SalonEntities1 db = new SalonEntities1();

        public WindowEnroll(Int32 masterId)
        {
            InitializeComponent();
            MasterId = masterId;
        }

        private void EnrollButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = DatePicker.SelectedDate;
            if (date == null)
            {
                App.ShowMessage("Выберете дату записи");
                return;
            }

            DateTime? time = MyTimePicker.Value;
            if(time == null)
            {
                App.ShowMessage("Выберете время записи");
                return;
            }

            time = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, 0);

            Entries[] entries = db.Entries.Where(a =>
                a.start_datetime.Year == date.Value.Year
                && a.start_datetime.Month == date.Value.Month
                && a.start_datetime.Day == date.Value.Day
                && a.Id_employee == MasterId).ToArray();

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

            Entries entry = new Entries();
            entry.Id_customer = App.GetCustomer().Id;
            entry.Id_employee = MasterId;
            entry.start_datetime = time.Value;

            db.Entries.Add(entry);
            db.SaveChanges();

            App.ShowMessage("Запись добавлена");
            this.Hide();
        }
    }
}
