using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.Drawing
{
    public static class MirrorDrawer
    {
        public static void Draw(Angle angle, Point SecondEnd, Point FirstEnd, bool Selected=false)
        {
            Draw(angle, CoordinateSystem.Instance.Converter(SecondEnd), CoordinateSystem.Instance.Converter(FirstEnd), Selected);
        }
        public static void Draw(Angle angle, Line ToDraw, bool Selected = false)
        {
            Draw(angle, ToDraw.FirstEnd, ToDraw.SecondEnd, Selected);
        }
        public static void Draw(Angle angle, SystemCoordinates SecondEnd, SystemCoordinates FirstEnd, bool Selected = false)
        {
            Point p1 = CoordinateSystem.Instance.Converter(SecondEnd);
            p1.Y += (int)(3 * angle.cos());
            p1.X += (int)(3 * angle.sin());
            Point pp1 = new Point(p1.X + (int)(Math.Ceiling(2 * angle.sin() * (-1))), p1.Y - (int)(Math.Ceiling(2 * angle.cos())));
            if (angle.GetInDegrees() >= 69 && angle.GetInDegrees() <= 90)
            {
                pp1.Y += 1;
            }
            DoubleExtention d = FirstEnd.Distance(SecondEnd);
            d *= CoordinateSystem.Instance.Scale;
            int length = (int)d;
            Point p2 = new Point((int)(length * angle.cos()) + p1.X, (int)(length * angle.sin() * (-1)) + p1.Y);
            Point pp2 = new Point((int)(length * angle.cos()) + pp1.X, (int)(length * angle.sin() * (-1)) + pp1.Y);
            LineDrawer.DrawLine(p1, p2, Color.Red, 5);
            if(Selected) LineDrawer.DrawLine(pp1, pp2, Color.Blue, 3);
            else LineDrawer.DrawLine(pp1, pp2, Color.Black, 3);
        }
    }
}
