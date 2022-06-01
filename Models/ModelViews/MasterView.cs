using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Models.ModelViews
{
    public class MasterView
    {
        public String EmployeeName { get; set; }
        public Int32 NumberOfEntries { get; set; }
        public Int32 NumberOfServices { get; set; }

        public MasterView(String employeeName, int numberOfEntries, int numberOfServices)
        {
            EmployeeName = employeeName;
            NumberOfEntries = numberOfEntries;
            NumberOfServices = numberOfServices;
        }
    }
}
