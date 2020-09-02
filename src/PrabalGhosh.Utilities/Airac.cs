using System;

namespace PrabalGhosh.Utilities
{
    public class Airac
    {
        private const int DurationPerCycle = 28;
        private int _cycles;
        private DateTime _epoch = new DateTime(2010, 1, 14, 2, 0, 0, DateTimeKind.Utc);

        public Airac(int cycles)
        {
            _cycles = cycles;
        }

        public Airac(DateTime dateTime)
        {
            TimeSpan duration = dateTime - _epoch;
            _cycles = (int)duration.TotalDays / DurationPerCycle;
        }

        public Airac(string airacCycle)
        {
            if (!Validate(airacCycle))
                throw new ArgumentException($"Invalid Airac Cycle provided! {airacCycle}");
            int year = Convert.ToInt32(airacCycle.Substring(0, 2));
            if (year >= 10)
                year += 2000;
            else
                year += 2100;
            int cycle = Convert.ToInt32(airacCycle.Substring(2, 2));
            Airac lastOfPreviousYear = new Airac(new DateTime(year - 1, 12, 31, 2, 0, 0, DateTimeKind.Utc));
            _cycles = lastOfPreviousYear._cycles + cycle;
        }

        private Airac()
        {
        }

        public DateTime GetDiscontinueDate()
        {
            return _epoch.AddDays((_cycles + 1) * DurationPerCycle).Subtract(new TimeSpan(0, 0, 1));
        }

        public DateTime GetEffectiveDate()
        {
            return _epoch.AddDays(_cycles * DurationPerCycle);
        }

        public int GetOrdinal()
        {
            return (GetEffectiveDate().DayOfYear - 1) / 28 + 1;
        }

        public int GetYear()
        {
            return GetEffectiveDate().Year;
        }

        public override string ToString()
        {
            return $"{(GetYear() % 100).ToString("D2")}{GetOrdinal().ToString("D2")}";
        }

        private bool Validate(string airacCycle)
        {
            if (airacCycle.Length != 4)
                return false;
            try
            {
                Convert.ToInt32(airacCycle);
            }
            catch
            {
                return false;
            }
            int cycle = Convert.ToInt32(airacCycle.Substring(2, 2));
            if ((cycle <= 0) || (cycle > 14))
                return false;
            return true;
        }

        public static string GetCurrentAiracCycle()
        {
            var airac = new Airac(DateTime.UtcNow);
            return airac.ToString();
        }
    }
}