using FlightPlanner.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {

        List<Airport> SearchAirports(string part);
    }
}
