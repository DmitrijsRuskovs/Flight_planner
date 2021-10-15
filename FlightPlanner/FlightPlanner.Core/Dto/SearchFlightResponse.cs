using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Dto
{

    public class SearchFlightResponse
    {

        private readonly static object balanceLock = new object();

        [Required]
        [JsonPropertyName("items")]
        public List<FlightResponse> Items { get; set; }

        [Required]
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [Required]
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        public SearchFlightResponse()
        {
            Page = 0;
            TotalItems = 0;
        }

        public SearchFlightResponse(List<FlightResponse> foundItems)
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
