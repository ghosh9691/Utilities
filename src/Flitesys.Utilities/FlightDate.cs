using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities
{
    public class FlightDate
    {
        private DateTime _dateTime;
        private TimeZoneInfo _timeZone;

        private FlightDate()
        {
        }

        public FlightDate(DateTime dateTime, TimeZoneInfo tzi, DateTimeKind kind)
        {
            this._dateTime = dateTime;
            this._timeZone = tzi;
        }

        public FlightDate(DateTime dateTime, TimeZoneInfo tzi)
        {
            this._dateTime = dateTime;
            this._timeZone = tzi;
        }
    }
}