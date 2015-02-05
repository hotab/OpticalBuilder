using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    public class InnerException:MyException
    {
        public override string Message
        {
            get
            {
                return "Inner program error. Try restarting the program.";
                //Внутренняя программная ошибка. Попробуйте перезапуск.
            }
        }
        public override string ToString()
        {
            return Message;
        }
        public InnerException()
            : base()
        {
            code = 1;
        }
    }
}
