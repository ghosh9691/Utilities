using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PyxisInt.AirCloud.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PyxisInt.AirCloud.Models.Identity
{
    public class User
    {
        [BsonId]
        [JsonProperty("id")]
        [Required]
        [StringLength(16)]
        public string Id { get; set; }

        [Required]
        [StringLength(20)]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("firstName")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        [StringLength(50)]
        public string LastName { get; set; }

        [JsonProperty("displayName")]
        [StringLength(128)]
        public string DisplayName { get; set; }

        [JsonProperty("email")]
        [StringLength(128)]
        public string Email { get; set; }

        [JsonProperty("allowedAirlines")]
        public List<string> AllowedAirlines { get; set; }

        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Roles Role { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("lastLogin")]
        public DateTime LastLogin { get; set; }
    }
}
