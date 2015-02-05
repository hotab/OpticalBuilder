using System;
using System.Drawing;
using System.Windows.Forms;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.OpticalObjects;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.Drawing
{
    public static class LineDrawer
    {
        public static void DrawLine(Point p1, Point p2, Color drawingColor, float width, bool Selected = false)
        {
            if (!Selected)
            {
                
                OGL.DrawLine(new Pen(drawingColor, width), p1, p2);
            }
            else {
                OGL.DrawLine(new Pen(Color.Blue, width), p1, p2);}
        }
        public static void DrawLine(SystemCoordinates p1, SystemCoordinates p2, Color drawingColor, float width, bool Selected = false)
        {
            Point pp1 = CoordinateSystem.Instance.Converter(p1);
            Point pp2 = CoordinateSystem.Instance.Converter(p2);
            if (!Selected)
            {
                
                OGL.DrawLine(new Pen(drawingColor, width), pp1, pp2);
            }
            else {
                OGL.DrawLine(new Pen(Color.Blue, width), pp1, pp2);
            }
    }
        public static void DrawLine(SystemCoordinates p, Line x, Color drawingColor, float width, bool Selected = false)
        {
            Point startingPoint = CoordinateSystem.Instance.Converter(p);
            try
            {
                SystemCoordinates endingPoint;
                if (x.Vertical)
                {
                    if (x.AngleForOnePointLines == 90)
                    {
                        bool intr, eq;
                        endingPoint =
                            x.IntersectWithLine(
                                new Line(
                                    new SystemCoordinates(CoordinateSystem.Instance.MinX, CoordinateSystem.Instance.MaxY),
                                    new SystemCoordinates(CoordinateSystem.Instance.MaxX, CoordinateSystem.Instance.MaxY)),
                                out intr, out eq);
                        if (intr)
                        {
                            DrawLine(startingPoint, CoordinateSystem.Instance.Converter(endingPoint), drawingColor,
                                     width, Selected);
                        }
                    }
                    else
                    {
                        bool intr, eq;
                        endingPoint =
                            x.IntersectWithLine(
                                new Line(
                                    new SystemCoordinates(CoordinateSystem.Instance.MinX, CoordinateSystem.Instance.MinY),
                                    new SystemCoordinates(CoordinateSystem.Instance.MaxX, CoordinateSystem.Instance.MinY)),
                                out intr, out eq);
                        if (intr)
                        {
                            DrawLine(startingPoint, CoordinateSystem.Instance.Converter(endingPoint), drawingColor,
                                     width, Selected);
                        }
                    }
                }
                else
                {
                    if (x.AngleForOnePointLines.GetInDegrees() > (DoubleExtention) 90 &&
                        x.AngleForOnePointLines.GetInDegrees() < (DoubleExtention) 270)
                    {
                        bool intr, eq;
                        endingPoint =
                            x.IntersectWithLine(
                                new Line(
                                    new SystemCoordinates(CoordinateSystem.Instance.MinX, CoordinateSystem.Instance.MinY),
                                    new SystemCoordinates(CoordinateSystem.Instance.MinX, CoordinateSystem.Instance.MaxY)),
                                out intr, out eq);
                        if (intr)
                        {
                            DrawLine(startingPoint, CoordinateSystem.Instance.Converter(endingPoint), drawingColor,
                                     width,Selected);
                        }
                    }
                    else
                    {
                        bool intr, eq;
                        Line a = new Line(
                            new SystemCoordinates(CoordinateSystem.Instance.MaxX, CoordinateSystem.Instance.MinY),
                            new SystemCoordinates(CoordinateSystem.Instance.MaxX, CoordinateSystem.Instance.MaxY));
                        endingPoint =
                            x.IntersectWithLine(a,
                                out intr, out eq);
                        if (intr)
                        {
                            DrawLine(startingPoint, CoordinateSystem.Instance.Converter(endingPoint), drawingColor,
                                     width,Selected);
                        }
                    }

                }
            }
            catch
            {
                var zzz = new Exception("Ошибка рисования линии");
                MessageBox.Show(zzz.Message);
            }
        }
        public static void RayAutoSelect(Line x, Color drawingColor, float width, bool Selected = false)
        {
            if(x.FirstEndSet && x.SecondEndSet) DrawLine(x.FirstEnd, x.SecondEnd, drawingColor, width, Selected);
            if (x.FirstEndSet && x.SecondEndSet == false) DrawLine(x.FirstEnd, x, drawingColor, width, Selected);
            if(x.FirstEndSet == false && x.SecondEndSet) DrawLine(x.SecondEnd, x, drawingColor, width, Selected);
        }
    }
}
