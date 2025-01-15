using System;

namespace PrabalGhosh.Utilities.Aviation
{
    /// <summary>
    /// Represents an AIRAC (Aeronautical Information Regulation And Control) cycle.
    /// </summary>
    public class Airac
    {
        private const int DurationPerCycle = 28;
        private int _cycles;
        private DateTime _epoch = new DateTime(2010, 1, 14, 2, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Initializes a new instance of the <see cref="Airac"/> class with the specified number of cycles.
        /// </summary>
        /// <param name="cycles">The number of AIRAC cycles.</param>
        public Airac(int cycles)
        {
            _cycles = cycles;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Airac"/> class with the specified date.
        /// </summary>
        /// <param name="dateTime">The date to calculate the AIRAC cycle from.</param>
        public Airac(DateTime dateTime)
        {
            TimeSpan duration = dateTime - _epoch;
            _cycles = (int) duration.TotalDays / DurationPerCycle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Airac"/> class with the specified AIRAC cycle string.
        /// </summary>
        /// <param name="airacCycle">The AIRAC cycle string in the format YYCC.</param>
        /// <exception cref="ArgumentException">Thrown when the provided AIRAC cycle string is invalid.</exception>
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

        /// <summary>
        /// Gets the discontinue date of the current AIRAC cycle.
        /// </summary>
        /// <returns>The discontinue date.</returns>
        public DateTime GetDiscontinueDate()
        {
            return _epoch.AddDays((_cycles + 1) * DurationPerCycle).Subtract(new TimeSpan(0, 0, 1));
        }

        /// <summary>
        /// Gets the effective date of the current AIRAC cycle.
        /// </summary>
        /// <returns>The effective date.</returns>
        public DateTime GetEffectiveDate()
        {
            return _epoch.AddDays(_cycles * DurationPerCycle);
        }

        /// <summary>
        /// Gets the ordinal number of the current AIRAC cycle within the year.
        /// </summary>
        /// <returns>The ordinal number.</returns>
        public int GetOrdinal()
        {
            return (GetEffectiveDate().DayOfYear - 1) / 28 + 1;
        }

        /// <summary>
        /// Gets the year of the current AIRAC cycle.
        /// </summary>
        /// <returns>The year.</returns>
        public int GetYear()
        {
            return GetEffectiveDate().Year;
        }

        /// <summary>
        /// Returns a string representation of the current AIRAC cycle in the format YYCC.
        /// </summary>
        /// <returns>The string representation of the AIRAC cycle.</returns>
        public override string ToString()
        {
            return $"{(GetYear() % 100).ToString("D2")}{GetOrdinal().ToString("D2")}";
        }

        /// <summary>
        /// Validates the provided AIRAC cycle string.
        /// </summary>
        /// <param name="airacCycle">The AIRAC cycle string to validate.</param>
        /// <returns><c>true</c> if the AIRAC cycle string is valid; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Gets the current AIRAC cycle based on the current UTC date and time.
        /// </summary>
        /// <returns>The current AIRAC cycle string in the format YYCC.</returns>
        public static string GetCurrentAiracCycle()
        {
            var airac = new Airac(DateTime.UtcNow);
            return airac.ToString();
        }
    }
}