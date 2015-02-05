using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.EventArguments
{
    public class LanguageChangeArgs : EventArgs
    {
        string newLanguage;
        public string NewLanguage
        {
            get { return newLanguage; }
        }
        public LanguageChangeArgs(string NewLanguage)
        {
            newLanguage = NewLanguage;
        }
    }
}
