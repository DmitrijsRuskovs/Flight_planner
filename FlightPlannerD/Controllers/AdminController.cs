using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlannerD.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly object balanceLock = new object();
        private readonly IEntityService<Flight> _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator> _validators;
        public AdminController(IFlightService flightService, IMapper mapper, IEnumerable<IValidator> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (balanceLock)
            {
                var flight = _flightService.GetById(id);
                return flight != null ? Ok(_mapper.Map<FlightResponse>(flight)) : NotFound();
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(FlightRequest request)
        {
            lock (balanceLock)
            {
                if (!_validators.All(s => s.IsValid(request)))
                {
                    return BadRequest();
                }
            }
                var flight = _mapper.Map<Flight>(request);
            lock (balanceLock)
            {
                if (_flightService.Exists(flight))
                {
                    return Conflict();
                }
            }
            lock (balanceLock)
            {
                _flightService.Create(flight);
                return Created("", _mapper.Map<FlightResponse>(flight));
            }           
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (balanceLock)
            {
                _flightService.DeleteFlightById(id);             
            }
            return Ok();
        } 
                
    }
}
