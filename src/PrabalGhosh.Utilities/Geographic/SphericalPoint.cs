using System;

namespace PrabalGhosh.Utilities.Geographic
{
    public class SphericalPoint
    {
        private const double B = 1.003364089859676;     //inverse complement of ellipticity - WGS84
        public double CoLatitude { get; set; }
        public double Longitude { get; set; }

        public GeographicPoint ToGeographic()
        {
            var geo = new GeographicPoint();
            geo.Latitude = Math.PI / 2.0 - this.CoLatitude;
            if ((geo.Latitude < Math.PI / 2.0) && (geo.Latitude > (-Math.PI / 2.0)))
            {
                geo.Latitude = Math.Atan(B * Math.Sin(geo.Latitude) / Math.Cos(geo.Latitude));
            }

            geo.Latitude = geo.Latitude * 180.0 / Math.PI;
            geo.Longitude = this.Longitude * 180.0 / Math.PI;
            return geo;
        }
    }
}