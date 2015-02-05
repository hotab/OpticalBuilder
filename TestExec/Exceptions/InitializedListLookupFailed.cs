using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    public class InitializedListLookupFailed:MyException
    {
        public override string Message
        {
            get
            {
                return OpticalBuilderLib.Configuration.STranslation.T["InitializedListLookupFailed"];
            }
        }
        public override string ToString()
        {
            return Message;
        }
        public InitializedListLookupFailed()
            : base()
        {
            code = 2;
        }
    }
}
