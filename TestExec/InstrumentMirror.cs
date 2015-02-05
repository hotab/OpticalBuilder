using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;

namespace OpticalBuilder
{
    public class InstrumentMirror : InstrumentBase
    {
        private Point p1;
        private Point p2;
        private int counter = 0;
        private Line x;
        public override void Action(Point cursorCoordinates)
        {
            if (counter == 0)
            {
                p1 = cursorCoordinates;
                counter++;
            }
            else
            {
                if (p1 != cursorCoordinates)
                {
                    p2 = cursorCoordinates;
                    if (alt_modifier == false)
                    {
                        if (control_modifier)
                        {
                            Point p = p1;
                            p1 = cursorCoordinates;
                            cursorCoordinates = p;
                            p2 = p;
                        }
                    }
                    else
                    {
                        SystemCoordinates pp1, pp2;
                        pp1 = CoordinateSystem.Instance.Converter(p1);
                        pp2 = CoordinateSystem.Instance.Converter(cursorCoordinates);
                        pp1 = pp1.BuildPointReflection(pp2);
                        p1 = pp1.ToScreen();
                        p2 = pp2.ToScreen();
                        cursorCoordinates = pp2.ToScreen();
                        if (control_modifier)
                        {
                            Point p = p1;
                            p1 = cursorCoordinates;
                            cursorCoordinates = p;
                            p2 = p;
                        }
                    }
                    x = new Line(CoordinateSystem.Instance.Converter(p1),
                                      CoordinateSystem.Instance.Converter(cursorCoordinates), false);
                    x.BuildAngle();
                    counter = 2;
                    if (p1 != p2)
                        CodeFunctions.AddMirror(x.Center, x.AngleForOnePointLines, x.FirstEnd.Distance(x.SecondEnd));
                    Deactivate();
                }
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
        private InstrumentMirror()
        {   
        }
        public InstrumentMirror(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            
        }
        
        public override void Draw( Point cursorCoordinates)
        {
            SystemCoordinates pp1 = CoordinateSystem.Instance.Converter(p1);
            SystemCoordinates curs = CoordinateSystem.Instance.Converter(cursorCoordinates);
            if (counter == 0)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 10);
            }
            if (counter == 1)
            {
                if(control_modifier == false)
                {
                    if(alt_modifier == true)
                    {
                        pp1 = pp1.BuildPointReflection(curs);
                    }
                    Line x = new Line(pp1,
                                  curs, false);
                    x.BuildAngle();
                    if (cursorCoordinates != p1)
                    {
                        MirrorDrawer.Draw(x.AngleForOnePointLines, pp1, curs);
                        OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 10);
                    }
                }
                else
                {
                    if (alt_modifier == true)
                    {
                        pp1 = pp1.BuildPointReflection(curs);
                    }
                    Line x = new Line(curs,
                                  pp1, false);
                    x.BuildAngle();
                    if (cursorCoordinates != p1)
                    {
                        MirrorDrawer.Draw(x.AngleForOnePointLines, curs, pp1);
                        OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 10);
                    }
                }
            }
            if(counter == 2)
            {
                MirrorDrawer.Draw(x.AngleForOnePointLines, x);
            }
        }
        public override void Deactivate()
        {
            counter = 0;
            base.Deactivate();
        }
    }
}
