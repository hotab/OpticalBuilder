using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;
using OpticalBuilderLib.OpticalObjects;

namespace OpticalBuilder
{
    public class InstrumentSphericalMirror:InstrumentBase
    {
        private int count = 0;
        private SystemCoordinates c1;
        private SystemCoordinates c2;
        private SystemCoordinates c3;
        private static InstrumentSphericalMirror instance;
        public static InstrumentSphericalMirror Instance
        {
            get { return instance; }
        }
        public override void Draw(Point cursorCoordinates)
        {
            if(count == 0)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), cursorCoordinates, 2);
            }
            if(count == 1)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), c1, 2);
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), cursorCoordinates, 2);
            }
            if(count == 2)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), c1, 2);
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), c2, 2);
                if(!SystemCoordinates.InOneLine(c1,c2,cursorCoordinates))
                {
                    Circle a = new Circle(c1, cursorCoordinates, c2);
                    ObjectProto x = new SphereMirror("A~~112e^", a.Center, a.Radius, c1, cursorCoordinates, c2, control_modifier);
                    x.Drawer();
                }
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), cursorCoordinates, 2);
            }
            if(count == 3)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), c1, 2);
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), c2, 2);
                OGL.DrawFilledCircle(new Pen(Color.Yellow, 4), c3, 2);
                if (!SystemCoordinates.InOneLine(c1, c2, c3))
                {
                    Circle a = new Circle(c1, c3, c2);
                    ObjectProto x = new SphereMirror("A~~112e^", a.Center, a.Radius, c1, c3, c2, control_modifier);
                    x.Drawer();
                }
            }
        }

        public override void Action(Point cursorCoordinates)
        {
            if(count == 0)
            {
                c1 = cursorCoordinates;
                count++;
            }
            else
            if(count == 1 && c1.ToScreen() != cursorCoordinates)
            {
                c2 = cursorCoordinates;
                count++;
            }
            else if (count == 2 && c1.ToScreen() != cursorCoordinates && c2.ToScreen() != cursorCoordinates && !SystemCoordinates.InOneLine(c1,c2,cursorCoordinates))
            {
                c3 = cursorCoordinates;
                count++;
                Circle a = new Circle(c1,c3,c2);
                ObjectCollection.Instance.AddObject(
                    new SphereMirror(ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.SphereMirror)), a.Center,
                                     a.Radius, c1, c3, c2, control_modifier));
                Deactivate();
            }
        }
        public InstrumentSphericalMirror(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            instance = this;
        }
        private InstrumentSphericalMirror()
        {
            
        }
        public override void Deactivate()
        {
            count = 0;
            base.Deactivate();
        }
    }
}
