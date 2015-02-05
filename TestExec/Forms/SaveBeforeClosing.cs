using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;

namespace OpticalBuilderLib.Forms
{
    public partial class SaveBeforeClosing : Form
    {
        public SaveBeforeClosing()
        {
            OpticalBuilderLib.Configuration.Translation.LanguageChange += new EventHandler<OpticalBuilderLib.EventArguments.LanguageChangeArgs>(Translation_LanguageChange);
            InitializeComponent();
        }

        void Translation_LanguageChange(object sender, OpticalBuilderLib.EventArguments.LanguageChangeArgs e)
        {
            this.Text = OpticalBuilderLib.Configuration.STranslation.T["SaveBeforeClosingProject"];
            label1.Text = OpticalBuilderLib.Configuration.STranslation.T["SaveBeforeClosingProject"];
            button1.Text = OpticalBuilderLib.Configuration.STranslation.T["Yes"];
            button2.Text = OpticalBuilderLib.Configuration.STranslation.T["No"];
            button3.Text = OpticalBuilderLib.Configuration.STranslation.T["Cancel"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
