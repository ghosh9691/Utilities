using Newtonsoft.Json;
using PyxisInt.AirCloud.Models.Enums;
using PyxisInt.Utilities.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PyxisInt.AirCloud.Models.Fleet
{
    public class Aircraft
    {
        [Key]
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Required]
        [JsonProperty("status")]
        public RecordStatuses Status { get; set; }

        [Required]
        [JsonProperty("airlineCode")]
        [StringLength(3, MinimumLength = 3)]
        public string AirlineCode { get; set; }

        [Required]
        [JsonProperty("tailNumber")]
        [StringLength(6)]
        public string TailNumber { get; set; }

        [JsonProperty("configuration")]
        [StringLength(12)]
        public string Configuration { get; set; }

        [Required]
        [JsonProperty("registration")]
        [StringLength(10)]
        public string Registration { get; set; }

        [Required]
        [JsonProperty("aircraftType")]
        public AircraftType AircraftType { get; set; }

        [JsonProperty("hub")]
        [StringLength(4)]
        public string Hub { get; set; }

        [Required]
        [JsonProperty("overrideWeights")]
        public bool OverrideWeights { get; set; }

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

        [JsonProperty("tankCapacity")]
        public Weight TankCapacity { get; set; }

        [JsonProperty("auxTankCapacity")]
        public Weight AuxTankCapacity { get; set; }

        [JsonProperty("maxPassengers")]
        public int MaxPassengers { get; set; }

        [Required]
        [JsonProperty("specificGravity")]
        public double SpecificGravity { get; set; }

        [JsonProperty("burnDegrades")]
        public BurnDegrade BurnDegrades { get; set; }

        [JsonProperty("speedDegrades")]
        public SpeedDegrade SpeedDegrades { get; set; }

        [StringLength(8)]
        [JsonProperty("mnpsAvionics")]
        public string MNPSAvionics { get; set; }

        [StringLength(2)]
        [JsonProperty("ssrEquipment")]
        public string SSREquipment { get; set; }

        [StringLength(3)]
        [JsonProperty("emergencyEquipment")]
        public string EmergencyEquipment { get; set; }

        [StringLength(4)]
        [JsonProperty("survivalEquipment")]
        public string SurvivalEquipment { get; set; }

        [StringLength(4)]
        [JsonProperty("lifeJackets")]
        public string LifeJackets { get; set; }

        [StringLength(2)]
        [JsonProperty("numberOfDinghies")]
        public string NumberOfDinghies { get; set; }

        [StringLength(3)]
        [JsonProperty("dinghyCapacity")]
        public string DinghyCapacity { get; set; }

        [JsonProperty("dinghiesCovered")]
        public bool DinghiesCovered { get; set; }

        [StringLength(8)]
        [JsonProperty("colorOfDinghies")]
        public string ColorOfDinghies { get; set; }

        [StringLength(64)]
        [JsonProperty("aircraftColor")]
        public string AircraftColor { get; set; }

        [StringLength(6)]
        [JsonProperty("selcal")]
        public string SELCAL { get; set; }

        [StringLength(3)]
        [JsonProperty("noiseClass")]
        public string NoiseClass { get; set; }

        [StringLength(3)]
        [JsonProperty("approachClass")]
        public string ApproachClass { get; set; }

        [StringLength(20)]
        [JsonProperty("atcOPR")]
        public string AtcOPR { get; set; }

        [StringLength(20)]
        [JsonProperty("atcSTS")]
        public string AtcSTS { get; set; }

        [StringLength(8)]
        [JsonProperty("atcPER")]
        public string AtcPER { get; set; }

        [StringLength(8)]
        [JsonProperty("atcCOM")]
        public string AtcCOM { get; set; }

        [StringLength(13)]
        [JsonProperty("atcNAV")]
        public string AtcNAV { get; set; }
    }
}
