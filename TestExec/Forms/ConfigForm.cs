using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilder.Forms
{
    public partial class ConfigForm : Form
    {
        private OpenFileDialog lng_l;
        private string path;
        public ConfigForm()
        {
            lng_l = new OpenFileDialog();
            path = null;
            Config.NewConfig += new EventHandler<OpticalBuilderLib.EventArguments.ConfigurationChangeArgs>(Config_NewConfig);
            Translation.LanguageChange += new EventHandler<OpticalBuilderLib.EventArguments.LanguageChangeArgs>(Translation_LanguageChange);
            InitializeComponent();
            comboAutoSave.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void Config_NewConfig(object sender, OpticalBuilderLib.EventArguments.ConfigurationChangeArgs e)
        {
            if(e.FullReload)
            {
                ReadValues();
                return;
            }
            if(e.Param == "Ray.ReflectionLimit")
            {
                textReflLimit.Text = SConfig.C["Ray.ReflectionLimit"];
            }
            if(e.Param == "Autosave")
            {
                if(Translation.Loaded)
                comboAutoSave.Text = (SConfig.C["Autosave"] == "1" ? STranslation.T["Yes"] : STranslation.T["No"]);
            }
        }

        void Translation_LanguageChange(object sender, OpticalBuilderLib.EventArguments.LanguageChangeArgs e)
        {
            Text = STranslation.T["Configform"];
            btnAccept.Text = STranslation.T["Accept"];
            btnDecline.Text = STranslation.T["Decline"];
            groupLangAdd.Text = STranslation.T["Langadd"];
            btnLookUp.Text = STranslation.T["Browse"];
            btnAddLang.Text = STranslation.T["Add"];
            labelAutoSave.Text = STranslation.T["Autosave"];
            labelReflLimit.Text = STranslation.T["ReflLimit"];
            labelDensity.Text = STranslation.T["EnvironDens"];
            comboAutoSave.Items.Clear();
            comboAutoSave.Items.Add(STranslation.T["Yes"]);
            comboAutoSave.Items.Add(STranslation.T["No"]);
            comboAutoSave.Text = (SConfig.C["Autosave"] == "1" ? STranslation.T["Yes"] : STranslation.T["No"]);
        }
        public void UnHook()
        {
            Config.NewConfig -= Config_NewConfig;
            Translation.LanguageChange -= Translation_LanguageChange;
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnLookUp_Click(object sender, EventArgs e)
        {
            DialogResult rs = lng_l.ShowDialog();
            if (rs == DialogResult.OK)
            {
                path = lng_l.FileName;
                textPath.Text = path;
            }
        }

        private void btnAddLang_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                Config.AddLanguage(path);
                path = null;
                textPath.Text = "";
            }
        }

        void ReadValues()
        {
            textReflLimit.Text = SConfig.C["Ray.ReflectionLimit"];
            textBox1.Text = OpticalBuilderLib.OpticalObjects.ObjectCollection.Instance.VneshDensity.ToString();
            if (Translation.Loaded)
            comboAutoSave.Text = (SConfig.C["Autosave"] == "1" ? STranslation.T["Yes"] : STranslation.T["No"]);
        }
        void SetChanges()
        {
            SConfig.C["Autosave"] = (comboAutoSave.Text == STranslation.T["Yes"] ? "1" : "0");
            string s = textReflLimit.Text;
            int refl;
            DoubleExtention denns;
            try
            {
                try
                {
                    refl = Convert.ToInt32(textReflLimit.Text);
                }
                catch
                {
                    throw new Exception(STranslation.T["FormatError"]);
                }
                if (refl < 1)
                {
                    throw new Exception(STranslation.T["ReflLimitCantBeLessThan1"]);
                }
                try
                {
                    textBox1.Text = textBox1.Text.Replace('.', ',');
                    denns = Convert.ToDouble(textBox1.Text);
                }
                catch
                {
                    throw new Exception(STranslation.T["FormatError"]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + STranslation.T["ReflSettingsResetToPreviousValue"]);
                textReflLimit.Text = SConfig.C["Ray.ReflectionLimit"];
                textBox1.Text = OpticalBuilderLib.OpticalObjects.ObjectCollection.Instance.VneshDensity.ToString();
                return;
            }
            SConfig.C["Ray.ReflectionLimit"] = refl.ToString();
            OpticalBuilderLib.OpticalObjects.ObjectCollection.Instance.VneshDensity = denns;
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            SetChanges();
            this.Close();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            ReadValues();
            this.Close();
        }

        private void labelReflLimit_Click(object sender, EventArgs e)
        {

        }
    }
}
