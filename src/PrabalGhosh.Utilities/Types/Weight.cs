using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PrabalGhosh.Utilities.Enums;
using System.Diagnostics;

namespace PrabalGhosh.Utilities.Types
{
    [Owned]
    [DebuggerDisplay("{Value} {Units}")]
    public class Weight
    {
        public Weight()
        {
        }

        public Weight(double val, WeightUnits unit)
        {
            this.Value = val;
            this.Units = unit;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public WeightUnits Units { get; set; }

        public double Value { get; set; }

        public override string ToString()
        {
            return $"{Value} {Units}";
        }
    }
}