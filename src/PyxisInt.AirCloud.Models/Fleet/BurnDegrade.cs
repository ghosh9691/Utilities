using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyxisInt.AirCloud.Models.Fleet
{
    public class BurnDegrade
    {
        [JsonProperty("climb")]
        public double Climb { get; set; }

        [JsonProperty("cruise")]
        public double Cruise { get; set; }

        [JsonProperty("descent")]
        public double Descent { get; set; }
    }
}
