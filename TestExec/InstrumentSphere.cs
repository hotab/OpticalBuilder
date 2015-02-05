using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilder
{
    public class InstrumentSphere:InstrumentBase
    {
        private SystemCoordinates center;
        private DoubleExtention radius;
        private int counter;
        public override void Draw( System.Drawing.Point cursorCoordinates)
        {
            if (counter == 0)
            {
                OpticalBuilderLib.Drawing.OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 10);
            }
            if (counter == 1)
            {
                OpticalBuilderLib.Drawing.OGL.DrawFilledCircle(new Pen(Color.Yellow), center, 10);
                
                DoubleExtention radius = center.Distance(CoordinateSystem.Instance.Converter(cursorCoordinates));
                OpticalBuilderLib.Drawing.CircleDrawer.Draw(false, center, radius * CoordinateSystem.Instance.Scale);
                
            }
            if (counter == 2)
            {
                OpticalBuilderLib.Drawing.OGL.DrawFilledCircle(new Pen(Color.Yellow), center, 10);
                OpticalBuilderLib.Drawing.CircleDrawer.Draw(false, center, radius * CoordinateSystem.Instance.Scale);
            }
        }

        public override void Action(System.Drawing.Point cursorCoordinates)
        {
            
            if (counter == 1)
            {
                radius = center.Distance(CoordinateSystem.Instance.Converter(cursorCoordinates));
                counter++;
            }
            if (counter == 2)
            {
                CodeFunctions.AddSphere(center, radius);
                Deactivate();
                return;
            }

            if (counter == 0)
            {
                center = CoordinateSystem.Instance.Converter(cursorCoordinates);
                counter++;
            }
        }
        public InstrumentSphere(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
        }
        private InstrumentSphere()
        {
            
        }
        public override void Deactivate()
        {
            counter = 0;
            base.Deactivate();
        }
    }
}
