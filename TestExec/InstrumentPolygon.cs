using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.OpticalObjects;

namespace OpticalBuilder
{
    public class InstrumentPolygon: InstrumentBase
    {
        public InstrumentPolygon Instance
        {
            get { return instance; }
        }

        private InstrumentPolygon instance;

        private List<SystemCoordinates> ring = new List<SystemCoordinates>();
        public override void Draw(Point cursorCoordinates)
        {
            if(ring.Count == 0)
            {
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
            }
            if(ring.Count == 1)
            {
                if((SystemCoordinates)cursorCoordinates != ring[0])
                {
                    Line drw = new Line(cursorCoordinates,ring[0],false);
                    drw.Drawer(Color.Black, 2);
                    OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
                }
            }
            if(ring.Count > 1)
            {
                if (ring[0] != ring[ring.Count - 1])
                {
                    for (int i = 0; i < ring.Count - 1; i++)
                    {
                        Line draw = new Line(ring[i], ring[i + 1], false);
                        draw.Drawer(Color.Black, 2);
                    }
                    if ((ring[0].Distance(cursorCoordinates)*CoordinateSystem.Instance.Scale) < 5)
                    {
                        cursorCoordinates = ring[0];
                    }
                    Line draww = new Line(ring[ring.Count - 1], cursorCoordinates, false);
                    draww.Drawer(Color.Black, 2);
                }
                else
                {
                    for (int i = 0; i < ring.Count - 1; i++)
                    {
                        Line draw = new Line(ring[i], ring[i + 1], false);
                        draw.Drawer(Color.Black, 2);
                    }
                }
                OGL.DrawFilledCircle(new Pen(Color.Yellow), cursorCoordinates, 5);
            }
        }

        public override void Action(Point cursorCoordinates)
        {
            if (ring.Count == 0)
                ring.Add(cursorCoordinates);
            else if (ring.Count == 1 && ring[0].ToScreen()!=cursorCoordinates)
                ring.Add(cursorCoordinates);
            else
            {
                bool isect, eq;
                if ((ring[0].Distance(cursorCoordinates) * CoordinateSystem.Instance.Scale) < 5)
                    cursorCoordinates = ring[0].ToScreen();
                Line chk = new Line(ring[ring.Count-1], cursorCoordinates, false);
                if(cursorCoordinates!=(Point)ring[0])
                for(int i = 0; i<ring.Count-1; i++)
                {
                    Line z = new Line(ring[i], ring[i+1], false);
                    z.IntersectWithLine(chk, out isect, out eq);
                    if (isect) return;
                }
                else
                    for (int i = 0; i < ring.Count-2; i++)
                    {
                        Line z = new Line(ring[i], ring[i + 1], false);
                        z.IntersectWithLine(chk, out isect, out eq);
                        if (isect) return;
                    }
                
                if(cursorCoordinates!=ring[0].ToScreen())
                {
                    ring.Add(cursorCoordinates);
                }
                else
                {
                    SystemCoordinates[] zz = ring.ToArray();
                    ring.Add(cursorCoordinates);
                    CodeFunctions.AddPoly(zz);
                    Deactivate();
                }
                
            }
        }
        public override void Activate()
        {
            ring = new List<SystemCoordinates>();
            base.Activate();
        }
        public override void Deactivate()
        {
            ring = new List<SystemCoordinates>();
            base.Deactivate();
        }
        public InstrumentPolygon(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            instance = this;
        }
    }
}
