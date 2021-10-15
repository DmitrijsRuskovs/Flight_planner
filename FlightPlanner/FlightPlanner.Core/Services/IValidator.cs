using FlightPlanner.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface IValidator
    {
        bool IsValid(FlightRequest request);
    }
}
