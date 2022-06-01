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
    /// Логика взаимодействия для AddServices.xaml
    /// </summary>
    public partial class AddServices : Window
    {
        SalonEntities1 db = new SalonEntities1();
        Services SpecificService;
        private readonly Action _callBack;
        public AddServices(Services services, Action callback)
        {

            InitializeComponent();
            SpecificService = services;
            _callBack = callback;

            if (SpecificService != null)
            {
                NameTextBox.Text = SpecificService.name;
                PriceTextBox.Text = SpecificService.price.ToString();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            String name = NameTextBox.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                App.ShowMessage("Введите наименование");
                return;
            }

            String price = PriceTextBox.Text;
            if (String.IsNullOrWhiteSpace(price))
            {
                App.ShowMessage("Введите цену");
                return;
            }
           
                if (SpecificService == null)
                {
                Services services = new Services();

                services.name = NameTextBox.Text;
                services.price = Convert.ToInt32(PriceTextBox.Text);

                db.Services.Add(services);
                db.SaveChanges();
                }                           
                else
                {
                Services services = db.Services.First(emp => emp.Id == SpecificService.Id);
                services.name = NameTextBox.Text;
                 services.price = Convert.ToInt32(PriceTextBox.Text);

                db.Entry(services).State = EntityState.Modified;
                db.SaveChanges();
                }

                App.ShowMessage("Сохранение прошло успешно");
            _callBack();
                Close();
        }

        private void PriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text[0])) e.Handled = true;
        }

        private void NameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text[0])) e.Handled = true;
        }
    }
}
