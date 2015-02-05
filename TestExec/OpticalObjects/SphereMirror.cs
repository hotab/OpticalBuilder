using System;
using System.Collections.Generic;
 
using System.Text;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;
using System.Drawing;

namespace OpticalBuilderLib.OpticalObjects
{
    public class SphereMirror:ObjectProto
    {
        private DoubleExtention r;
        private Circle a;
        private SystemCoordinates p2;
        private bool vognutoye;
        public DoubleExtention R
        {
            get { return r; }
            set
            {
                SystemCoordinates P1 = a.LLimitPoint, P2 = p2, P3 = a.ULimitPoint; 
                if(r>0)
                r = value;
                Line tmp;
                tmp = new Line(a.Center, P1, false);
                tmp.BuildAngle();
                P1 = new SystemCoordinates(a.Center.X + r * tmp.AngleForOnePointLines.cos(), a.Center.Y + r * tmp.AngleForOnePointLines.sin());
                tmp = new Line(a.Center, P2, false);
                tmp.BuildAngle();
                P2 = new SystemCoordinates(a.Center.X + r * tmp.AngleForOnePointLines.cos(), a.Center.Y + r * tmp.AngleForOnePointLines.sin());
                tmp = new Line(a.Center, P3, false);
                tmp.BuildAngle();
                P3 = new SystemCoordinates(a.Center.X + r * tmp.AngleForOnePointLines.cos(), a.Center.Y + r * tmp.AngleForOnePointLines.sin());
                a = new Circle(P1, P2, P3);
                p2 = P2;
                RaiseChanged();
            }
        }
        public bool IsVogn
        {
            get { return vognutoye; }
            set { vognutoye = value; RaiseChanged();}
        }
        public DoubleExtention UpperAngle
        {
            get { if (a.UpperLimitAngle.GetInDegrees() < a.LowerLimitAngle.GetInDegrees()) return a.UpperLimitAngle.GetInDegrees(); else return a.LowerLimitAngle.GetInDegrees(); }
            set
            {
                if (a.UpperLimitAngle.GetInDegrees() < a.LowerLimitAngle.GetInDegrees())
                {
                    Angle x = new Angle(value,false);
                    if (x.GetInDegrees() == a.LowerLimitAngle.GetInDegrees()) return;
                    DoubleExtention sum = a.LowerLimitAngle.GetInDegrees() + value;
                    sum /= 2;
                    sum += 180;
                    Angle t = new Angle(sum, false);
                    SystemCoordinates P1 = new SystemCoordinates(a.Center.X + a.Radius*a.LowerLimitAngle.cos(),
                                                                 a.Center.Y + a.Radius*a.LowerLimitAngle.sin());
                    SystemCoordinates P2 = new SystemCoordinates(a.Center.X + a.Radius*t.cos(),
                                                                 a.Center.Y + a.Radius*t.sin());
                    t = new Angle(value, false);
                    SystemCoordinates P3 = new SystemCoordinates(a.Center.X + a.Radius*t.cos(),
                                                                 a.Center.Y + a.Radius*t.sin());
                    a = new Circle(P1, P2, P3);
                    RaiseChanged();
                }
                else
                {
                    Angle x = new Angle(value, false);
                    if (x.GetInDegrees() == a.UpperLimitAngle.GetInDegrees()) return;
                    DoubleExtention sum = a.UpperLimitAngle.GetInDegrees() + value;
                    sum /= 2;
                    Angle t = new Angle(sum, false);
                    SystemCoordinates P1 = new SystemCoordinates(a.Center.X + a.Radius * a.UpperLimitAngle.cos(),
                                                                 a.Center.Y + a.Radius * a.UpperLimitAngle.sin());
                    SystemCoordinates P2 = new SystemCoordinates(a.Center.X + a.Radius * t.cos(),
                                                                 a.Center.Y + a.Radius * t.sin());
                    t = new Angle(value, false);
                    SystemCoordinates P3 = new SystemCoordinates(a.Center.X + a.Radius * t.cos(),
                                                                 a.Center.Y + a.Radius * t.sin());
                    a = new Circle(P1, P2, P3);
                    RaiseChanged();
                }
            }
        }
        public DoubleExtention LowerAngle
        {
            get { if (a.UpperLimitAngle.GetInDegrees() < a.LowerLimitAngle.GetInDegrees()) return a.LowerLimitAngle.GetInDegrees(); else return a.UpperLimitAngle.GetInDegrees(); }
            set
            {
                if (a.UpperLimitAngle.GetInDegrees() < a.LowerLimitAngle.GetInDegrees())
                {
                    Angle x = new Angle(value, false);
                    if (x.GetInDegrees() == a.UpperLimitAngle.GetInDegrees()) return;
                    DoubleExtention sum = a.UpperLimitAngle.GetInDegrees() + value;
                    sum /= 2;
                    sum += 180;
                    Angle t = new Angle(sum, false);
                    SystemCoordinates P1 = new SystemCoordinates(a.Center.X + a.Radius * a.UpperLimitAngle.cos(),
                                                                 a.Center.Y + a.Radius * a.UpperLimitAngle.sin());
                    SystemCoordinates P2 = new SystemCoordinates(a.Center.X + a.Radius * t.cos(),
                                                                 a.Center.Y + a.Radius * t.sin());
                    t = new Angle(value, false);
                    SystemCoordinates P3 = new SystemCoordinates(a.Center.X + a.Radius * t.cos(),
                                                                 a.Center.Y + a.Radius * t.sin());
                    a = new Circle(P1, P2, P3);
                    RaiseChanged();
                }
                else
                {
                    Angle x = new Angle(value, false);
                    if (x.GetInDegrees() == a.LowerLimitAngle.GetInDegrees()) return;
                    DoubleExtention sum = a.LowerLimitAngle.GetInDegrees() + value;
                    sum /= 2;
                    Angle t = new Angle(sum, false);
                    SystemCoordinates P1 = new SystemCoordinates(a.Center.X + a.Radius * a.LowerLimitAngle.cos(),
                                                                 a.Center.Y + a.Radius * a.LowerLimitAngle.sin());
                    SystemCoordinates P2 = new SystemCoordinates(a.Center.X + a.Radius * t.cos(),
                                                                 a.Center.Y + a.Radius * t.sin());
                    t = new Angle(value, false);
                    SystemCoordinates P3 = new SystemCoordinates(a.Center.X + a.Radius * t.cos(),
                                                                 a.Center.Y + a.Radius * t.sin());
                    a = new Circle(P1, P2, P3);
                    RaiseChanged();
                }
            }
        }
        public SphereMirror(string savestring)
        {
            string[] saves = savestring.Split('^');
            if (saves.Length != 13)
            {
                //TODO: Generate error
            }
            else
            {
                name = saves[1];
                coordinates = new SystemCoordinates(Convert.ToDouble(saves[2]), Convert.ToDouble(saves[3]));
                r = Convert.ToDouble(saves[4]);
                id = Convert.ToInt32(saves[5]);
                a = new Circle(new SystemCoordinates(Convert.ToDouble(saves[10]), Convert.ToDouble(saves[11])), new SystemCoordinates(Convert.ToDouble(saves[8]), Convert.ToDouble(saves[9])), new SystemCoordinates(Convert.ToDouble(saves[6]), Convert.ToDouble(saves[7])));
                p2 = new SystemCoordinates(Convert.ToDouble(saves[8]), Convert.ToDouble(saves[9]));
                vognutoye = Convert.ToBoolean(saves[12]);
            }

        }
        public SphereMirror(string name, SystemCoordinates coords, DoubleExtention R, SystemCoordinates p1, SystemCoordinates p2, SystemCoordinates p3, bool Vognutoye)
        {
            this.name = name;
            coordinates = coords;
            r = R;
            a = new Circle(p1,p2,p3);
            this.p2 = p2;
            vognutoye = Vognutoye;
        }
        public override string GetTypeSpecifier()
        {
            return GetSpec(6);
        }

        public override void Drawer()
        {
            DoubleExtention l1 = a.LowerLimitAngle.GetInRadians(), l2 = a.UpperLimitAngle.GetInRadians();
            if (l1 > l2) l1 -= 2*Math.PI;
            if(vognutoye == false)
            OGL.DrawCirclePart(new Pen(Color.Red, 4), a.Center, ((r * CoordinateSystem.Instance.Scale - 2) <= 0) ? 1 : (r * CoordinateSystem.Instance.Scale - 2), l1, l2);
            else
                OGL.DrawCirclePart(new Pen(Color.Red, 4), a.Center, r * CoordinateSystem.Instance.Scale + 2, l1, l2);
            a.Draw(Selected);
            //OGL.DrawFilledCircle(new Pen(Color.Red, 4), coordinates, ((r * CoordinateSystem.Instance.Scale - 2) <= 0) ? 1 : (r * CoordinateSystem.Instance.Scale - 2));
            //a.Draw(Selected);
            //OGL.DrawCircle(new Pen((!Selected)?Color.Black:Color.Blue), coordinates, r*CoordinateSystem.Instance.Scale);
        }

        public override string GenerateSaveString()
        {
            string x = GetTypeSpecifier();
            x += '^' + name + '^' + coordinates.X + '^' + coordinates.Y + '^' + r + '^' + id + '^' + a.ULimitPoint.ToString() + '^' + p2.ToString() + '^' + a.LLimitPoint.ToString() + '^' + vognutoye.ToString();
            return x;
        }

        public override SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray)
        {
            SystemCoordinates point;
            Line prlLine = a.BuildKasatelnaya(ToIntersect, out PointFound, out point);
            prlLine.BuildAngle();
            if(PointFound == false)
            {
                angle_got = false;
                anglee = Angle.Zero;
                return point;
            }
            bool intersect;
            bool equal;
            PointFound = false;
            SystemCoordinates A = ToIntersect.IntersectWithLine(prlLine, out intersect, out equal);
            if (intersect) PointFound = true;
            if (equal) PointFound = false;
            if (PointFound)
            {
                bool usloviye;
                anglee = BuildAngle(out usloviye, ToIntersect.AngleForOnePointLines, prlLine.AngleForOnePointLines);
                if (usloviye)
                {
                    Line beta = new Line(ToIntersect);
                    beta.SecondEnd = A;
                    if ((!RayGoesFromInside(beta) && !vognutoye) || (RayGoesFromInside(beta) && vognutoye))
                    {
                        angle_got = true;
                    }
                    else
                    {
                        angle_got = false;
                    }
                }
                else angle_got = false;


            }
            else
            {
                angle_got = false;
                anglee = Angle.Zero;
            }
            if (!PointFound) return SystemCoordinates.Zero;
            else return A;
        }
        public override int DistanceToPointS(Point X)
        {
            Line alpha = CoordinateSystem.Instance.Converter(X) + coordinates;
            List<SystemCoordinates> t = a.IntersectionWithInfLine(alpha);
            t.Add(a.ULimitPoint);
            t.Add(a.LLimitPoint);
            DoubleExtention d = t[0].Distance(CoordinateSystem.Instance.Converter(X));
            foreach (var systemCoordinatese in t)
                if (d > systemCoordinatese.Distance(CoordinateSystem.Instance.Converter(X))) d = systemCoordinatese.Distance(CoordinateSystem.Instance.Converter(X));
            return (int)(d*CoordinateSystem.Instance.Scale);
        }
        Angle BuildAngle(out bool is_built, Angle a, Angle angle)
        {
            Angle b = new Angle(a.GetInDegrees() - angle.GetInDegrees(), false);
            if (b.GetInDegrees() <= 180)
            {
                b = new Angle(360 - b.GetInDegrees() + angle.GetInDegrees(), false);
                is_built = true;
                return b;
            }
            else
            {
                b = new Angle(360 - b.GetInDegrees() + angle.GetInDegrees(), false);
                is_built = true;
                return b;
            }
        }
        bool RayGoesFromInside(Line x)
        {
            bool isect, equal;
            x.BuildOrthogonalLine(coordinates).IntersectWithLine(x, out isect, out equal);
            if(!isect)
            {
                if(x.FirstEnd.Distance(coordinates) < r) isect = true;
            }
            return isect;
        }
        public override void MoveTo(SystemCoordinates to, SystemCoordinates from)
        {
            base.MoveTo(to, from);
            a.MoveTo(from, to);
            p2 = new SystemCoordinates(p2.X + to.X - from.X, p2.Y + to.Y - from.Y);
        }
    }
}
