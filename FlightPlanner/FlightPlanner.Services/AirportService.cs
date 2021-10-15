using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        private readonly object balanceLock = new object();
        public AirportService(IFlightPlannerDbContext context) : base((FlightPlannerDbContext)context)
        {
        }

        public List<Airport> SearchAirports(string part)
        {
            part = part.Trim().ToUpper();
                return (
               from a in _context.Airports
               where (a.AirportCode.Trim().ToUpper().Contains(part) ||
                      a.City.Trim().ToUpper().Contains(part) ||
                      a.Country.Trim().ToUpper().Contains(part))
               select a).ToList();
        }
    }
}
