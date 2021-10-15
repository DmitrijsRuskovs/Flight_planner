using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlannerD.Models;

namespace FlightPlanner.Core.Services
{      
    public interface IEntityService<T> where T : Entity
    {
        IQueryable<T> Query();

        T GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
        void DeleteFlightById(int id);
        bool Exists(Flight flight);
        Flight GetFullFlightById(int id);
        List<T> SearchAirports(string search);
        List<T> SearchFlights(SearchFlightRequest request);
    }
}
