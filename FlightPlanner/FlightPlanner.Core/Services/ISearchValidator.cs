using FlightPlanner.Core.Dto;
using FlightPlannerD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface ISearchValidator
    {
        bool IsValid(SearchFlightRequest request);
    }
}

