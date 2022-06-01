using Microsoft.Win32;
using Salon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        SalonEntities1 db = new SalonEntities1();
        private readonly Action _callBack;
        private byte[] newByteImage;
        string pathImage;

        string PathImage
        {
            get
            {
                return pathImage;
            }
            set
            {
                pathImage = value;
            }
        }
        string sPathImage
        {
            get
            {
                return pathImage.Substring(1);
            }
        }

        Employee Master;

        public AddEmployee(Employee employee, Action callback)
        {
            InitializeComponent();
            //вывод данных в текстовые поля
            _callBack = callback;
            Master = employee;

            if (Master != null)
            {
                FIOTextBox.Text = Master.FIO;
                PhoneTextBox.Text = Master.phone;
                AddressTextBox.Text = Master.address;
                EmailTextBox.Text = Master.email;

                LoginTextBox.Text = Master.User.login;
                PasswordBox.Password = Master.User.password;
                pathImage = Master.photo;

                MemoryStream ms = new MemoryStream(Master.Photo);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.EndInit();

                PhotoImageBox.Source = image;
                PhotoTextBox.Content = "";
            }
        }

        private void ImageChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string[] extensions = { ".jpg", ".bmp", ".png", ".jpeg" };
            if (ofd.ShowDialog() == true)
            {

                if (extensions.Contains(System.IO.Path.GetExtension(ofd.FileName)))
                {
                    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                    {
                        newByteImage = new byte[fs.Length];
                        fs.Read(newByteImage, 0, newByteImage.Length);
                    }

                    MemoryStream ms = new MemoryStream(newByteImage);

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.EndInit();

                    PhotoImageBox.Source = image;
                    PhotoTextBox.Content = "";

                    PathImage = $"/masters/{System.IO.Path.GetFileName(ofd.FileName)}";

                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    using (FileStream stream = new FileStream(PathImage.Substring(1), FileMode.Create)) encoder.Save(stream);
                }
                else
                {
                    MessageBox.Show("Выбранный файл не является фотографией", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            String fio = FIOTextBox.Text;
            if (String.IsNullOrWhiteSpace(fio))
            {
                App.ShowMessage("Введите ФИО");
                return;
            }

            String address = AddressTextBox.Text;
            if (String.IsNullOrWhiteSpace(address))
            {
                App.ShowMessage("Введите адрес");
                return;
            }

            String phone = PhoneTextBox.Text;
            if (String.IsNullOrWhiteSpace(phone))
            {
                App.ShowMessage("Введите телефон");
                return;
            }

            String email = EmailTextBox.Text;
            if (String.IsNullOrWhiteSpace(email))
            {
                App.ShowMessage("Введите email");
                return;
            }

            String login = LoginTextBox.Text;
            if (String.IsNullOrWhiteSpace(login))
            {
                App.ShowMessage("Введите логин");
                return;
            }

            String password = PasswordBox.Password;
            if (String.IsNullOrWhiteSpace(password))
            {
                App.ShowMessage("Введите пароль");
                return;
            }

            User existUser = db.User.FirstOrDefault(u => u.login == login);
            if (Master == null && existUser != null)
            {
                App.ShowMessage("Мастер с таким логином уже существует");
                return;
            }

            Image photo = PhotoImageBox;
            if (photo == null)
            {
                App.ShowMessage("Вставьте фотографию");
                return;
            }

            Int32 userId;
            if (Master == null)
            {
                User[] users = db.User.ToArray();
                userId = users.Last().Id + 1;

                User user = new User();
                user.login = login;
                user.password = password;
                user.role = (Int32)Role.Master;

                db.User.Add(user);
                db.SaveChanges();

                Employee master = new Employee();

                Position[] positions = db.Position.ToArray();
                master.Id_position = positions.First(p => p.position == "Мастер").Id;

                master.FIO = fio;
                master.address = address;
                master.email = email;
                master.phone = phone;
                master.photo = PathImage;
                master.Id_user = userId;

                db.Employee.Add(master);
                db.SaveChanges();
            }
            else
            {
                //сохранение редактирования
                User user = db.User.First(u => u.Id == Master.User.Id);
                userId = user.Id;
                user.login = login;
                user.password = password;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                Employee master = db.Employee.First(emp => emp.Id == Master.Id);
            
                master.FIO = fio;
                master.address = address;
                master.email = email;
                master.phone = phone;
                master.photo = PathImage;

                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
            }

            App.ShowMessage("Сохранение прошло успешно");
            _callBack();
            this.Close();
        }

        private void FIOTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text[0])) e.Handled = true;
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text[0])) e.Handled = true;
        }

        private void LoginTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text[0])) e.Handled = true;
        }

        private void PasswordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text[0])) e.Handled = true;
        }
    }
}
