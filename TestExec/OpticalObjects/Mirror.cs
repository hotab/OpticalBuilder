using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;
using OpticalBuilderLib.Drawing;
namespace OpticalBuilderLib.OpticalObjects
{
    public class Mirror : ObjectProto
    {
        protected Angle angle;
        public Angle Anglee
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                ToDraw.AngleForOnePointLines = angle;
                RaiseChanged();
                ObjectCollection.Instance.RaiseObjectsChange();
            }
        }

        private DoubleExtention length;
        public DoubleExtention Length
        {
            get
            {
                return ToDraw.FirstEnd.Distance(ToDraw.SecondEnd);
            }
            set
            {
                SystemCoordinates cen = ToDraw.Center.Clone();
                double l = value/2;
                if(ToDraw.AngleForOnePointLines.GetInDegrees() == 90 || ToDraw.AngleForOnePointLines.GetInDegrees() == 270)
                {
                    ToDraw.SecondEnd = new SystemCoordinates(ToDraw.Center.X, ToDraw.Center.Y - ToDraw.AngleForOnePointLines.sin_d()*l);
                    ToDraw.FirstEnd = cen.BuildPointReflection(ToDraw.SecondEnd);
                    //ToDraw.FirstEnd = new SystemCoordinates(ToDraw.FirstEnd.X, ToDraw.FirstEnd.Y + ToDraw.AngleForOnePointLines.sin() * l);
                }
                else
                {
                    ToDraw.FirstEnd = new SystemCoordinates(cen.X + l * ToDraw.AngleForOnePointLines.cos_d(), cen.Y + l * ToDraw.AngleForOnePointLines.sin_d());
                    ToDraw.SecondEnd = cen.BuildPointReflection(ToDraw.FirstEnd);
                }
                ObjectCollection.Instance.RaiseObjectsChange();
            }
        }
        private Line ToDraw;
        public Mirror(string Name, SystemCoordinates Center, Angle Anglee, DoubleExtention Length)
        {
            angle = Anglee;
            name = Name;
            coordinates = Center;
            length = Length;
            
            if (((DoubleExtention)angle != (DoubleExtention)90) && ((DoubleExtention)angle != (DoubleExtention)270))
            {
                DoubleExtention y = Center.X * angle.tg();
                DoubleExtention b = Center.Y - y;
                ToDraw = new Line(angle.tg(), b);ToDraw.AngleForOnePointLines = angle;
                ToDraw.FirstEnd = new SystemCoordinates((length / 2) * angle.cos() + Center.X, (length / 2) * angle.sin() + Center.Y);
                ToDraw.SecondEnd = Center.BuildPointReflection(ToDraw.FirstEnd);
            }
            else
            {
                ToDraw = new Line(Center.X);
                if((DoubleExtention)angle == (DoubleExtention)90)
                {
                    ToDraw.FirstEnd = new SystemCoordinates(Center.X, Center.Y + length / 2); ToDraw.AngleForOnePointLines = angle;
                    ToDraw.SecondEnd = Center.BuildPointReflection(ToDraw.FirstEnd);
                }
                if ((DoubleExtention)angle == (DoubleExtention)270)
                {
                    ToDraw.FirstEnd = new SystemCoordinates(Center.X, Center.Y - length / 2); ToDraw.AngleForOnePointLines = angle;
                    ToDraw.SecondEnd = Center.BuildPointReflection(ToDraw.FirstEnd);
                }
            }
        }
        public Mirror(string SaveStr)
        {
            string[] splitted = SaveStr.Split('^');
            if (splitted.Length != 7) throw new OpticalBuilderLib.Exceptions.ErrorWhileReading();
            try
            {
                string Name = splitted[1];
                SystemCoordinates Center = new SystemCoordinates(Convert.ToDouble(splitted[2]),Convert.ToDouble(splitted[3]));
                Angle Anglee = Convert.ToDouble(splitted[4]);
                DoubleExtention Length = Convert.ToDouble(splitted[5]);
                id = Convert.ToInt32(splitted[6]);
                angle = Anglee;
                name = Name;
                coordinates = Center;
                length = Length;
                if (((DoubleExtention)angle != (DoubleExtention)90) && ((DoubleExtention)angle != (DoubleExtention)270))
                {
                    DoubleExtention y = Center.X * angle.tg();
                    DoubleExtention b = Center.Y - y;
                    ToDraw = new Line(angle.tg(), b);
                }
                else
                    ToDraw = new Line(Center.X);
                if (((DoubleExtention)angle != (DoubleExtention)90) && ((DoubleExtention)angle != (DoubleExtention)270))
                {
                    DoubleExtention y = Center.X * angle.tg();
                    DoubleExtention b = Center.Y - y;
                    ToDraw = new Line(angle.tg(), b);
                    ToDraw.FirstEnd = new SystemCoordinates((length / 2) * angle.cos() + Center.X, (length / 2) * angle.sin() + Center.Y);
                    ToDraw.SecondEnd = Center.BuildPointReflection(ToDraw.FirstEnd);
                }
                else
                {
                    ToDraw = new Line(Center.X);
                    if ((DoubleExtention)angle == (DoubleExtention)90)
                    {
                        ToDraw.FirstEnd = new SystemCoordinates(Center.X, Center.Y + length / 2);
                        ToDraw.SecondEnd = Center.BuildPointReflection(ToDraw.FirstEnd);
                    }
                    if ((DoubleExtention)angle == (DoubleExtention)270)
                    {
                        ToDraw.FirstEnd = new SystemCoordinates(Center.X, Center.Y - length / 2);
                        ToDraw.SecondEnd = Center.BuildPointReflection(ToDraw.FirstEnd);
                    }
                }

                ToDraw.AngleForOnePointLines = angle;
            }
            catch 
            {
                throw new OpticalBuilderLib.Exceptions.ErrorWhileReading();
            }
        }
        public override void Drawer()
        {
            MirrorDrawer.Draw(angle,ToDraw.SecondEnd, ToDraw.FirstEnd, Selected);
        }
        public override string GenerateSaveString()
        {
            string SaveStr = GetTypeSpecifier() + "^"+Name+"^"+coordinates.X.ToString()+"^"+coordinates.Y.ToString()+"^"+angle.ToString()+"^"+length.ToString()+"^"+id.ToString();
            return SaveStr;
        }
        public override SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray)
        {
            bool intersect;
            bool equal;
            PointFound = false;
            SystemCoordinates A = ToIntersect.IntersectWithLine(ToDraw, out intersect, out equal);
            if (intersect) PointFound = true;
            if (equal) PointFound = false;
            if(PointFound)
            {
                bool usloviye;
                anglee = BuildAngle(out usloviye, ToIntersect.AngleForOnePointLines);
                angle_got = usloviye;
                
            }
            else
            {
                angle_got = false;
                anglee = Angle.Zero;
            }
            if (!PointFound) return SystemCoordinates.Zero;
            else return A;
        }
        public override string GetTypeSpecifier()
        {
            return GetSpec(2);
        }
        public DoubleExtention DistanceToPoint(Point X)
        {
            return ToDraw.DistanceToPoint(X);
        }
        public DoubleExtention DistanceToPoint(SystemCoordinates X)
        {
            return ToDraw.DistanceToPoint(X);
        }
        public override int DistanceToPointS(Point X)
        {
            return (int)Math.Ceiling(ToDraw.DistanceToPoint(X)*CoordinateSystem.Instance.Scale);
        }
        public int DistanceToPointS(SystemCoordinates X)
        {
            return (int)Math.Ceiling(ToDraw.DistanceToPoint(X) * CoordinateSystem.Instance.Scale);
        }
        Angle BuildAngle(out bool is_built, Angle a)
        {
            Angle b = new Angle(a.GetInDegrees()-angle.GetInDegrees(), false);
            if (b.GetInDegrees() <= 180) 
            { 
                is_built = false;
                return Angle.Zero;
            }
            else
            {
                b = new Angle(360-b.GetInDegrees() + angle.GetInDegrees(), false);
                is_built = true;
                return b;
            }
        }
        public override void MoveTo(SystemCoordinates to, SystemCoordinates from)
        {
            base.MoveTo(to, from);
            ToDraw.MoveTo(from,to);
            coordinates.X = ToDraw.Center.X;
            coordinates.Y = ToDraw.Center.Y;
        }
    }
}
