using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerD.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly object balanceLock = new object();

        private readonly IDbServiceExtended _service;

        public TestingController(IDbServiceExtended service)
        {
            _service = service;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _service.DeleteAll<Flight>();
            _service.DeleteAll<Airport>();
            return Ok();
        }
    }
}
