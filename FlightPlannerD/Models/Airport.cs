using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace FlightPlannerD.Models
{
    
    public class Airport
    {
        [JsonIgnore] 
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        [JsonPropertyName("airport")] 
        public string AirportCode { get; set; }
    }
}
