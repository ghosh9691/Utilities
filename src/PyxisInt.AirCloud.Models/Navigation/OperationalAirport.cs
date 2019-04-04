using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PyxisInt.AirCloud.Models.Enums;
namespace PyxisInt.AirCloud.Models.Navigation
{
    public class OperationalAirport
    {
        [JsonIgnore]
        public Guid AirlineId { get; set; }

        [JsonProperty("airline")]
        public Airline Airline { get; set; }

        [JsonIgnore]
        public Guid AirportId { get; set; }

        [JsonProperty("airport")]
        public Airport Airport { get; set; }

        [JsonProperty("timeZone")]
        [StringLength(50)]
        public string TimeZone { get; set; }

        [JsonProperty("airportType")]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public AirportTypes AirportType { get; set; }

        [JsonProperty("city")]
        [StringLength(50)]
        public string City { get; set; }

        [JsonProperty("country")]
        [StringLength(50)]
        public string Country { get; set; }
    }
}
