﻿using Flitesys.Utilities.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities.Types
{
    public class Weight
    {
        public double Value { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
        public WeightUnits Units { get; set; }

		public Weight()
		{

		}

        public Weight(double val, WeightUnits unit)
        {
            this.Value = val;
            this.Units = unit;
        }
    }
}