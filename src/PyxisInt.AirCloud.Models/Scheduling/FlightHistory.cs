using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PyxisInt.AirCloud.Models.Scheduling
{
    public class FlightHistory
    {
        [Key]
        [Required]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public FlightHistoryItems HistoryItem { get; set; }
        public string Location { get; set; }
        public DateTime HistoryDateUtc { get; set; }
    }
}
