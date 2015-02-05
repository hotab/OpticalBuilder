using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.OpticalObjects;

namespace OpticalBuilder
{
    public class Option
    {
        public bool IsDouble;
        public bool IsCombo;
        private string param;
        public string Param
        {
            get
            {
                if (IsDouble)
                    return param.Replace(',', '.');
                else return param;
            }
            set
            {
                if(IsDouble)
                {
                    string conv = value.Replace('.', ',');
                    try
                    {
                        Convert.ToDouble(conv);
                    }
                    catch
                    {
                        MessageBox.Show("Wrong format", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    param = value;
                }
                else
                {
                    if (value.Contains("^"))
                        MessageBox.Show("Character '^' is forbidden", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else param = value;
                }
            }
        }
        private TextBox boundTextBox;
        private ComboBox boundComboBox;
        private ObjectProto boundObj;
        private Props bind_p;
        private string prevtext;
        public Option(ComboBox bind, string  objBind, Props bind_property)
        {
            IsCombo = true;
            boundComboBox = bind;
            boundObj = ObjectCollection.Instance.GetObjectByName(objBind);
            boundComboBox.SelectedIndexChanged += new EventHandler(boundComboBox_SelectedIndexChanged);
            boundObj.ObjectChanged += new EventHandler<EventArgs>(boundObj_ObjectChanged);
            bind_p = bind_property;
            prevtext = "";
        }

        void boundComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(bind_p == Props.IsConcave)
            {
                if (boundComboBox.Text == STranslation.T["Yes"])
                    ((SphereMirror) boundObj).IsVogn = true;
                else 
                    ((SphereMirror) boundObj).IsVogn = false;
            }
            ObjectCollection.Instance.RaiseObjectsChange();
        }
        public Option(TextBox bind, string objBind, Props bind_property,bool Ray = false)
        {
            if (bind_property != Props.Name)
            {
                IsDouble = true;
            }
            else IsDouble = false;
            if (IsDouble)
                param = "0,000000";
            else 
                param = "";
            boundTextBox = bind;
            if(Ray)
            {
                boundObj = ObjectCollection.Instance.GetRayByName(objBind);
            }
            else
            {
                boundObj = ObjectCollection.Instance.GetObjectByName(objBind);
            }
            bind_p = bind_property;
            boundTextBox.Leave += new EventHandler(boundTextBox_Leave);
            boundTextBox.KeyUp += new KeyEventHandler(boundTextBox_KeyUp);
            boundObj.ObjectChanged += new EventHandler<EventArgs>(boundObj_ObjectChanged);
            prevtext = "";
        }

        void boundObj_ObjectChanged(object sender, EventArgs e)
        {
            if(bind_p == Props.IsConcave)
            {
                if (((SphereMirror)boundObj).IsVogn) boundComboBox.Text = STranslation.T["Yes"];
                else boundComboBox.Text = STranslation.T["No"];
                ObjectCollection.Instance.RaiseObjectsChange();
                return;
            }
            if (bind_p == Props.Name) boundTextBox.Text = boundObj.Name;
            if (bind_p == Props.CoordinateX) boundTextBox.Text = boundObj.Coordinates.X.ToString();
            if (bind_p == Props.CoordinateY) boundTextBox.Text = boundObj.Coordinates.Y.ToString();
            if( bind_p == Props.Angle)
            {
                if (boundObj is Mirror)
                {
                    boundTextBox.Text = ((Mirror) boundObj).Anglee.GetInDegrees().ToString();
                }
                if (boundObj is Ray)
                {
                    boundTextBox.Text = ((Ray)boundObj).Angle.GetInDegrees().ToString();
                }
                if (boundObj is OPolygon)
                {
                    boundTextBox.Text = ((OPolygon)boundObj).Anglee.GetInDegrees().ToString();
                }
                if (boundObj is Lense)
                {
                    boundTextBox.Text = ((Lense)boundObj).Anglee.GetInDegrees().ToString();
                } 
            }
            if(bind_p == Props.OpticalDensity)
            {
                boundTextBox.Text = ((IPreloml) boundObj).OpticalDensity.ToString();
            }
            if(bind_p == Props.Radius)
            {
                if(boundObj is Sphere)
                boundTextBox.Text = ((Sphere)boundObj).R.ToString();
                if(boundObj is SphereMirror)
                boundTextBox.Text = ((SphereMirror)boundObj).R.ToString();
            }
            if(bind_p == Props.Length)
            {
                if (boundObj is Mirror)
                {
                    boundTextBox.Text = ((Mirror)boundObj).Length.ToString();
                }
            }
            if(bind_p == Props.UpperAngle)
            {
                if (boundObj is SphereMirror)
                {
                    boundTextBox.Text = ((SphereMirror)boundObj).UpperAngle.ToString();
                }
            }
            if (bind_p == Props.LowerAngle)
            {
                if (boundObj is SphereMirror)
                {
                    boundTextBox.Text = ((SphereMirror)boundObj).LowerAngle.ToString();
                }
            }
        }

        void boundTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                Form1.form.simpleGL1.Focus();
            }
        }

        void boundTextBox_Leave(object sender, EventArgs e)
        {
            if(IsDouble)
            {
                string txt = boundTextBox.Text.Replace('.', ',');
                try
                {
                    Convert.ToDouble(txt);
                }
                catch
                {
                    MessageBox.Show(STranslation.T["FormatError"], " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if(bind_p == Props.CoordinateX)
                    {
                        boundTextBox.Text = boundObj.Coordinates.X.ToString();
                    }
                    if (bind_p == Props.CoordinateY)
                    {
                        boundTextBox.Text = boundObj.Coordinates.Y.ToString();
                    }
                    if (bind_p == Props.Angle && boundObj is Ray)
                    {
                        boundTextBox.Text = ((Ray)boundObj).Angle.GetInDegrees().ToString();
                    }
                    if(bind_p == Props.Angle && boundObj is Mirror)
                    {
                        boundTextBox.Text = ((Mirror)boundObj).Anglee.GetInDegrees().ToString();
                    }
                    if (bind_p == Props.Angle && boundObj is Lense)
                    {
                        boundTextBox.Text = ((Lense)boundObj).Anglee.GetInDegrees().ToString();
                    }
                    if (bind_p == Props.Angle && boundObj is OPolygon)
                    {
                        boundTextBox.Text = ((OPolygon)boundObj).Anglee.GetInDegrees().ToString();
                    }
                    if(bind_p == Props.OpticalDensity)
                    {
                        boundTextBox.Text = ((IPreloml)boundObj).OpticalDensity.ToString();
                    }
                    if(bind_p == Props.Radius && boundObj is Sphere)
                    {
                        boundTextBox.Text = ((Sphere)boundObj).R.ToString();
                    }
                    if (bind_p == Props.Radius && boundObj is SphereMirror)
                    {
                        boundTextBox.Text = ((SphereMirror)boundObj).R.ToString();
                    }
                    if (bind_p == Props.LenseR1)
                    {
                        boundTextBox.Text = ((Lense)boundObj).R1.ToString();
                    }
                    if (bind_p == Props.LenseR2)
                    {
                        boundTextBox.Text = ((Lense)boundObj).R2.ToString();
                    }
                    if(bind_p == Props.Length && boundObj is Mirror)
                    {
                        boundTextBox.Text = ((Mirror) boundObj).Length.ToString();
                    }
                    if (bind_p == Props.UpperAngle)
                    {
                        if (boundObj is SphereMirror)
                        {
                            boundTextBox.Text = ((SphereMirror)boundObj).UpperAngle.ToString();
                        }
                    }
                    return;
                }
                
                double prp = Convert.ToDouble(txt);
                if(bind_p == Props.CoordinateY)
                {
                    boundObj.Coordinates = new SystemCoordinates(boundObj.Coordinates.X, prp);
                    boundTextBox.Text = prp.ToString();
                }
                if (bind_p == Props.CoordinateX)
                {
                    boundObj.Coordinates = new SystemCoordinates(prp, boundObj.Coordinates.Y);
                    boundTextBox.Text = prp.ToString();
                }
                if(bind_p == Props.OpticalDensity && boundObj is IPreloml)
                {
                    ((IPreloml) boundObj).OpticalDensity = prp;
                    boundTextBox.Text = prp.ToString();
                }
                if(bind_p == Props.Radius && boundObj is Sphere)
                {
                    if (prp <= 0)
                    {
                        MessageBox.Show(STranslation.T["RadiusError"], " ", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                        boundTextBox.Text = ((Sphere)boundObj).R.ToString();
                        return;
                    }
                    ((Sphere) boundObj).R = prp;
                    boundTextBox.Text = prp.ToString();
                }
                if (bind_p == Props.Radius && boundObj is SphereMirror)
                {
                    if (prp <= 0)
                    {
                        MessageBox.Show(STranslation.T["RadiusError"], " ", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                        boundTextBox.Text = ((SphereMirror)boundObj).R.ToString();
                        return;
                    }
                    ((SphereMirror)boundObj).R = prp;
                    boundTextBox.Text = prp.ToString();
                }
                if(bind_p == Props.Angle && boundObj is Ray)
                {
                    ((Ray)boundObj).Angle = new Angle(prp, false);
                    boundTextBox.Text = ((Ray) boundObj).Angle.GetInDegrees().ToString();
                }
                if(bind_p == Props.Angle && boundObj is Mirror)
                {

                    ((Mirror)boundObj).Anglee = new Angle(prp, false);
                    boundTextBox.Text = ((Mirror)boundObj).Anglee.GetInDegrees().ToString();
                }
                if (bind_p == Props.Angle && boundObj is Lense)
                {
                    ((Lense)boundObj).Anglee = new Angle(prp, false);
                    boundTextBox.Text = ((Lense)boundObj).Anglee.GetInDegrees().ToString();
                }
                if (bind_p == Props.Angle && boundObj is OPolygon)
                {
                    ((OPolygon)boundObj).Anglee = new Angle(prp, false);
                    boundTextBox.Text = ((OPolygon)boundObj).Anglee.GetInDegrees().ToString();
                }
                if(bind_p == Props.Length && boundObj is Mirror)
                {
                    ((Mirror) boundObj).Length = prp;
                    boundTextBox.Text = ((Mirror)boundObj).Length.ToString();
                }
                if(bind_p == Props.LenseR1)
                {
                    ((Lense)boundObj).R1 = prp;
                    boundTextBox.Text = ((Lense)boundObj).R1.ToString();
                }
                if (bind_p == Props.LenseR2)
                {
                    ((Lense)boundObj).R2 = prp;
                    boundTextBox.Text = ((Lense)boundObj).R2.ToString();
                }
                if (bind_p == Props.UpperAngle)
                {
                    ((SphereMirror)boundObj).UpperAngle = prp;
                    boundTextBox.Text = ((SphereMirror) boundObj).UpperAngle.ToString();
                }
                if (bind_p == Props.LowerAngle)
                {
                    ((SphereMirror)boundObj).LowerAngle = prp;
                    boundTextBox.Text = ((SphereMirror)boundObj).LowerAngle.ToString();
                }
                ObjectCollection.Instance.RaiseObjectsChange();
                prevtext = txt.Replace(',','.');
                if(bind_p != Props.Angle) boundTextBox.Text = prevtext;
            }
            else
            {
                if(bind_p == Props.Name && boundTextBox.Text.Contains("^"))
                {
                    MessageBox.Show(STranslation.T["ForbiddenChar"], " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    boundTextBox.Text = boundObj.Name;
                }
                else
                {
                    prevtext = boundTextBox.Text;
                    boundObj.Name = boundTextBox.Text;
                }
            }
            //Form1.form.simpleGL1.Focus();
        }
        public void UnHook()
        {
            if(IsCombo)
            {
                boundComboBox.SelectedIndexChanged -= boundComboBox_SelectedIndexChanged;
                boundObj.ObjectChanged -= boundObj_ObjectChanged;
            }
            else
            {
                boundTextBox.KeyUp -= boundTextBox_KeyUp;
                boundTextBox.Leave -= boundTextBox_Leave;
                boundObj.ObjectChanged -= boundObj_ObjectChanged;
            }
        }
    }
    public enum Props
    {
        OpticalDensity,
        Angle,
        CoordinateX,
        CoordinateY,
        Length,
        LenseR1,
        LenseR2,
        Radius,
        Name,
        IsConcave,
        UpperAngle,
        LowerAngle,
    }
}
