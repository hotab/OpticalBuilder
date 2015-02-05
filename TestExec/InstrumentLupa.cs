using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Drawing;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilder
{
    public class InstrumentLupa:InstrumentBase
    {
        private static InstrumentLupa instance;
        private static Cursor ZoomInCursor;
        private static Cursor ZoomOutCursor;
        bool ctrl_mod;
        public static InstrumentLupa Instance
        {
            get { return instance; }
        }
        public override void Draw(Point cursorCoordinates)
        {
            if(Form1.ActiveForm!=null)
                if (control_modifier != ctrl_mod)
                {
                    if (control_modifier)
                        Form1.form.simpleGL1.Cursor = ZoomInCursor;
                    else
                        Form1.form.simpleGL1.Cursor = ZoomOutCursor;
                    ctrl_mod = control_modifier;
                }
            
        }

        public override void Action(Point cursorCoordinates)
        {
            if(!control_modifier)
            {
                CoordinateSystem.Instance.Scale = CoordinateSystem.Instance.Scale * 2;
                SystemCoordinates NewCenter = cursorCoordinates;
                CoordinateSystem.Instance.DifferenceSize = NewCenter;
            }
            else
            {
                CoordinateSystem.Instance.Scale = CoordinateSystem.Instance.Scale / 2;
                SystemCoordinates NewCenter = cursorCoordinates;
                CoordinateSystem.Instance.DifferenceSize = NewCenter;
            }
        }
        public InstrumentLupa(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            instance = this;
            ZoomInCursor = new Cursor(Application.StartupPath + "\\Images\\cursors\\CursorZoomIn.cur");
            ZoomOutCursor = new Cursor(Application.StartupPath + "\\Images\\cursors\\CursorZoomout.cur");
        }
        private InstrumentLupa()
        {
            
        }
        public override void Deactivate()
        {
            ((Form1)(Form1.ActiveForm)).simpleGL1.Cursor = Cursors.Default;
            base.Deactivate();
        }
        public override void Activate()
        {
            ctrl_mod = control_modifier;
            if (control_modifier)
                Form1.form.simpleGL1.Cursor = ZoomInCursor;
            else
                Form1.form.simpleGL1.Cursor = ZoomOutCursor;
            base.Activate();
        }
    }
}
