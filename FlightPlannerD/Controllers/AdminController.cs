using FlightPlannerD.Models;
using FlightPlannerD.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (balanceLock)
            {
                var flight = FlightStorage.GetById(id);
                return flight != null ? Ok(flight) : NotFound();
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {

            lock (balanceLock)
            {
                if (!FlightStorage.IsFlight(flight))
                {
                    return BadRequest();
                }
                else
                {
                    if (FlightStorage.Exists(flight))
                    {
                        return Conflict();
                    }
                    else
                    {
                        return Created("", FlightStorage.AddFlight(flight));
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
                return FlightStorage.DeleteFlight(id) ? Ok() : Ok();
            }
        }
    }
}
