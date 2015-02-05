using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    public class ErrorWhileReading:MyException
    {
        public override string Message
        {
            get
            {
                return OpticalBuilderLib.Configuration.STranslation.T["ErrorWhileReading"];
            }
        }
        public override string ToString()
        {
            return Message;
        }
        public ErrorWhileReading()
            : base()
        {
            code = 1;
        }
    }
}
