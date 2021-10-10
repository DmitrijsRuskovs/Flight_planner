using FlightPlannerD.DbContext;
using FlightPlannerD.Models;
using FlightPlannerD.Storage;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult SearchFlightById(int id)
        {
            lock (balanceLock)
            {
                var flight = FlightStorage.GetByID(_context, id);
                return  flight != null ? Ok(flight) : NotFound();
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
                    return Ok(new SearchFlightsRequestReturn(FlightStorage.SearchFlights(_context, req)));
                }
            }
        }

        [HttpGet]
        [Route("airports/")]      
        public IActionResult SearchAirport(string search)
        {
            lock (balanceLock)
            {
                List<Airport> airport = FlightStorage.SearchAirports(_context, search);
                return Ok(airport);
            }
        }
    }  
}
