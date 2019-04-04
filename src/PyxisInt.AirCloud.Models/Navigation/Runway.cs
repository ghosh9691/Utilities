using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PyxisInt.AirCloud.Models.Navigation
{
    public class Runway
    {
        [JsonIgnore]
        public Guid AirportId { get; set; }

        [JsonProperty("airport")]
        public Airport Airport { get; set; }

        [JsonProperty("runwayId")]
        [Required]
        [StringLength(6)]
        public string RunwayId { get; set; }

        [JsonProperty("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonProperty("bearing")]
        public double Bearing { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
