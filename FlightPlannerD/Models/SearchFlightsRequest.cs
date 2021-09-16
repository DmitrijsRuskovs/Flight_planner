using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerD.Models
{
    public class SearchFlightsRequest
    {
        public string to { get; set; }
        public string from { get; set; }
        public string departureDate { get; set; }

        public bool IsSearchFlightRequest()
        {
            bool result = true;
            if (to == null || from == null || departureDate == null)
            {
                result = false;
            }
            else 
            {
                if (to.Trim().Length < 3 || from.Trim().Length < 3 || departureDate.Trim().Length < 10 || to.Trim().ToUpper() == from.Trim().ToUpper())
                {
                    result = false;
                }
            }

            return result;
        }
    }   
}

