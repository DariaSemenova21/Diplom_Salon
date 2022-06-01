using Salon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Tools
{
   public static class DataBase
    {
        public static void Query(SalonEntities1 db, Action<SalonEntities1> action)
        {
            db = new SalonEntities1();
            action(db);
        }
        public static T Query<T>(SalonEntities1 db, Func<SalonEntities1, T> action)
        {
            db = new SalonEntities1();
           return action(db);
        }
    }
}
