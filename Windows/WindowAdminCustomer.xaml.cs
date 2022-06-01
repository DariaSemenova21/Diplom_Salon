using ClosedXML.Excel;
using Microsoft.Win32;
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
    /// Логика взаимодействия для WindowAdminCustomer.xaml
    /// </summary>
    public partial class WindowAdminCustomer : Window
    {
        SalonEntities1 db = new SalonEntities1();
        private String _sort = "";
        private String _search = "";
        public Customer[] Customer { get; set; }
        public WindowAdminCustomer()
        {
            InitializeComponent();
            GetData();
           
        }
        public void GetData()
        {
            Customer = DataBase.Query(db, db =>
           {
             return db.Customer.ToArray();
           });

            if (!String.IsNullOrEmpty(_search))
            {
                String search = _search.ToLower();
                Customer = Customer.Where(e => e.FIO.ToLower().Contains(search) || e.phone.ToLower().Contains(search)).ToArray();
            } 

            if (!String.IsNullOrEmpty(_sort))
            {
                if (_sort == "nameDescending") Customer = Customer.OrderByDescending(e => e.FIO).ToArray();
                else Customer = Customer.OrderBy(e => e.FIO).ToArray();
            } 

            MainData.ClearValue(ListView.ItemsSourceProperty);
            MainData.ItemsSource = Customer;
        }
            private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer add = new AddCustomer(null, GetData);
            add.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = App.ShowMessage("Вы уверены, что хотите удалить клиента?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;


            Int32 customerId = Convert.ToInt32((sender as Button).Tag);
            Customer customer = db.Customer.FirstOrDefault(emp => emp.Id == customerId);
            User user = db.User.FirstOrDefault(u => u.Id == customer.Id_user);

            db.Entry(customer).State = EntityState.Deleted;
            db.Customer.Remove(customer);

            db.SaveChanges();

            if (user != null)
            {
                db.Entry(user).State = EntityState.Deleted;
                db.User.Remove(user);
                db.SaveChanges();
            }

            GetData();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExcButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files | *.xlsx";
            if (sfd.ShowDialog() == false) return;

            Customer[] customers = db.Customer.ToArray();

            XLWorkbook book = new XLWorkbook();
            IXLWorksheet sheet = book.Worksheets.Add("Задолженности");

            // Title
            sheet.Cell(2, 2).Value = "Клиенты";
            sheet.Range(2, 2, 2, 6).Merge().AddToNamed("Title");

            // Headers
            sheet.Cell(3, 2).Value = "ФИО";
            sheet.Cell(3, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 2).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);
            sheet.Cell(3, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 2).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);

            sheet.Cell(3, 3).Value = "Адрес";
            sheet.Cell(3, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 3).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);
            sheet.Cell(3, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 3).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);

            sheet.Cell(3, 4).Value = "Постоянство";
            sheet.Cell(3, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 4).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);
            sheet.Cell(3, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 4).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);

            sheet.Cell(3, 5).Value = "Кол-во записей";
            sheet.Cell(3, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 5).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);
            sheet.Cell(3, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 5).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);

            sheet.Cell(3, 6).Value = "Телефон";
            sheet.Cell(3, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sheet.Cell(3, 6).Style.Border.RightBorderColor = XLColor.FromArgb(41, 90, 43);

            // Data
            for (int i = 0; i < customers.Length; i++)
            {
                Int32 row = 4 + i;

                Customer customer = customers[i];
                Entries[] entries = db.Entries.Where(u => u.Id_customer == customer.Id).ToArray();

                sheet.Cell(row, 2).Value = customer.FIO;
                sheet.Cell(row, 3).Value = customer.address;
                sheet.Cell(row, 4).Value = customer.regular_customer;
                sheet.Cell(row, 5).Value = entries.Length;
                sheet.Cell(row, 6).Value = customer.phone;
            }

            #region Styles

            IXLStyle titlesStyle = book.NamedRanges.NamedRange("Title").Ranges.Style;
            titlesStyle.Font.Bold = true;
            titlesStyle.Font.FontColor = XLColor.White;
            titlesStyle.Fill.BackgroundColor = XLColor.FromArgb(41, 90, 43);

            sheet.Rows().Style.Font.FontSize = 18;
            sheet.Rows().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            #endregion Styles

            sheet.Columns().AdjustToContents();
            sheet.PageSetup.CenterHorizontally = true;
            sheet.PageSetup.Margins.Left = 0.1;
            sheet.PageSetup.Margins.Right = 0.1;

            book.SaveAs(sfd.FileName);

            App.ShowMessage("Файл импортирован");
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

        private void MainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainData.SelectedItem != null)
            {
                AddCustomer add = new AddCustomer(MainData.SelectedItem as Customer, GetData);
                add.Owner = this;
                add.ShowDialog();
                MainData.SelectedItem = null;
            }
        }
    }
}
