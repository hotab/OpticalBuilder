using System;
using System.Collections.Generic;
using System.Drawing;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.MathLib
{
    public class Line
    {
        private static Line zero = null;
        public static Line Zero
        {
            get
            {
                if(zero == null) zero = new Line(0,0);
                return zero;
            }
        }
        Angle angle;
        public Angle AngleForOnePointLines
        {
            get { return angle; }
            set
            {
                if(FirstEndSet && SecondEndSet)
                {
                    double hLength = firstEnd.Distance(SecondEnd).X/2;
                    if (value == new Angle(90, false) || value == new Angle(270, false))
                    {
                        vertical = true;
                        angle = value;
                        b = Center.X;
                        k = 200000;
                        SystemCoordinates qw = new SystemCoordinates(Center);
                        if(value == new Angle(90, false))
                        {
                            firstEnd = new SystemCoordinates(qw.X, qw.Y + hLength);
                            secondEnd = new SystemCoordinates(qw.X, qw.Y - hLength);
                        }
                        else
                        {
                            firstEnd = new SystemCoordinates(qw.X, qw.Y - hLength);
                            secondEnd = new SystemCoordinates(qw.X, qw.Y + hLength);
                        }
                    }
                    else
                    {
                        SystemCoordinates qw = new SystemCoordinates(Center);
                        angle = value;
                        k = angle.tg();
                        firstEnd = new SystemCoordinates(qw.X + hLength * angle.cos_d(), qw.Y + hLength*angle.sin_d());
                        secondEnd = qw.BuildPointReflection(FirstEnd);
                        k = (secondEnd.Y - firstEnd.Y) / (secondEnd.X - firstEnd.X);
                        b = -((secondEnd.Y - firstEnd.Y) / (secondEnd.X - firstEnd.X)) * firstEnd.X + firstEnd.Y;
                        //SecondEnd = new SystemCoordinates(qw.X + hLength * angle.cos(), qw.Y + angle.sin());
                    }
                }
                else
                {
                    angle = value;
                    angleSet = true;
                }
            }
        }
        public void SetAngle(Angle a)
        {
            angle = a;
            angleSet = true;
        }
        private bool angleSet = false;
        public bool AngleSet
        {
            get { return angleSet; }
        }
        DoubleExtention k;
        public DoubleExtention K
        {
            get { return k; }
        }
        DoubleExtention b;
        public DoubleExtention B
        {
            get { return b; }
        }
        bool vertical;
        bool firstEndSet;
        bool secondEndSet;
        SystemCoordinates firstEnd;
        SystemCoordinates secondEnd;
        public bool FirstEndSet
        {
            get { return firstEndSet; }
        }
        public bool SecondEndSet
        {
            get { return secondEndSet; }
        }
        public SystemCoordinates FirstEnd
        {
            get { return firstEnd; }
            set
            {
                firstEndSet = true;
                if (vertical == true)
                {
                    if(b == value.X)
                    {
                        firstEnd = value;
                    }
                    else
                    {
                        firstEnd = value;
                        firstEnd.X = b;
                    }
                }
                else
                {
                    if (DoubleComparator.IsEqual(value.Y, k*value.X + b))
                    {
                        firstEnd = value;
                    }
                    else
                    {
                        value.Y = k*value.X + b;
                        firstEnd = value;
                    }
                }
            }
        }
        public SystemCoordinates SecondEnd
        {
            get { return secondEnd; }
            set
            {
                secondEndSet = true;
                if (vertical == true)
                {
                    secondEnd = value;
                    secondEnd.X = b;
                }
                else
                {
                    if (DoubleComparator.IsEqual(value.Y, k*value.X + b))
                    {
                        secondEnd = value;
                    }
                    else
                    {
                        value.Y = k*value.X + b;
                        secondEnd = value;
                    }
                }
            }
        }
        public bool Vertical
        {
            get { return vertical; }
        }
        public bool Horizontal
        {
            get
            {
                return k == 0;
            }
        }
        public SystemCoordinates Center
        {
            get
            {
                if (FirstEndSet == true && SecondEndSet == true)
                    return new SystemCoordinates((firstEnd.X + secondEnd.X) / 2, (firstEnd.Y + secondEnd.Y) / 2);
                else return SystemCoordinates.Zero;
            }
        }
        public Line(DoubleExtention b)
        {
            vertical = true;
            k = 2000000000;
            this.b = b;
        }
        public Line(DoubleExtention k, DoubleExtention b)
        {
            this.k = k;
            this.b = b;
            vertical = false;
        }

        public Line(SystemCoordinates p1, SystemCoordinates p2, bool Infinite = true)
        {
            if (p1.X == p2.X)
            {
                vertical = true;
                k = 2000000000;
                b = p1.X;
            }
            else
            {
                k = (p2.Y - p1.Y) / (p2.X - p1.X);
                b = -((p2.Y - p1.Y) / (p2.X - p1.X)) * p1.X + p1.Y;
                vertical = false;
            }
            if (Infinite == false)
            {
                FirstEnd = p1;
                SecondEnd = p2;
            }

        }
        public Line(SystemCoordinates p, Angle alpha)
        {
            
            if((DoubleExtention) alpha == 90 || (DoubleExtention) alpha == 270)
            {
                vertical = true;
                b = p.X;
                k = 2000000000;
            }
            else
            if((DoubleExtention) alpha == 0 || (DoubleExtention) alpha == 180)
            {
                b = p.Y;
                k = 0;
                vertical = false;
            }
            else
            {
                k = alpha.tg();
                DoubleExtention ty = k*p.X;
                DoubleExtention tb = p.Y - ty;
                b = tb;
                vertical = false;
            }
            FirstEnd = p;
            AngleForOnePointLines = alpha;
        }
        public Line(Line a)
        {
            this.k = a.k;
            this.b = a.b;
            vertical = a.Vertical;
            if (a.FirstEndSet)
                this.FirstEnd = a.FirstEnd;
            if(a.SecondEndSet)
                this.SecondEnd = a.SecondEnd;
            if(a.AngleSet)
                BuildAngle();
        }
        public List<SystemCoordinates> IntersectionWithCircle(Circle circle)
        {
            return circle.IntersectionWithLine(this);
        }
        public DoubleExtention DistanceToPoint(SystemCoordinates x)
        {
            Line X = BuildOrthogonalLine(x);
            bool Intersect, Equal;
            SystemCoordinates alpha = X.IntersectWithLine(this, out Intersect, out Equal);
            if(Intersect == true)
                return alpha.Distance(x);
            else
            {
                if (firstEndSet && secondEndSet) return Math.Min(x.Distance(firstEnd), x.Distance(secondEnd));
                if (firstEndSet) return x.Distance(firstEnd);
                return x.Distance(secondEnd);
            }

        }
        public DoubleExtention DistanceToPoint(Point x)
        {
            return DistanceToPoint(CoordinateSystem.Instance.Converter(x));
        }
        public Line BuildOrthogonalLine(SystemCoordinates a)
        {
            if (vertical == true)
                if (a.X != b) return new Line(a, new SystemCoordinates(b, a.Y));
                else return new Line(new SystemCoordinates(a.X - 1, a.Y), new SystemCoordinates(b, a.Y));
            else
                if (k != 0)
                    return new Line(-1 / K, a.Y - (-1 / K) * a.X);
                else
                    return new Line(a.X);
        }
        public Line BuildOrthogonalLine_save(DoubleExtention X, DoubleExtention Y)
        {
            if (vertical == false)
                return BuildOrthogonalLineX(X);
            else
                return BuildOrthogonalLineY(Y);
        }
        public Line BuildOrthogonalLineX(DoubleExtention X)
        {
            if (vertical == true)
            {
                Exception e = new Exception("Прямая вертикальна. Построение перпендикуляра по координате X невозможно");
                throw e;
            }
            else
            {
                DoubleExtention newB;
                if (k == 0)
                {
                    newB = X;
                    return new Line(newB);
                }
                DoubleExtention Y = k * X + b;
                DoubleExtention newK = -1 / k;
                newB = Y - newK * X;
                return new Line(newK, newB);
            }
        }
        public Line BuildOrthogonalLineY(double Y)
        {
            if (k == 0)
            {
                Exception e = new Exception("Прямая горизонатльна. Построение перпендикуляра по координате Y невозможно");
                throw e;
            }
            else
            {
                DoubleExtention newK;
                DoubleExtention newB;
                if (vertical == true)
                {
                    newK = 0;
                    newB = Y;
                    return new Line(newK, newB);
                }
                newK = -1 / k;
                DoubleExtention X = (Y - b) / k;
                newB = Y - newK * X;
                return new Line(newK, newB);
            }
        }
        public bool PointTopLeft(SystemCoordinates x)
        {
            if(vertical)
            {
                if (x.X < b) return true;
                else return false;
            }
            else
            {
                if (k == 0 && x.Y > b)
                    return true;
                if (k == 0) return false;
                SystemCoordinates sss = BuildSecondCoordinate(x.X);
                if (sss.Y > x.Y)
                    return true;
                else
                    return false;
            }
        }
        public void Drawer()
        {
            if (firstEndSet || secondEndSet)
                Drawing.LineDrawer.RayAutoSelect(this, Color.Black, 4);
        }
        public void Drawer(int width)
        {
            if (firstEndSet || secondEndSet)
                Drawing.LineDrawer.RayAutoSelect(this, Color.Black, width);
        }
        public void Drawer(Color cl)
        {
            if (firstEndSet || secondEndSet)
                Drawing.LineDrawer.RayAutoSelect(this, cl, 4);
        }
        public void Drawer(Color cl, int width)
        {
            if (firstEndSet || secondEndSet)
                Drawing.LineDrawer.RayAutoSelect(this, cl, width);
        }
        public SystemCoordinates IntersectWithLine(Line line, out bool intersect, out bool equal)
        {
            if (line.Vertical == true)
                #region This line is vertical
                if (this.Vertical == true)
                    if (this.B == line.B)
                    {
                        intersect = true;
                        equal = true;
                        return SystemCoordinates.Zero;
                    }
                    else
                    {
                        intersect = false;
                        equal = false;
                        return SystemCoordinates.Zero;
                    }
                #endregion
                #region This line is not vertical
                else
                {
                    intersect = true;
                    equal = false;
                    SystemCoordinates toReturn = new SystemCoordinates(line.B, this.K * line.B + this.B);
                    if (IsBetweenEnds(toReturn) && line.IsBetweenEnds(toReturn))
                        return toReturn;
                    else
                    {
                        intersect = false;
                        return SystemCoordinates.Zero;
                    }
                }
                #endregion
            else
                #region This line is vertical
                if (this.Vertical == true)
                {
                    intersect = true;
                    equal = false;
                    SystemCoordinates toReturn = new SystemCoordinates(this.B, line.K * this.B + line.B);
                    if (IsBetweenEnds(toReturn) && line.IsBetweenEnds(toReturn)) return toReturn;
                    else
                    {
                        intersect = false;
                        return SystemCoordinates.Zero;
                    }
                }
                #endregion
                #region This line is not vertical
                else
                {
                    #region K && B equality check 
                    if (line.K == this.K)
                    {
                        if (line.B == this.B)
                        {
                            intersect = true;
                            equal = true;
                            return SystemCoordinates.Zero;
                        }
                        else
                        {
                            intersect = false;
                            equal = false;
                            return SystemCoordinates.Zero;
                        }
                    }
                    #endregion
                    intersect = true;
                    equal = false;
                    SystemCoordinates toReturn = new SystemCoordinates((line.B - this.B) / (this.K - line.K), this.K * ((line.B - this.B) / (this.K - line.K)) + this.B);
                    if (IsBetweenEnds(toReturn) && line.IsBetweenEnds(toReturn)) return toReturn;
                    else
                    {
                        intersect = false;
                        return SystemCoordinates.Zero;
                    }
                }
                #endregion
        }
        public SystemCoordinates IntersectWithLine(Line line, Angle direction, out bool intersect, out bool equal)
        {
            if (firstEndSet == true)
            {
                if (SecondEndSet == true)
                    if (line.Vertical == true)
                        #region This line is vertical
                        if (this.Vertical == true)
                            if (this.B == line.B)
                            {
                                intersect = true;
                                equal = true;
                                return SystemCoordinates.Zero;
                            }
                            else
                            {
                                intersect = false;
                                equal = false;
                                return SystemCoordinates.Zero;
                            }
                        #endregion
                        #region This line is not vertical
                        else
                        {
                            intersect = true;
                            equal = false;
                            return new SystemCoordinates(line.B, this.K * line.B + this.B);
                        }
                        #endregion
                    else
                        #region This line is vertical
                        if (this.Vertical == true)
                        {
                            intersect = true;
                            equal = false;
                            return new SystemCoordinates(this.B, line.K * this.B + line.B);
                        }
                        #endregion
                        #region This line is not vertical
                        else
                        {
                            #region K && B equality check
                            if (line.K == this.K)
                            {
                                if (line.B == this.B)
                                {
                                    intersect = true;
                                    equal = true;
                                    return SystemCoordinates.Zero;
                                }
                                else
                                {
                                    intersect = false;
                                    equal = false;
                                    return SystemCoordinates.Zero;
                                }
                            }
                            #endregion
                            intersect = true;
                            equal = false;
                            return new SystemCoordinates((line.B - this.B) / (this.K - line.K), this.K * ((line.B - this.B) / (this.K - line.K)) + this.B);
                        }
                        #endregion
                else
                {
                    intersect = true;
                    equal = true;
                    return SystemCoordinates.Zero;
                }
            }
            else
            {
                if (SecondEndSet == true)
                {
                    intersect = true;
                    equal = true;
                    return SystemCoordinates.Zero;
                }
                else
                {
                   return IntersectWithLine(line, out intersect, out equal);
                }
            }
        }
        public bool IsBetweenEnds(SystemCoordinates X)
        {
            if(!FirstEndSet && !SecondEndSet) return true;
            if (firstEndSet && SecondEndSet)
            {
                if (firstEnd.Y == secondEnd.Y && X.X < DoubleExtention.MaximumOf(firstEnd.X, secondEnd.X) && X.X > DoubleExtention.MinimumOf(firstEnd.X, secondEnd.X))
                    return true;
                else if (firstEnd.Y == secondEnd.Y)
                return false;
                if (firstEnd.X == secondEnd.X && X.Y < DoubleExtention.MaximumOf(firstEnd.Y, secondEnd.Y) && X.Y > DoubleExtention.MinimumOf(firstEnd.Y, secondEnd.Y))
                    return true;
                else if (firstEnd.X == secondEnd.X)
                    return false;
                DoubleExtention max_x = DoubleExtention.MaximumOf(firstEnd.X, secondEnd.X);
                DoubleExtention max_y = DoubleExtention.MaximumOf(firstEnd.Y, secondEnd.Y);
                DoubleExtention min_x = DoubleExtention.MinimumOf(firstEnd.X, secondEnd.X);
                DoubleExtention min_y = DoubleExtention.MinimumOf(firstEnd.Y, secondEnd.Y);
                if (X.X < max_x && X.X > min_x && X.Y < max_y && X.Y > min_y)
                    return true;
                else return false;

            }
            if((FirstEndSet || SecondEndSet) && angleSet)
            {
                SystemCoordinates al;
                al = FirstEndSet ? firstEnd : secondEnd;
                if (angle.GetInDegrees() == 90)
                    if (X.Y > al.Y && X.X == al.X) return true;
                    else return false;
                if(angle.GetInDegrees() == 270)
                    if (X.Y < al.Y && X.X == al.X) return true;
                    else return false;
                if (angle.GetInDegrees() == 180)
                    if (X.Y == al.Y && X.X < al.X) return true; else return false;
                if (angle.GetInDegrees() == 360 || angle.GetInDegrees() == 0)
                    if (X.Y == al.Y && X.X > al.X) return true; else return false;
                if (angle.GetInDegrees() > 0 && angle.GetInDegrees() < 90)
                    if (X.X > al.X && X.Y > al.Y) return true;
                    else return false;
                if (angle.GetInDegrees() > 90 && angle.GetInDegrees() < 180)
                    if (X.X < al.X && X.Y > al.Y) return true;
                    else return false;
                if (angle.GetInDegrees() > 180 && angle.GetInDegrees() < 270)
                    if (X.X < al.X && X.Y < al.Y) return true;
                    else return false;
                if (angle.GetInDegrees() > 270 && angle.GetInDegrees() < 360)
                    if (X.X > al.X && X.Y < al.Y) return true;
                    else return false;
                return false;

            } 
            if ((FirstEndSet || SecondEndSet) && angleSet == false) return true;
            return true;
        }
        public void BuildAngle()
        {
            if (FirstEndSet && SecondEndSet)
            {
                DoubleExtention cos, sin, R;
                R = firstEnd.Distance(secondEnd);
                cos = (secondEnd.X - firstEnd.X)/R;
                sin = (secondEnd.Y - firstEnd.Y)/R;
                AngleForOnePointLines = new Angle(sin, cos);
                return;
            }
            if(vertical)
            {
                AngleForOnePointLines = new Angle(90, false);
            }
            else
            {
                AngleForOnePointLines = new Angle((Math.Atan(k)/Math.PI)*180, false);
            }
        }
        public SystemCoordinates BuildSecondCoordinate(DoubleExtention a, bool isX = true)
        {
            if (isX == true)
            {
                if(vertical == true)
                    return new SystemCoordinates(a, 1);
                return new SystemCoordinates(a, k*a + b);
            }
            else
            {
                if(vertical == true)
                    return new SystemCoordinates(b,a);
                if(k == 0)
                    return new SystemCoordinates(1,a);
                return new SystemCoordinates((a-b)/k,a);
            }

        }
        public Angle Prelom(DoubleExtention From, DoubleExtention To, Line ToPrelom, out bool the_same)
        {
            the_same = false;
            if(!this.AngleSet) BuildAngle();
            if (!ToPrelom.AngleSet) ToPrelom.BuildAngle();
            Line ToPrelom2 = new Line(ToPrelom.FirstEnd, ToPrelom.AngleForOnePointLines - this.AngleForOnePointLines);
            if(!ToPrelom2.AngleSet) ToPrelom2.BuildAngle();
            if (ToPrelom2.AngleForOnePointLines == 90 || ToPrelom2.AngleForOnePointLines == 270)
            {
                return ToPrelom.AngleForOnePointLines;
            }
            Angle angleToPerp;
            DoubleExtention PocazPreloml = To / From;
            if (ToPrelom2.AngleForOnePointLines > 0 && ToPrelom2.AngleForOnePointLines < 90) angleToPerp = new Angle(90 - ToPrelom2.AngleForOnePointLines.GetInDegrees(), false);
            else
                if (ToPrelom2.AngleForOnePointLines > 90 && ToPrelom2.AngleForOnePointLines < 180) angleToPerp = new Angle(-90 + ToPrelom2.AngleForOnePointLines.GetInDegrees(), false);
                else
                    if (ToPrelom2.AngleForOnePointLines > 180 && ToPrelom2.AngleForOnePointLines < 270) angleToPerp = new Angle(270 - ToPrelom2.AngleForOnePointLines.GetInDegrees(), false);
                    else angleToPerp = new Angle(-270 + ToPrelom2.AngleForOnePointLines.GetInDegrees(), false);
            if (From > To)
            {
                DoubleExtention ASin = (Math.Asin(PocazPreloml) / Math.PI) * 180;
                if (ASin <= angleToPerp.GetInDegrees())
                {
                    DoubleExtention bb = 360 - ToPrelom2.AngleForOnePointLines.GetInDegrees();
                    bb += this.AngleForOnePointLines.GetInDegrees();
                    the_same = true;
                    return new Angle(bb, false);
                }
            }
            DoubleExtention b = Math.Asin(angleToPerp.sin() / PocazPreloml) * 180f / Math.PI;
            if (ToPrelom2.AngleForOnePointLines > 0 && ToPrelom2.AngleForOnePointLines < 90) b = 90 - b;
            else
                if (ToPrelom2.AngleForOnePointLines > 90 && ToPrelom2.AngleForOnePointLines < 180) b += 90;
                else
                    if (ToPrelom2.AngleForOnePointLines > 180 && ToPrelom2.AngleForOnePointLines < 270) b = 270 - b;
                    else b = 270 + b;
            b += this.AngleForOnePointLines.GetInDegrees();
            return new Angle(b, false);
        }
        public void MoveTo(SystemCoordinates from, SystemCoordinates to)
        {
            if(FirstEndSet && SecondEndSet)
            {
                firstEnd = new SystemCoordinates(FirstEnd.X - from.X + to.X, FirstEnd.Y - from.Y + to.Y);
                secondEnd = new SystemCoordinates(SecondEnd.X - from.X + to.X, SecondEnd.Y - from.Y + to.Y);
                if (FirstEnd.X == SecondEnd.X)
                {
                    vertical = true;
                    k = 2000000000;
                    b = FirstEnd.X;
                }
                else
                {
                    k = (SecondEnd.Y - FirstEnd.Y) / (SecondEnd.X - FirstEnd.X);
                    b = -((SecondEnd.Y - FirstEnd.Y) / (SecondEnd.X - FirstEnd.X)) * FirstEnd.X + FirstEnd.Y;
                    vertical = false;
                }
            }
        }
    }
}
