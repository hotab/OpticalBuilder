using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.OpticalObjects;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilder
{
    public class InstrumentLenseMenisksTwoOkrs : InstrumentBase
    {
        private int count;
        private SystemCoordinates p1;
        private SystemCoordinates p2;
        private SystemCoordinates p3;
        private SystemCoordinates p4;
        private SystemCoordinates p5;
        private SystemCoordinates p6;
        private static InstrumentLenseMenisksTwoOkrs instance;
        public static InstrumentLenseMenisksTwoOkrs Instance
        {
            get { return instance; }
        }
        public override void Draw(Point cursorCoordinates)
        {
            SystemCoordinates P1, P3, P4, P6;
            if(count == 0)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
            }
            if(count == 1)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
            }
            if (count == 2 && cursorCoordinates != p1.ToScreen() && cursorCoordinates != p2.ToScreen())
            {
                Line x = p1 + p2;
                DoubleExtention l = x.DistanceToPoint(cursorCoordinates);
                Line z = x.BuildOrthogonalLine(p2);
                z.BuildAngle();
                if (z.AngleForOnePointLines == new Angle(90, false) || z.AngleForOnePointLines == new Angle(90, false))
                {
                    z.FirstEnd = new SystemCoordinates(p2.X, p2.Y + l);
                    z.SecondEnd = new SystemCoordinates(p2.X, p2.Y - l);
                }
                else
                {
                    z.FirstEnd = new SystemCoordinates(p2.X + l * z.AngleForOnePointLines.cos(), p2.Y + l * z.AngleForOnePointLines.sin());
                    z.SecondEnd = new SystemCoordinates(p2.X - l * z.AngleForOnePointLines.cos(), p2.Y + l * z.AngleForOnePointLines.sin());
                }
                Line u = x.BuildOrthogonalLine(p1);
                u.BuildAngle();
                if (u.AngleForOnePointLines == new Angle(90, false) || u.AngleForOnePointLines == new Angle(90, false))
                {
                    u.FirstEnd = new SystemCoordinates(p1.X, p1.Y + l);
                    u.SecondEnd = new SystemCoordinates(p1.X, p1.Y - l);
                }
                else
                {
                    u.FirstEnd = new SystemCoordinates(p1.X + l * u.AngleForOnePointLines.cos(), p1.Y + l * u.AngleForOnePointLines.sin());
                    u.SecondEnd = new SystemCoordinates(p1.X - l * u.AngleForOnePointLines.cos(), p1.Y + l * u.AngleForOnePointLines.sin());
                }
                P1 = z.FirstEnd;
                P3 = z.SecondEnd;
                P4 = u.FirstEnd;
                P6 = u.SecondEnd;
                u = new Line(P1, P3, false);
                z = new Line(P4, P6, false);
                u.Drawer(2);
                z.Drawer(2);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), P1, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), P3, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), P4, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), P6, 5);
            }
            if (count == 3 && cursorCoordinates != p1.ToScreen() && cursorCoordinates != p3.ToScreen() && cursorCoordinates != p4.ToScreen() && cursorCoordinates != p6.ToScreen())
            {
                Line z = new Line(p1, p3, false);
                Line u = new Line(p4, p6, false);
                z.Drawer(2);
                u.Drawer(2);
                
                if(p1.Distance(p4) < p1.Distance(p6))
                {
                    Circle beta = null;
                    Line alpha = null;
                    if (SystemCoordinates.InOneLine(p1, cursorCoordinates, p4))
                        alpha = new Line(p1, p4, false);
                    else beta = new Circle(p1, cursorCoordinates, p4);
                    if(alpha!=null)alpha.Drawer(Color.Black, 2);
                    if(beta!=null)beta.Draw(false);
                }
                else
                {
                    Circle beta = null;
                    Line alpha = null;
                    if(SystemCoordinates.InOneLine(p1,cursorCoordinates,p6))
                        alpha = new Line(p1,p6, false);
                    else
                    beta = new Circle(p1, cursorCoordinates, p6 );
                    if (alpha != null) alpha.Drawer(Color.Black, 2);
                    if (beta != null) beta.Draw(false);
                }
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p3, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p4, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p6, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
            }
            if (count == 4 && cursorCoordinates != p1.ToScreen() && cursorCoordinates != p3.ToScreen() && cursorCoordinates != p4.ToScreen() && cursorCoordinates != p6.ToScreen())
            {
                Line z = new Line(p1, p3, false);
                Line u = new Line(p4, p6, false);
                z.Drawer(2);
                u.Drawer(2);
                if (p1.Distance(p4) < p1.Distance(p6))
                {
                    Line beta_x = null;
                    Line alpha_x = null;
                    Circle alpha = null, beta = null;
                    if(SystemCoordinates.InOneLine(p1,p2,p4))
                        beta_x = new Line(p1,p4,false);
                    else
                        beta = new Circle(p1, p2, p4 );
                    if(SystemCoordinates.InOneLine(p3,cursorCoordinates,p6))
                        alpha_x = new Line(p3,p6,false);
                    else
                        alpha = new Circle(p3, cursorCoordinates, p6);
                    if(beta!=null)beta.Draw(false);
                    if (alpha != null) alpha.Draw(false);
                    if(beta_x!=null)beta_x.Drawer(2);
                    if(alpha_x!=null)alpha_x.Drawer(2);
                }
                else
                {
                    Line beta_x = null;
                    Line alpha_x = null;
                    Circle beta = null;
                    Circle alpha = null;
                    if (SystemCoordinates.InOneLine(p1, p2, p6))
                        beta_x = new Line(p1, p6, false);
                    else
                        beta = new Circle(p1, p2, p6);
                    if (SystemCoordinates.InOneLine(p3, cursorCoordinates, p4))
                        alpha_x = new Line(p3, p4, false);
                    else
                        alpha = new Circle(p3, cursorCoordinates, p4);
                    if (beta != null) beta.Draw(false);
                    if (alpha != null) alpha.Draw(false);
                    if (beta_x != null) beta_x.Drawer(2);
                    if (alpha_x != null) alpha_x.Drawer(2);
                }
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p3, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p4, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p6, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p2, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
            }
            if(count == 5)
            {
                Line z = new Line(p1, p3, false);
                Line u = new Line(p4, p6, false);
                z.Drawer(2);
                u.Drawer(2);
                if (p1.Distance(p4) < p1.Distance(p6))
                {
                    Line beta_x = null;
                    Line alpha_x = null;
                    Circle alpha = null, beta = null;
                    if (SystemCoordinates.InOneLine(p1, p2, p4))
                        beta_x = new Line(p1, p4, false);
                    else
                        beta = new Circle(p1, p2, p4);
                    if (SystemCoordinates.InOneLine(p3, p5, p6))
                        alpha_x = new Line(p3, p6, false);
                    else
                        alpha = new Circle(p3, p5, p6);
                    if (beta != null) beta.Draw(false);
                    if (alpha != null) alpha.Draw(false);
                    if (beta_x != null) beta_x.Drawer(2);
                    if (alpha_x != null) alpha_x.Drawer(2);
                }
                else
                {
                    Line beta_x = null;
                    Line alpha_x = null;
                    Circle beta = null;
                    Circle alpha = null;
                    if (SystemCoordinates.InOneLine(p1, p2, p6))
                        beta_x = new Line(p1, p6, false);
                    else
                        beta = new Circle(p1, p2, p6);
                    if (SystemCoordinates.InOneLine(p3, p5, p4))
                        alpha_x = new Line(p3, p4, false);
                    else
                        alpha = new Circle(p3, p5, p4);
                    if (beta != null) beta.Draw(false);
                    if (alpha != null) alpha.Draw(false);
                    if (beta_x != null) beta_x.Drawer(2);
                    if (alpha_x != null) alpha_x.Drawer(2);
                }
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p1, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p3, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p4, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p6, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p2, 5);
                OGL.DrawFilledCircle(new Pen(Color.Yellow), p5, 5);
            }

        }

        public override void Action(Point cursorCoordinates)
        {
            if(count == 0)
            {
                p1 = cursorCoordinates;
                count++;
            }
            else if(count == 1 && cursorCoordinates != p1.ToScreen())
            {
                p2 = cursorCoordinates;
                count++;
            }
            else if (count == 2 && cursorCoordinates != p1.ToScreen() && cursorCoordinates != p2.ToScreen())
            {
                Line x = p1 + p2;
                DoubleExtention l = x.DistanceToPoint(cursorCoordinates);
                Line z = x.BuildOrthogonalLine(p2);
                z.BuildAngle();
                if(z.AngleForOnePointLines == new Angle(90,false) || z.AngleForOnePointLines == new Angle(90,false))
                {
                    z.FirstEnd = new SystemCoordinates(p2.X, p2.Y + l);
                    z.SecondEnd = new SystemCoordinates(p2.X, p2.Y - l);
                }
                else
                {
                    z.FirstEnd = new SystemCoordinates(p2.X + l*z.AngleForOnePointLines.cos(), p2.Y + l*z.AngleForOnePointLines.sin());
                    z.SecondEnd = new SystemCoordinates(p2.X - l*z.AngleForOnePointLines.cos(), p2.Y + l*z.AngleForOnePointLines.sin());
                }
                Line u = x.BuildOrthogonalLine(p1);
                u.BuildAngle();
                if (u.AngleForOnePointLines == new Angle(90, false) || u.AngleForOnePointLines == new Angle(90, false))
                {
                    u.FirstEnd = new SystemCoordinates(p1.X, p1.Y + l);
                    u.SecondEnd = new SystemCoordinates(p1.X, p1.Y - l);
                }
                else
                {
                    u.FirstEnd = new SystemCoordinates(p1.X + l * u.AngleForOnePointLines.cos(), p1.Y + l * u.AngleForOnePointLines.sin());
                    u.SecondEnd = new SystemCoordinates(p1.X - l * u.AngleForOnePointLines.cos(), p1.Y + l * u.AngleForOnePointLines.sin());
                }
                p1 = z.FirstEnd;
                p3 = z.SecondEnd;
                p4 = u.FirstEnd;
                p6 = u.SecondEnd;
                count++;
            }
            else if(count == 3 && cursorCoordinates!=p1.ToScreen() && cursorCoordinates!=p3.ToScreen() && cursorCoordinates != p4.ToScreen() && cursorCoordinates != p6.ToScreen())
            {
                p2 = cursorCoordinates;
                count++;
            }
            else if (count == 4 && cursorCoordinates != p1.ToScreen() && cursorCoordinates != p3.ToScreen() && cursorCoordinates != p4.ToScreen() && cursorCoordinates != p6.ToScreen() && p2.ToScreen()!=cursorCoordinates)
            {
                p5 = cursorCoordinates;
                count++;
            }
            if(count == 5)
            {
                SystemCoordinates center;
                if (p1.Distance(p4) > p1.Distance(p6))
                {
                    Line z = new Line(p1, p4, false);
                    Line u = new Line(p3, p6, false);
                    bool isect, equal;
                    center = z.IntersectWithLine(u, out isect, out equal);
                }
                else
                {
                    Line z = new Line(p1, p6, false);
                    Line u = new Line(p3, p4, false);
                    bool isect, equal;
                    center = z.IntersectWithLine(u, out isect, out equal);
                }
                if(p1.Distance(p4) < p1.Distance(p6))
                {
                    ObjectCollection.Instance.AddObject(
                    new Lense(ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.Lense)), center, p1, p2, p4, p3, p5,
                              p6));
                }
                else
                {
                    ObjectCollection.Instance.AddObject(
                    new Lense(ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.Lense)), center, p1, p2, p6, p3, p5,
                              p4));
                }
                Deactivate();
            }
        }
        public InstrumentLenseMenisksTwoOkrs(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            instance = this;
        }

        private InstrumentLenseMenisksTwoOkrs()
        {

        }
        public override void Deactivate()
        {
            count = 0;
            base.Deactivate();
        }
    }
}

