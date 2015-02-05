using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.OpticalObjects;
using OpticalBuilderLib.Drawing;
namespace OpticalBuilder
{
    class InstrumentBrightPoint:InstrumentBase
    {
        private Point p1;
        private int counter = 0;
        public override void Draw( System.Drawing.Point cursorCoordinates)
        {
            if (counter == 0)
                OGL.DrawFilledCircle(new Pen(Color.Black, 6), new Point(cursorCoordinates.X, cursorCoordinates.Y), 6);
            else
                OGL.DrawFilledCircle(new Pen(Color.Blue, 6), new Point(p1.X, p1.Y), 6);
        }
        private InstrumentBrightPoint()
        {   
        }
        public InstrumentBrightPoint(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
        }
        public override void Action(System.Drawing.Point cursorCoordinates)
        {
            p1 = cursorCoordinates;
            counter++;
            CodeFunctions.AddBrightPoint(ObjectCollection.SysInstance.Converter(p1));
            Deactivate();
        }
        public override void Deactivate()
        {
            counter = 0;
            base.Deactivate();
        }
    }
}
