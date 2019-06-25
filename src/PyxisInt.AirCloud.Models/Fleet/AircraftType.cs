using Newtonsoft.Json;
using PyxisInt.Utilities.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PyxisInt.AirCloud.Models.Fleet
{
    public class AircraftType
    {
        [Key]
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(4)]
        [JsonProperty("icao")]
        public string Icao { get; set; }

        [Required]
        [StringLength(3)]
        [JsonProperty("iata")]
        public string Iata { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty("Model")]
        public string Model { get; set; }

        [StringLength(128)]
        [JsonProperty("description")]
        public string Description { get; set; }

        [StringLength(1)]
        [JsonProperty("turbulenceCategory")]
        public string TurbulenceCategory { get; set; }

        [JsonProperty("numberOfEngines")]
        public int NumberOfEngines { get; set; }

        [StringLength(1)]
        [JsonProperty("landingCategory")]
        public string LandingCategory { get; set; }

        [JsonProperty("wingspan")]
        public int Wingspan { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("maxRamp")]
        public Weight MaxRamp { get; set; }

        [JsonProperty("maxTakeoff")]
        public Weight MaxTakeoff { get; set; }

        [JsonProperty("maxLanding")]
        public Weight MaxLanding { get; set; }

        [JsonProperty("maxZeroFuel")]
        public Weight MaxZeroFuel { get; set; }

        [JsonProperty("operatingEmpty")]
        public Weight OperatingEmpty { get; set; }

        [JsonProperty("fuelCapacity")]
        public Weight FuelCapacity { get; set; }

        [JsonProperty("auxFuelCapacity")]
        public Weight AuxFuelCapacity { get; set; }

        [JsonProperty("ceiling")]
        public int Ceiling { get; set; }

        [JsonProperty("averageTAS")]
        public int AverageTAS { get; set; }
    }
}
