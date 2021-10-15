using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services.Validators
{
    public class CityValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.To?.City) &&
                   !string.IsNullOrEmpty(request?.From?.City);
        }
    }
}
