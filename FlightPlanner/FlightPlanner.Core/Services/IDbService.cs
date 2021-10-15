using FlightPlanner.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface IDbService
    {            
        IQueryable<T> Query<T>() where T : Entity;

        T GetById<T>(int id) where T : Entity;

        void Create<T>(T entity) where T : Entity;

        void Update<T>(T entity) where T : Entity;

        void Delete<T>(T entity) where T : Entity;
    }
}
