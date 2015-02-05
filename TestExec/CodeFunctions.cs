using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib;
using OpticalBuilderLib.OpticalObjects;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.EventArguments;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilder
{
    static class CodeFunctions
    {
        public static void AddMirror(SystemCoordinates c, Angle a, DoubleExtention Length)
        {
            string s1 = ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.Mirror));
                ObjectCollection.Instance.AddObject(new Mirror(s1, c, a, Length));
        }
        public static void AddBrightPoint(SystemCoordinates c)
        {
            string s1 = ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.BrightPoint));
                ObjectCollection.Instance.AddObject(new BrightPoint(s1, c));
        }
        public static void AddSphere(SystemCoordinates c, DoubleExtention l)
        {
            string name = ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.Sphere));
            ObjectCollection.Instance.AddObject(new Sphere(name, c, l, 1.2));
        }
        public static void AddPoly(SystemCoordinates[] x)
        {
            string name = ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.Polygon));
            ObjectCollection.Instance.AddObject(new OPolygon(new Polygon(x), 1.2, name));
        }
        public static void Init()
        {
            OpticalBuilderLib.SaveFiles.SaveLoad.Instance.ToString();
        }
        public static DialogResult InputBox(string promptText, ref string value, string title = " ")
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = STranslation.T["Cancel"];
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        public static void CreateRay(Point p1, Point p2, BrightPoint bpoint, bool GenNew)
        {
            string s1 = ObjectProto.GenName(ObjectProto.GetSpec(ObjectTypes.Ray), true);
            ObjectCollection.Instance.AddRay(s1, p1, p2, bpoint, GenNew);
        }
    }
}
