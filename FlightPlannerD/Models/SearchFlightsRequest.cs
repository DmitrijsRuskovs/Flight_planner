using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerD.Models
{
    public class SearchFlightsRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string DepartureDate { get; set; }

        public bool IsSearchFlightRequest()
        {
            bool result = true;
            if (To == null || From == null || DepartureDate == null)
            {
                result = false;
            }
            else 
            {
                if (To.Trim().Length < 3 || From.Trim().Length < 3 || DepartureDate.Trim().Length < 10 || To.Trim().ToUpper() == From.Trim().ToUpper())
                {
                    result = false;
                }
            }

            return result;
        }
    }   
}

