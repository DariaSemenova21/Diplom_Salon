using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Models
{
   internal class CustomerViews
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string regular_customer { get; set; }
    }
}
