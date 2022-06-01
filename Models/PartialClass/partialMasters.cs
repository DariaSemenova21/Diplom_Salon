using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace Salon.Models
{
    public partial class Employee
    {
        public SolidColorBrush ColorFrame { get; set; } = new SolidColorBrush(Colors.LightGray);
        public byte[] Photo
        {
            get
            {
                if (photo == null || photo.Length == 0) return new Byte[] { };

                if (File.Exists($"{photo.Substring(1)}"))
                {
                    return File.ReadAllBytes($"{photo.Substring(1)}");
                }
                if (File.Exists($"masters/picture.png"))
                {
                    return File.ReadAllBytes("masters\\picture.png");
                }
                return null;
            }
        }

        public override string ToString()
        {
            return FIO;
        }

        public Double Rating
        {
            get
            {
                return Math.Round(MasterRatings.Count == 0 ? 0 : MasterRatings.Average(r => r.rating)); // линкью́
            }
        }
    }
}

