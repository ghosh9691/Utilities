using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace PyxisInt.AirCloud.Models
{
    public class Airline
    {
        [BsonId]
        [Required]
        [JsonProperty("id")]
        public ObjectId Id { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [JsonProperty("icao")]
        public string Icao { get; set; }

        [StringLength(2, MinimumLength = 2)]
        [JsonProperty("iata")]
        public string Iata { get; set; }

        [StringLength(50)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [StringLength(20)]
        [JsonProperty("callSign")]
        public string CallSign { get; set; }

        [StringLength(4, MinimumLength = 4)]
        [Required]
        [JsonProperty("baseAirport")]
        public string BaseAirport { get; set; }

        [Required]
        [JsonProperty("effective")]
        public DateTime Effective { get; set; }

        [Required]
        [JsonProperty("discontinue")]
        public DateTime Discontinue { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }

        [JsonIgnore]
        public DateTime LastModified { get; set; }
    }
}
