using FlightPlannerD.DbContext;
using FlightPlannerD.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FlightPlannerD.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly object balanceLock = new object();

        private readonly FlightPlannerDbContext _context;

        public TestingController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            lock (balanceLock)
            {
                _context.RemoveRange(_context.Airports);                                               
            }
            lock (balanceLock)
            {
                _context.RemoveRange(_context.Flights);
            }
            lock (balanceLock)
            {
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
