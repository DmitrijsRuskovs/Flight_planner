using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlannerD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(FlightPlannerDbContext context) : base(context)
        {
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

        public T GetById(int id)
        {
            return GetById<T>(id);
        }

        public void Create(T entity)
        {
            Create<T>(entity);
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }

        public void DeleteFlightById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Flight flight)
        {
            throw new NotImplementedException();
        }

        public List<T> SearchAirports(string search)
        {
            throw new NotImplementedException();
        }

        public Flight GetFullFlightById(int id)
        {
            throw new NotImplementedException();
        }

        public object SearchFlights(SearchFlightRequest request)
        {
            throw new NotImplementedException();
        }

        List<T> IEntityService<T>.SearchFlights(SearchFlightRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
