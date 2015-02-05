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
    public class InstrumentSelectAndMove:InstrumentBase
    {
        private static InstrumentSelectAndMove instance;
        public static InstrumentSelectAndMove Instance
        {
            get { return instance; }
        }
        public override void Draw(Point cursorCoordinates)
        {
        }

        public override void Action(Point cursorCoordinates)
        {
        }
        public InstrumentSelectAndMove(ThreePosBtn instrumentPic)
            : base(instrumentPic)
        {
            control_modifier = false;
            alt_modifier = false;
            shift_modifier = false;
            instance = this;
        }
        private InstrumentSelectAndMove()
        {
            
        }
        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
