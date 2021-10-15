using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlannerD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlannerD.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {    
        private readonly object balanceLock = new object();
        private readonly IEntityService<Flight> _flightService;
        private readonly IEntityService<Airport> _airportService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<ISearchValidator> _searchValidators;

        public CustomerController(IFlightService flightService, IAirportService airportService, IMapper mapper, IEnumerable<ISearchValidator> searchValidators)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
            _searchValidators = searchValidators;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult SearchFlightById(int id)
        {
            lock (balanceLock)
            {
                var flight = _flightService.GetFullFlightById(id);
                return flight != null ? Ok(_mapper.Map<FlightResponse>(flight)) : NotFound();
            }
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(SearchFlightRequest request)
        {
            lock (balanceLock)
            {
                if (!_searchValidators.All(s => s.IsValid(request)))
                {
                    return BadRequest();
                }

                var flights = _flightService.SearchFlights(request);
                List<FlightResponse> flightResponse = new List<FlightResponse>();
                foreach (var f in (List<Flight>)flights)
                {
                    flightResponse.Add(_mapper.Map<FlightResponse>(f));
                }

                return Ok(new SearchFlightResponse(flightResponse));
            }
        }

        [HttpGet]
        [Route("airports/")]
        public IActionResult SearchAirport(string search)
        {
            lock (balanceLock)
            {
                var airport = _airportService.SearchAirports(search);
                List<AirportResponse> airportResponse = new List<AirportResponse>();
                foreach (var a in airport)
                {
                    airportResponse.Add(_mapper.Map<AirportResponse>(a));
                }

                return Ok(airportResponse);
            }
        }
    }  
}
