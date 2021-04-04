namespace PrabalGhosh.Utilities.Geographic
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z = 1.0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector Cross(Vector b)
        {
            return new Vector(
                Y * b.Z - Z * b.Y,
                Z * b.X - X * b.Z,
                X * b.Y - Y * b.X
                );
        }

        public void Norm()
        {
            X /= Z;
            Y /= Z;
            Z = 1.0;
        }
    }
}