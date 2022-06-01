using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Draw;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using Microsoft.Win32;
using iText.IO.Image;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAdminEntries.xaml
    /// </summary>
    public partial class WindowAdminEntries : Window
    {
        SalonEntities1 db = new SalonEntities1();
        private String _search = "";
        public Entries[] Entries { get; set; }
        public WindowAdminEntries()
        {
            InitializeComponent();
            GetData();

        }
        public void GetData()
        {
            Employee[] employees = db.Employee.ToArray();
            Entries = DataBase.Query(db, db =>
            {
                return db.Entries.ToArray();
            });

            if (!String.IsNullOrEmpty(_search))
            {
                String search = _search.ToLower();
                Entries = Entries.Where(e => e.start_datetime.ToLongDateString().Contains(search)).ToArray();
            }

            MainData.ClearValue(ListView.ItemsSourceProperty);
            MainData.ItemsSource = Entries;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = App.ShowMessage("Вы уверены, что хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;


            Int32 entryId = Convert.ToInt32((sender as Button).Tag);
            Entries entries = db.Entries.FirstOrDefault(emp => emp.Id == entryId);

            db.Entry(entries).State = EntityState.Deleted;
            db.Entries.Remove(entries);
            db.SaveChanges();
            GetData();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            Entries entries = MainData.SelectedItem as Entries;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Files | *.pdf";

            if (sfd.ShowDialog() == false) return;

            Services[] services = db.EntryServicesRefs.Where(r => r.Id_entry == entries.Id).Select(s => s.Services).ToArray();

            PdfWriter writer = new PdfWriter(sfd.FileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            pdf.SetDefaultPageSize(PageSize.A5);

            PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\Arial.ttf");

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory.Create(@".\Resource\logo.jpg"))
                .SetTextAlignment(TextAlignment.CENTER);

            document.Add(img);

            Paragraph header = new Paragraph("Салон «Alexandra»")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20)
               .SetFont(font);

            document.Add(header);

            Paragraph check = new Paragraph("ЧЕК")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20)
               .SetFont(font);

            document.Add(check);

            LineSeparator ls = new LineSeparator(new SolidLine());

            document.Add(ls);

            Table table = new Table(2, false);
            table.SetMarginTop(10);
            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

            Cell header1 = new Cell(1, 1)
             .SetBackgroundColor(ColorConstants.GRAY)
             .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(font)
             .Add(new Paragraph("Услуга"));
            table.AddCell(header1);

            Cell header2 = new Cell(1, 2)
             .SetBackgroundColor(ColorConstants.GRAY)
             .SetTextAlignment(TextAlignment.CENTER)
             .Add(new Paragraph("Цена"))
             .SetFont(font);
            table.AddCell(header2);

            for (int i = 0; i < services.Length; i++)
            {
                Services service = services[i];

                Cell cell1 = new Cell(i + 1, 1)
                 .SetTextAlignment(TextAlignment.CENTER)
                 .Add(new Paragraph(service.name))
                 .SetFont(font);
                table.AddCell(cell1);

                Cell cell2 = new Cell(i + 1, 2)
                 .SetTextAlignment(TextAlignment.CENTER)
                 .Add(new Paragraph($"{service.price} руб."))
                 .SetFont(font);
                table.AddCell(cell2);
            }

            document.Add(table);

            Paragraph total = new Paragraph($"Итоговая цена: {entries.grand_total} руб.")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(16)
               .SetFont(font);
            document.Add(total);

            Employee adminEmployee = db.Employee.First(u => u.User.role == (Int32)Role.Admin);
            Paragraph admin = new Paragraph($"Сотрудник: {adminEmployee.FIO}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12)

               .SetFont(font);
            document.Add(admin);

            Paragraph date = new Paragraph($"Даты выдачи чека: {DateTime.Now.ToString("dd:MM:yyyy")}")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(12)
               .SetFont(font);

            document.Add(date);

            CheckButton.IsEnabled = false;

            document.Close();

            App.ShowMessage("Чек сохранен в формате PDF");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddEntryButton_Click(object sender, RoutedEventArgs e)
        {
           AddEntries add = new AddEntries(null, GetData);
            add.ShowDialog();
            CheckButton.IsEnabled = false;
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
            CheckButton.IsEnabled = false;
            GetData();
        }

        private void MainData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainData.SelectedItem != null)
            {
                Entries entry = MainData.SelectedItem as Entries;
                if (entry.end_datetime is null)
                {
                    AddEntries add = new AddEntries(entry, GetData);
                    add.Owner = this;
                    add.ShowDialog();
                    MainData.SelectedItem = null;
                    CheckButton.IsEnabled = false;
                }
                else
                {
                    CheckButton.IsEnabled = true;
                }
            }
        }
    }
}
