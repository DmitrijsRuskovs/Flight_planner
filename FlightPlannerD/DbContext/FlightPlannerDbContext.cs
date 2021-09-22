using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlannerD.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerD.DbContext
{
    public class FlightPlannerDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base(options)
        {
            
    }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }

    }
}
