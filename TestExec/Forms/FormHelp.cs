using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Configuration;

namespace OpticalBuilder.Forms
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            this.TopMost = true;
            Translation.LanguageChange += new EventHandler<OpticalBuilderLib.EventArguments.LanguageChangeArgs>(Translation_LanguageChange);
            InitializeComponent();
        }

        void Translation_LanguageChange(object sender, OpticalBuilderLib.EventArguments.LanguageChangeArgs e)
        {
            Text = STranslation.T["Help"];
        }

        private void FormHelp_Load(object sender, EventArgs e)
        {
            axAcroPDF1.Location = new Point(0, 0);
            axAcroPDF1.Size = new Size(Width - 20, Height - 50);
            axAcroPDF1.LoadFile(Application.StartupPath + "\\Help\\Help.pdf");
            axAcroPDF1.setZoom(100);
        }

        private void FormHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;
            e.Cancel = true;
            this.Hide();
            Form1.form.WindowState = FormWindowState.Maximized;
        }

        private void FormHelp_SizeChanged(object sender, EventArgs e)
        {
            axAcroPDF1.Size = new Size(Width-20, Height-50);
        }

        private void FormHelp_Shown(object sender, EventArgs e)
        {
           
        }

        private void FormHelp_Activated(object sender, EventArgs e)
        {
            if(Form1.form!=null) Form1.form.WindowState = FormWindowState.Minimized;
            //Form1.form.TopMost = false;
        }
    }
}
