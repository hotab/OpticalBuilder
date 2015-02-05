using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
 
using System.Text;
using System.Windows.Forms;

namespace OpticalBuilder
{
    public partial class ThreePosBtn : UserControl
    {
        private Image NotPressed;
        private Image Pressed;
        private Image Over;
        private Image PressedOver;
        public bool pressed;
        private bool MouseOver;
        public ThreePosBtn()
        {
            pressed = false;
            MouseOver = false;
            InitializeComponent();
        }
        public void Activate()
        {
            pressed = true;
            if (MouseOver) BackgroundImage = PressedOver;
            else BackgroundImage = Pressed;
        }
        public void Deactivate()
        {
            pressed = false;
            if (MouseOver) BackgroundImage = Over;
            else BackgroundImage = NotPressed;
        }
        private void ThreePosBtn_MouseEnter(object sender, EventArgs e)
        {
            if (pressed)
            {
                this.BackgroundImage = PressedOver;
            }
            else
            {
                this.BackgroundImage = Over;
            }
            MouseOver = true;
        }
        /// <summary>
        /// NotDown, Down, Over, DownOver
        /// </summary>
        /// <param name="path1">NotDown</param>
        /// <param name="path2">Down</param>
        /// <param name="path3">Over</param>
        /// <param name="path4">DownOver</param>
        public void LoadImages(string path1, string path2, string path3, string path4)
        {
            NotPressed = Image.FromFile(path1);
            Pressed = Image.FromFile(path2);
            Over = Image.FromFile(path3);
            PressedOver = Image.FromFile(path4);
            BackgroundImage = NotPressed;
        }

        private void ThreePosBtn_MouseClick(object sender, MouseEventArgs e)
        {
            pressed = !pressed;
            if (pressed)
            {
                BackgroundImage = PressedOver;
            }
            else
            {
                BackgroundImage = Over;
            }
        }

        private void ThreePosBtn_MouseLeave(object sender, EventArgs e)
        {
            if (!pressed)
            {
                this.BackgroundImage = NotPressed;
            }
            else
            {
                this.BackgroundImage = Pressed;
            }
            MouseOver = false;
        }

        private void ThreePosBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if(pressed)
                BackgroundImage = PressedOver;
            else
                BackgroundImage = Over;
        }

        private void ThreePosBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if(pressed)
            {
                BackgroundImage = PressedOver;
            }
            else
            {
                BackgroundImage = Over;
            }
        }
    }
}
