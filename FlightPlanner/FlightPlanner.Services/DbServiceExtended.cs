using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services
{
    public class DbServiceExtended : DbService, IDbServiceExtended
    {
        public DbServiceExtended(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void DeleteAll<T>() where T : Entity
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }
    }
}
