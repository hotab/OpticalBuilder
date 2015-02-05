using System;
using System.Collections.Generic;
 
using System.Text;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.OpticalObjects
{
    interface IPreloml
    {
        DoubleExtention OpticalDensity { get; set; }
    }
}
