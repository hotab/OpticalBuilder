﻿namespace OpticalBuilder
{
    partial class ThreePosBtn
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ThreePosBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "ThreePosBtn";
            this.Size = new System.Drawing.Size(65, 65);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ThreePosBtn_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ThreePosBtn_MouseDown);
            this.MouseEnter += new System.EventHandler(this.ThreePosBtn_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ThreePosBtn_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ThreePosBtn_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
