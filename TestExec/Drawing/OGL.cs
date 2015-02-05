using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using Tao.FreeGlut;
using Tao.OpenGl;
using OpticalBuilderLib.TypeExtentions;
namespace OpticalBuilderLib.Drawing
{
    public static class OGL
    {
        public static void DrawLine(Pen pen, Point p1, Point p2)
        {
            Gl.glColor4f((float) (pen.Color.R)/255, (float) (pen.Color.G)/255, (float) (pen.Color.B)/255,
                         (float) 1);
            Gl.glLineWidth(pen.Width);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2i(p1.X, p1.Y);
            Gl.glVertex2i(p2.X,p2.Y);
            Gl.glEnd();
        }
        public static void DrawCircle(Pen pen, Point center, DoubleExtention r)
        {
            Gl.glColor4f((float)(pen.Color.R) / 255, (float)(pen.Color.G) / 255, (float)(pen.Color.B) / 255,
                         (float)1);
            Gl.glLineWidth(pen.Width);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            for (float i = 0; i <= Math.PI * 2; i += 0.01f)
                Gl.glVertex2d(center.X + r * Math.Cos(i), center.Y + r * Math.Sin(i));
            Gl.glEnd();
        }
        public static void DrawCirclePart(Pen pen, Point center, DoubleExtention r, DoubleExtention l1, DoubleExtention l2)
        {
            Gl.glColor4f((float)(pen.Color.R) / 255, (float)(pen.Color.G) / 255, (float)(pen.Color.B) / 255,
                         (float)1);
            Gl.glLineWidth(pen.Width);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            if(l1 > l2)
            {
                DoubleExtention tmp = l1;
                l1 = l2;
                l2 = tmp;
            }        
            for (DoubleExtention i = l1; i<=l2; i += 0.005)
                Gl.glVertex2d(center.X + r * Math.Cos(i), center.Y - r * Math.Sin(i));
            Gl.glEnd();
        }
        public static void DrawFilledCircle(Pen pen, Point center, DoubleExtention r)
        {
            Gl.glColor4f((float)(pen.Color.R) / 255, (float)(pen.Color.G) / 255, (float)(pen.Color.B) / 255,
                         (float)1);
            Gl.glPointSize(pen.Width > (float)r ? pen.Width:(float)r);
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glVertex2i(center.X, center.Y);
            Gl.glEnd();
        }
        public static void PrintText(float x, float y, string text)
        {
            Gl.glRasterPos2f(x, y);
            foreach (char chdr in text)
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_8_BY_13, chdr);
        }
    }
}
