using FlightPlannerD.DbContext;
using FlightPlannerD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerD.Storage
{
    public class FlightStorage
    {
        private readonly static object balanceLock = new object();

        public static Flight GetByID(FlightPlannerDbContext context, int id)
        {
            lock (balanceLock)
            {
                return context.Flights
                    .Include(f => f.To)
                    .Include(f => f.From)
                    .SingleOrDefault(f => f.Id == id);
            }
        }

        public static List<Flight> SearchFlights(FlightPlannerDbContext context, SearchFlightsRequest flight)
        {           
            List<Flight> flights = new List<Flight>();
            lock (balanceLock)
            {
                flights = (
                    from f in context.Flights
                    where (f.To.AirportCode.Trim().ToUpper() == flight.To.Trim().ToUpper() &&
                           f.From.AirportCode.Trim().ToUpper() == flight.From.Trim().ToUpper() &&
                           f.DepartureTime.Contains(flight.DepartureDate))
                    select f)
                    .Include(a => a.To)
                    .Include(a => a.From).
                    ToList();
            }

            return flights;
        }

        public static List<Airport> SearchAirports(FlightPlannerDbContext context, string part)
        {
            part = part.Trim().ToUpper();
            lock (balanceLock)
            {
                return (
                   from a in context.Airports
                   where (a.AirportCode.Trim().ToUpper().Contains(part) ||
                          a.City.Trim().ToUpper().Contains(part) ||
                          a.Country.Trim().ToUpper().Contains(part))
                   select a).ToList();
            }
        }

        public static void AddFlight(FlightPlannerDbContext context, Flight flight)
        {
            lock (balanceLock)
            {
                context.Flights.Add(flight);
            }
            lock (balanceLock)
            {
                context.SaveChanges();
            }
        }

        public static void RemoveFlight(FlightPlannerDbContext context, Flight flight)
        {
            lock (balanceLock)
            {
                context.Flights.Remove(flight);
            }
            lock (balanceLock)
            {
                context.SaveChanges();
            }
        }

        public static void RemoveAirport(FlightPlannerDbContext context, Airport airport)
        {
            lock (balanceLock)
            {
                context.Airports.Remove(airport);
            }
            lock (balanceLock)
            {
                context.SaveChanges();
            }
        }

        public static bool FlightExists(FlightPlannerDbContext context, Flight flight)
        {
            lock (balanceLock)
            {
                return context.Flights
                    .Include(f => f.To)
                    .Include(f => f.From).ToList().Any(f => 
                    ((f.DepartureTime.Trim().ToUpper() == flight.DepartureTime.Trim().ToUpper()) &&
                     (f.ArrivalTime.Trim().ToUpper() == flight.ArrivalTime.Trim().ToUpper()) &&
                     (f.Carrier.Trim().ToUpper() == flight.Carrier.Trim().ToUpper()) &&
                     (f.To.AirportCode.Trim().ToUpper() == flight.To.AirportCode.Trim().ToUpper()) &&
                     (f.From.AirportCode.Trim().ToUpper() == flight.From.AirportCode.Trim().ToUpper())));
            }
        }

        public static bool IsValidFlight(Flight flight)
        {
            lock (balanceLock)
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
        }
    }
}
