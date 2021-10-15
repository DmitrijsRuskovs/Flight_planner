using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FlightPlanner.Core.Models
{
    public class Airport : Entity
    {
       
        public string Country { get; set; }
        public string City { get; set; }

        public string AirportCode { get; set; }
    }
}
