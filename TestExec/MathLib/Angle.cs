using System;
using System.Collections.Generic;
using System.Text;
using OpticalBuilderLib.Exceptions;
using OpticalBuilderLib.TypeExtentions;
namespace OpticalBuilderLib.MathLib
{
    public class Angle
    {
        private static bool in_radians = false;
        DoubleExtention angle;
        private static Angle zero = new Angle(0,false);
        public static Angle Zero { get { return zero; } }
        public DoubleExtention Anglee
        {
            get 
            {
                if (in_radians != true)
                    return angle;
                else
                    return (angle / 180) * Math.PI;
            }
        }
        public bool IsVertical()
        {
            if ((angle == 90) || (angle == 270))
                return true;
            else return false;
        }
        public bool IsUp()
        {
            if (IsVertical() == false)
                if (angle > 0 && angle < 180) return true;
                else if (angle > 180) return false;
                else throw new Exception("Line is horizontal");
            else
                if (angle == 90) return true;
                else return false;
        }
        public Angle(DoubleExtention Angle, bool Radians)
        {
            if (Radians)
            {
                angle = (Angle / (Math.PI * 2)) * 360;
                if (angle < (DoubleExtention)0)
                {
                    angle = -(DoubleExtention)(angle);
                    int torem = (int)((DoubleExtention)angle / 360);
                    if ((torem * 360) == angle)
                        angle = 0;
                    else if (((torem + 1) * 360) == angle)
                        angle = 0;
                    else
                    {
                        angle -= (DoubleExtention)(torem * 360);
                        angle = 360 - (DoubleExtention)angle;
                    }
                }
                else
                    if ((DoubleExtention)angle >= 360)
                    {
                        int torem = (int)((DoubleExtention)angle / 360);
                        if ((torem * 360) == angle)
                            angle = 0;
                        else if (((torem + 1) * 360) == angle)
                            angle = 0;
                        else
                            angle -= (DoubleExtention)(torem * 360);
                    }
            }
            else
            {
                angle = Angle;
                if (angle < (DoubleExtention)0)
                {
                    angle = -(DoubleExtention)(angle);
                    int torem = (int)((DoubleExtention)angle / 360);
                    if ((torem * 360) == angle)
                        angle = 0;
                    else if (((torem + 1) * 360) == angle)
                        angle = 0;
                    else
                    {
                        angle -= (DoubleExtention)(torem * 360);
                        angle = 360 - (DoubleExtention)angle;
                    }
                }
                else
                    if ((DoubleExtention)angle >= 360)
                    {
                        int torem = (int)((DoubleExtention)angle / 360);
                        if ((torem * 360) == angle)
                            angle = 0;
                        else if (((torem + 1) * 360) == angle)
                            angle = 0;
                        else
                            angle -= (DoubleExtention)(torem * 360);
                    }
            }
        }
        public Angle(Angle a)
        {
            angle = a.Anglee;
        }
        public Angle(DoubleExtention sin,DoubleExtention cos)
        {
            angle = -1;
            if (cos == 0)
            {
                if (sin == 1) angle = new Angle(90, false);
                if (sin == -1) angle = new Angle(270, false);
            }
            else
            if (sin == 0)
            {
                if (cos == 1) angle = new Angle(0, false);
                if (cos == -1) angle = new Angle(180, false);
            }
            else
            {
                DoubleExtention lower_limit = 0, upper_limit = 0;
                if (cos > 0) 
                { 
                    lower_limit = -90;
                }
                if (cos < 0) 
                { 
                    lower_limit = 90;
                }
                if(sin > 0)
                {
                    if (lower_limit == -90) lower_limit = 0;
                }
                if(sin < 0)
                {
                    if (lower_limit == -90)
                        lower_limit = 270;
                    if (lower_limit == 90) lower_limit = 180;
                }
                if((lower_limit == 0) || (lower_limit == 90))
                {
                    angle = Math.Acos(cos);
                    angle = (angle/(2*Math.PI))*360;
                }
                if(lower_limit == 180 || lower_limit == 270)
                {
                    angle = Math.Acos(cos);
                    angle = (angle / (2 * Math.PI)) * 360;
                    angle = -angle;
                    angle += 360;
                }
                if(angle == -1) throw new InnerException();
            }
        }
        private double GetInRadians_d()
        {
            return ((angle.X / 360) * Math.PI * 2);
        }
        public DoubleExtention GetInRadians()
        {
            return ((angle / 360) * Math.PI * 2);
        }
        public DoubleExtention GetInDegrees()
        {
            return angle;
        }
        public void SetInRadians(DoubleExtention NewAngle)
        {
            
            angle = (NewAngle / (Math.PI * 2)) * 360;
            if (angle < (DoubleExtention)0)
            {
                angle = -(DoubleExtention)(angle);
                int torem = (int)((DoubleExtention)angle / 360);
                if ((torem * 360) == angle)
                    angle = 0;
                else if (((torem + 1) * 360) == angle)
                    angle = 0;
                else
                {
                    angle -= (DoubleExtention)(torem * 360);
                    angle = 360 - (DoubleExtention)angle;
                }
            }
            else
                if ((DoubleExtention)angle >= 360)
                {
                    int torem = (int)((DoubleExtention)angle / 360);
                    if ((torem * 360) == angle)
                        angle = 0;
                    else if (((torem + 1) * 360) == angle)
                        angle = 0;
                    else
                        angle -= (DoubleExtention)(torem * 360);
                }
        }
        public void SetInDegrees(DoubleExtention NewAngle)
        {
            angle = NewAngle;
            if (angle < (DoubleExtention)0)
            {
                angle = -(DoubleExtention)(angle);
                int torem = (int)((DoubleExtention)angle / 360);
                if ((torem * 360) == angle)
                    angle = 0;
                else if (((torem + 1) * 360) == angle)
                    angle = 0;
                else
                {
                    angle -= (DoubleExtention)(torem * 360);
                    angle = 360 - (DoubleExtention)angle;
                }
            }
            else
                if ((DoubleExtention)angle >= 360)
                {
                    int torem = (int)((DoubleExtention)angle / 360);
                    if ((torem * 360) == angle)
                        angle = 0;
                    else if (((torem + 1) * 360) == angle)
                        angle = 0;
                    else
                        angle -= (DoubleExtention)(torem * 360);
                }
        }
        public double cos_d()
        {
            return Math.Cos(GetInRadians_d());
        }
        public double sin_d()
        {
            return Math.Sin(GetInRadians_d());
        }
        public double tg_d()
        {
            return Math.Tan(GetInRadians_d());
        }
        public double ctg_d()
        {
            if (Math.Tan(GetInRadians()) == 0)
                throw new DivideByZeroException("Тангенс угла равен нулю. Невозможно получить катангенс");
            return (1 / Math.Tan(GetInRadians_d()));
        }
        public DoubleExtention cos()
        {
            return Math.Cos(GetInRadians());
        }
        public DoubleExtention sin()
        {
            return Math.Sin(GetInRadians());
        }
        public DoubleExtention tg()
        {
            return Math.Tan(GetInRadians());
        }
        public DoubleExtention ctg()
        {
            if (Math.Tan(GetInRadians()) == 0)
                throw new DivideByZeroException("Тангенс угла равен нулю. Невозможно получить катангенс");
            return (1/Math.Tan(GetInRadians()));
        }
        public static bool operator ==(Angle a1, Angle a2)
        {
            return a1.Anglee == a2.Anglee;
        }
        public static bool operator !=(Angle a1, Angle a2)
        {
            return a1.Anglee != a2.Anglee;
        }

        public static bool operator >(Angle a1, Angle a2)
        {
            return a1.angle > a2.angle;
        }
        public static bool operator <(Angle a1, Angle a2)
        {
            return a1.angle < a2.angle;
        }
        public static bool operator >=(Angle a1, Angle a2)
        {
            return a1.angle >= a2.angle;
        }
        public static bool operator <=(Angle a1, Angle a2)
        {
            return a1.angle <= a2.angle;
        }
        public override bool  Equals(object obj)
        {
            if (base.Equals(obj)) return true;
            if (obj is Angle)
                return ((Angle)obj).Anglee == this.Anglee;
            else
                return false;
        }
        public static Angle operator + (Angle a1, Angle a2)
        {
            double angle = a1.GetInDegrees() + a2.GetInDegrees();
            if (angle > 360) angle -= 360;
            return new Angle(angle, false);
        }
        public static Angle operator - (Angle a1, Angle a2)
        {
            double angle = a1.GetInDegrees() - a2.GetInDegrees();
            if (angle < 0) angle += 360;
            return new Angle(angle, false);
        }
        public static implicit operator Angle(double from)
        {
            if(in_radians)
                from = (from / Math.PI * 2) * 360;
            while (DoubleComparator.IsLower(from, 0))
                from += 360;
            while (DoubleComparator.IsNotLower(from, 360))
                from -= 360;
            return new Angle(from, false);
        }
        public static implicit operator Angle(int frm)
        {
            double from = frm;
            if (in_radians)
                from = (from / Math.PI * 2) * 360;
            while (DoubleComparator.IsLower(from, 0))
                from += 360;
            while (DoubleComparator.IsNotLower(from, 360))
                from -= 360;
            return new Angle(from, false);
        }
        public static implicit operator Angle(DoubleExtention from)
        {
            if (in_radians)
                from = (from / Math.PI * 2) * 360;
            while (from<0)
                from += 360;
            while (from>=360)
                from -= 360;
            return new Angle(from, false);
        }
        public static explicit operator double(Angle angle)
        {
            if (Angle.in_radians) return angle.GetInRadians();
            else return angle.GetInDegrees();
        }
        public static implicit operator DoubleExtention(Angle angle)
        {
            if (Angle.in_radians) return angle.GetInRadians();
            else return angle.GetInDegrees();
        }
        public static explicit operator int(Angle angle)
        {
            if (Angle.in_radians) return angle.GetInRadians().ToInt32();
            else return angle.GetInDegrees().ToInt32();
        }
        public static implicit operator string(Angle angle)
        {
            return angle.angle.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return this;
        }
    }
}
