using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.OpticalObjects;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.MathLib
{
    public class Circle
    {
        SystemCoordinates center;
        DoubleExtention radius;
        private DoubleExtention lowerLimitAngle = -1;
        private DoubleExtention upperLimitAngle = -1;
        private bool lLimitSet = false;
        private bool uLimitSet = false;
        public Angle LowerLimitAngle
        {
            get { return new Angle( lowerLimitAngle, true); }
            set { lowerLimitAngle = value.GetInRadians();
                lLimitSet = true;
            }
        }
        public Angle UpperLimitAngle
        {
            get { return new Angle(upperLimitAngle, true); }
            set
            {
                upperLimitAngle = value.GetInRadians();
                uLimitSet = true;
            }
        }
        public bool LLimitSet
        {
            get { return lLimitSet; }
        }
        public bool ULimitSet
        {
            get { return uLimitSet; }
        }
        public SystemCoordinates LLimitPoint
        {
            get
            {
                return new SystemCoordinates(center.X + radius*LowerLimitAngle.cos(), center.Y + radius*LowerLimitAngle.sin());
            }
        }
        public SystemCoordinates ULimitPoint
        {
            get
            {
                return new SystemCoordinates(center.X + radius * UpperLimitAngle.cos(), center.Y + radius * UpperLimitAngle.sin());
            }
        }
        public SystemCoordinates Center
        {
            get { return new SystemCoordinates(center); }
            set
            {
                center = value;
            }
        }
        public DoubleExtention Radius
        {
            get { return radius; }
        }

        public Circle(DoubleExtention CenterX, DoubleExtention CenterY, DoubleExtention Radius)
        {
            center.X = CenterX;
            center.Y = CenterY;
            radius = Radius;
        }
        public Circle(SystemCoordinates Center, DoubleExtention Radius)
        {
            center = Center;
            radius = Radius;
        }
        public Circle(SystemCoordinates p1, SystemCoordinates p2, SystemCoordinates p3)
        {
            DoubleExtention x1 = p1.X;
            DoubleExtention x2 = p2.X;
            DoubleExtention x3 = p3.X;
            DoubleExtention y1 = p1.Y;
            DoubleExtention y2 = p2.Y;
            DoubleExtention y3 = p3.Y;
            DoubleExtention alpha = (x1 * x1 + y1 * y1 - x3 * x3 - y3 * y3) / 2;
            DoubleExtention betaa = (x2 * x2 + y2 * y2 - x3 * x3 - y3 * y3) / 2;
            DoubleExtention b;
            DoubleExtention a;
            if (x1 == x3)
            {
                b = alpha / (y1 - y3);
                a = (betaa - b * (y2 - y3)) / (x2 - x3);
            }
            else
                if (x2 == x3)
                {
                    b = betaa / (y2 - y3);
                    a = (alpha - b * (y1 - y3)) / (x1 - x3);
                }
                else
                {
                    DoubleExtention gamma = alpha / (x1 - x3) - betaa / (x2 - x3);
                    DoubleExtention lambda = (y1 - y3) / (x1 - x3) - (y2 - y3) / (x2 - x3);
                    b = gamma / lambda;
                    a = (alpha - b * (y1 - y3)) / (x1 - x3);
                }
            center = new SystemCoordinates(a, b);
            radius = center.Distance(p1);
            Line Z = new Line(center, p1,false);
            Z.BuildAngle();
            UpperLimitAngle =Z.AngleForOnePointLines;
            Z = new Line(center, p3, false);
            Z.BuildAngle();
            LowerLimitAngle = Z.AngleForOnePointLines;
            while (lowerLimitAngle > upperLimitAngle) lowerLimitAngle -= (Math.PI*2);
            if(!PointIsBetweenEdges(p2))
            {
                DoubleExtention t = lowerLimitAngle;
                lowerLimitAngle = upperLimitAngle;
                upperLimitAngle = t;
                while (lowerLimitAngle > upperLimitAngle) lowerLimitAngle -= (Math.PI * 2);
                while (upperLimitAngle < 0) { lowerLimitAngle += (Math.PI * 2);
                    upperLimitAngle += (Math.PI*2);
                }
            }
        }
        public List<SystemCoordinates> IntersectionWithLine(Line line)
        {
            List<SystemCoordinates> ToReturn = new List<SystemCoordinates>();
            if (line.Vertical == true)
            {
                //Якщо пряма вертикальна, то line.B == X;
                if ((line.B < (center.X - radius)) || (line.B > (center.X + radius)))
                {
                    //Якщо вертикальня лінія не торкається кола - нічого не робимо
                }
                else
                    if ((line.B == (center.X - radius)) || (line.B == (center.X + radius)))
                    {
                        //Якщо вертикальня лінія дотикаєтся до кола - додаємо координати дотику
                        SystemCoordinates Coords = new SystemCoordinates(line.B, center.Y);
                        ToReturn.Add(Coords);
                    }
                    else
                        if (line.B == center.X)
                        {
                            //Якщо вертикальня лінія перетинає коло по діаметру - додаємо координати перетину 
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y + radius));
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y - radius));
                        }
                        else
                        {
                            //Якщо вертикальня лінія перетинає коло не по діаметру - додаємо координати перетину 
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y + Math.Sqrt(radius * radius - (line.B - center.X) * (line.B - center.X))));
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y - Math.Sqrt(radius * radius - (line.B - center.X) * (line.B - center.X))));
                        }
            }
            else
            {
                //Якщо пряма не вертикальна, то використовуємо формули, виведені з таких двох рівнянь:
                //y = k*x + b
                //(x-a)^2 + (y-b)^2 = R^2
                DoubleExtention C = -(radius*radius - center.X*center.X - center.Y*center.Y - line.B*line.B +
                                    2*line.B*center.Y);
                DoubleExtention B = 2*(line.K*line.B - center.X - line.K*center.Y);
                DoubleExtention A = 1 + line.K*line.K;
                DoubleExtention D = B*B - 4*A*C;
                if(D == 0) //Якщо пряма дотикається до кола
                {
                    DoubleExtention X = -B/(2*A);
                    DoubleExtention Y = line.K*X + line.B;
                    ToReturn.Add(new SystemCoordinates(X, Y));
                }
                else if (D > 0) //Якщо пряма пернетинає коло
                {
                    DoubleExtention X1 = (-B + D.sqrt())/(2*A);
                    DoubleExtention X2 = (-B - D.sqrt())/(2*A);
                    ToReturn.Add(new SystemCoordinates(X1, line.K * X1 + line.B));
                    ToReturn.Add(new SystemCoordinates(X2, line.K * X2 + line.B));
                }
            }
            List<SystemCoordinates> Toreturn = new List<SystemCoordinates>();
            foreach (var e in ToReturn)
            {
                if(line.IsBetweenEnds(e))
                    Toreturn.Add(e);
            } //Перевірка на належність отриманих координат прямій (бо пряма може бути променєм або відрізком)
            if(ULimitSet && LLimitSet)
            {
                List<SystemCoordinates> a = new List<SystemCoordinates>();
                foreach(var z in Toreturn)
                    if(PointIsBetweenEdges(z)) a.Add(z);
                Toreturn = a;
            } //Перевірка на належність отриманих координат колу (бо коло може бути лише частиною кола, а не повним колом)
            return Toreturn;
        }
        public List<SystemCoordinates> IntersectionWithInfLine(Line line)
        {
            List<SystemCoordinates> ToReturn = new List<SystemCoordinates>();
            if (line.Vertical == true)
            {
                if ((line.B < (center.X - radius)) || (line.B > (center.X + radius)))
                {
                    //return ToReturn;
                }
                else
                    if ((line.B == (center.X - radius)) || (line.B == (center.X + radius)))
                    {
                        //List<SystemCoordinates> ToReturn = new List<SystemCoordinates>();
                        SystemCoordinates Coords = new SystemCoordinates(line.B, center.Y);
                        ToReturn.Add(Coords);
                        //return ToReturn;
                    }
                    else
                        if (line.B == center.X)
                        {
                            //List<SystemCoordinates> ToReturn = new List<SystemCoordinates>();
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y + radius));
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y - radius));
                            //return ToReturn;
                        }
                        else
                        {
                            //List<SystemCoordinates> ToReturn = new List<SystemCoordinates>();
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y + Math.Sqrt(radius * radius - (line.B - center.X) * (line.B - center.X))));
                            ToReturn.Add(new SystemCoordinates(line.B, center.Y - Math.Sqrt(radius * radius - (line.B - center.X) * (line.B - center.X))));
                            //return ToReturn;
                        }
            }
            else
            {
                /*DoubleExtention a = line.K * line.K + 1;
                DoubleExtention b = center.X - line.K * line.B + line.K * center.Y;
                DoubleExtention c = center.X * center.X + (center.Y - line.B) * (center.Y - line.B) - radius * radius;
                DoubleExtention x1 = (-b + Math.Sqrt(b * b - a * c)) / a;
                DoubleExtention x2 = (-b - Math.Sqrt(b * b - a * c)) / a;
                DoubleExtention y1 = line.K * x1 + line.B;
                DoubleExtention y2 = line.K * x2 + line.B;*/
                DoubleExtention C = -(radius * radius - center.X * center.X - center.Y * center.Y - line.B * line.B +
                                    2 * line.B * center.Y);
                DoubleExtention B = 2 * (line.K * line.B - center.X - line.K * center.Y);
                DoubleExtention A = 1 + line.K * line.K;
                DoubleExtention D = B * B - 4 * A * C;
                if (D == 0)
                {
                    DoubleExtention X = -B / (2 * A);
                    DoubleExtention Y = line.K * X + line.B;
                    ToReturn.Add(new SystemCoordinates(X, Y));
                }
                else if (D > 0)
                {
                    DoubleExtention X1 = (-B + D.sqrt()) / (2 * A);
                    DoubleExtention X2 = (-B - D.sqrt()) / (2 * A);
                    ToReturn.Add(new SystemCoordinates(X1, line.K * X1 + line.B));
                    ToReturn.Add(new SystemCoordinates(X2, line.K * X2 + line.B));
                }
                //List<SystemCoordinates> ToReturn = new List<SystemCoordinates>();
                /*ToReturn.Add(new SystemCoordinates(x1, y1));
                ToReturn.Add(new SystemCoordinates(x2, y2));*/
                //return ToReturn;
            }
            if (ULimitSet && LLimitSet)
            {
                List<SystemCoordinates> a = new List<SystemCoordinates>();
                foreach (var z in ToReturn)
                    if (PointIsBetweenEdges(z)) a.Add(z);
                ToReturn = a;
            }
            return ToReturn;
        }

        public DoubleExtention DistanceToPoint(SystemCoordinates alpha)
        {
            List<DoubleExtention> dist = new List<DoubleExtention>();
            if(ULimitSet && LLimitSet)
            {
                dist.Add(alpha.Distance(ULimitPoint));
                dist.Add(alpha.Distance(LLimitPoint));
            }
            Line a = new Line(alpha, center);
            a.FirstEnd = center;
            List<SystemCoordinates> p = IntersectionWithInfLine(a);
            //if(IntersectionWithLine(a).Count!=0)
            foreach(var x in p)
            {
                dist.Add(x.Distance(alpha));
            }
            //dist.Add(this.IntersectionWithLine(a)[0].Distance(alpha));
            DoubleExtention min = dist[0];
            foreach (var z in dist)
            {
                if (z < min) min = z;
            }
            return min;
        }
        public void Draw(bool selected = false)
        {
            if(!(ULimitSet && LLimitSet))
            Drawing.CircleDrawer.Draw(selected, center, radius*CoordinateSystem.Instance.Scale);
            else
            {
                DoubleExtention l1 = lowerLimitAngle;
                DoubleExtention l2 = upperLimitAngle;
                if (l1 > l2) l1 -= 2*Math.PI; 
                Drawing.CircleDrawer.Draw(selected, center, radius * CoordinateSystem.Instance.Scale, l1, l2);
                OGL.DrawFilledCircle(new Pen(selected?Color.Blue:Color.Black), ULimitPoint,1);
                
            }
           
        }
        public bool PointIsBetweenEdges(SystemCoordinates point)
        {
            if (LLimitSet && ULimitSet)
            {
                Line a = new Line(center, point, false);
                a.BuildAngle();
                DoubleExtention b = a.AngleForOnePointLines.GetInRadians();
                if ((b >= lowerLimitAngle && b <= upperLimitAngle) || ((b - Math.PI * 2) >= lowerLimitAngle && (b - Math.PI * 2) <= upperLimitAngle) || ((b + Math.PI * 2) >= lowerLimitAngle && (b + Math.PI * 2) <= upperLimitAngle)) return true;
                else return false;
            }
            else
            return true;
        }
        public Line BuildKasatelnaya(SystemCoordinates p)
        {
            Line Radius = new Line(p, center, false);
            Radius.BuildAngle();
            Line ret = Radius.BuildOrthogonalLine(p);
            ret.BuildAngle();
            return ret;
        }
        public Line BuildKasatelnaya(Line Intersect, out bool PointFound, out SystemCoordinates IntersectPoint)
        {
            try
            {
                var points_i = this.IntersectionWithLine(Intersect);
                var points_inf = this.IntersectionWithInfLine(Intersect);
                if (points_i.Count == 0)
                {
                    PointFound = false;
                    IntersectPoint = SystemCoordinates.Zero;
                    return Line.Zero;
                }
                if (points_i.Count == 1 && points_inf.Count == 1 && ULimitSet == false)
                {
                    PointFound = false;
                    IntersectPoint = SystemCoordinates.Zero;
                    return Line.Zero;
                }
                SystemCoordinates point;
                if (points_i.Count == 1)
                {
                    point = points_i[0];
                }
                else if (points_i.Count == 2)
                {
                    if (Intersect.FirstEndSet)
                    {
                        DoubleExtention d = Intersect.FirstEnd.Distance(points_i[0]);
                        if (d > Intersect.FirstEnd.Distance(points_i[1])) point = points_i[1];
                        else point = points_i[0];
                    }
                    else
                    {
                        DoubleExtention d = Intersect.SecondEnd.Distance(points_i[0]);
                        if (d > Intersect.SecondEnd.Distance(points_i[1])) point = points_i[1];
                        else point = points_i[0];
                    }

                }
                else point = points_i[0];
                IntersectPoint = point;
                Line prlLine = this.BuildKasatelnaya(point);
                PointFound = true;
                return prlLine;
            }

            catch (Exception)
            {

                throw;
            }
        }
        public void MoveTo(SystemCoordinates from, SystemCoordinates to)
        {
            SystemCoordinates diff = to - from;
            center = new SystemCoordinates(center.X + diff.X, center.Y + diff.Y);
        }
    }
}
