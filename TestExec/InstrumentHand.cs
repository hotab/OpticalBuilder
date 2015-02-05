using System;
using System.Collections.Generic;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;

namespace OpticalBuilder
{
    public class InstrumentHand:InstrumentBase
    {
        private static InstrumentHand instance;
        public static InstrumentHand Instance
        {
            get { return instance; }
        }
        public override void Draw(Point cursorCoordinates)
        {
        }

        public override void Action(Point cursorCoordinates)
        {

        }
        public InstrumentHand(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            instance = this;
        }
        private InstrumentHand()
        {
            
        }
        public override void Deactivate()
        {
                Form1.form.simpleGL1.Cursor = Cursors.Default;
            base.Deactivate();
        }
    }
}
