using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FlightPlannerD.Models
{
    public class Flight
    {
        public Flight(int id)
        {
            Id = id;
        }

        public Flight()
        {
        }

        public int Id { get; set; }
        public Airport To { get; set; }
        public Airport From { get; set; }
        public string Carrier{ get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
