using Salon.Models;
using Salon.Tools;
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
using System.Windows.Shapes;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowRating.xaml
    /// </summary>
    public partial class WindowRating : Window
    {
        SalonEntities1 db = new SalonEntities1();

        private readonly BitmapImage _star;
        private readonly BitmapImage _selectedStar;
        private readonly Boolean[] Stars = new Boolean[] { false, false, false, false, false };
        private readonly Int32 _employeeId;
        private readonly Action _callback;

        public WindowRating(Int32 employeeId, Action callback)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _callback = callback;

            this.DataContext = this;

            Image img = new Image();
            Byte[] star;

            using (FileStream fs = new FileStream("Resource/selectedStar.png", FileMode.Open))
            {
                star = new byte[fs.Length];
                fs.Read(star, 0, star.Length);
            }

            MemoryStream ms = new MemoryStream(star);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();

            _selectedStar = image;

            using (FileStream fs = new FileStream("Resource/star.png", FileMode.Open))
            {
                star = new byte[fs.Length];
                fs.Read(star, 0, star.Length);
            }

            ms = new MemoryStream(star);

            image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();

            _star = image;

            UpdateStars();

        }

        public void UpdateStars()
        {
            starsListBox.Items.Clear();

            for (int i = 0; i < Stars.Length; i++)
            {
                Image img = new Image();
                img.Source = Stars[i] ? _selectedStar : _star;
                img.Width = 30;
                img.Height = 30;
                img.Tag = i + 1;

                starsListBox.Items.Add(img);
            }
        }

        private void starsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Image selected = (sender as ListBox).SelectedItem as Image;
            if (selected is null) return;

            Int32 starsCount = (Int32) selected.Tag;
            for (int i = 0; i < starsCount; i++)
            {
                Stars[i] = true;
            }

            for (int i = 4; i >= starsCount; i--)
            {
                Stars[i] = false;
            }

            starsListBox.SelectedItem = null;
            starsListBox.SelectedIndex = -1;

            UpdateStars();
        }

        private void RatingButton_Click(object sender, RoutedEventArgs e)
        {
            MasterRatings rating = new MasterRatings();
            rating.rating = Stars.Where(s => s).Count();
            rating.Id_master = _employeeId;
            rating.date = DateTime.Now;

            DataBase.Query(db, db =>
            {
                db.Entry(rating).State = EntityState.Added;
                db.MasterRatings.Add(rating);
                db.SaveChanges();
            });

            _callback();

            Close();
        }
    }
}
