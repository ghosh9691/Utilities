using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities
{
    public struct Weight
    {
        public double Value;

        [JsonConverter(typeof(StringEnumConverter))]
        public WeightUnits Units;

        public Weight(double value, WeightUnits units)
        {
            this.Value = value;
            this.Units = units;
        }
    }
}