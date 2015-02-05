using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    class DrawingLineError:MyException
    {
        public override string Message
        {
            get
            {
                return OpticalBuilderLib.Configuration.STranslation.T["LineError"];
            }
        }
        public override string ToString()
        {
            return Message;
        }
        public DrawingLineError()
            : base()
        {
            code = 1;
        }
    }
}
