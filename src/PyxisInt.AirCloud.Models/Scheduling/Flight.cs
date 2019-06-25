using Newtonsoft.Json;
using PyxisInt.Utilities.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PyxisInt.AirCloud.Models.Scheduling
{
    public class Flight
    {
        [Key]
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public string AirlineIcao { get; set; }
        public string FlightNumber { get; set; }
        public string FlightSuffix { get; set; }
        public int LegSequence { get; set; }
        public string Origin { get; set; }
        public FlightDate DepartureTime { get; set; }
        public string Destination { get; set; }
        public FlightDate ArrivalTime { get; set; }
        public string AircraftRegistration { get; set; }


    }
}
