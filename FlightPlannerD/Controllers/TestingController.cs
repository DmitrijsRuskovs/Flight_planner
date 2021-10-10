using FlightPlannerD.DbContext;
using Microsoft.AspNetCore.Mvc;

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
                _context.RemoveRange(_context.Flights);        
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
