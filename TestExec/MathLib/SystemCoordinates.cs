using System.Drawing;
using OpticalBuilderLib.TypeExtentions;
namespace OpticalBuilderLib.MathLib
{
    public struct SystemCoordinates
    {
        DoubleExtention x;
        public DoubleExtention X
        {
            get { return x; }
            set { x = value; }
        }
        DoubleExtention y;
        public DoubleExtention Y
        {
            get { return y; }
            set { y = value; }
        }
        static SystemCoordinates zero = new SystemCoordinates(0,0);
        public static SystemCoordinates Zero
        {
            get { return zero; }
        }
        public SystemCoordinates(DoubleExtention X, DoubleExtention Y)
        {
            x = X;
            y = Y;
        }
        public SystemCoordinates(SystemCoordinates Coords)
        {
            x = Coords.X;
            y = Coords.Y;
        }
        public SystemCoordinates BuildPointReflection(SystemCoordinates point)
        {
            DoubleExtention a = point.X;
            a -= this.x;
            point.X -= (2 * a);
            a = point.Y;
            a -= this.y;
            point.Y -= (2 * a);
            return point;
        }
        public DoubleExtention Distance(SystemCoordinates point)
        {
            DoubleExtention a = (this.x - point.X) * (this.x - point.X) + (this.y - point.Y) * (this.y - point.Y);
            a.sqrt_set();
            return a;
        }
        public Point ToScreen()
        {
            return new Point(CoordinateSystem.Instance.Converter(this).X, CoordinateSystem.Instance.Converter(this).Y);
        }
        public static bool operator ==(SystemCoordinates A, SystemCoordinates B)
        {
            return A.X == B.X && A.Y == B.Y;
        }
        public static bool operator !=(SystemCoordinates A, SystemCoordinates B)
        {
            return !(A == B);
        }
        public static Line operator +(SystemCoordinates first, SystemCoordinates second)
        {
            return new Line(first, second);
        }
        public static SystemCoordinates operator -(SystemCoordinates a, SystemCoordinates b)
        {
            return new SystemCoordinates(a.X - b.X, a.Y-b.Y);
        }
        public SystemCoordinates Clone()
        {
            return new SystemCoordinates(this);
        }

        public override bool Equals(object obj)
        {
            if(obj is SystemCoordinates)
            {
                if(((SystemCoordinates)obj) == this)
                    return true;
                else return false;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return X.ToString() + '^' + Y.ToString();
        
        }
        public static implicit operator SystemCoordinates(Point p)
        {
            return CoordinateSystem.Instance.Converter(p);
        }
        public static implicit operator Point(SystemCoordinates p)
        {
            return CoordinateSystem.Instance.Converter(p);
        }
        public static bool InOneLine(SystemCoordinates p1, SystemCoordinates p2, SystemCoordinates p3)
        {
            Line l = p1 + p2;
            if (l.Vertical && p3.X == p1.X && p2.X == p1.X)
                return true;
            if (l.Vertical) 
                return false;
            if (l.BuildSecondCoordinate(p3.X).Y == p3.Y)
                return true;
            else
                return false;

        }
    }
}
