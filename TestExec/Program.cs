using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
 
using System.Windows.Forms;

namespace OpticalBuilder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            Process cur = Process.GetCurrentProcess();
            Process[] a = Process.GetProcessesByName(cur.ProcessName);
            if (a.Length > 1)
            {
                MessageBox.Show("Program already running");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length != 0)
                Application.Run(new Form1(args));
            else
                Application.Run(new Form1());
        }
    }
}
