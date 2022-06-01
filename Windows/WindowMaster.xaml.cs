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
using Salon.Tools;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowMaster.xaml
    /// </summary>
    public partial class WindowMaster : Window
    {
        public Entries[] Entries { get; set; }

        SalonEntities1 db = new SalonEntities1();

        public WindowMaster()
        {
            InitializeComponent();

            GetData();
        }

        public void GetData()
        {
            Employee employee = App.GetEmployee();
            Entries = DataBase.Query(db, db =>
           {
             return db.Entries.Where(i => i.Employee.Id == employee.Id).ToArray();
           });

            MainData.ClearValue(ListView.ItemsSourceProperty);
            MainData.ItemsSource = Entries;
        }
              
        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            int entryId = (int)(sender as Button).Tag;
            Entries entry = Entries.First(x => x.Id == entryId);

            TimeSpan different = DateTime.Now.Subtract(entry.start_datetime);
            if (different.Ticks < 0)
            {
                App.ShowMessage("Завершить можно только в дату записи, либо позже");
                return;
            }

            WindowEndEntry window = new WindowEndEntry(entryId, GetData);
            window.ShowDialog();
           
        }

        private void MainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            WindowUser window1 = new WindowUser();
            window1.Show();
            Close();
        }
    }
}
