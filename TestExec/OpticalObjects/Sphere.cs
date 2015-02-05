using System;
using System.Collections.Generic;
 
using System.Text;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;
using System.Drawing;

namespace OpticalBuilderLib.OpticalObjects
{
    public class Sphere:ObjectProto,IPreloml
    {
        private DoubleExtention r;
        private Circle a;
        public DoubleExtention R
        {
            get { return r; }
            set
            {
                if(r>0)
                r = value;
                a = new Circle(a.Center, r);
                RaiseChanged();
            }
        }
        private DoubleExtention opticaldensity;
        public DoubleExtention OpticalDensity
        {
            get { return opticaldensity; }
            set 
            { 
                opticaldensity = value; 
                RaiseChanged(); 
            }
        }
        public Sphere(string savestring)
        {
            //Name^X^Y^R^id;
            string[] saves = savestring.Split('^');
            if (saves.Length != 7)
            {
                //TODO: Generate error
            }
            else
            {
                name = saves[1];
                coordinates = new SystemCoordinates(Convert.ToDouble(saves[2]), Convert.ToDouble(saves[3]));
                r = Convert.ToDouble(saves[4]);
                id = Convert.ToInt32(saves[5]);
                a = new Circle(coordinates, r);
                opticaldensity = Convert.ToDouble(saves[6]);
            }

        }
        public Sphere(string name, SystemCoordinates coords, DoubleExtention R, DoubleExtention od)
        {
            this.name = name;
            coordinates = coords;
            r = R;
            //this.id = id;
            a = new Circle(coords, R);
            opticaldensity = od;
        }
        public override string GetTypeSpecifier()
        {
            return GetSpec(3);
        }

        public override void Drawer()
        {
            CircleDrawer.Draw(Selected,coordinates,r*CoordinateSystem.Instance.Scale);
            //a.Draw(Selected);
            //OGL.DrawCircle(new Pen((!Selected)?Color.Black:Color.Blue), coordinates, r*CoordinateSystem.Instance.Scale);
        }

        public override string GenerateSaveString()
        {
            string x = GetTypeSpecifier();
            x += '^' + name + '^' + coordinates.X + '^' + coordinates.Y + '^' + r + '^' + id + '^' + opticaldensity.ToString();
            return x;
        }

        public override SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray)
        {
            SystemCoordinates point;
            Line prlLine = a.BuildKasatelnaya(ToIntersect, out PointFound, out point);
            if(PointFound == false)
            {
                angle_got = false;
                anglee = Angle.Zero;
                return point;
            }
            if(ObjectCollection.Instance.VneshDensity == OpticalDensity)
            {
                PointFound = true;
                angle_got = true;
                anglee = ToIntersect.AngleForOnePointLines;
                return point;
            }
            else
            {
                PointFound = true;
                angle_got = true;
                if(ray.Current_density == OpticalDensity)
                {
                    bool the_same;
                    anglee = prlLine.Prelom(ray.Current_density, ObjectCollection.Instance.VneshDensity, ToIntersect,
                                            out the_same);
                    ray.swap_density = !the_same;
                }
                else
                {
                    bool the_same;
                    anglee = prlLine.Prelom(ray.Current_density, OpticalDensity, ToIntersect, out the_same);
                    ray.swap_density = !the_same;
                }
                return point;
            }
        }
        public override int DistanceToPointS(Point X)
        {
            Line alpha = CoordinateSystem.Instance.Converter(X) + coordinates;
            List<SystemCoordinates> t = a.IntersectionWithInfLine(alpha);
            DoubleExtention d = t[0].Distance(CoordinateSystem.Instance.Converter(X));
            foreach (var systemCoordinatese in t)
                if (d > systemCoordinatese.Distance(CoordinateSystem.Instance.Converter(X))) d = systemCoordinatese.Distance(CoordinateSystem.Instance.Converter(X));
            return (int)(d*CoordinateSystem.Instance.Scale);
        }
        public override bool IsPreloml()
        {
            return true;
        }
        public override void MoveTo(SystemCoordinates to, SystemCoordinates from)
        {
            base.MoveTo(to, from);
            a = new Circle(coordinates, r);
        }
    }
}
