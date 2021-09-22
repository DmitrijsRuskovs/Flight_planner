using FlightPlannerD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerD.Storage
{
    public class FlightStorage
    {
        private readonly static object balanceLock = new object();
           
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
