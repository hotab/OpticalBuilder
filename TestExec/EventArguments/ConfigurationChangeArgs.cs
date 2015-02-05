using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.EventArguments
{
    public class ConfigurationChangeArgs:EventArgs
    {
        bool fullReload;
        string param;
        public bool FullReload
        {
            get { return fullReload; } 
        }
        public string Param
        {
            get { return param; }
        }
        public ConfigurationChangeArgs(bool FullReload, string Param)
        {
            fullReload = FullReload;
            param = Param;
        }
    }
}
