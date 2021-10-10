using FlightPlannerD.DbContext;
using FlightPlannerD.Models;
using FlightPlannerD.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
      
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (balanceLock)
            {
                var flight = FlightStorage.GetByID(_context, id);
                return flight != null ? Ok(flight) : NotFound();
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {

            if (!FlightStorage.IsValidFlight(flight))
            {
                return BadRequest();
            }
            else if (FlightStorage.FlightExists(_context, flight))
            {
                return Conflict();
            }
            else
            {
                lock (balanceLock)
                {
                    FlightStorage.AddFlight(_context, flight);                   
                    return Created("", flight);
                }
            }                         
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (balanceLock)
            {
                var flight = FlightStorage.GetByID(_context, id);
                if (flight?.To != null)
                {
                    FlightStorage.RemoveAirport(_context, flight.To);
                }

                if (flight?.From != null)
                {
                    FlightStorage.RemoveAirport(_context, flight.From);
                }

                FlightStorage.RemoveFlight(_context, flight);
                return Ok();
            }
        }
    }
}
