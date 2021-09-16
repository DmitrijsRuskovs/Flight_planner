using FlightPlannerD.Models;
using FlightPlannerD.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult SearchFlightById(int id)
        {
            lock (balanceLock)
            {
                var flight = FlightStorage.GetById(id);
                return flight != null ? Ok(flight) : NotFound();
            }
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(SearchFlightsRequest req)
        {
            lock (balanceLock)
            {
                if (!req.IsSearchFlightRequest())
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(new SearchFlightsRequestReturn(FlightStorage.SearchFlights(req)));
                }
            }
        }

        [HttpGet]
        [Route("airports")]      
        public IActionResult SearchAirport(string search)
        {
            lock (balanceLock)
            {
                List<Airport> airport = FlightStorage.SearchAirport(search);
                return Ok(airport);
            }
        }
    }  
}
