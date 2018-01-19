using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities
{
    public struct Weight
    {
        public double Value;
        public WeightUnits Units;

        public Weight(double value, WeightUnits units)
        {
            this.Value = value;
            this.Units = units;
        }
    }
}