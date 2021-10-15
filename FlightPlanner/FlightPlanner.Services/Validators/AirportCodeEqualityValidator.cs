using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services.Validators
{
    public class AirportCodeEqualityValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return request?.From?.Airport?.Trim().ToLower() !=
                   request?.To?.Airport?.Trim().ToLower();
        }
    }
}
