using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.OpticalObjects;

namespace OpticalBuilder
{
    public class InstrumentLenseNoMenisksTwoOkrs : InstrumentBase
    {
        private int count;
        private SystemCoordinates p1;
        private SystemCoordinates p2;
        private SystemCoordinates p3;
        private SystemCoordinates p4;
        private static InstrumentLenseNoMenisksTwoOkrs instance;
        public static InstrumentLenseNoMenisksTwoOkrs Instance
        {
            get { return instance; }
        }
        public override void Draw(Point cursorCoordinates)
        {
            if(count == 0)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
            }
            if(count == 1)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
            }
            if (count == 2 && (SystemCoordinates)cursorCoordinates != p1 && (SystemCoordinates)cursorCoordinates != p2)
            {
                
                if (SystemCoordinates.InOneLine(p1, cursorCoordinates, p2))
                {
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p2, 5);
                    Line a = new Line(p1, p2, false);
                    a.Drawer(Color.Black, 2);
                }
                else
                {
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p2, 5);
                    Circle a = new Circle(p1, cursorCoordinates, p2);
                    a.Draw();
                }
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
                
            }
            if(count == 3 && (SystemCoordinates)cursorCoordinates != p1 && (SystemCoordinates)cursorCoordinates != p2 && (SystemCoordinates)cursorCoordinates!=p3 )
            {
                if (SystemCoordinates.InOneLine(p1, cursorCoordinates, p2))
                {
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p2, 5);
                    Line a = new Line(p1, p2, false);
                    a.Drawer(Color.Black, 2);
                }
                else
                {
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), p2, 5);
                    Circle a = new Circle(p1, cursorCoordinates, p2);
                    a.Draw();
                }
                
                if (!SystemCoordinates.InOneLine(p1, p3, p2))
                {
                    Circle b = new Circle(p1, p3, p2);
                    b.Draw();
                }
                else
                {
                    Line b = new Line(p1, p2, false);
                    b.Drawer(Color.Black, 2);
                }
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p3, 5);
            }
        }

        public override void Action(Point cursorCoordinates)
        {
            if (count == 0)
            {
                p1 = cursorCoordinates;
                count++;
            }
            else
            {
                if (count == 1 && cursorCoordinates != p1.ToScreen())
                {
                    p2 = cursorCoordinates;
                    count++;
                }
                else
                {
                    if (count == 2 && cursorCoordinates != p1.ToScreen() && cursorCoordinates != p2.ToScreen())
                    {
                        p3 = cursorCoordinates;
                        count++;
                    }
                    else if (count == 3 && cursorCoordinates != p1.ToScreen() && cursorCoordinates != p2.ToScreen() &&
                             cursorCoordinates != p3.ToScreen())
                    {
                        if (!(SystemCoordinates.InOneLine(p1, p3, p2) && SystemCoordinates.InOneLine(p1, cursorCoordinates, p2)))
                        {
                            p4 = cursorCoordinates;
                            count++;
                        }
                    }
                    if(count == 4)
                    {
                        Line aa = new Line(p1, p2, false);
                        ObjectCollection.Instance.AddObject(
                            new Lense(ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.Lense)), aa.Center, p1, p3,
                                      p2, p1,
                                      p4, p2));
                        Deactivate();
                    }
                }
            }
        }
        public InstrumentLenseNoMenisksTwoOkrs(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            instance = this;
        }

        private InstrumentLenseNoMenisksTwoOkrs()
        {

        }
        public override void Deactivate()
        {
            count = 0;
            base.Deactivate();
        }
    }
}

