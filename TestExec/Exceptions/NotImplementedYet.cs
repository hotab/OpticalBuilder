using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    public class NotImplementedYet:MyException
    {
        public override string Message
        {
            get
            {
                return "Not implemented yet";
            }
        }
        public override string ToString()
        {
            return Message;
        }
        public NotImplementedYet()
            : base()
        {
            code = 1;
        }
    }
}
