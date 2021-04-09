using System;

namespace PrabalGhosh.Utilities.Aviation
{
    public class Atmosphere
    {
        private const double StdSeaLevelTempInC = 15.0;
        private const double StdSeaLevelTempInK = 288.15;
        private const double StdLapseRate = 0.0019811;
        private const double CentigradeToKelvin = 273.15;
        private const double TroposphereBoundary = 36089.0;
        private const double RhoAtSeaLevel = 2376.89; //lb * sec2/ft4 or 0.00237689 slugs/ft3

        /// <summary>
        /// Theta is the ratio of temperature at the specified altitude to the temperature at sea level in Kelvin
        /// </summary>
        /// <param name="altitude"></param>
        /// <returns></returns>
        public static double Theta(double altitude)
        {
            if (altitude > TroposphereBoundary)
                return 0.7519;
            else
            {
                return (StdSeaLevelTempInK - altitude * StdLapseRate / StdSeaLevelTempInK);
            }
        }

        /// <summary>
        /// Delta is the ratio of pressure at altitude to pressure at sea level
        /// </summary>
        /// <param name="altitude"></param>
        /// <returns></returns>
        public static double Delta(double altitude)
        {
            if (altitude > TroposphereBoundary)
            {
                return 0.223361 * Math.Exp((TroposphereBoundary - altitude) / 20806);
            }
            else
            {
                return Math.Exp(5.255876 * Math.Log(Theta(altitude)));
            }
        }

        /// <summary>
        /// Sigma is the ratio of density at altitude to density at sea level
        /// </summary>
        /// <param name="altitude"></param>
        /// <returns></returns>
        public static double Sigma(double altitude)
        {
            if (altitude > TroposphereBoundary)
            {
                return 0.297076 * Math.Exp((TroposphereBoundary - altitude) / 20806);
            }
            else
            {
                return Math.Exp(4.256 * Math.Log(Theta(altitude)));
            }
        }

        /// <summary>
        /// Rho is the density at altitude
        /// </summary>
        /// <param name="altitude"></param>
        /// <returns></returns>
        public static double Rho(double altitude)
        {
            return RhoAtSeaLevel * Sigma(altitude);
        }

        /// <summary>
        /// Converts altitude and temp deviation to outside air temp
        /// </summary>
        /// <param name="altitude"></param>
        /// <param name="TempDev"></param>
        /// <returns></returns>
        public static double OutsideAirTemp(double altitude, double TempDev)
        {
            return StdSeaLevelTempInK * Theta(altitude) - CentigradeToKelvin + TempDev;
        }

        /// <summary>
        /// Calculates temperature deviation given an altitude and outside air temperature
        /// </summary>
        /// <param name="altitude"></param>
        /// <param name="outsideAirTemp"></param>
        /// <returns></returns>
        public static double TemperatureDeviation(double altitude, double outsideAirTemp)
        {
            return outsideAirTemp - StdSeaLevelTempInK * Theta(altitude) + CentigradeToKelvin;
        }

        /// <summary>
        /// Convert a mach number to indicated airspeed at a given altitude
        /// </summary>
        /// <param name="mach"></param>
        /// <param name="altitude"></param>
        /// <param name="positionCorrection"></param>
        /// <returns></returns>
        public static double MachToIndicatedAirspeed(double mach, double altitude, double positionCorrection)
        {
            var x = Delta(altitude) * (Math.Exp(3.5 * Math.Log((1 + 0.2 * Math.Pow(mach, 2.0)))) - 1.0) + 1.0;
            return 1479.1 * Math.Sqrt(Math.Exp(Math.Log(x) / 3.5) - 1.0) - positionCorrection;
        }

        /// <summary>
        /// Convert indicated airspeed to mach number at an altitude
        /// </summary>
        /// <param name="ias"></param>
        /// <param name="altitude"></param>
        /// <param name="positionCorrection"></param>
        /// <returns></returns>
        public static double IndicatedAirspeedToMach(double ias, double altitude, double positionCorrection)
        {
            if (ias == 0.0)
                return 0.0;
            var x = Math.Exp(3.5 * Math.Log((1 + 0.2 * Math.Pow((ias + positionCorrection) / 661.5, 2))));
            return Math.Sqrt(5.0 * Math.Exp(Math.Log((x - 1.0) / Delta(altitude) + 1.0) / 3.5) - 5.0);
        }

        /// <summary>
        /// Convert mach number to true airspeed at given outside air temp
        /// </summary>
        /// <param name="mach"></param>
        /// <param name="outsideAirTemp"></param>
        /// <returns></returns>
        public static double MachToTrueAirspeed(double mach, double outsideAirTemp)
        {
            return 38.967 * mach * Math.Sqrt(outsideAirTemp + CentigradeToKelvin);
        }

        /// <summary>
        /// Convert true airspeed to mach number at given outside air temperature
        /// </summary>
        /// <param name="tas"></param>
        /// <param name="outsideAirTemp"></param>
        /// <returns></returns>
        public static double TrueAirspeedToMach(double tas, double outsideAirTemp)
        {
            return tas / (38.967 * Math.Sqrt(outsideAirTemp + CentigradeToKelvin));
        }

        /// <summary>
        /// Converts from True airspeed to Indicated Airspeed
        /// </summary>
        /// <param name="tas"></param>
        /// <param name="altitude"></param>
        /// <param name="oat"></param>
        /// <param name="positionCorrection"></param>
        /// <returns></returns>
        public static double TrueToIndicatedAirspeed(double tas, double altitude,
            double oat, double positionCorrection)
        {
            return MachToIndicatedAirspeed(
                TrueAirspeedToMach(tas, oat),
                altitude,
                positionCorrection);
        }

        /// <summary>
        /// Converts from Indicated to True airspeed
        /// </summary>
        /// <param name="ias"></param>
        /// <param name="altitude"></param>
        /// <param name="oat"></param>
        /// <param name="positionCorrection"></param>
        /// <returns></returns>
        public static double IndicatedToTrueAirspeed(double ias, double altitude, double oat, double positionCorrection)
        {
            return MachToTrueAirspeed(
                IndicatedAirspeedToMach(ias, altitude, positionCorrection),
                oat);
        }

        /// <summary>
        /// Converts from Celcius to Fahrenheit
        /// </summary>
        /// <param name="celcius"></param>
        /// <returns></returns>
        public static double CelciusToFahrenheit(double celcius)
        {
            return ((celcius * 18.0 + 5.0) / 10.0) + 32.0;
        }

        /// <summary>
        /// Convert Fahrenheit to Celcius
        /// </summary>
        /// <param name="fahrenheit"></param>
        /// <returns></returns>
        public static double FahrenheitToCelcius(double fahrenheit)
        {
            return (((fahrenheit - 32.0) * 10.0) - 5.0) / 18.0;
        }

        /// <summary>
        /// Convert a pressure in millibars to a pressure altitude
        /// </summary>
        /// <param name="pressureInMillibar"></param>
        /// <returns></returns>
        public static double PressureAltitude(double pressureInMillibar)
        {
            var targetDelta = pressureInMillibar / 1015.0;
            var alt = 30000.0;
            var dAlt = 20000.0;
            do
            {
                var testDelta = Delta(alt);
                if (testDelta < targetDelta)
                    alt -= dAlt;
                else
                    alt += dAlt;
                dAlt *= 0.5;
            } while (dAlt < 0.5);

            return alt;
        }
    }
}