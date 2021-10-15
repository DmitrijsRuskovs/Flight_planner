using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services.Validators
{
    public class TimeFrameValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            try
            {
                return DateTime.Parse(request.ArrivalTime) > DateTime.Parse(request.DepartureTime);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
