using System;
namespace PyxisInt.AirCloud.Models.Navigation
{
    public class OperationalAirport
    {
        public Guid AirportId { get; set; }
        public Airport Airport { get; set; }

        public Guid AirlineId { get; set; }
        public Airline Airline { get; set; }
    }
}
