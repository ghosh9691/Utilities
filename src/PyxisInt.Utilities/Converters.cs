using System;

namespace PyxisInt.Utilities
{
    public class Converters
    {
        public static int ToInt32(string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return 0;
            }
        }

        public static double ToDouble(string val)
        {
            try
            {
                return Convert.ToDouble(val);
            }
            catch
            {
                return 0.0;
            }
        }
    }
}