using NodaTime;
using NodaTime.Extensions;
using System;
using System.Collections.Generic;

namespace Flitesys.Utilities.Types
{
	/// <summary>
	/// FlightDate refers to the date &amp; time a flight departs. It can be used where ever a
	/// zoned datetime is required.
	/// </summary>
	public class FlightDate
	{
		private DateTime _dateTime;
		private DateTimeZone _timeZone;

		public DateTime DateTime
		{
			get
			{
				return GetDateTime();
			}
			set
			{
				SetDateTime(value);
			}
		}

		public string TimeZone
		{
			get { return _timeZone?.Id; }
			set { SetTimeZone(value); }
		}

		public FlightDate()
		{
		}

		public FlightDate(int year, int month, int day, int hour, int minute, string timeZone)
		{
			_dateTime = new DateTime(year, month, day, hour, minute, 0);
			_timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZone);
		}

		public FlightDate(DateTime dateTime, string timeZone)
		{
			_dateTime = dateTime;
			_timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZone);
		}

		public DateTime ToLocal(string targetZone)
		{
			if (_timeZone.IsNotNull())
			{
				LocalDateTime local = new LocalDateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, _dateTime.Hour, _dateTime.Minute);
				ZonedDateTime zoned = local.InZoneStrictly(_timeZone);
				DateTimeZone targetZoneInfo = DateTimeZoneProviders.Tzdb.GetZoneOrNull(targetZone);
				if (targetZoneInfo.IsNotNull())
				{
					var targetZonedDateTime = zoned.WithZone(targetZoneInfo);
					return targetZonedDateTime.ToDateTimeUnspecified();
				}
			}
			return DateTimeOffset.MinValue.DateTime;
		}

		public DateTime ToLocal()
		{
			return ToLocal(_timeZone.Id);
		}

		public DateTime ToUtc()
		{
			if (_timeZone.IsNotNull())
			{
				LocalDateTime local = new LocalDateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, _dateTime.Hour, _dateTime.Minute);
				ZonedDateTime zoned = local.InZoneStrictly(_timeZone);
				DateTimeZone targetZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Etc/UTC");
				return zoned.WithZone(targetZone).ToDateTimeUnspecified();
			}
			return DateTimeOffset.MinValue.DateTime;
		}

		public static List<string> GetTimeZones()
		{
			List<string> timeZones = new List<string>();
			foreach (DateTimeZone dtz in DateTimeZoneProviders.Tzdb.GetAllZones())
			{
				timeZones.Add(dtz.Id);
			}
			return timeZones;
		}

		private DateTime GetDateTime()
		{
			if ((_dateTime.IsNotNull()) && (_timeZone.IsNotNull()))
			{
				LocalDateTime local = new LocalDateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, _dateTime.Hour, _dateTime.Minute);
				ZonedDateTime zoned = local.InZoneStrictly(_timeZone);
				return zoned.ToDateTimeUnspecified();
			}
			throw new ArgumentException("FlightDate is not initialized!");
		}

		private void SetDateTime(DateTime value)
		{
			_dateTime = value;
		}

		private void SetTimeZone(string value)
		{
			_timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(value);
		}
	}
}