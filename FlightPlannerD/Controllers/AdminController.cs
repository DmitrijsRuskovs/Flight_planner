using FlightPlannerD.DbContext;
using FlightPlannerD.Models;
using FlightPlannerD.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerD.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly object balanceLock = new object();

        private readonly FlightPlannerDbContext _context;
        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        private bool FlightExists(Flight flight)
        {
            lock (balanceLock)
            {
                bool result = false;           
                result = (_context.Flights.Any(f =>
                      (f.To.AirportCode.Trim().ToUpper() == flight.To.AirportCode.Trim().ToUpper()) &&
                      (f.From.AirportCode.Trim().ToUpper() == flight.From.AirportCode.Trim().ToUpper()) &&
                      (f.DepartureTime.Trim().ToUpper() == flight.DepartureTime.Trim().ToUpper()) &&
                      (f.ArrivalTime.Trim().ToUpper() == flight.ArrivalTime.Trim().ToUpper()) &&
                      (f.Carrier.Trim().ToUpper() == flight.Carrier.Trim().ToUpper())
                ));

                return result;
            }          
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
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

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            lock (balanceLock)
            {
                if (!FlightStorage.IsValidFlight(flight))
                {
                    return BadRequest();
                }
                else
                {
                    if (FlightExists(flight))
                    {
                        return Conflict();
                    }
                    else
                    {
                        lock (balanceLock)
                        {
                            _context.Flights.Add(flight);
                            _context.SaveChanges();
                            return Created("", flight);
                        }                      
                    }
                }
            }

        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (balanceLock)
            {
                var flight = _context.Flights
                    .Include(f => f.To)
                    .Include(f => f.From)
                    .SingleOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    if (flight.To != null)
                    {
                        _context.Airports.Remove(flight.To);
                    }

                    if (flight.From != null)
                    {
                        _context.Airports.Remove(flight.From);
                    }

                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }

                return Ok();
            }
        }
    }
}
