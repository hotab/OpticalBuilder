using System;
using System.Collections.Generic;
 
using System.Text;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.Exceptions;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;
using System.Drawing;

namespace OpticalBuilderLib.OpticalObjects
{
    public class Lense:ObjectProto,IPreloml
    {
        private Line menisk1;
        private Line menisk2;
        private SystemCoordinates p2, p5;
        private DoubleExtention menisk;
        private Circle c1;
        bool ic1;
        private Line l1;
        private Circle c2;
        bool ic2;
        private Line l2;
        public Angle Anglee
        {
            get
            {
                Line l = new Line(P1,P3,false);
                l.FirstEnd = P1;
                l.BuildAngle();
                return l.AngleForOnePointLines;
            }
            set
            {
                Rotate(value.GetInDegrees());
            }
        }
        SystemCoordinates P1
        {
            get
            {
                if (ic1)
                    return new SystemCoordinates(c1.Center.X + c1.Radius * c1.LowerLimitAngle.cos(), c1.Center.Y + c1.Radius * c1.LowerLimitAngle.sin());
                else
                    return new SystemCoordinates(l1.FirstEnd);
            }
        }
        SystemCoordinates P3
        {
            get
            {
                if (ic1)
                    return new SystemCoordinates(c1.Center.X + c1.Radius * c1.UpperLimitAngle.cos(), c1.Center.Y + c1.Radius * c1.UpperLimitAngle.sin());
                else
                    return new SystemCoordinates(l1.SecondEnd);
            }
        }
        SystemCoordinates P4
        {
            get
            {
                if (ic2)
                return new SystemCoordinates(c2.Center.X + c2.Radius * c2.LowerLimitAngle.cos(), c2.Center.Y + c2.Radius * c2.LowerLimitAngle.sin());
                else
                    return new SystemCoordinates(l2.FirstEnd);
            }
        }
        SystemCoordinates P6
        {
            get
            {
                if (ic2)
                return new SystemCoordinates(c2.Center.X + c2.Radius * c2.UpperLimitAngle.cos(), c2.Center.Y + c2.Radius * c2.UpperLimitAngle.sin());
                else
                    return new SystemCoordinates(l2.SecondEnd);
            }
        }
        SystemCoordinates P2
        {
            get { return p2; }
        }
        SystemCoordinates P5
        {
            get { return p5; }
        }
        public DoubleExtention D1
        {
            get
            {
                if(ic1)
                {
                    return c1.DistanceToPoint(P2);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value == 0)
                {
                    c1 = null;
                    ic1 = false;
                    l1 = new Line(P1, P3, false);
                }
                else
                {
                    l1 = new Line(P1, P3, false);
                    Line z;
                    if(value < 0)
                    {
                        value = -value;
                        z = l1.BuildOrthogonalLine(l1.Center);
                        if(z.Vertical)
                            z.FirstEnd = new SystemCoordinates(l1.Center.X, l1.Center.Y + value);
                        else
                        {
                            z.BuildAngle();
                            DoubleExtention x = -z.AngleForOnePointLines.cos()*value + l1.Center.X;
                            z.FirstEnd = z.BuildSecondCoordinate(x);
                        }
                    }
                    else
                    {
                        z = l1.BuildOrthogonalLine(l1.Center);
                        if (z.Vertical)
                            z.FirstEnd = new SystemCoordinates(l1.Center.X, l1.Center.Y - value);
                        else
                        {
                            z.BuildAngle();
                            DoubleExtention x = z.AngleForOnePointLines.cos() * value + l1.Center.X;
                            z.FirstEnd = z.BuildSecondCoordinate(x);
                        }
                    }
                    c1 = new Circle(P1,z.FirstEnd, P3);
                    p2 = z.FirstEnd;
                    ic1 = true;
                }
            }
        }
        public DoubleExtention D2
        {
            get
            {
                if (ic2)
                {
                    return c2.DistanceToPoint(P5);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value == 0)
                {
                    c2 = null;
                    ic2 = false;
                    l2 = new Line(P4, P6, false);
                }
                else
                {
                    
                    l2 = new Line(P4, P6, false);
                    Line z;
                    if (value < 0)
                    {
                        value = -value;
                        z = l2.BuildOrthogonalLine(l2.Center);
                        if (z.Vertical)
                            z.FirstEnd = new SystemCoordinates(l2.Center.X, l2.Center.Y + value);
                        else
                        {
                            z.BuildAngle();
                            DoubleExtention x = -z.AngleForOnePointLines.cos() * value + l2.Center.X;
                            z.FirstEnd = z.BuildSecondCoordinate(x);
                        }
                    }
                    else
                    {
                        z = l2.BuildOrthogonalLine(l2.Center);
                        if (z.Vertical)
                            z.FirstEnd = new SystemCoordinates(l2.Center.X, l2.Center.Y - value);
                        else
                        {
                            z.BuildAngle();
                            DoubleExtention x = z.AngleForOnePointLines.cos() * value + l2.Center.X;
                            z.FirstEnd = z.BuildSecondCoordinate(x);
                        }
                    }
                    c2 = new Circle(P4, z.FirstEnd, P6);
                    p5 = z.FirstEnd;
                    ic2 = true;
                }
            }
        }
        public DoubleExtention R1
        {
            get
            {
                Line p = new Line(P1, P3, false);
                if (ic1)
                    if (p.PointTopLeft(p2)) return -c1.Radius;
                    else return c1.Radius;
                else return 0;
            }
            set
            {
                if (value == 0)
                {
                    ic1 = false;
                    l1 = new Line(P1,P3,false);
                    D1 = 0;
                    return;
                }
                //l*l/4 = x*(D-x)
                //z = l * l/4;
                //z = Dx - x*x;
                //h*h - Dh + z = 0;
                DoubleExtention D = Math.Abs(value*2);
                if ((D * D) < (P1.Distance(P3)) * (P1.Distance(P3)))
                {
                    return;
                }
                DoubleExtention z = (P1.Distance(P3))*(P1.Distance(P3));
                DoubleExtention h = (D - ((D*D - z).sqrt()))/2;
                if (value > 0)
                {
                    D1 = h;
                }
                else
                {
                    D1 = -h;
                }
            }
        }
        public DoubleExtention R2
        {
            get
            {
                Line p = new Line(P4, P6, false);
                if (ic2)
                    if (p.PointTopLeft(p5)) return -c2.Radius;
                    else return c2.Radius;
                else return 0;
            }
            set
            {
                if (value == 0)
                {
                    D2 = 0;
                    return;
                }
                DoubleExtention D = Math.Abs(value * 2);
                if ((D * D) <= (P4.Distance(P6)) * (P4.Distance(P6)))
                {
                    return;
                }
                DoubleExtention z = (P4.Distance(P6)) * (P4.Distance(P6));
                DoubleExtention h = (D - ((D * D - z).sqrt())) / 2;
                if (value > 0)
                {
                    D2 = h;
                }
                else
                {
                    D2 = -h;
                }
            }
        }
        public override string GetTypeSpecifier()
        {
            return GetSpec(ObjectTypes.Lense);
        }
        public Lense(string name, SystemCoordinates center, SystemCoordinates p1, SystemCoordinates p2, SystemCoordinates p3, SystemCoordinates p4, SystemCoordinates p5, SystemCoordinates p6)
        {
            ic1 = false;
            ic2 = false;
            if (SystemCoordinates.InOneLine(p1, p2, p3))
                l1 = new Line(p1, p3, false);
            else
            {
                ic1 = true;
                c1 = new Circle(p1, p2, p3);
            }
            if (SystemCoordinates.InOneLine(p4, p5, p6))
                l2 = new Line(p4, p6, false);
            else
            {
                ic2 = true;
                c2 = new Circle(p4, p5, p6);
            }
            Line l = new Line(p1,p3, false);
            menisk = DoubleExtention.MinimumOf(p3.Distance(p4), p3.Distance(p6));
            if(menisk != 0)
            if(p3.Distance(p4) < p3.Distance(p6))
            {
                menisk1 = new Line(p3,p4,false);
                menisk2 = new Line(p1,p6,false);
            }
            else
            {
                menisk1 = new Line(p3, p6, false);
                menisk2 = new Line(p1, p4, false);
            }
            coordinates = center;
            this.name = name;
            this.p2 = p2;
            this.p5 = p5;
        }
        public Lense(string savestr)
        {
            string[] splitted = savestr.Split('^');
            if(splitted.Length!=18)
            {
                throw new ErrorWhileReading();
            }
            else
            {
                try
                {
                    name = splitted[1];
                    coordinates = new SystemCoordinates(Convert.ToDouble(splitted[2]), Convert.ToDouble(splitted[3]));
                    SystemCoordinates p1, p2, p3, p4, p5, p6;
                    p1 = new SystemCoordinates(Convert.ToDouble(splitted[4]), Convert.ToDouble(splitted[5]));
                    p2 = new SystemCoordinates(Convert.ToDouble(splitted[6]), Convert.ToDouble(splitted[7]));
                    p3 = new SystemCoordinates(Convert.ToDouble(splitted[8]), Convert.ToDouble(splitted[9]));
                    p4 = new SystemCoordinates(Convert.ToDouble(splitted[10]), Convert.ToDouble(splitted[11]));
                    p5 = new SystemCoordinates(Convert.ToDouble(splitted[12]), Convert.ToDouble(splitted[13]));
                    p6 = new SystemCoordinates(Convert.ToDouble(splitted[14]), Convert.ToDouble(splitted[15]));
                    ic1 = false;
                    ic2 = false;
                    if (SystemCoordinates.InOneLine(p1, p2, p3))
                        l1 = new Line(p1, p3, false);
                    else
                    {
                        ic1 = true;
                        c1 = new Circle(p1, p2, p3);
                    }
                    if (SystemCoordinates.InOneLine(p4, p5, p6))
                        l2 = new Line(p4, p6, false);
                    else
                    {
                        ic2 = true;
                        c2 = new Circle(p4, p5, p6);
                    }
                    Line l = new Line(p1, p3, false);
                    menisk = DoubleExtention.MinimumOf(p3.Distance(p4), p3.Distance(p6));
                    if (menisk != 0)
                        if (p3.Distance(p4) < p3.Distance(p6))
                        {
                            menisk1 = new Line(p3, p4, false);
                            menisk2 = new Line(p1, p6, false);
                        }
                        else
                        {
                            menisk1 = new Line(p3, p6, false);
                            menisk2 = new Line(p1, p4, false);
                        }
                    id = Convert.ToInt32(splitted[16]);
                    opticalDensity = Convert.ToDouble(splitted[17]);
                    this.p2 = p2;
                    this.p5 = p5;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
        public override void Drawer()
        {
            if (ic1)
                c1.Draw(Selected);
            else
                l1.Drawer(Selected ? Color.Blue : Color.Black, 2);
            if (ic2)
                c2.Draw(Selected);
            else
                l2.Drawer(Selected ? Color.Blue : Color.Black, 2);
            if(menisk)
            {
                menisk1.Drawer(Selected ? Color.Blue : Color.Black, 2);
                menisk2.Drawer(Selected ? Color.Blue : Color.Black, 2);
            }
        }

        public override string GenerateSaveString()
        {
            return GetTypeSpecifier() + '^' + name + '^' + coordinates + '^' + P1 + '^' + P2 + '^' + P3 + '^' + P4 + '^' +
                   P5 + '^' + P6 + '^' + id + '^' + opticalDensity.ToString();
        }

        public override SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray)
        {
            SystemCoordinates aa;
            List<SystemCoordinates> a;
            if (ic1)
            {
                a = c1.IntersectionWithLine(ToIntersect);
                aa = SystemCoordinates.Zero;
                if (a.Count != 0)
                {
                    aa = a[0];
                    foreach (var v in a)
                        if (v.Distance(ToIntersect.FirstEnd) < aa.Distance(ToIntersect.FirstEnd)) aa = v;
                }
            }
            else
            {
                bool isect, equal;
                aa = l1.IntersectWithLine(ToIntersect, out isect, out equal);
                a = new List<SystemCoordinates>();
                if (!equal && isect) a.Add(aa);
            }
            List<SystemCoordinates> b;
            SystemCoordinates bb;
            if (ic2)
            {
                b = c2.IntersectionWithLine(ToIntersect);
                bb = SystemCoordinates.Zero;
                if (b.Count != 0)
                {
                    bb = b[0];
                    foreach (var v in b)
                        if (v.Distance(ToIntersect.FirstEnd) < bb.Distance(ToIntersect.FirstEnd)) bb = v;
                }
            }
            else
            {
                bool isect, equal;
                bb = l2.IntersectWithLine(ToIntersect, out isect, out equal);
                b = new List<SystemCoordinates>();
                if (!equal && isect) b.Add(bb);
            }
            SystemCoordinates iMenisk1 = SystemCoordinates.Zero;
            SystemCoordinates iMenisk2 = SystemCoordinates.Zero;
            bool isect1, isect2, equal1, equal2;
            if (menisk != 0)
            {
                menisk1.BuildAngle();
                menisk2.BuildAngle();
                iMenisk1 = menisk1.IntersectWithLine(ToIntersect, out isect1, out equal1);
                iMenisk2 = menisk2.IntersectWithLine(ToIntersect, out isect2, out equal2);

                if (equal1) isect1 = false;
                if (equal2) isect2 = false;
            }
            else
            {
                isect1 = false;
                isect2 = false;
            }
            if(b.Count == 0 && a.Count == 0 && !isect1 && !isect2)
            {
                PointFound = false;
                angle_got = false;
                anglee = Angle.Zero;
                return SystemCoordinates.Zero;
            }
            Line prlLine;
            SystemCoordinates point;
            Dictionary<SystemCoordinates, Line> Sootv = new Dictionary<SystemCoordinates, Line>();
            if(a.Count > 0)
            {
                if (c1 != null) Sootv[aa] = c1.BuildKasatelnaya(aa);
                else Sootv[aa] = l1;
            }
            if (b.Count > 0)
            {
                if (c2 != null) Sootv[bb] = c2.BuildKasatelnaya(bb);
                else Sootv[bb] = l2;
            }
            List<SystemCoordinates> points = new List<SystemCoordinates>();
            if (isect1) Sootv[iMenisk1] = menisk1;
            if (isect2) Sootv[iMenisk2] = menisk2;
            SystemCoordinates min;
            if(b.Count > 0)
                points.Add(bb);
            if (a.Count > 0)
                points.Add(aa);
            if(isect1)
                points.Add(iMenisk1);
            if (isect2)
                points.Add(iMenisk2);
            min = points[0];
            prlLine = Sootv[min];
            foreach(var beta in points)
            {
                if(beta.Distance(ToIntersect.FirstEnd) <= min.Distance(ToIntersect.FirstEnd))
                {
                    min = beta;
                    prlLine = Sootv[min];
                }
            }
            point = min;
            if (ObjectCollection.Instance.VneshDensity == OpticalDensity)
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
                if (ray.Current_density == OpticalDensity)
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
            DoubleExtention x,y,a,b;
            if (ic1)
                x = c1.DistanceToPoint(X);
            else
                x = l1.DistanceToPoint(X);
            if (ic2)
                y = c2.DistanceToPoint(X);
            else
                y = l2.DistanceToPoint(X);
            if(menisk!=0)
            {
                a = menisk1.DistanceToPoint(X);
                b = menisk2.DistanceToPoint(X);
            }
            else
            {
                a = x;
                b = y;
            }
            return
                DoubleExtention.MinimumOf(x*CoordinateSystem.Instance.Scale,
                                          y*CoordinateSystem.Instance.Scale,
                                          a*CoordinateSystem.Instance.Scale,
                                          b*CoordinateSystem.Instance.Scale).ToInt32();
            //return (coordinates.Distance(X)*CoordinateSystem.Instance.Scale).ToInt32();
        }

        public override void MoveTo(SystemCoordinates to, SystemCoordinates from)
        {
            if (ic1)
            {
                c1.Center = new SystemCoordinates(c1.Center.X - from.X + to.X, c1.Center.Y - from.Y + to.Y);
            }
            else
            {
                l1.MoveTo(from, to);
            }
            if (ic2)
            {
                c2.Center = new SystemCoordinates(c2.Center.X - from.X + to.X, c2.Center.Y - from.Y + to.Y);
            }
            else
            {
                l2.MoveTo(from, to);
            }
            if(menisk != 0)
            {
                menisk1.MoveTo(from,to);
                menisk2.MoveTo(from,to);
            }
            p2 = new SystemCoordinates(p2.X - from.X + to.X, p2.Y - from.Y + to.Y);
            p5 = new SystemCoordinates(p5.X - from.X + to.X, p5.Y - from.Y + to.Y);
            base.MoveTo(to, from);
        }
        public void Rotate(Angle new_an)
        {
            SystemCoordinates z = new SystemCoordinates(coordinates);
            SystemCoordinates p1, p2, p3, p4, p5, p6;
            DoubleExtention l;
            Angle a;
            Line f;
            //P1
            l = z.Distance(P1);
            f = new Line(z, P1, false);
            f.BuildAngle();
            a = new Angle(new_an.GetInDegrees() + f.AngleForOnePointLines.GetInDegrees() - Anglee.GetInDegrees(), false);
            p1 = new SystemCoordinates(l*a.cos() + z.X, l*a.sin() + z.Y);
            //P2
            l = z.Distance(P2);
            f = new Line(z, P2, false);
            f.BuildAngle();
            a = new Angle(new_an.GetInDegrees() + f.AngleForOnePointLines.GetInDegrees() - Anglee.GetInDegrees(), false);
            p2 = new SystemCoordinates(l * a.cos() + z.X, l * a.sin() + z.Y);
            //P3
            l = z.Distance(P3);
            f = new Line(z, P3, false);
            f.BuildAngle();
            a = new Angle(new_an.GetInDegrees() + f.AngleForOnePointLines.GetInDegrees() - Anglee.GetInDegrees(), false);
            p3 = new SystemCoordinates(l * a.cos() + z.X, l * a.sin() + z.Y);
            //P4
            l = z.Distance(P4);
            f = new Line(z, P4, false);
            f.BuildAngle();
            a = new Angle(new_an.GetInDegrees() + f.AngleForOnePointLines.GetInDegrees() - Anglee.GetInDegrees(), false);
            p4 = new SystemCoordinates(l * a.cos() + z.X, l * a.sin() + z.Y);
            //P5
            l = z.Distance(P5);
            f = new Line(z, P5, false);
            f.BuildAngle();
            a = new Angle(new_an.GetInDegrees() + f.AngleForOnePointLines.GetInDegrees() - Anglee.GetInDegrees(), false);
            p5 = new SystemCoordinates(l * a.cos() + z.X, l * a.sin() + z.Y);
            //P6
            l = z.Distance(P6);
            f = new Line(z, P6, false);
            f.BuildAngle();
            a = new Angle(new_an.GetInDegrees() + f.AngleForOnePointLines.GetInDegrees() - Anglee.GetInDegrees(), false);
            p6 = new SystemCoordinates(l * a.cos() + z.X, l * a.sin() + z.Y);
            RebuildLense(coordinates, p1, p2, p3, p4, p5, p6);
            RaiseChanged();
            ObjectCollection.Instance.RaiseObjectsChange();
        }
        public override void Rotate(SystemCoordinates to, SystemCoordinates from)
        {
            Line aa = new Line(coordinates, to, false);
            aa.BuildAngle();
            Line bb = new Line(coordinates, from, false);
            bb.BuildAngle();
            Angle c = new Angle(aa.AngleForOnePointLines.GetInDegrees() - bb.AngleForOnePointLines.GetInDegrees(), false);
            base.Rotate(to, from);
        }

        private void RebuildLense(SystemCoordinates center, SystemCoordinates p1, SystemCoordinates p2,
                                  SystemCoordinates p3, SystemCoordinates p4, SystemCoordinates p5, SystemCoordinates p6)
        {
            ic1 = false;
            ic2 = false;
            if (SystemCoordinates.InOneLine(p1, p2, p3))
                l1 = new Line(p1, p3, false);
            else
            {
                ic1 = true;
                c1 = new Circle(p1, p2, p3);
            }
            if (SystemCoordinates.InOneLine(p4, p5, p6))
                l2 = new Line(p4, p6, false);
            else
            {
                ic2 = true;
                c2 = new Circle(p4, p5, p6);
            }
            //length = p1.Distance(p3);
            Line l = new Line(p1, p3, false);
            menisk = DoubleExtention.MinimumOf(p3.Distance(p4), p3.Distance(p6));
            if (menisk != 0)
                if (p3.Distance(p4) < p3.Distance(p6))
                {
                    menisk1 = new Line(p3, p4, false);
                    menisk2 = new Line(p1, p6, false);
                }
                else
                {
                    menisk1 = new Line(p3, p6, false);
                    menisk2 = new Line(p1, p4, false);
                }
            coordinates = center;
            this.p2 = p2;
            this.p5 = p5;
        }
        #region IPreloml Members

        private DoubleExtention opticalDensity = 1.2;
        public DoubleExtention OpticalDensity
        {
            get { return opticalDensity; }
            set { opticalDensity = value; }
        }

        #endregion
    }
}
