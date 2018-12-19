using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PyxisInt.AirCloud.Models.Navigation
{
    public class Airport
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [JsonProperty("icao")]
        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Icao { get; set; }

        [JsonProperty("country")]
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; set; }

        [JsonProperty("iata")]
        [StringLength(3, MinimumLength = 3)]
        public string Iata { get; set; }

        [JsonProperty("name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonProperty("latitude")]
        [Required]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        [Required]
        public double Longitude { get; set; }

        [JsonProperty("magneticVariation")]
        [Required]
        public double MagneticVariation { get; set; }

        [JsonProperty("elevation")]
        [Required]
        public int Elevation { get; set; }

        [JsonProperty("longestRunway")]
        public int? LongestRunway { get; set; }

        [JsonIgnore]
        public List<string> Airlines { get; set; }

        public Airport()
        {
            Airlines = new List<string>();
        }
    }
}