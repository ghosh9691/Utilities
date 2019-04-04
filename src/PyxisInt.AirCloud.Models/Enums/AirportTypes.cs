using System;
namespace PyxisInt.AirCloud.Models.Enums
{
    [Flags]
    public enum AirportTypes : short
    {
        None = 0,
        Departure = 1,
        Arrival = 2,
        PrimaryAlternate = 4,
        SecondaryAlternate = 8,
        RefileAlternate = 16,
        TakeoffAlternate = 32,
        EnrouteAlternate = 64,
        ETOPSAlternate = 128,
        All = 256
    }
}
