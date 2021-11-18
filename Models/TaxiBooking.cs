using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiBookingAPI.Models
{
    public class TaxiBooking
    {

        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public String Pick_up_point { get; set; }
        public string Destination { get; set; }
        public string Current_Location_Latitude { get; set; }
        public string Current_Location_Longitude { get; set; }
    }

    public class currentlocation
    {
        public string ip { get; set; }
        public string country_code { get; set; }
        public String country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string time_zone { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string metro_code { get; set; }
    }
}
