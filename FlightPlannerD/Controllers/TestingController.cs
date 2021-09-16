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
        private readonly object balanceLock1 = new object();

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            lock (balanceLock1)
            {
                FlightStorage.ClearFlights();
                return Ok();
            }
        }
    }
}
