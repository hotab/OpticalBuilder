using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.OpticalObjects;

namespace OpticalBuilder
{
    public partial class PropertiesList : UserControl
    {
        private TextBox textBox1, textBox2, textBox3, textBox4, textBox5;
        private Label label1, label2, label3, label4, label5;
        private Dictionary<string, string> SootvStrok;
        private Dictionary<string, string> SootvStrokRays;
        private Dictionary<string, string> SootvStrokB;
        private Dictionary<string, string> SootvStrokRaysB;
        private List<Option> props;
        public event EventHandler<EventArgs> OnUserSelect;
        public string selected_name = null;
        public bool is_ray = false;
        private bool raise = true;
        public PropertiesList()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedValueChanged += comboBox1_SelectedIndexChanged;
            this.Controls.Clear();
            this.Controls.Add(comboBox1);
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_name = comboBox1.Text;
            if (Form1.form.SelectedItem != null)
            {
                if (Form1.form.SelectedItem.Name == selected_name) return;
            }
            
            if (selected_name != null && selected_name!="")
            {
                int j = 0;
                bool add = false;
                string s_string ="";
                string t_spec = "";
                for (j = selected_name.Length - 1; j > 0; j--)
                {
                    if (add == false)
                    {
                        if (selected_name[j] == '-' && selected_name[j + 1] == ' ' && selected_name[j - 1] == ' ')
                        {
                            add = true;
                            j--;
                            t_spec = t_spec.Substring(1);
                        }
                        else
                        t_spec = selected_name[j] + t_spec;
                    }
                    else
                    {
                        s_string = selected_name[j] + s_string;
                    }
                }
                    if (t_spec == STranslation.T[Ray.GetSpec(ObjectTypes.Ray)])
                    {
                        is_ray = true;
                        selected_name = SootvStrokRaysB[selected_name];
                    }
                    else
                    {
                        is_ray = false;
                        selected_name = SootvStrokB[selected_name];
                    }
            }
                //System.Threading.Thread.Sleep(10);
                if (OnUserSelect != null)
                {
                    OnUserSelect.Invoke(this, new EventArgs());
                    Instance_OnSelectedChange(this, new SelectedChangeArgs(Form1.form.SelectedItem));
                }
                if(Form1.form.SelectedItem != null)
                {
                    if(Form1.form.SelectedItem.Coordinates.X < CoordinateSystem.Instance.MinX ||
                       Form1.form.SelectedItem.Coordinates.X > CoordinateSystem.Instance.MaxX ||
                       Form1.form.SelectedItem.Coordinates.Y < CoordinateSystem.Instance.MinY ||
                       Form1.form.SelectedItem.Coordinates.Y > CoordinateSystem.Instance.MaxY)
                    {
                        CoordinateSystem.Instance.DifferenceX = Form1.form.SelectedItem.Coordinates.X;
                        CoordinateSystem.Instance.DifferenceY = Form1.form.SelectedItem.Coordinates.Y;
                    }
                }
        }
        public void UnHook()
        {
            Form1.form.SelectedItem = null;
            Instance_OnSelectedChange(this, new SelectedChangeArgs(null));
        }
        public void HookEvents()
        {
            ObjectCollection.Instance.OnObjectsRaysChange += new EventHandler<EventArgs>(ReloadList);
            ObjectCollection.Instance.OnSelectedChange += new EventHandler<SelectedChangeArgs>(Instance_OnSelectedChange);
            Translation.LanguageChange += new EventHandler<OpticalBuilderLib.EventArguments.LanguageChangeArgs>(Translation_LanguageChange);
        }

        void Translation_LanguageChange(object sender, OpticalBuilderLib.EventArguments.LanguageChangeArgs e)
        {
            ReloadList(sender, new EventArgs());
            Instance_OnSelectedChange(this, new SelectedChangeArgs(Form1.form.SelectedItem));
        }
        public void Instance_OnSelectedChange(object sender, SelectedChangeArgs e)
        {
            try
            {
                //aise = false;
                if (Form1.form.SelectedItem == null)
                {
                    comboBox1.SelectedItem = null;
                }
                else if (Form1.form.SelectedItem is Ray)
                {
                    comboBox1.SelectedItem = SootvStrokRays[Form1.form.SelectedItem.Name];
                }
                else
                {
                    comboBox1.SelectedItem = SootvStrok[Form1.form.SelectedItem.Name];
                }
                
                if (props != null)
                    foreach (var g in props)
                    {
                        g.UnHook();
                    }
                if (Controls != null)
                {
                    foreach (Control x in Controls)
                    {
                        if(x!=comboBox1)
                            x.Dispose();
                    }

                }
                this.Controls.Clear();
                this.Controls.Add(comboBox1);
                props = new List<Option>();

                #region Ray

                if (Form1.form.SelectedItem is Ray)
                {
                    this.textBox1 = new System.Windows.Forms.TextBox();
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.textBox2 = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.textBox3 = new System.Windows.Forms.TextBox();
                    this.label4 = new System.Windows.Forms.Label();
                    this.textBox4 = new System.Windows.Forms.TextBox();
                    // 
                    // textBox1
                    // 
                    this.textBox1.Location = new System.Drawing.Point(Width - 123, 27);
                    this.textBox1.Name = "textBox1";
                    this.textBox1.Size = new System.Drawing.Size(123, 20);
                    this.textBox1.TabIndex = 1;
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label1.Location = new System.Drawing.Point(3, 27);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(41, 24);
                    this.label1.TabIndex = 2;
                    this.label1.Text = STranslation.T["Name"];
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label2.Location = new System.Drawing.Point(3, 51);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(136, 24);
                    this.label2.TabIndex = 3;
                    this.label2.Text = STranslation.T["CoordinateX"];
                    // 
                    // textBox2
                    // 
                    this.textBox2.Location = new System.Drawing.Point(Width - 123, 51);
                    this.textBox2.Name = "textBox2";
                    this.textBox2.Size = new System.Drawing.Size(123, 20);
                    this.textBox2.Enabled = false;
                    this.textBox2.TabIndex = 1;
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label3.Location = new System.Drawing.Point(3, 75);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(136, 24);
                    this.label3.TabIndex = 3;
                    this.label3.Text = STranslation.T["CoordinateY"];
                    // 
                    // textBox3
                    // 
                    this.textBox3.Location = new System.Drawing.Point(Width - 123, 75);
                    this.textBox3.Name = "textBox3";
                    this.textBox3.Size = new System.Drawing.Size(123, 20);
                    this.textBox3.Enabled = false;
                    this.textBox3.TabIndex = 1;
                    // 
                    // label4
                    // 
                    this.label4.AutoSize = true;
                    this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label4.Location = new System.Drawing.Point(3, 99);
                    this.label4.Name = "label4";
                    this.label4.Size = new System.Drawing.Size(52, 24);
                    this.label4.TabIndex = 3;
                    this.label4.Text = STranslation.T["Angle"];
                    // 
                    // textBox4
                    // 
                    this.textBox4.Location = new System.Drawing.Point(Width - 123, 99);
                    this.textBox4.Name = "textBox4";
                    this.textBox4.Size = new System.Drawing.Size(123, 20);
                    this.textBox4.TabIndex = 1;
                    this.Controls.Add(this.label4);
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.label1);
                    this.Controls.Add(this.textBox4);
                    this.Controls.Add(this.textBox3);
                    this.Controls.Add(this.textBox2);
                    this.Controls.Add(this.textBox1);
                    props.Add(new Option(textBox1, Form1.form.SelectedItem.Name, Props.Name, true));
                    textBox1.Text = Form1.form.SelectedItem.Name;
                    props.Add(new Option(textBox2, Form1.form.SelectedItem.Name, Props.CoordinateX, true));
                    textBox2.Text = Form1.form.SelectedItem.Coordinates.X.ToString();
                    props.Add(new Option(textBox3, Form1.form.SelectedItem.Name, Props.CoordinateY, true));
                    textBox3.Text = Form1.form.SelectedItem.Coordinates.Y.ToString();
                    props.Add(new Option(textBox4, Form1.form.SelectedItem.Name, Props.Angle, true));
                    textBox4.Text = ((Ray) Form1.form.SelectedItem).Angle.GetInDegrees().ToString();
                }

                #endregion

                #region OPolygon

                if (Form1.form.SelectedItem is OPolygon)
                {
                    this.textBox1 = new System.Windows.Forms.TextBox();
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.textBox2 = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.textBox3 = new System.Windows.Forms.TextBox();
                    this.label4 = new System.Windows.Forms.Label();
                    this.textBox4 = new System.Windows.Forms.TextBox();
                    this.label5 = new System.Windows.Forms.Label();
                    this.textBox5 = new System.Windows.Forms.TextBox();
                    // 
                    // textBox1
                    // 
                    this.textBox1.Location = new System.Drawing.Point(Width - 123, 27);
                    this.textBox1.Name = "textBox1";
                    this.textBox1.Size = new System.Drawing.Size(123, 20);
                    this.textBox1.TabIndex = 1;
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label1.Location = new System.Drawing.Point(3, 27);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(41, 24);
                    this.label1.TabIndex = 2;
                    this.label1.Text = STranslation.T["Name"];
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label2.Location = new System.Drawing.Point(3, 51);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(136, 24);
                    this.label2.TabIndex = 3;
                    this.label2.Text = STranslation.T["CoordinateX"];
                    // 
                    // textBox2
                    // 
                    this.textBox2.Location = new System.Drawing.Point(Width - 123, 51);
                    this.textBox2.Name = "textBox2";
                    this.textBox2.Size = new System.Drawing.Size(123, 20);
                    this.textBox2.TabIndex = 1;
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label3.Location = new System.Drawing.Point(3, 75);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(136, 24);
                    this.label3.TabIndex = 3;
                    this.label3.Text = STranslation.T["CoordinateY"];
                    // 
                    // textBox3
                    // 
                    this.textBox3.Location = new System.Drawing.Point(Width - 123, 75);
                    this.textBox3.Name = "textBox3";
                    this.textBox3.Size = new System.Drawing.Size(123, 20);
                    this.textBox3.TabIndex = 1;
                    // 
                    // label4
                    // 
                    this.label4.AutoSize = true;
                    this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label4.Location = new System.Drawing.Point(3, 99);
                    this.label4.Name = "label4";
                    this.label4.Size = new System.Drawing.Size(52, 24);
                    this.label4.TabIndex = 3;
                    this.label4.Text = STranslation.T["ODensity"];
                    // 
                    // textBox4
                    // 
                    this.textBox4.Location = new System.Drawing.Point(Width - 123, 99);
                    this.textBox4.Name = "textBox4";
                    this.textBox4.Size = new System.Drawing.Size(123, 20);
                    this.textBox4.TabIndex = 1;
                    // 
                    // label5
                    // 
                    this.label5.AutoSize = true;
                    this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label5.Location = new System.Drawing.Point(3, 123);
                    this.label5.Name = "label5";
                    this.label5.Size = new System.Drawing.Size(66, 24);
                    this.label5.TabIndex = 3;
                    this.label5.Text = STranslation.T["Angle"];
                    // 
                    // textBox5
                    // 
                    this.textBox5.Location = new System.Drawing.Point(Width - 123, 123);
                    this.textBox5.Name = "textBox5";
                    this.textBox5.Size = new System.Drawing.Size(123, 20);
                    this.textBox5.TabIndex = 1;
                    this.Controls.Add(this.label5);
                    this.Controls.Add(this.label4);
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.label1);
                    this.Controls.Add(this.textBox5);
                    this.Controls.Add(this.textBox4);
                    this.Controls.Add(this.textBox3);
                    this.Controls.Add(this.textBox2);
                    this.Controls.Add(this.textBox1);
                    props.Add(new Option(textBox1, Form1.form.SelectedItem.Name, Props.Name));
                    textBox1.Text = Form1.form.SelectedItem.Name;
                    props.Add(new Option(textBox2, Form1.form.SelectedItem.Name, Props.CoordinateX));
                    textBox2.Text = Form1.form.SelectedItem.Coordinates.X.ToString();
                    props.Add(new Option(textBox3, Form1.form.SelectedItem.Name, Props.CoordinateY));
                    textBox3.Text = Form1.form.SelectedItem.Coordinates.Y.ToString();
                    props.Add(new Option(textBox4, Form1.form.SelectedItem.Name, Props.OpticalDensity));
                    textBox4.Text = ((OPolygon) Form1.form.SelectedItem).OpticalDensity.ToString();
                    props.Add(new Option(textBox5, Form1.form.SelectedItem.Name, Props.Angle));
                    textBox5.Text = ((OPolygon)Form1.form.SelectedItem).Anglee.GetInDegrees().ToString();
                }

                #endregion

                #region Mirror

                if (Form1.form.SelectedItem is Mirror)
                {
                    this.textBox1 = new System.Windows.Forms.TextBox();
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.textBox2 = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.textBox3 = new System.Windows.Forms.TextBox();
                    this.label4 = new System.Windows.Forms.Label();
                    this.textBox4 = new System.Windows.Forms.TextBox();
                    this.label5 = new System.Windows.Forms.Label();
                    this.textBox5 = new System.Windows.Forms.TextBox();
                    // 
                    // textBox1
                    // 
                    this.textBox1.Location = new System.Drawing.Point(Width - 123, 27);
                    this.textBox1.Name = "textBox1";
                    this.textBox1.Size = new System.Drawing.Size(123, 20);
                    this.textBox1.TabIndex = 1;
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label1.Location = new System.Drawing.Point(3, 27);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(41, 24);
                    this.label1.TabIndex = 2;
                    this.label1.Text = STranslation.T["Name"];
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label2.Location = new System.Drawing.Point(3, 51);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(136, 24);
                    this.label2.TabIndex = 3;
                    this.label2.Text = STranslation.T["CoordinateX"];
                    // 
                    // textBox2
                    // 
                    this.textBox2.Location = new System.Drawing.Point(Width - 123, 51);
                    this.textBox2.Name = "textBox2";
                    this.textBox2.Size = new System.Drawing.Size(123, 20);
                    this.textBox2.TabIndex = 1;
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label3.Location = new System.Drawing.Point(3, 75);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(136, 24);
                    this.label3.TabIndex = 3;
                    this.label3.Text = STranslation.T["CoordinateY"];
                    // 
                    // textBox3
                    // 
                    this.textBox3.Location = new System.Drawing.Point(Width - 123, 75);
                    this.textBox3.Name = "textBox3";
                    this.textBox3.Size = new System.Drawing.Size(123, 20);
                    this.textBox3.TabIndex = 1;
                    // 
                    // label4
                    // 
                    this.label4.AutoSize = true;
                    this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label4.Location = new System.Drawing.Point(3, 99);
                    this.label4.Name = "label4";
                    this.label4.Size = new System.Drawing.Size(52, 24);
                    this.label4.TabIndex = 3;
                    this.label4.Text = STranslation.T["Angle"];
                    // 
                    // textBox4
                    // 
                    this.textBox4.Location = new System.Drawing.Point(Width - 123, 99);
                    this.textBox4.Name = "textBox4";
                    this.textBox4.Size = new System.Drawing.Size(123, 20);
                    this.textBox4.TabIndex = 1;
                    // 
                    // label5
                    // 
                    this.label5.AutoSize = true;
                    this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label5.Location = new System.Drawing.Point(3, 123);
                    this.label5.Name = "label5";
                    this.label5.Size = new System.Drawing.Size(66, 24);
                    this.label5.TabIndex = 3;
                    this.label5.Text = STranslation.T["Length"];
                    // 
                    // textBox5
                    // 
                    this.textBox5.Location = new System.Drawing.Point(Width - 123, 123);
                    this.textBox5.Name = "textBox5";
                    this.textBox5.Size = new System.Drawing.Size(123, 20);
                    this.textBox5.TabIndex = 1;
                    this.Controls.Add(this.label5);
                    this.Controls.Add(this.label4);
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.label1);
                    this.Controls.Add(this.textBox5);
                    this.Controls.Add(this.textBox4);
                    this.Controls.Add(this.textBox3);
                    this.Controls.Add(this.textBox2);
                    this.Controls.Add(this.textBox1);
                    props.Add(new Option(textBox1, Form1.form.SelectedItem.Name, Props.Name));
                    textBox1.Text = Form1.form.SelectedItem.Name;
                    props.Add(new Option(textBox2, Form1.form.SelectedItem.Name, Props.CoordinateX));
                    textBox2.Text = Form1.form.SelectedItem.Coordinates.X.ToString();
                    props.Add(new Option(textBox3, Form1.form.SelectedItem.Name, Props.CoordinateY));
                    textBox3.Text = Form1.form.SelectedItem.Coordinates.Y.ToString();
                    props.Add(new Option(textBox4, Form1.form.SelectedItem.Name, Props.Angle));
                    textBox4.Text = ((Mirror) Form1.form.SelectedItem).Anglee.GetInDegrees().ToString();
                    props.Add(new Option(textBox5, Form1.form.SelectedItem.Name, Props.Length));
                    textBox5.Text = ((Mirror) Form1.form.SelectedItem).Length.ToString();
                }

                #endregion

                #region BrightPoint
                if (Form1.form.SelectedItem is BrightPoint)
                {
                    this.textBox1 = new System.Windows.Forms.TextBox();
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.textBox2 = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.textBox3 = new System.Windows.Forms.TextBox();
                    // 
                    // textBox1
                    // 
                    this.textBox1.Location = new System.Drawing.Point(Width - 123, 27);
                    this.textBox1.Name = "textBox1";
                    this.textBox1.Size = new System.Drawing.Size(123, 20);
                    this.textBox1.TabIndex = 1;
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label1.Location = new System.Drawing.Point(3, 27);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(41, 24);
                    this.label1.TabIndex = 2;
                    this.label1.Text = STranslation.T["Name"];
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label2.Location = new System.Drawing.Point(3, 51);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(136, 24);
                    this.label2.TabIndex = 3;
                    this.label2.Text = STranslation.T["CoordinateX"];
                    // 
                    // textBox2
                    // 
                    this.textBox2.Location = new System.Drawing.Point(Width-123, 51);
                    this.textBox2.Name = "textBox2";
                    this.textBox2.Size = new System.Drawing.Size(123, 20);
                    this.textBox2.TabIndex = 1;
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label3.Location = new System.Drawing.Point(3, 76);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(136, 24);
                    this.label3.TabIndex = 3;
                    this.label3.Text = STranslation.T["CoordinateY"];
                    // 
                    // textBox3
                    // 
                    this.textBox3.Location = new System.Drawing.Point(Width - 123, 76);
                    this.textBox3.Name = "textBox3";
                    this.textBox3.Size = new System.Drawing.Size(123, 20);
                    this.textBox3.TabIndex = 1;
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.textBox3);
                    this.Controls.Add(this.textBox2);
                    this.Controls.Add(this.label1);
                    this.Controls.Add(this.textBox1);
                    props.Add(new Option(textBox1, Form1.form.SelectedItem.Name, Props.Name));
                    textBox1.Text = Form1.form.SelectedItem.Name;
                    textBox2.Text = Form1.form.SelectedItem.Coordinates.X.ToString();
                    props.Add(new Option(textBox2, Form1.form.SelectedItem.Name, Props.CoordinateX));
                    textBox3.Text = Form1.form.SelectedItem.Coordinates.Y.ToString();
                    props.Add(new Option(textBox3, Form1.form.SelectedItem.Name, Props.CoordinateY));
                }

                #endregion

                #region Sphere
                if (Form1.form.SelectedItem is Sphere)
                {
                    this.textBox1 = new System.Windows.Forms.TextBox();
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.textBox2 = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.textBox3 = new System.Windows.Forms.TextBox();
                    this.label4 = new System.Windows.Forms.Label();
                    this.textBox4 = new System.Windows.Forms.TextBox();
                    this.label5 = new System.Windows.Forms.Label();
                    this.textBox5 = new System.Windows.Forms.TextBox();
                    // 
                    // textBox1
                    // 
                    this.textBox1.Location = new System.Drawing.Point(Width-123, 27);
                    this.textBox1.Name = "textBox1";
                    this.textBox1.Size = new System.Drawing.Size(123, 20);
                    this.textBox1.TabIndex = 1;
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label1.Location = new System.Drawing.Point(3, 27);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(41, 24);
                    this.label1.TabIndex = 2;
                    this.label1.Text = STranslation.T["Name"];
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label2.Location = new System.Drawing.Point(3, 51);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(136, 24);
                    this.label2.TabIndex = 3;
                    this.label2.Text = STranslation.T["CoordinateX"];
                    // 
                    // textBox2
                    // 
                    this.textBox2.Location = new System.Drawing.Point(Width - 123, 51);
                    this.textBox2.Name = "textBox2";
                    this.textBox2.Size = new System.Drawing.Size(123, 20);
                    this.textBox2.TabIndex = 1;
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label3.Location = new System.Drawing.Point(3, 75);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(136, 24);
                    this.label3.TabIndex = 3;
                    this.label3.Text = STranslation.T["CoordinateY"];
                    // 
                    // textBox3
                    // 
                    this.textBox3.Location = new System.Drawing.Point(Width - 123, 75);
                    this.textBox3.Name = "textBox3";
                    this.textBox3.Size = new System.Drawing.Size(123, 20);
                    this.textBox3.TabIndex = 1;
                    // 
                    // label4
                    // 
                    this.label4.AutoSize = true;
                    this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label4.Location = new System.Drawing.Point(3, 99);
                    this.label4.Name = "label4";
                    this.label4.Size = new System.Drawing.Size(163, 24);
                    this.label4.TabIndex = 3;
                    this.label4.Text = STranslation.T["ODensity"];
                    // 
                    // textBox4
                    // 
                    this.textBox4.Location = new System.Drawing.Point(Width - 123, 99);
                    this.textBox4.Name = "textBox4";
                    this.textBox4.Size = new System.Drawing.Size(123, 20);
                    this.textBox4.TabIndex = 1;
                    // 
                    // label5
                    // 
                    this.label5.AutoSize = true;
                    this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte) (204)));
                    this.label5.Location = new System.Drawing.Point(3, 123);
                    this.label5.Name = "label5";
                    this.label5.Size = new System.Drawing.Size(67, 24);
                    this.label5.TabIndex = 3;
                    this.label5.Text = STranslation.T["Radius"];
                    // 
                    // textBox5
                    // 
                    this.textBox5.Location = new System.Drawing.Point(Width - 123, 123);
                    this.textBox5.Name = "textBox5";
                    this.textBox5.Size = new System.Drawing.Size(123, 20);
                    this.textBox5.TabIndex = 1;
                    this.Controls.Add(this.label5);
                    this.Controls.Add(this.label4);
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.label1);
                    this.Controls.Add(this.textBox5);
                    this.Controls.Add(this.textBox4);
                    this.Controls.Add(this.textBox3);
                    this.Controls.Add(this.textBox2);
                    this.Controls.Add(this.textBox1);
                    props.Add(new Option(textBox1, Form1.form.SelectedItem.Name, Props.Name));
                    textBox1.Text = Form1.form.SelectedItem.Name;
                    props.Add(new Option(textBox2, Form1.form.SelectedItem.Name, Props.CoordinateX));
                    textBox2.Text = Form1.form.SelectedItem.Coordinates.X.ToString();
                    props.Add(new Option(textBox3, Form1.form.SelectedItem.Name, Props.CoordinateY));
                    textBox3.Text = Form1.form.SelectedItem.Coordinates.Y.ToString();
                    props.Add(new Option(textBox4, Form1.form.SelectedItem.Name, Props.OpticalDensity));
                    textBox4.Text = ((Sphere)Form1.form.SelectedItem).OpticalDensity.ToString();
                    props.Add(new Option(textBox5, Form1.form.SelectedItem.Name, Props.Radius));
                    textBox5.Text = ((Sphere)Form1.form.SelectedItem).R.ToString();
                }

                #endregion
                
                #region Lense
                if(Form1.form.SelectedItem is Lense)
                {
                    this.textBox1 = new System.Windows.Forms.TextBox();
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.textBox2 = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.textBox3 = new System.Windows.Forms.TextBox();
                    this.label4 = new System.Windows.Forms.Label();
                    this.textBox4 = new System.Windows.Forms.TextBox();
                    this.label5 = new System.Windows.Forms.Label();
                    this.textBox5 = new System.Windows.Forms.TextBox();
                    this.label6 = new System.Windows.Forms.Label();
                    this.textBox6 = new System.Windows.Forms.TextBox();
                    this.label7 = new System.Windows.Forms.Label();
                    this.textBox7 = new System.Windows.Forms.TextBox();
                    // 
                    // textBox1
                    // 
                    this.textBox1.Location = new System.Drawing.Point(Width-123, 27);
                    this.textBox1.Name = "textBox1";
                    this.textBox1.Size = new System.Drawing.Size(123, 20);
                    this.textBox1.TabIndex = 1;
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label1.Location = new System.Drawing.Point(3, 27);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(41, 24);
                    this.label1.TabIndex = 2;
                    this.label1.Text = STranslation.T["Name"];
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label2.Location = new System.Drawing.Point(3, 51);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(136, 24);
                    this.label2.TabIndex = 3;
                    this.label2.Text = STranslation.T["CoordinateX"]; ;
                    // 
                    // textBox2
                    // 
                    this.textBox2.Location = new System.Drawing.Point(Width - 123, 51);
                    this.textBox2.Name = "textBox2";
                    this.textBox2.Size = new System.Drawing.Size(123, 20);
                    this.textBox2.TabIndex = 1;
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label3.Location = new System.Drawing.Point(3, 75);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(136, 24);
                    this.label3.TabIndex = 3;
                    this.label3.Text = STranslation.T["CoordinateY"]; ;
                    // 
                    // textBox3
                    // 
                    this.textBox3.Location = new System.Drawing.Point(Width - 123, 75);
                    this.textBox3.Name = "textBox3";
                    this.textBox3.Size = new System.Drawing.Size(123, 20);
                    this.textBox3.TabIndex = 1;
                    // 
                    // label4
                    // 
                    this.label4.AutoSize = true;
                    this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label4.Location = new System.Drawing.Point(3, 99);
                    this.label4.Name = "label4";
                    this.label4.Size = new System.Drawing.Size(163, 24);
                    this.label4.TabIndex = 3;
                    this.label4.Text = STranslation.T["ODensity"];
                    // 
                    // textBox4
                    // 
                    this.textBox4.Location = new System.Drawing.Point(Width - 123, 99);
                    this.textBox4.Name = "textBox4";
                    this.textBox4.Size = new System.Drawing.Size(123, 20);
                    this.textBox4.TabIndex = 1;
                    // 
                    // label5
                    // 
                    this.label5.AutoSize = true;
                    this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label5.Location = new System.Drawing.Point(3, 123);
                    this.label5.Name = "label5";
                    this.label5.Size = new System.Drawing.Size(82, 24);
                    this.label5.TabIndex = 3;
                    this.label5.Text = STranslation.T["Radius"]+" 1" ;
                    // 
                    // textBox5
                    // 
                    this.textBox5.Location = new System.Drawing.Point(Width - 123, 123);
                    this.textBox5.Name = "textBox5";
                    this.textBox5.Size = new System.Drawing.Size(123, 20);
                    this.textBox5.TabIndex = 1;
                    // 
                    // textBox6
                    // 
                    this.textBox6.Location = new System.Drawing.Point(Width - 123, 149);
                    this.textBox6.Name = "textBox6";
                    this.textBox6.Size = new System.Drawing.Size(123, 20);
                    this.textBox6.TabIndex = 1;
                    // 
                    // label6
                    // 
                    this.label6.AutoSize = true;
                    this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label6.Location = new System.Drawing.Point(3, 149);
                    this.label6.Name = "label6";
                    this.label6.Size = new System.Drawing.Size(82, 24);
                    this.label6.TabIndex = 3;
                    this.label6.Text = STranslation.T["Radius"] + " 2";
                    // 
                    // textBox7
                    // 
                    this.textBox7.Location = new System.Drawing.Point(Width-123, 175);
                    this.textBox7.Name = "textBox7";
                    this.textBox7.Size = new System.Drawing.Size(123, 20);
                    this.textBox7.TabIndex = 1;
                    // 
                    // label7
                    // 
                    this.label7.AutoSize = true;
                    this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label7.Location = new System.Drawing.Point(3, 175);
                    this.label7.Name = "label7";
                    this.label7.Size = new System.Drawing.Size(41, 24);
                    this.label7.TabIndex = 3;
                    this.label7.Text = STranslation.T["Angle"];
                    this.Controls.Add(this.label7);
                    this.Controls.Add(this.label6);
                    this.Controls.Add(this.label5);
                    this.Controls.Add(this.label4);
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.label1);
                    this.Controls.Add(this.textBox7);
                    this.Controls.Add(this.textBox6);
                    this.Controls.Add(this.textBox5);
                    this.Controls.Add(this.textBox4);
                    this.Controls.Add(this.textBox3);
                    this.Controls.Add(this.textBox2);
                    this.Controls.Add(this.textBox1);
                    props.Add(new Option(textBox1, Form1.form.SelectedItem.Name, Props.Name));
                    textBox1.Text = Form1.form.SelectedItem.Name;
                    props.Add(new Option(textBox2, Form1.form.SelectedItem.Name, Props.CoordinateX));
                    textBox2.Text = Form1.form.SelectedItem.Coordinates.X.ToString();
                    props.Add(new Option(textBox3, Form1.form.SelectedItem.Name, Props.CoordinateY));
                    textBox3.Text = Form1.form.SelectedItem.Coordinates.Y.ToString();
                    props.Add(new Option(textBox4, Form1.form.SelectedItem.Name, Props.OpticalDensity));
                    textBox4.Text = ((Lense)Form1.form.SelectedItem).OpticalDensity.ToString();
                    props.Add(new Option(textBox5, Form1.form.SelectedItem.Name, Props.LenseR1));
                    textBox5.Text = ((Lense)Form1.form.SelectedItem).R1.ToString();
                    props.Add(new Option(textBox6, Form1.form.SelectedItem.Name, Props.LenseR2));
                    textBox6.Text = ((Lense)Form1.form.SelectedItem).R2.ToString();
                    props.Add(new Option(textBox7, Form1.form.SelectedItem.Name, Props.Angle));
                    textBox7.Text = ((Lense)Form1.form.SelectedItem).Anglee.GetInDegrees().ToString();
                }
                #endregion

                #region SphereMirror
                if (Form1.form.SelectedItem is SphereMirror)
                {
                    this.textBox1 = new System.Windows.Forms.TextBox();
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.textBox2 = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.textBox3 = new System.Windows.Forms.TextBox();
                    this.label4 = new System.Windows.Forms.Label();
                    this.textBox4 = new System.Windows.Forms.TextBox();
                    this.label5 = new System.Windows.Forms.Label();
                    this.textBox5 = new System.Windows.Forms.TextBox();
                    this.label6 = new System.Windows.Forms.Label();
                    this.textBox6 = new System.Windows.Forms.TextBox();
                    this.label7 = new System.Windows.Forms.Label();
                    this.textBox7 = new System.Windows.Forms.TextBox();
                    // 
                    // textBox1
                    // 
                    this.textBox1.Location = new System.Drawing.Point(Width - 123, 27);
                    this.textBox1.Name = "textBox1";
                    this.textBox1.Size = new System.Drawing.Size(123, 20);
                    this.textBox1.TabIndex = 1;
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label1.Location = new System.Drawing.Point(3, 27);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(41, 24);
                    this.label1.TabIndex = 2;
                    this.label1.Text = STranslation.T["Name"];
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label2.Location = new System.Drawing.Point(3, 51);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(136, 24);
                    this.label2.TabIndex = 3;
                    this.label2.Text = STranslation.T["CoordinateX"]; ;
                    // 
                    // textBox2
                    // 
                    this.textBox2.Location = new System.Drawing.Point(Width - 123, 51);
                    this.textBox2.Name = "textBox2";
                    this.textBox2.Size = new System.Drawing.Size(123, 20);
                    this.textBox2.TabIndex = 1;
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label3.Location = new System.Drawing.Point(3, 75);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(136, 24);
                    this.label3.TabIndex = 3;
                    this.label3.Text = STranslation.T["CoordinateY"]; ;
                    // 
                    // textBox3
                    // 
                    this.textBox3.Location = new System.Drawing.Point(Width - 123, 75);
                    this.textBox3.Name = "textBox3";
                    this.textBox3.Size = new System.Drawing.Size(123, 20);
                    this.textBox3.TabIndex = 1;
                    // 
                    // label4
                    // 
                    this.label4.AutoSize = true;
                    this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label4.Location = new System.Drawing.Point(3, 99);
                    this.label4.Name = "label4";
                    this.label4.Size = new System.Drawing.Size(163, 24);
                    this.label4.TabIndex = 3;
                    this.label4.Text = STranslation.T["Radius"];
                    // 
                    // textBox4
                    // 
                    this.textBox4.Location = new System.Drawing.Point(Width - 123, 99);
                    this.textBox4.Name = "textBox4";
                    this.textBox4.Size = new System.Drawing.Size(123, 20);
                    this.textBox4.TabIndex = 1;
                    // 
                    // label5
                    // 
                    this.label5.AutoSize = true;
                    this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label5.Location = new System.Drawing.Point(3, 123);
                    this.label5.Name = "label5";
                    this.label5.Size = new System.Drawing.Size(82, 24);
                    this.label5.TabIndex = 3;
                    this.label5.Text = STranslation.T["Concave"];
                    // 
                    // textBox5
                    // 
                    this.textBox5.Location = new System.Drawing.Point(Width - 123, 123);
                    this.textBox5.Name = "textBox5";
                    this.textBox5.Size = new System.Drawing.Size(123, 20);
                    this.textBox5.TabIndex = 1;
                    // 
                    // textBox6
                    // 
                    this.textBox6.Location = new System.Drawing.Point(Width - 123, 149);
                    this.textBox6.Name = "textBox6";
                    this.textBox6.Size = new System.Drawing.Size(123, 20);
                    this.textBox6.TabIndex = 1;
                    // 
                    // label6
                    // 
                    this.label6.AutoSize = true;
                    this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label6.Location = new System.Drawing.Point(3, 149);
                    this.label6.Name = "label6";
                    this.label6.Size = new System.Drawing.Size(82, 24);
                    this.label6.TabIndex = 3;
                    this.label6.Text = STranslation.T["BAngle"];
                    // 
                    // textBox7
                    // 
                    this.textBox7.Location = new System.Drawing.Point(Width - 123, 175);
                    this.textBox7.Name = "textBox7";
                    this.textBox7.Size = new System.Drawing.Size(123, 20);
                    this.textBox7.TabIndex = 1;
                    // 
                    // label7
                    // 
                    this.label7.AutoSize = true;
                    this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.label7.Location = new System.Drawing.Point(3, 175);
                    this.label7.Name = "label7";
                    this.label7.Size = new System.Drawing.Size(41, 24);
                    this.label7.TabIndex = 3;
                    this.label7.Text = STranslation.T["EAngle"];
                    this.Controls.Add(this.label7);
                    this.Controls.Add(this.label6);
                    this.Controls.Add(this.label5);
                    this.Controls.Add(this.label4);
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.label1);
                    this.Controls.Add(this.textBox7);
                    this.Controls.Add(this.textBox6);
                    //this.Controls.Add(this.textBox5);
                    this.Controls.Add(this.textBox4);
                    this.Controls.Add(this.textBox3);
                    this.Controls.Add(this.textBox2);
                    this.Controls.Add(this.textBox1);
                    props.Add(new Option(textBox1, Form1.form.SelectedItem.Name, Props.Name));
                    textBox1.Text = Form1.form.SelectedItem.Name;
                    props.Add(new Option(textBox2, Form1.form.SelectedItem.Name, Props.CoordinateX));
                    textBox2.Text = Form1.form.SelectedItem.Coordinates.X.ToString();
                    props.Add(new Option(textBox3, Form1.form.SelectedItem.Name, Props.CoordinateY));
                    textBox3.Text = Form1.form.SelectedItem.Coordinates.Y.ToString();
                    props.Add(new Option(textBox4, Form1.form.SelectedItem.Name, Props.Radius));
                    textBox4.Text = ((SphereMirror)Form1.form.SelectedItem).R.ToString();
                    ComboBox cb = new ComboBox();
                    cb.FormattingEnabled = true;
                    cb.Location = new System.Drawing.Point(Width - 123, 123);
                    cb.Name = "comboBox2";
                    cb.Size = new System.Drawing.Size(123, 20);
                    cb.TabIndex = 0;
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                    cb.Items.Add(STranslation.T["Yes"]);
                    cb.Items.Add(STranslation.T["No"]);
                    cb.Text = ((SphereMirror) (Form1.form.SelectedItem)).IsVogn
                                  ? STranslation.T["Yes"]
                                  : STranslation.T["No"];
                    props.Add(new Option(cb, Form1.form.SelectedItem.Name,Props.IsConcave));
                    this.Controls.Add(cb);
                    props.Add(new Option(textBox6, Form1.form.SelectedItem.Name, Props.UpperAngle));
                    textBox6.Text = ((SphereMirror)Form1.form.SelectedItem).UpperAngle.ToString();
                    props.Add(new Option(textBox7, Form1.form.SelectedItem.Name, Props.LowerAngle));
                    textBox7.Text = ((SphereMirror)Form1.form.SelectedItem).LowerAngle.ToString();
                }
                #endregion

                foreach(Control a in Controls)
                {
                    if (a is Label)
                        a.BackColor = Color.Transparent;
                }
            }
            catch
            {
                
            }
        }
        private void PropertiesList_SizeChanged(object sender, EventArgs e)
        {
            comboBox1.Width = this.Width;
        }
        public void ReloadList(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            SootvStrok = new Dictionary<string, string>();
            SootvStrokRays = new Dictionary<string, string>();
            SootvStrokB = new Dictionary<string, string>();
            SootvStrokRaysB = new Dictionary<string, string>();
            string addtext;
            foreach (var i in ObjectCollection.Instance.rays)
            {
                addtext = i.Name + " - " + STranslation.T[i.GetTypeSpecifier()];
                comboBox1.Items.Add(addtext);
                SootvStrokRays[i.Name] = addtext;
                SootvStrokRaysB[addtext] = i.Name;
            }
            foreach (var i in ObjectCollection.Instance.objects)
            {
                addtext = i.Name + " - "  + STranslation.T[i.GetTypeSpecifier()];
                comboBox1.Items.Add(addtext);
                SootvStrok[i.Name] = addtext;
                SootvStrokB[addtext] = i.Name;
            }
            if (Form1.form.SelectedItem != null)
            {
                raise = false;
                if (Form1.form.SelectedItem is Ray)
                {
                    string outq;
                    if (SootvStrokRays.TryGetValue(Form1.form.SelectedItem.Name, out outq))
                        comboBox1.SelectedItem = outq;
                    else
                        comboBox1.SelectedItem = null;
                }
                else
                {
                    string outq;
                    if (SootvStrok.TryGetValue(Form1.form.SelectedItem.Name, out outq))
                        comboBox1.SelectedItem = outq;
                    else
                        comboBox1.SelectedItem = null;
                }
            }
        }
    }
}
