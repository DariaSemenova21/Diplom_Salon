using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Models.ModelViews
{
    public class ServiceView
    {
        public String ServiceName { get; set; }
        public Int32 NumberOfUse { get; set; }
        public Int32 TotalPrice { get; set; }

        public ServiceView(string serviceName, int numberOfUse, int totalPrice)
        {
            ServiceName = serviceName;
            NumberOfUse = numberOfUse;
            TotalPrice = totalPrice;
        }
    }
}
