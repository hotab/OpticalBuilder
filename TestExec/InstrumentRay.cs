using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.OpticalObjects;

namespace OpticalBuilder
{
    public class InstrumentRay : InstrumentBase
    {
        private Point p1;
        private Point p2;
        private int counter = 0;
        private BrightPoint bound = null;
        private bool GenNew = false;
        public override void Action(Point cursorCoordinates)
        {
            if (counter == 0)
            {
                p1 = cursorCoordinates;
                var a = ObjectCollection.Instance.objects;
                foreach(var b in a)
                {
                    if (b is BrightPoint)
                        if (b.DistanceToPointS(p1) <= 6)
                            bound = (BrightPoint)b;
                }
                if(bound == null)
                {
                    GenNew = true;
                    bound = new BrightPoint(ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.BrightPoint)), p1);
                }
                else
                    p1 = bound.Coordinates;
                counter++;
            }
            else
            {
                p2 = cursorCoordinates;
                counter = 2;
                CodeFunctions.CreateRay(p1, p2, bound, GenNew);
                Deactivate();
            }
        }
        public Point FCoord
        {
            get { return p1; }
        }
        public Point SCoord
        {
            get { return p2; }
        }
        private InstrumentRay()
        {   
        }
        public InstrumentRay(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
        }
        
        public override void Draw( Point cursorCoordinates)
        {
            if(counter == 0)
            {
                BrightPoint tmp = null;
                var a = ObjectCollection.Instance.objects;
                foreach (var b in a)
                {
                    if (b is BrightPoint)
                        if (b.DistanceToPointS(cursorCoordinates) <= 6)
                    tmp = (BrightPoint) b;
                }
                if (tmp == null)
                {
                    OGL.DrawFilledCircle(new Pen(Color.Yellow, 6),cursorCoordinates,6);
                }
                else
                {
                    OGL.DrawFilledCircle(new Pen(Color.Yellow, 3), tmp.Coordinates, 3);
                }
            }
            if (counter == 1)
            {
                if (GenNew) OGL.DrawFilledCircle(new Pen(Color.Black, 6), p1, 6);
                LineDrawer.DrawLine(p1, cursorCoordinates, Color.DarkGreen, 1);
            }
            if (counter == 2)
            {
                if (GenNew) OGL.DrawFilledCircle(new Pen(Color.Black, 6), p1, 6);
                LineDrawer.DrawLine(p1, p2, Color.DarkGreen, 1);
            }
        }
        public override void Deactivate()
        {
            counter = 0;
            GenNew = false;
            bound = null;
            base.Deactivate();
        }
    }
}
