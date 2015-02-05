using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;

namespace OpticalBuilderLib.EventArguments
{
    public class ImageChangeArgs:EventArgs
    {
        Size newSize;
        int newScaling;
        public int NewScaling
        {
            get { return newScaling; }
        }
        public Size NewSize
        {
            get { return newSize; }
        }
        public ImageChangeArgs(Size NewSize, int NewScaling)
        {
            newSize = NewSize;
            newScaling = NewScaling;
        }
    }
}
