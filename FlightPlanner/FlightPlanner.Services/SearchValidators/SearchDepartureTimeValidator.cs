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
    public class SearchDepartureTimeValidator : ISearchValidator
    {
        bool ISearchValidator.IsValid(SearchFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureDate);
        }
    }
}
