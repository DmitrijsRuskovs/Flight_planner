using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using FlightPlannerD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services.SearchValidators
{
    public class SearchAirportCodeValidator : ISearchValidator
    {
        public bool IsValid(SearchFlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.To) &&
                   !string.IsNullOrEmpty(request?.From) &&
                   request?.To?.Trim().Length >=3 &&
                   request?.From?.Trim().Length >= 3;
        }
    }
}
