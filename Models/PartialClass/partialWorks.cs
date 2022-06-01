using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace Salon.Models
{
    public partial class Works
    {
        public byte[] Works1
        {
            get
            {
                if (works == null || works.Length == 0) return new Byte[] { };

                if (File.Exists($"{works.Substring(1)}"))
                {
                    return File.ReadAllBytes($"{works.Substring(1)}");
                }
                if (File.Exists($"masters/picture.png"))
                {
                    return File.ReadAllBytes("masters\\picture.png");
                }
                return null;
            }
        }

        public Double height;
        public double Width
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
    }
}
