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

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowWorks.xaml
    /// </summary>
    public partial class WindowWorks : Window
    {
        SalonEntities1 db = new SalonEntities1();
        private readonly Int32 _employeeId;
        public Works[] Works { get; set; }
        public WindowWorks(Int32 employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            GetData();

        }
        public void GetData()
        {
            Works = DataBase.Query(db, db =>
            {
                return db.Works.Where(w => w.Id_employee == _employeeId).ToArray();
            });

            foreach (Works works in Works)
            {
                works.Width = MainData.Width / 3;
            }

            MainData.ClearValue(ListView.ItemsSourceProperty);
            MainData.ItemsSource = Works;
        }
        private void MainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
