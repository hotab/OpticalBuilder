using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.Drawing
{
    public static class CircleDrawer
    {
        public static void Draw(bool selected, SystemCoordinates center, DoubleExtention radius)
        {
            OGL.DrawCircle(new Pen(selected ? Color.Blue : Color.Black, 2), center, radius);
        }
        public static void Draw(bool selected, SystemCoordinates center, DoubleExtention radius, DoubleExtention l1, DoubleExtention l2)
        {
            OGL.DrawCirclePart(new Pen(selected ? Color.Blue : Color.Black, 2), center, radius,l1,l2);
        }
    }
}
