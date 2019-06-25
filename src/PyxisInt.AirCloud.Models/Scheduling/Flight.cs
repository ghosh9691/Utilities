using Newtonsoft.Json;
using PyxisInt.AirCloud.Models.Enums;
using PyxisInt.AirCloud.Models.Fleet;
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

        public RecordStatuses Status { get; set; }
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string FlightSuffix { get; set; }
        public int FlightSequence { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public FlightDate SkedDeparture { get; set; }
        public FlightDate SkedArrival { get; set; }
        public FlightDate ActualDeparture { get; set; }
        public FlightDate ActualArrival { get; set; }
        public Aircraft Aircraft { get; set; }
        public FlightDate Out { get; set; }
        public FlightDate Off { get; set; }
        public FlightDate On { get; set; }
        public FlightDate In { get; set; }
        public string CockpitCrewOwner { get; set; }
        public string CabinCrewOwner { get; set; }
    }
}
