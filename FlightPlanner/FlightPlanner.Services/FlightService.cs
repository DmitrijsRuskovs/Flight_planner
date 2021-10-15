using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlannerD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        private readonly object balanceLock = new object();
        public FlightService(IFlightPlannerDbContext context) : base((FlightPlannerDbContext)context)
        {
        }

        public List<Flight> SearchFlights(SearchFlightRequest flight)
        {
                List<Flight> flights = new List<Flight>();
                flights = (
                    from f in _context.Flights
                    .Include(f => f.To)
                    .Include(f => f.From)
                    where (f.To.AirportCode.Trim().ToUpper() == flight.To.Trim().ToUpper() &&
                           f.From.AirportCode.Trim().ToUpper() == flight.From.Trim().ToUpper() &&
                           f.DepartureTime.Contains(flight.DepartureDate))
                    select f)
                    .ToList();
                return flights;
        }

        public Flight GetFullFlightById(int id)
        {
                return _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .SingleOrDefault(f => f.Id == id);
        }

        public void DeleteFlightById(int id)
        {
                var flight = GetFullFlightById(id);
                if (flight != null)
                {
                    if (flight.To != null)
                    {
                        Delete(flight.To);
                    }
                    if (flight.From != null)
                    {
                        Delete(flight.From);
                    }
                    Delete(flight);
                }
        }

        public bool Valid(Flight flight)
        {
                if (flight == null)
                {
                    return false;
                }
                else
                {
                    return
                        (!string.IsNullOrEmpty(flight.ArrivalTime) &&
                        !string.IsNullOrEmpty(flight.DepartureTime) &&
                        !string.IsNullOrEmpty(flight.Carrier) &&
                        flight.Carrier.Trim().Length >= 3 &&
                        flight.To != null &&
                        !string.IsNullOrEmpty(flight.To.AirportCode) &&
                        !string.IsNullOrEmpty(flight.To.City) &&
                        !string.IsNullOrEmpty(flight.To.Country) &&
                        flight.From != null &&
                        !string.IsNullOrEmpty(flight.From.AirportCode) &&
                        !string.IsNullOrEmpty(flight.From.City) &&
                        !string.IsNullOrEmpty(flight.From.Country) &&
                        flight.To.AirportCode.Trim().ToUpper() != flight.From.AirportCode.Trim().ToUpper() &&
                        DateTime.Parse(flight.ArrivalTime) > DateTime.Parse(flight.DepartureTime));
                }
        }

        public bool Exists(Flight flight)
        {
                return Query()
                    .Include(f => f.To)
                    .Include(f => f.From).ToList().Any(f =>
                    ((f.DepartureTime.Trim().ToUpper() == flight.DepartureTime.Trim().ToUpper()) &&
                     (f.ArrivalTime.Trim().ToUpper() == flight.ArrivalTime.Trim().ToUpper()) &&
                     (f.Carrier.Trim().ToUpper() == flight.Carrier.Trim().ToUpper()) &&
                     (f.To.AirportCode.Trim().ToUpper() == flight.To.AirportCode.Trim().ToUpper()) &&
                     (f.From.AirportCode.Trim().ToUpper() == flight.From.AirportCode.Trim().ToUpper())));
        }

    }
}
