using Flitesys.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities.Types
{
    public struct Weight
    {
        public double Value;
        public WeightUnits Units;

        public Weight(double val, WeightUnits unit)
        {
            this.Value = val;
            this.Units = unit;
        }
    }
}