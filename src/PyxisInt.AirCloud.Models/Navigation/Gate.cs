using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace PyxisInt.AirCloud.Models.Navigation
{
    public class Gate
    {
        [JsonIgnore]
        public Guid AirportId { get; set; }

        [JsonProperty("airport")]
        public Airport Airport { get; set; }

        [JsonProperty("gateId")]
        [StringLength(8)]
        public string GateId { get; set; }

        [JsonProperty("airacCycle")]
        [StringLength(4)]
        [Required]
        public string AiracCycle { get; set; }

        [JsonProperty("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
