using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    public class MyException:Exception
    {
        protected int code;
        public int Code
        {
            get
            {
                return code;
            }
        }
        protected MyException()
            : base()
        {
        }
    }
}
