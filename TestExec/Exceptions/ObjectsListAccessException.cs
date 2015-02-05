using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    public class ObjectsListAccessException:MyException
    {
        public ObjectsListAccessException()
            : base()
        {
            code = 3;
        }
        public override string Message
        {
            get
            {
                return "Неправомерное обращение к списку объектов";
            }
        }
        public override string ToString()
        {
            return Message;
        }
    }
}
