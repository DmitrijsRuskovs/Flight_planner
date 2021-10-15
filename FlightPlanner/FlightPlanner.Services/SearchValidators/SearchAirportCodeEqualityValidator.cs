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
    public class SearchAirportCodeEqualityValidator : ISearchValidator
    {
        public bool IsValid(SearchFlightRequest request)
        {
            return request?.From?.Trim().ToLower() !=
                   request?.To?.Trim().ToLower();
        }
    }
}
