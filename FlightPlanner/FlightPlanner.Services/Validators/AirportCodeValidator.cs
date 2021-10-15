using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services.Validators
{
    public class AirportCodeValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.To?.Airport) &&
                   !string.IsNullOrEmpty(request?.From?.Airport) &&
                   request?.To?.Airport.Trim().Length >=3 &&
                   request?.From?.Airport.Trim().Length >= 3;
        }
    }
}
