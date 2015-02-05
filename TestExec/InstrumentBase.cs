using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.OpticalObjects;

namespace OpticalBuilder
{
    public abstract class InstrumentBase
    {
        protected ThreePosBtn pic;
        protected bool control_modifier;
        protected bool alt_modifier;
        protected bool shift_modifier;
        private bool activated = false;
        public InstrumentBase()
        {
            throw new Exception("I cannot be created");
        }
        public InstrumentBase(ThreePosBtn instrumentPic)
        {
            pic = instrumentPic;
        }
        public void DrawInstrumentPic()
        {
        }
        public virtual void Activate()
        {
            activated = true;
            pic.Activate();
        }
        public void Deselect()
        {
            pic.Deactivate();
            activated = false;
        }
        public abstract void Draw(Point cursorCoordinates);
        public abstract void Action(Point cursorCoordinates);
        public virtual void Deactivate()
        {
            Form1 x;
            if(ObjectCollection.Instance.WhereToDraw.Parent is Form1)
                x = (Form1)(ObjectCollection.Instance.WhereToDraw.Parent);
            else
                throw new OpticalBuilderLib.Exceptions.InnerException();
            
        }
        public void ControlPressed(bool State)
        {
            control_modifier = State;
        }
        public void AltPressed(bool State)
        {
            alt_modifier = State;
        }
        public void ShiftPressed(bool State)
        {
            shift_modifier = State;
        }
    }
}
