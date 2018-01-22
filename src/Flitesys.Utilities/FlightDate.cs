using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities
{
    public class FlightDate
    {
        public DateTime Date { get; set; }
        public TimeZoneInfo TimeZone { get; set; }

        private FlightDate()
        {
        }

        public FlightDate(DateTime dateTime, TimeZoneInfo tzi, DateTimeKind kind)
        {
            this.Date = dateTime;
            this.TimeZone = tzi;
        }

        public FlightDate(DateTime dateTime, TimeZoneInfo tzi)
        {
            this.Date = dateTime;
            this.TimeZone = tzi;
        }
    }
}