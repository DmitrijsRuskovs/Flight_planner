using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerD.DbContext
{
   
        public static class EntityExtensions
        {
            public static void Clear<T>(this DbSet<T> dbSet) where T : class
            {
                dbSet.RemoveRange(dbSet);
            }
        }
    
}
