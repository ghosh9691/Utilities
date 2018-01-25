using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities.Types
{
    public struct FlightDate
    {
        public ZonedClock Date;

        public FlightDate(ZonedClock date)
        {
            this.Date = date;
        }
    }
}