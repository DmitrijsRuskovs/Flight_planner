﻿using FlightPlannerD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerD.Storage
{
    public class FlightStorage
    {
        
        private static List<Flight> _flights = new List<Flight>();
        private readonly static object balanceLock = new object();
        private static int _lastId = 0;

        public static bool Exists(Flight flight)
        {
            bool result = false;
            lock (balanceLock)
            {
                foreach (Flight f in _flights.ToList())
                {
                    if (f != null)
                    {
                        if (f.To != null && f.From != null && f.DepartureTime != null && f.ArrivalTime != null && f.Carrier != null)
                        {
                            if ((f.To.AirportCode.Trim().ToUpper() == flight.To.AirportCode.Trim().ToUpper()) &&
                                 (f.From.AirportCode.Trim().ToUpper() == flight.From.AirportCode.Trim().ToUpper()) &&
                                 (f.DepartureTime.Trim().ToUpper() == flight.DepartureTime.Trim().ToUpper()) &&
                                 (f.ArrivalTime.Trim().ToUpper() == flight.ArrivalTime.Trim().ToUpper()) &&
                                 (f.Carrier.Trim().ToUpper() == flight.Carrier.Trim().ToUpper()))
                            {
                                result = true;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static List<Airport> SearchAirport(string part)
        {
            part = part.Trim().ToUpper();
            List<Airport> airport = new List<Airport>();
            lock (balanceLock)
            {
                foreach (var f in _flights)
                {
                    if (f.To.AirportCode.Trim().ToUpper().Contains(part) || f.To.City.Trim().ToUpper().Contains(part) || f.To.Country.Trim().ToUpper().Contains(part))
                    {
                        if (!airport.Contains(f.To))
                        {
                            airport.Add(f.To);
                        }
                    }
                    if (f.From.AirportCode.Trim().ToUpper().Contains(part) || f.From.City.Trim().ToUpper().Contains(part) || f.From.Country.Trim().ToUpper().Contains(part))
                    {
                        if (!airport.Contains(f.From))
                        {
                            airport.Add(f.From);
                        }
                    }
                }
            }

            return airport;
        }

        public static List<Flight> SearchFlights(SearchFlightsRequest flight)
        {          
            List<Flight> flights = new List<Flight>();
            lock (balanceLock)
            {
                foreach (var f in _flights)
                {
                    if (f.To.AirportCode.Trim().ToUpper() == flight.To.Trim().ToUpper() &&
                        f.From.AirportCode.Trim().ToUpper() == flight.From.Trim().ToUpper() &&
                        f.DepartureTime.Contains(flight.DepartureDate))
                    {
                        flights.Add(f);
                    }
                }
            }
            return flights;
        }

        public static bool IsFlight(Flight flight)
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

        public static bool IdExists(int id)
        {
            bool result = false;
            lock (balanceLock)
            {
                foreach (Flight f in _flights.ToList())
                {
                    if (f != null)
                    {
                        if (f.Id == id)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public static int AssignUniqueId()
        {
            return ++_lastId;
        }

        public static Flight GetById(int id)
        {
            lock (balanceLock)
            {
                return _flights.SingleOrDefault(f => f.Id == id);
            }
        }

        public static Flight AddFlight(Flight flight)
        {
            lock (balanceLock)
            {
                flight.Id = AssignUniqueId();
            }
            lock (balanceLock)
            {
                _flights.Add(flight);
            }
            return flight;
        }

        public static void ClearFlights()
        {
            lock (balanceLock)
            {
                _flights.Clear();
            }
        }

        public static bool DeleteFlight(int id)
        {
            Flight flight = GetById(id);
            if (flight == null)
            {
                return false;
            }
            else
            {
                lock (balanceLock)
                {
                    _flights.Remove(flight);
                }
                return true;
            }
        }
    }
}
