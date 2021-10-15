using FlightPlanner.Core.Models;
using FlightPlannerD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlightById(int id);

        void DeleteFlightById(int id);

        List<Flight> SearchFlights(SearchFlightRequest flight);

        bool Exists(Flight flight);

        bool Valid(Flight flight);
    }
}
