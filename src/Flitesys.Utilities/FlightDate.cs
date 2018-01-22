using System;
using System.Collections.Generic;
using System.Text;

namespace Flitesys.Utilities
{
    public class FlightDate
    {
        private DateTime _date;
        private TimeZoneInfo _timeZone;

        public DateTime Local
        {
            get
            {
                return _date.ToLocalTime();
            }
        }

        public DateTime Utc
        {
            get
            {
                return _date.ToUniversalTime();
            }
        }

        private FlightDate()
        {
        }

        public FlightDate(DateTime dateTime, TimeZoneInfo tzi, DateTimeKind kind)
        {
            this._date = dateTime;
            this._timeZone = tzi;
        }

        public FlightDate(DateTime dateTime, TimeZoneInfo tzi)
        {
            this._date = dateTime;
            this._timeZone = tzi;
        }
    }
}