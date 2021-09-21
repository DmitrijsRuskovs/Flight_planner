using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FlightPlannerD.Models
{
    public class SearchFlightsRequestReturn
    {
        private readonly static object balanceLock = new object();

        [Required]
        [JsonPropertyName("items")]
        public List<Flight> Items { get; set; }

        [Required]
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [Required]
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        public SearchFlightsRequestReturn()
        {
            Page = 0; 
            TotalItems = 0;
        }

        public SearchFlightsRequestReturn(List<Flight> foundItems)
        {
            lock (balanceLock)
            {
                Items = foundItems;
                Page = 0;
                TotalItems = Items.Count;
            }
        }
    }
}
