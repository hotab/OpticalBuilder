using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.OpticalObjects
{
    public class OPolygon: ObjectProto,IPreloml
    {
        private Polygon poly;
        public override string GetTypeSpecifier()
        {
            return GetSpec(ObjectTypes.Polygon);
        }

        public override void Drawer()
        {
            Line[] x = poly.Lines;
            foreach (Line z in x)
                z.Drawer(Selected ? Color.Blue : Color.Black, 2);
        }
        public OPolygon(Polygon pl, DoubleExtention density, string name)
        {
            poly = pl;
            this.density = density;
            this.name = name;
            coordinates = poly.Center.Clone();
        }
        public OPolygon(string savestr)
        {
            string[] splitted = savestr.Split('^');
            name = splitted[1];
            int count = Convert.ToInt32(splitted[3]);
            SystemCoordinates[] arr = new SystemCoordinates[count];
            density = Convert.ToDouble(splitted[2]);
            for(int i = 0; i<count; i++)
            {
                DoubleExtention x = Convert.ToDouble(splitted[2*i+4]);
                DoubleExtention y = Convert.ToDouble(splitted[2*i+5]);
                arr[i].X = x;
                arr[i].Y = y;
            }
            poly = new Polygon(arr);
            coordinates = poly.Center.Clone();
        }
        public override string GenerateSaveString()
        {
            string rt = "";
            rt = rt + GetTypeSpecifier() + '^' + name + '^' + density;
            SystemCoordinates[] rng = poly.Ring;
            rt = rt + '^' + rng.Length;
            for(int i = 0; i<rng.Length; i++)
                rt = rt + '^' + rng[i].ToString();
            return rt;

        }
        public Angle Anglee
        {
            get{return new Angle(0,false);}
            set { poly.Rotate(value);
                coordinates = poly.Center.Clone();
            }
        }
        public override SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray)
        {
            bool isects;
            SystemCoordinates point;
            Line isector = poly.IntersectWithLine(ToIntersect, out isects, out point);
            if (isects == false)
            {
                PointFound = false;
                angle_got = false;
                anglee = Angle.Zero;
                return SystemCoordinates.Zero;
            }
            else
            {
                PointFound = true;
                angle_got = true;
                if (ray.Current_density == OpticalDensity)
                {
                    bool the_same;
                    anglee = isector.Prelom(ray.Current_density, ObjectCollection.Instance.VneshDensity, ToIntersect,
                                            out the_same);
                    ray.swap_density = !the_same;
                }
                else
                {
                    bool the_same;
                    anglee = isector.Prelom(ray.Current_density, OpticalDensity, ToIntersect, out the_same);
                    ray.swap_density = !the_same;
                }
                return point;
            }
        }

        public override int DistanceToPointS(Point X)
        {
            Line[] z = poly.Lines;
            DoubleExtention dst = z[0].DistanceToPoint(X);
            DoubleExtention tmp;
            for(int i = 1; i<z.Length; i++)
            {
                tmp = z[i].DistanceToPoint(X);
                if (tmp < dst) dst = tmp;
            }
            dst *= CoordinateSystem.Instance.Scale;
            return dst.ToInt32();
        }
        public override bool IsPreloml()
        {
            return true;
        }
        public override void MoveTo(SystemCoordinates to, SystemCoordinates from)
        {
            poly.MoveTo(from,to);
            coordinates = poly.Center.Clone();
        }
        public override void Rotate(SystemCoordinates to, SystemCoordinates from)
        {
            Angle fromm, too;
            DoubleExtention dst =
                ((from.X - poly.Center.X)*(from.X - poly.Center.X) + (from.Y - poly.Center.Y)*(from.Y - poly.Center.Y)).
                    sqrt();
            fromm = new Angle((from.Y - poly.Center.Y)/dst, (from.X - poly.Center.X)/dst);
            dst =
                ((to.X - poly.Center.X) * (to.X - poly.Center.X) + (to.Y - poly.Center.Y) * (to.Y - poly.Center.Y)).
                    sqrt();
            too = new Angle((to.Y - poly.Center.Y) / dst, (to.X - poly.Center.X) / dst);
            poly.Rotate(new Angle(too.GetInDegrees() - fromm.GetInDegrees(), false));
            coordinates = poly.Center.Clone();
        }
        public void Rotate(Angle an)
        {
            poly.Rotate(an);
            coordinates = poly.Center.Clone();
        }
        #region IPreloml Members

        private DoubleExtention density;
        public DoubleExtention OpticalDensity
        {
            get
            {
                return density;
            }
            set
            {
                density = value;
            }
        }

        #endregion
    }
}
