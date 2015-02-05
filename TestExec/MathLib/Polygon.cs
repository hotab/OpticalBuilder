using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.MathLib
{
    public class Polygon
    {
        Line[] lineCol;
        SystemCoordinates[] ringCol;
        public Line[] Lines
        {
            get { return lineCol; }
        }
        public SystemCoordinates[] Ring
        {
            get { return ringCol; }
        }
        SystemCoordinates center;
        public SystemCoordinates Center
        {
            get { return center; }
            set { MoveTo(center,value);}
        }
        public Polygon(SystemCoordinates[] ring)
        {
            DoubleExtention minX = ring[0].X, maxX = ring[0].X, minY = ring[0].Y, maxY = ring[0].Y;
            lineCol = new Line[ring.Length];
            for (int i = 0; i < ring.Length - 1; i++)
            {
                lineCol[i] = new Line(ring[i].Clone(), ring[i + 1].Clone(), false);
                minX = DoubleExtention.MinimumOf(minX, ring[i].Clone().X);
                maxX = DoubleExtention.MaximumOf(maxX, ring[i].Clone().X);
                minY = DoubleExtention.MinimumOf(minY, ring[i].Clone().Y);
                maxY = DoubleExtention.MaximumOf(maxY, ring[i].Clone().Y);
            }
            lineCol[ring.Length - 1] = new Line(ring[ring.Length - 1].Clone(), ring[0].Clone(), false);
            minX = DoubleExtention.MinimumOf(minX, ring[ring.Length - 1].X);
            maxX = DoubleExtention.MaximumOf(maxX, ring[ring.Length - 1].X);
            minY = DoubleExtention.MinimumOf(minY, ring[ring.Length - 1].Y);
            maxY = DoubleExtention.MaximumOf(maxY, ring[ring.Length - 1].Y);
            center.X = (maxX + minX) / 2;
            center.Y = (maxY + minY) / 2;
            ringCol = new SystemCoordinates[ring.Length];
            for (int i = 0; i < ring.Length; i++) ringCol[i] = ring[i].Clone();
        }
        public void MoveTo(SystemCoordinates from, SystemCoordinates to)
        {
            center.X += to.X - from.X;
            center.Y += to.Y - from.Y;
            for (int aa = 0; aa < ringCol.Length; aa++)
            {
                ringCol[aa].X += to.X - from.X;
                ringCol[aa].Y += to.Y - from.Y;
            }
            lineCol = new Line[ringCol.Length];
            for (int aa = 0; aa < ringCol.Length-1; aa++)
                lineCol[aa] = new Line(ringCol[aa].Clone(),ringCol[aa+1].Clone(),false);
            lineCol[ringCol.Length - 1] = new Line(ringCol[ringCol.Length-1].Clone(), ringCol[0].Clone(), false);
        }
        public void Rotate(Angle angle)
        {
            for(int i = 0; i<ringCol.Length; i++)
            {
                if (ringCol[i] == center) continue;
                var dx = ringCol[i].X - center.X;
                var dy = ringCol[i].Y - center.Y;
                var len = (dx*dx + dy*dy).sqrt();
                var ang = new Angle(dy/len, dx/len);
                ang = new Angle(ang.GetInDegrees() + angle.GetInDegrees(), false);
                dx = len*ang.cos();
                dy = len*ang.sin();
                ringCol[i].X = center.X + dx;
                ringCol[i].Y = center.Y + dy;
            }
            lineCol = new Line[ringCol.Length];
            for (int i = 0; i < ringCol.Length - 1; i++)
                lineCol[i] = new Line(ringCol[i].Clone(), ringCol[i + 1].Clone(), false);
            lineCol[ringCol.Length - 1] = new Line(ringCol[ringCol.Length - 1].Clone(), ringCol[0].Clone(), false);
        }
        public Line IntersectWithLine(Line toIntersect, out bool intersects, out SystemCoordinates point)
        {
            
            DoubleExtention dst;
            SystemCoordinates s;
            bool isIsecting = false;
            Line isector = Line.Zero;
            point = SystemCoordinates.Zero;
            DoubleExtention distane = 10000;
            foreach(var x in lineCol)
            {
                bool isect;
                bool equal;
                s = x.IntersectWithLine(toIntersect, out isect, out equal);
                if(isect)
                {
                    if(isIsecting == false)
                    {
                        isIsecting = true;
                        isector = x;
                        distane = s.Distance(toIntersect.FirstEnd);
                        point = s;
                    }
                    else
                    {
                        dst = s.Distance(toIntersect.FirstEnd);
                        if(dst < distane)
                        { 
                            isector = x;
                            distane = dst;
                            point = s;
                        }
                    }
                }
            }
            intersects = isIsecting;
            return isector;
        }
    }
}
