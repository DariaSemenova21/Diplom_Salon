using Salon.Models;
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
    /// Логика взаимодействия для EndEntryAddService.xaml
    /// </summary>
    public partial class EndEntryAddService : Window
    {
        SalonEntities1 db = new SalonEntities1();
        private readonly Action<Services> _addAction;
        private readonly List<Services> _existsServices;

        public Int32 Price { get; set; }

        public EndEntryAddService(Action<Services> addAction, List<Services> existsServices)
        {
            InitializeComponent();
            _addAction = addAction;
            _existsServices = existsServices;

            Services[] services = db.Services.ToArray();
            ServicesBox.ItemsSource = services;

            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Services services = ServicesBox.SelectedItem as Services;
            if (services == null)
            {
                App.ShowMessage("Выберите услугу");
                return;
            }

            Services existService = _existsServices.FirstOrDefault(s => s.Id == services.Id);
            if (existService != null)
            {
                App.ShowMessage("Услуга уже добавлена в текущую запись");
                return;
            }

            _addAction(services);
            Close();
        }

        private void ServicesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Services services = ServicesBox.SelectedItem as Services;
            if (services == null)
            {
                Price = 0;
            }
            else
            {
                Price = services.price;
            }
            
            DataContext = null;
            DataContext = this;
        }
    }
}
