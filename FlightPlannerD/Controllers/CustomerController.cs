using FlightPlannerD.DbContext;
using FlightPlannerD.Models;
using FlightPlannerD.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FlightPlannerD.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly object balanceLock = new object();

        private readonly FlightPlannerDbContext _context;
        
        public CustomerController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        private List<Airport> SearchAirports(string part)
        {
            part = part.Trim().ToUpper();
            List<Airport> airport = new List<Airport>();
            lock (balanceLock)
            {
                airport = (
                   from a in _context.Airports
                   where (a.AirportCode.Trim().ToUpper().Contains(part) ||
                          a.City.Trim().ToUpper().Contains(part) ||
                          a.Country.Trim().ToUpper().Contains(part))
                   select a).ToList();               
            }

            return airport;
        }

        private List<Flight> SearchFlights(SearchFlightsRequest flight)
        {
            List<Flight> flights = new List<Flight>();
            lock (balanceLock)
            {
                flights = (
                    from f in _context.Flights 
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

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult SearchFlightById(int id)
        {
            lock (balanceLock)
            {
                var flight = _context.Flights                  
                    .Include(f => f.To)
                    .Include(f => f.From)
                    .SingleOrDefault(f => f.Id == id);

                return flight != null ? Ok(flight) : NotFound();
            }
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(SearchFlightsRequest req)
        {
            lock (balanceLock)
            {
                if (!req.IsValidSearchFlightRequest())
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(new SearchFlightsRequestReturn(SearchFlights(req)));
                }
            }
        }

        [HttpGet]
        [Route("airports")]      
        public IActionResult SearchAirport(string search)
        {
            lock (balanceLock)
            {
                List<Airport> airport = SearchAirports(search);
                return Ok(airport);
            }
        }
    }  
}
