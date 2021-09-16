using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerD.Models
{
    public class SearchFlightsRequestReturn
    {
        private readonly static object balanceLock = new object();

        [Required]
        public List<Flight> items { get; set; }

        [Required]
        public int page { get; set; }

        [Required]
        public int totalItems { get; set; }

        public SearchFlightsRequestReturn()
        {
            page = 0; 
            totalItems = 0;
        }

        public SearchFlightsRequestReturn(List<Flight> foundItems)
        {
            lock (balanceLock)
            {
                items = foundItems;
                page = 0;
                totalItems = items.Count;
            }
        }
    }
}
