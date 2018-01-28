using Flitesys.Utilities.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities.Types
{
    public struct Weight
    {
        public double Value;

		[JsonConverter(typeof(StringEnumConverter))]
        public WeightUnits Units;

        public Weight(double val, WeightUnits unit)
        {
            this.Value = val;
            this.Units = unit;
        }
    }
}