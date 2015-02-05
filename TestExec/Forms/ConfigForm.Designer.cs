namespace OpticalBuilder.Forms
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.groupLangAdd = new System.Windows.Forms.GroupBox();
            this.btnAddLang = new System.Windows.Forms.Button();
            this.btnLookUp = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.textReflLimit = new System.Windows.Forms.TextBox();
            this.labelReflLimit = new System.Windows.Forms.Label();
            this.labelAutoSave = new System.Windows.Forms.Label();
            this.comboAutoSave = new System.Windows.Forms.ComboBox();
            this.labelDensity = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupLangAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnAccept.Location = new System.Drawing.Point(199, 233);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "btnAccept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnDecline.Location = new System.Drawing.Point(280, 233);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 23);
            this.btnDecline.TabIndex = 2;
            this.btnDecline.Text = "btnDecline";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // groupLangAdd
            // 
            this.groupLangAdd.Controls.Add(this.btnAddLang);
            this.groupLangAdd.Controls.Add(this.btnLookUp);
            this.groupLangAdd.Controls.Add(this.textPath);
            this.groupLangAdd.Location = new System.Drawing.Point(12, 127);
            this.groupLangAdd.Name = "groupLangAdd";
            this.groupLangAdd.Size = new System.Drawing.Size(343, 100);
            this.groupLangAdd.TabIndex = 3;
            this.groupLangAdd.TabStop = false;
            this.groupLangAdd.Text = "groupLangAdd";
            // 
            // btnAddLang
            // 
            this.btnAddLang.Location = new System.Drawing.Point(262, 48);
            this.btnAddLang.Name = "btnAddLang";
            this.btnAddLang.Size = new System.Drawing.Size(75, 23);
            this.btnAddLang.TabIndex = 2;
            this.btnAddLang.Text = "btnAddLang";
            this.btnAddLang.UseVisualStyleBackColor = true;
            this.btnAddLang.Click += new System.EventHandler(this.btnAddLang_Click);
            // 
            // btnLookUp
            // 
            this.btnLookUp.Location = new System.Drawing.Point(262, 19);
            this.btnLookUp.Name = "btnLookUp";
            this.btnLookUp.Size = new System.Drawing.Size(75, 23);
            this.btnLookUp.TabIndex = 1;
            this.btnLookUp.Text = "btnLookUp";
            this.btnLookUp.UseVisualStyleBackColor = true;
            this.btnLookUp.Click += new System.EventHandler(this.btnLookUp_Click);
            // 
            // textPath
            // 
            this.textPath.Enabled = false;
            this.textPath.Location = new System.Drawing.Point(6, 22);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(250, 20);
            this.textPath.TabIndex = 0;
            // 
            // textReflLimit
            // 
            this.textReflLimit.Location = new System.Drawing.Point(225, 11);
            this.textReflLimit.Name = "textReflLimit";
            this.textReflLimit.Size = new System.Drawing.Size(130, 20);
            this.textReflLimit.TabIndex = 4;
            // 
            // labelReflLimit
            // 
            this.labelReflLimit.AutoSize = true;
            this.labelReflLimit.Location = new System.Drawing.Point(15, 14);
            this.labelReflLimit.Name = "labelReflLimit";
            this.labelReflLimit.Size = new System.Drawing.Size(35, 13);
            this.labelReflLimit.TabIndex = 5;
            this.labelReflLimit.Text = "label1";
            this.labelReflLimit.Click += new System.EventHandler(this.labelReflLimit_Click);
            // 
            // labelAutoSave
            // 
            this.labelAutoSave.AutoSize = true;
            this.labelAutoSave.Location = new System.Drawing.Point(15, 36);
            this.labelAutoSave.Name = "labelAutoSave";
            this.labelAutoSave.Size = new System.Drawing.Size(35, 13);
            this.labelAutoSave.TabIndex = 6;
            this.labelAutoSave.Text = "label1";
            // 
            // comboAutoSave
            // 
            this.comboAutoSave.FormattingEnabled = true;
            this.comboAutoSave.Location = new System.Drawing.Point(225, 33);
            this.comboAutoSave.Name = "comboAutoSave";
            this.comboAutoSave.Size = new System.Drawing.Size(130, 21);
            this.comboAutoSave.TabIndex = 7;
            // 
            // labelDensity
            // 
            this.labelDensity.AutoSize = true;
            this.labelDensity.Location = new System.Drawing.Point(15, 59);
            this.labelDensity.Name = "labelDensity";
            this.labelDensity.Size = new System.Drawing.Size(35, 13);
            this.labelDensity.TabIndex = 5;
            this.labelDensity.Text = "label1";
            this.labelDensity.Click += new System.EventHandler(this.labelReflLimit_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(225, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 20);
            this.textBox1.TabIndex = 4;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 268);
            this.Controls.Add(this.comboAutoSave);
            this.Controls.Add(this.labelAutoSave);
            this.Controls.Add(this.labelDensity);
            this.Controls.Add(this.labelReflLimit);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textReflLimit);
            this.Controls.Add(this.groupLangAdd);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.groupLangAdd.ResumeLayout(false);
            this.groupLangAdd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.GroupBox groupLangAdd;
        private System.Windows.Forms.Button btnAddLang;
        private System.Windows.Forms.Button btnLookUp;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.TextBox textReflLimit;
        private System.Windows.Forms.Label labelReflLimit;
        private System.Windows.Forms.Label labelAutoSave;
        private System.Windows.Forms.ComboBox comboAutoSave;
        private System.Windows.Forms.Label labelDensity;
        private System.Windows.Forms.TextBox textBox1;
    }
}