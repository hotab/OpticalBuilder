using Tao.Platform.Windows;

namespace OpticalBuilder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.f1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.simpleGL1 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.separator6 = new OpticalBuilder.Separator();
            this.separator5 = new OpticalBuilder.Separator();
            this.groupBox1 = new OpticalBuilder.PropertiesList();
            this.threePosBtn8 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn10 = new OpticalBuilder.ThreePosBtn();
            this.separator4 = new OpticalBuilder.Separator();
            this.threePosBtn6 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn5 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn4 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn7 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn3 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn2 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn1 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn11 = new OpticalBuilder.ThreePosBtn();
            this.threePosBtn9 = new OpticalBuilder.ThreePosBtn();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.f1ToolStripMenuItem,
            this.f2ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(87, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // f1ToolStripMenuItem
            // 
            this.f1ToolStripMenuItem.Name = "f1ToolStripMenuItem";
            this.f1ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.f1ToolStripMenuItem.Text = "F1";
            // 
            // f2ToolStripMenuItem
            // 
            this.f2ToolStripMenuItem.Name = "f2ToolStripMenuItem";
            this.f2ToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.f2ToolStripMenuItem.Text = "F2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.saveAsToolStripMenuItem.Text = "SaveAs";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(108, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(108, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pastleToolStripMenuItem,
            this.configToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pastleToolStripMenuItem
            // 
            this.pastleToolStripMenuItem.Name = "pastleToolStripMenuItem";
            this.pastleToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.pastleToolStripMenuItem.Text = "Pastle";
            this.pastleToolStripMenuItem.Click += new System.EventHandler(this.pastleToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.configToolStripMenuItem.Text = "Config";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(79, 22);
            this.sToolStripMenuItem.Text = "s";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // simpleGL1
            // 
            this.simpleGL1.AccumBits = ((byte)(0));
            this.simpleGL1.AutoCheckErrors = false;
            this.simpleGL1.AutoFinish = false;
            this.simpleGL1.AutoMakeCurrent = true;
            this.simpleGL1.AutoSwapBuffers = true;
            this.simpleGL1.BackColor = System.Drawing.Color.Black;
            this.simpleGL1.ColorBits = ((byte)(32));
            this.simpleGL1.ContextMenuStrip = this.contextMenuStrip1;
            this.simpleGL1.DepthBits = ((byte)(16));
            this.simpleGL1.Location = new System.Drawing.Point(12, 98);
            this.simpleGL1.Name = "simpleGL1";
            this.simpleGL1.Size = new System.Drawing.Size(317, 189);
            this.simpleGL1.StencilBits = ((byte)(0));
            this.simpleGL1.TabIndex = 10;
            this.simpleGL1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.simpleGL1_MouseDown);
            this.simpleGL1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.simpleGL1_MouseMove);
            this.simpleGL1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.simpleGL1_MouseUp);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // separator6
            // 
            this.separator6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("separator6.BackgroundImage")));
            this.separator6.Location = new System.Drawing.Point(83, 27);
            this.separator6.Name = "separator6";
            this.separator6.Size = new System.Drawing.Size(3, 65);
            this.separator6.TabIndex = 22;
            // 
            // separator5
            // 
            this.separator5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("separator5.BackgroundImage")));
            this.separator5.Location = new System.Drawing.Point(234, 27);
            this.separator5.Name = "separator5";
            this.separator5.Size = new System.Drawing.Size(3, 65);
            this.separator5.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Location = new System.Drawing.Point(414, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 181);
            this.groupBox1.TabIndex = 1;
            // 
            // threePosBtn8
            // 
            this.threePosBtn8.BackColor = System.Drawing.Color.White;
            this.threePosBtn8.Location = new System.Drawing.Point(456, 27);
            this.threePosBtn8.Name = "threePosBtn8";
            this.threePosBtn8.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn8.TabIndex = 21;
            this.threePosBtn8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn10
            // 
            this.threePosBtn10.BackColor = System.Drawing.Color.White;
            this.threePosBtn10.Location = new System.Drawing.Point(527, 27);
            this.threePosBtn10.Name = "threePosBtn10";
            this.threePosBtn10.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn10.TabIndex = 21;
            this.threePosBtn10.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // separator4
            // 
            this.separator4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("separator4.BackgroundImage")));
            this.separator4.Location = new System.Drawing.Point(669, 27);
            this.separator4.Name = "separator4";
            this.separator4.Size = new System.Drawing.Size(3, 65);
            this.separator4.TabIndex = 22;
            // 
            // threePosBtn6
            // 
            this.threePosBtn6.BackColor = System.Drawing.Color.White;
            this.threePosBtn6.Location = new System.Drawing.Point(385, 27);
            this.threePosBtn6.Name = "threePosBtn6";
            this.threePosBtn6.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn6.TabIndex = 19;
            this.threePosBtn6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn5
            // 
            this.threePosBtn5.BackColor = System.Drawing.Color.White;
            this.threePosBtn5.Location = new System.Drawing.Point(314, 27);
            this.threePosBtn5.Name = "threePosBtn5";
            this.threePosBtn5.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn5.TabIndex = 18;
            this.threePosBtn5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn4
            // 
            this.threePosBtn4.BackColor = System.Drawing.Color.White;
            this.threePosBtn4.Location = new System.Drawing.Point(163, 27);
            this.threePosBtn4.Name = "threePosBtn4";
            this.threePosBtn4.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn4.TabIndex = 17;
            this.threePosBtn4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn7
            // 
            this.threePosBtn7.BackColor = System.Drawing.Color.White;
            this.threePosBtn7.Location = new System.Drawing.Point(678, 27);
            this.threePosBtn7.Name = "threePosBtn7";
            this.threePosBtn7.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn7.TabIndex = 20;
            this.threePosBtn7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn3
            // 
            this.threePosBtn3.BackColor = System.Drawing.Color.White;
            this.threePosBtn3.Location = new System.Drawing.Point(243, 27);
            this.threePosBtn3.Name = "threePosBtn3";
            this.threePosBtn3.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn3.TabIndex = 16;
            this.threePosBtn3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn2
            // 
            this.threePosBtn2.BackColor = System.Drawing.Color.White;
            this.threePosBtn2.Location = new System.Drawing.Point(92, 27);
            this.threePosBtn2.Name = "threePosBtn2";
            this.threePosBtn2.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn2.TabIndex = 15;
            this.threePosBtn2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn1
            // 
            this.threePosBtn1.BackColor = System.Drawing.Color.White;
            this.threePosBtn1.Location = new System.Drawing.Point(12, 27);
            this.threePosBtn1.Name = "threePosBtn1";
            this.threePosBtn1.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn1.TabIndex = 14;
            this.threePosBtn1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn11
            // 
            this.threePosBtn11.BackColor = System.Drawing.Color.White;
            this.threePosBtn11.Location = new System.Drawing.Point(598, 27);
            this.threePosBtn11.Name = "threePosBtn11";
            this.threePosBtn11.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn11.TabIndex = 20;
            this.threePosBtn11.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // threePosBtn9
            // 
            this.threePosBtn9.BackColor = System.Drawing.Color.White;
            this.threePosBtn9.Location = new System.Drawing.Point(749, 27);
            this.threePosBtn9.Name = "threePosBtn9";
            this.threePosBtn9.Size = new System.Drawing.Size(65, 65);
            this.threePosBtn9.TabIndex = 20;
            this.threePosBtn9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SelectEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(823, 298);
            this.Controls.Add(this.separator6);
            this.Controls.Add(this.separator5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.threePosBtn8);
            this.Controls.Add(this.threePosBtn10);
            this.Controls.Add(this.threePosBtn6);
            this.Controls.Add(this.threePosBtn5);
            this.Controls.Add(this.threePosBtn4);
            this.Controls.Add(this.separator4);
            this.Controls.Add(this.threePosBtn3);
            this.Controls.Add(this.threePosBtn2);
            this.Controls.Add(this.threePosBtn1);
            this.Controls.Add(this.threePosBtn7);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.simpleGL1);
            this.Controls.Add(this.threePosBtn11);
            this.Controls.Add(this.threePosBtn9);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem f1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem f2ToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastleToolStripMenuItem;
        public SimpleOpenGlControl simpleGL1;
        private ThreePosBtn threePosBtn1;
        private ThreePosBtn threePosBtn2;
        private ThreePosBtn threePosBtn3;
        private ThreePosBtn threePosBtn4;
        private ThreePosBtn threePosBtn5;
        private ThreePosBtn threePosBtn6;
        private ThreePosBtn threePosBtn7;
        private ThreePosBtn threePosBtn8;
        private Separator separator1;
        private Separator separator2;
        private Separator separator3;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private ThreePosBtn threePosBtn9;
        private Separator separator4;
        private Separator separator5;
        private Separator separator6;
        private ThreePosBtn threePosBtn10;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        public PropertiesList groupBox1;
        private ThreePosBtn threePosBtn11;
    }
}

