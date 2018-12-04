using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PyxisInt.Utilities.Enums;

namespace PyxisInt.Utilities.Types
{
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
    }
}