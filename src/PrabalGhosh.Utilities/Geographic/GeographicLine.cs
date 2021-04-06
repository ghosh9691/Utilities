using System;
using NetTopologySuite.Geometries;
using PyxisInt.GeographicLib;

namespace PrabalGhosh.Utilities.Geographic
{
    public class GeographicLine
    {
        public GeographicPoint Start { get; set; }
        public GeographicPoint End { get; set; }

        public GeographicLine()
        {
            
        }

        public GeographicLine(GeographicPoint start, GeographicPoint end)
        {
            this.Start = start;
            this.End = end;
        }

        public GeographicLine(Point start, Point end)
        {
            this.Start = new GeographicPoint(start);
            this.End = new GeographicPoint(end);
        }

        public GeographicResult GreatCircle()
        {
            return Start.DistanceTo(End);
        }

        public GeographicPoint Intersect(GeographicLine line)
        {
            var geod = Geodesic.WGS84;
            var gn = new Gnomonic(geod);
            //guess the intersection point...
            var gc = line.GreatCircle();
            var dist = gc.Distance / 2.0;
            var intersection = line.Start.DestinationPoint(gc.InitialCourse, dist);
            var latI = intersection.Latitude;
            var lonI = intersection.Longitude;
            ConsoleEx.WriteMessage($"Initial Guess: {latI}/{lonI}");
            for (int i = 0; i < 10; i++)
            {
                var xa1 = gn.Forward(latI, lonI, line.Start.Latitude, line.Start.Longitude);
                var xa2 = gn.Forward(latI, lonI, line.End.Latitude, line.End.Longitude);
                var xb1 = gn.Forward(latI, lonI, this.Start.Latitude, this.Start.Longitude);
                var xb2 = gn.Forward(latI, lonI, this.End.Latitude, this.End.Longitude);
                var va1 = new Vector(xa1.x, xa1.y);
                var va2 = new Vector(xa2.x, xa2.y);
                var vb1 = new Vector(xb1.x, xb1.y);
                var vb2 = new Vector(xb2.x, xb2.y);
                var la = va1.Cross(va2);
                var lb = vb1.Cross(vb2);
                var p0 = la.Cross(lb);
                p0.Norm();
                var solved = gn.Reverse(latI, lonI, p0.X, p0.Y);
                latI = solved.PointLatitude;
                lonI = solved.PointLongitude;
                ConsoleEx.WriteMessage($"Run {i}: Guessed {latI}/{lonI}");
            }

            var intPoint = new GeographicPoint(latI, lonI);
            var gd1 = geod.Inverse(line.Start.Latitude, line.Start.Longitude, intPoint.Latitude, intPoint.Longitude);
            var gd2 = geod.Inverse(intPoint.Latitude, intPoint.Longitude, line.End.Latitude, line.End.Longitude);
            if (Math.Abs(gd1.FinalAzimuth - gd2.InitialAzimuth) <= 0.001)
            {
                return intPoint;
            }
            return null;
        }
    }
}