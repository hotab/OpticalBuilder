using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.Exceptions
{
    public class PrecisionError:MyException
    {
        int Type;
        public override string Message
        {
            get
            {
                try
                {
                    if (Type == 1)
                        return OpticalBuilderLib.Configuration.STranslation.T["PrecisionLessThanZero"];
                    else
                        if (Type == 2)
                            return "Sensless for type";
                        else
                            throw new NotSupportedException();
                }
                catch
                {
                    return "Ошибка точности";
                }
            }
        }
        public override string ToString()
        {
            return Message;
        }
        //type 1 - Precision < 0
        //type 2 - Precision > 8 (sensless for program)
        public PrecisionError(int type)
            : base()
        {
            Type = type;
            code = 4;
        }
    }
}
