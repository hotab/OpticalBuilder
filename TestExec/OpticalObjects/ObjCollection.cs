using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpticalBuilder;
using OpticalBuilderLib.MathLib;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.EventArguments;
using OpticalBuilderLib.TypeExtentions;
using OpticalBuilderLib.Forms;
using Tao.Platform.Windows;

namespace OpticalBuilderLib.OpticalObjects
{
    public class ObjectCollection
    {
        #region Events
        public event EventHandler<SelectedChangeArgs> OnSelectedChange;
        public event EventHandler<EventArgs> OnObjectsRaysChange;
        #endregion
        #region Variables
        private bool one_obj = false;
        private bool SubscribedToSaveLoadEvents;
        private static bool created = false;
        private static ObjectCollection instance;
        private SimpleOpenGlControl whereToDraw;
        private static CoordinateSystem sysInstance;
        public static CoordinateSystem SysInstance
        {
            get
            {
                return sysInstance;
            }
        }
        public static ObjectCollection Instance
        {
            get
            {
                return GetInstance();
            }
        }
        public SimpleOpenGlControl WhereToDraw
        {
            get { return whereToDraw; }
        }

        private DoubleExtention vneshDensity = 1;
        public DoubleExtention VneshDensity
        {
            get { return vneshDensity; }
            set { vneshDensity = value;}
        }
        event EventHandler ObjectsChanged; 
        List<ObjectProto> Objects;
        bool allow_list_access;
        public bool Allow_List_Access
        {
            get { return allow_list_access; } 
        }
        public List<ObjectProto> objects
        {
            get
            {
                try
                {
                    //if (allow_list_access)
                        return Objects;
                    //else throw new Exceptions.ObjectsListAccessException();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
            set
            {
                try
                {
                    if (allow_list_access)
                    {
                        if(!one_obj)
                        foreach(ObjectProto obj in Objects)
                        {
                            obj.BeingRemoved();
                        }
                        Objects = value;
                        if (Objects.Count == 0)
                            objMaxID = 1;
                        else
                            objMaxID = Objects[Objects.Count - 1].ID + 1;
                        //for (int i = 0; i < Rays.Count; i++) Rays[i].ObjectsChanged(this, new EventArgs());
                        if (Rays.Count != 0) ObjectsChanged.Invoke(this, new EventArgs());
                    }
                    else throw new Exceptions.ObjectsListAccessException();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        int objMaxID;
        public int ObjMaxID {get {return objMaxID;} }
        List<Ray> Rays;
        public List<Ray> rays
        {
            get
            {
                try
                {
                    //if (allow_list_access)
                        return Rays;
                    //else throw new Exceptions.ObjectsListAccessException();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
            set
            {
                try
                {
                    if (allow_list_access)
                    {
                        foreach (Ray ray in Rays)
                        {
                            if(!one_obj)ray.BeingRemoved();
                            ObjectsChanged -= ray.ObjectsChanged;
                        }
                        ObjectsChanged = null;
                        Rays = value;
                        foreach (Ray ray in Rays)
                            ObjectsChanged += new EventHandler(ray.ObjectsChanged);
                        if (Rays.Count == 0) rayMaxID = 1;
                        else
                            rayMaxID = Rays[Rays.Count - 1].ID + 1;
                    }
                    else throw new Exceptions.ObjectsListAccessException();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        int rayMaxID;
        public int RayMaxID {get {return rayMaxID;} }
        #endregion
        #region Functions
        private ObjectCollection()
        {
            objMaxID = 1;
            rayMaxID = 1;
            Objects = new List<ObjectProto>();
            Rays = new List<Ray>();
            allow_list_access = false;
            SubscribedToSaveLoadEvents = false;
            formClear = new ConfirmClear();
        }
        public int AddRay(string Name, Point beginning, Point p2, BrightPoint bp, bool GenNew)
        {
            if (WarnedRays == false && WarnedRaysFinalized == false && rays.Count >= 10)
            {
                DialogResult dr = 
                MessageBox.Show(STranslation.T["WarnRay"] + '\n' + STranslation.T["WantToContinue"],
                                STranslation.T["Warning"], MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                    WarnedRays = false;
                else WarnedRays = true;
                WarnedRaysFinalized = true;
            }
            if (WarnedRaysFinalized == false || WarnedRays == false)
            {
                Line l1 = new Line(CoordinateSystem.Instance.Converter(beginning),
                                   CoordinateSystem.Instance.Converter(p2), false);
                l1.BuildAngle();
                Angle a = l1.AngleForOnePointLines;

                return AddRay(Name, CoordinateSystem.Instance.Converter(beginning), a, bp, GenNew);
            }
            return -1;
        }
        public int AddRay(string Name, SystemCoordinates Beginning, Angle angle, BrightPoint bpoint, bool GenNew)
        {
            if (WarnedRays == false && WarnedRaysFinalized == false && rays.Count >= 10)
            {
                if (MessageBox.Show(STranslation.T["WarnRay"] + '\n' + STranslation.T["WantToContinue"],
                                    STranslation.T["Warning"], MessageBoxButtons.OKCancel) == DialogResult.OK)
                    WarnedRays = false;
                else WarnedRays = true;
                WarnedRaysFinalized = true;
            }
            if (WarnedRaysFinalized == false || WarnedRays == false)
            {
                if (GenNew && !ObjectExists(bpoint.Name))
                {
                    AddObject(bpoint);
                }
                Ray ToAdd = new Ray(Name, rayMaxID, Beginning, angle, bpoint.Name);
                CheckExistObj = ToAdd;
                ObjectProto Found = Rays.Find(CheckExistance);
                if (Found != null)
                {
                    MessageBox.Show(STranslation.T["FoundAnotherRayWithThisName"]);
                    return -1;
                }
                ObjectsChanged += new EventHandler(ToAdd.ObjectsChanged);
                Rays.Add(ToAdd);
                Rays[Rays.Count - 1].ObjectsChanged(this, new EventArgs());
                rayMaxID++;
                if (OnObjectsRaysChange != null) OnObjectsRaysChange.Invoke(this, new EventArgs());
                return rayMaxID - 1;
            }
            return -1;
        }
        public int AddObject(ObjectProto ObjectToAdd)
        {
            int count = 0;
            foreach (ObjectProto x in objects)
                if (!(x is BrightPoint)) count++;
            if (WarnedObjects == false && WarnedObjectsFinalized == false && count >= 10)
            {
                DialogResult dr =
                MessageBox.Show(STranslation.T["WarnObject"] + '\n' + STranslation.T["WantToContinue"],
                                STranslation.T["Warning"], MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                    WarnedObjects = false;
                else WarnedObjects = true;
                WarnedObjectsFinalized = true;
            }
            if (WarnedObjectsFinalized == false || WarnedObjects == false)
            {
                ObjectToAdd.ID = objMaxID;
                CheckExistObj = ObjectToAdd;
                ObjectProto Found = Objects.Find(CheckExistance);
                if (Found != null)
                {
                    MessageBox.Show(STranslation.T["FoundAnotherObjectWithThisName"]);
                    return -1;
                }
                Objects.Add(ObjectToAdd);
                objMaxID++;
                if (Rays.Count != 0) ObjectsChanged.Invoke(this, new EventArgs());
                if (OnObjectsRaysChange != null) OnObjectsRaysChange.Invoke(this, new EventArgs());
                return objMaxID - 1;
            }
            else if(ObjectToAdd is BrightPoint)
            {
                ObjectToAdd.ID = objMaxID;
                CheckExistObj = ObjectToAdd;
                ObjectProto Found = Objects.Find(CheckExistance);
                if (Found != null)
                {
                    MessageBox.Show(STranslation.T["FoundAnotherObjectWithThisName"]);
                    return -1;
                }
                Objects.Add(ObjectToAdd);
                objMaxID++;
                if (Rays.Count != 0) ObjectsChanged.Invoke(this, new EventArgs());
                if (OnObjectsRaysChange != null) OnObjectsRaysChange.Invoke(this, new EventArgs());
                return objMaxID - 1;
            }
            return -1;
        }
        public Ray GetRayByID(int ID)
        {
            foreach(Ray ray in Rays)
                if (ray.ID == ID) return ray; 
            return null;
        }
        public ObjectProto GetObjectByID(int ID)
        {
            foreach (var obj in Objects)
                if (obj.ID == ID) return obj;
            return null;
        }
        public Ray GetRayByName(string name)
        {
            foreach (var ray in Rays)
            {
                if (ray.Name == name) return ray;
            }
            return null;
        }
        public ObjectProto GetObjectByName(string name)
        {
            foreach (var objectProto in Objects)
            {
                if (objectProto.Name == name) return objectProto;
            }
            return null;
        }
        public void InitDrawing(ref SimpleOpenGlControl Where, int Scale, Point Center, Point Sizes)
        {
            whereToDraw = Where;
            sysInstance.ResetSizes(Scale, Center, Sizes);
        }
        public void InitDrawing(ref SimpleOpenGlControl Where, int Scale, Point Center)
        {
            whereToDraw = Where;
            sysInstance.ResetSizes(Scale,Center,new Point(Where.Size));
        }
        public void InitDrawing(ref SimpleOpenGlControl Where, int Scale)
        {
            whereToDraw = Where;
            sysInstance.ResetSizes(Scale, new Point(Where.Size.Width / 2, Where.Size.Height / 2), new Point(Where.Size));
        }
        public void InitDrawing(ref SimpleOpenGlControl Where)
        {
            whereToDraw = Where;
            sysInstance.ResetSizes(50, new Point(Where.Size.Width / 2, Where.Size.Height / 2), new Point(Where.Size));
        }
        public void Redraw()
        {
            for (int i = 0; i < Rays.Count; i++)
            {
                Rays[i].Drawer();
            }
            //g.FillRectangle(new SolidBrush(Color.White), 0, 0, whereToDraw.Width, whereToDraw.Height);
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Drawer();
            }
                /*foreach (ObjectProto obj in Objects)
                    obj.Drawer();*/
            
        }
        public void SubscribeToSaveLoadEvents()
        {
            if (SubscribedToSaveLoadEvents == false)
            {
                SaveFiles.SaveLoad.Instance.StartLoad += new EventHandler(SaveLoad_load);
                SaveFiles.SaveLoad.Instance.FinishLoad += new EventHandler(SaveLoad_endload);
                Config.NewConfig += new EventHandler<ConfigurationChangeArgs>(Config_NewConfig);
                SubscribedToSaveLoadEvents = true;
            }
        }

        void Config_NewConfig(object sender, ConfigurationChangeArgs e)
        {
            if(e.Param == "Ray.ReflectionLimit" || e.FullReload == true)
            {
                if(ObjectsChanged!=null)ObjectsChanged.Invoke(this, new EventArgs());
            }
        }
        public void Reset(bool confirm)
        {
            if (confirm)
            {
                DialogResult res = formClear.ShowDialog();
                if(res == DialogResult.OK)
                {
                    allow_list_access = true;
                    rays = new List<Ray>();
                    objects = new List<ObjectProto>();
                    objMaxID = 1;
                    rayMaxID = 1;
                    allow_list_access = false;
                    Form1.form.groupBox1.ReloadList(Form1.form, new EventArgs());
                    Form1.form.SelectedItem = null;
                    Form1.form.groupBox1.Instance_OnSelectedChange(Form1.form, new SelectedChangeArgs(null));
                }
                
            }
            else
            {
                allow_list_access = true;
                rays = new List<Ray>();
                objects = new List<ObjectProto>();
                objMaxID = 1;
                rayMaxID = 1;
                allow_list_access = false;
                Form1.form.groupBox1.ReloadList(Form1.form, new EventArgs());
                Form1.form.SelectedItem = null;
                Form1.form.groupBox1.Instance_OnSelectedChange(Form1.form, new SelectedChangeArgs(null));
            }
        }
        public void UnSelect()
        {
            foreach (var ray in Rays)
            {
                ray.Selected = false;
            }
            foreach (var ray in Objects)
            {
                ray.Selected = false;
            }
        }
        public ObjectProto Select(Point X, bool raise = true)
        {
            UnSelect();
            ObjectProto newSelect;
            ObjectProto ray;
            ObjectProto objectProto;
            int i;
            for (i = Objects.Count - 1; i >= 0; i--)
            {
                objectProto = Objects[i];
                int dst = objectProto.DistanceToPointS(X);
                if (objectProto.DistanceToPointS(X) <= 5 && objectProto.DistanceToPointS(X) != -1)
                {
                    objectProto.Selected = true;
                    if (raise)
                    if (OnSelectedChange != null) OnSelectedChange.Invoke(this, new SelectedChangeArgs(objectProto));
                    return objectProto;
                }
            }
            for (i = Rays.Count - 1; i >= 0; i--)
            {
                ray = Rays[i];
                if (ray.DistanceToPointS(X) <= 5 && ray.DistanceToPointS(X) != -1)
                {
                    ray.Selected = true;
                    if(raise)
                    if (OnSelectedChange != null) OnSelectedChange.Invoke(this, new SelectedChangeArgs(ray));
                    return ray;
                }
            }
            if(raise)
            if (OnSelectedChange != null) OnSelectedChange.Invoke(this, new SelectedChangeArgs(null));
            return null;
        }
        public ObjectProto Select(ObjectProto x, bool raise = true)
        {
            UnSelect();
            if (x != null)
            {
                x.Selected = true;
                if (raise)
                    if (OnSelectedChange != null) OnSelectedChange.Invoke(this, new SelectedChangeArgs(x));
            }
            return x;
        }
        public void MoveTo(string name, Point fromPoint, Point toPoint, bool Ray = false)
        {
            if(!Ray)
            {
                var obj = GetObjectByName(name);
                if (obj != null)
                {
                    obj.MoveTo(toPoint, fromPoint);
                    if(ObjectsChanged!=null)ObjectsChanged.Invoke(this, new EventArgs());
                }
            }
            else
            {
                var obj = GetRayByName(name);
                if (obj != null)
                {
                    obj.MoveTo(toPoint, fromPoint);
                    ((Ray)obj).ObjectsChanged(this, new EventArgs());
                }
            }
        }
        public void DeleteObjectRay(ObjectProto X)
        {
            List<Ray> Rayss = new List<Ray>();
            List<ObjectProto> Objectss = new List<ObjectProto>();
            X.BeingRemoved();
            foreach (var alpha in Rays)
            {
                if (alpha != X)
                    Rayss.Add(alpha);
            }
            foreach (var alpha in Objects)
            {
                if (alpha != X)
                    Objectss.Add(alpha);
            }
            allow_list_access = true;
            one_obj = true;
            rays = Rayss;
            objects = Objectss;
            one_obj = false;
            allow_list_access = false;
            if (Form1.form != null && Form1.form.z != null) {Form1.form.SelectedItem = null;Form1.form.z.Instance_OnSelectedChange(this, new SelectedChangeArgs(null));}
            if (OnObjectsRaysChange != null) OnObjectsRaysChange.Invoke(this, new EventArgs());
            
        }
        public bool RayExists(string name)
        {
            foreach (var ray in Rays)
            {
                if(ray.Name == name) return true;
            }
            return false;
        }
        public bool ObjectExists(string name)
        {
            foreach (var objectProto in Objects)
            {
                if (objectProto.Name == name) return true;
            }
            return false;
        }
        public void RaiseObjectsChange()
        {
            if(OnObjectsRaysChange!=null) OnObjectsRaysChange.Invoke(this, new EventArgs());
            if(ObjectsChanged!=null) ObjectsChanged.Invoke(this, new EventArgs());
        }
        #endregion
        #region Event listeners
        public void imageSizeForm_SizeReset(object sender, ImageChangeArgs e)
        {
            InitDrawing(ref whereToDraw, e.NewScaling);
        }
        private void SaveLoad_load(object sender, EventArgs e)
        {
            allow_list_access = true;
        }
        private void SaveLoad_endload(object sender, EventArgs e)
        {
            allow_list_access = false;
        }
        #endregion
        #region Inner functions
        private static ObjectCollection GetInstance()
        {
            if (created == false) { instance = new ObjectCollection(); sysInstance = CoordinateSystem.Instance; created = true; }
            return instance;
        }
        private bool CheckExistance(ObjectProto y)
        {
            return (CheckExistObj.Name == y.Name);
        }
        

        #endregion
        #region Inner variables

        ObjectProto CheckExistObj;
        private bool WarnedObjects = false;
        private bool WarnedObjectsFinalized = false;
        private bool WarnedRays = false;
        private bool WarnedRaysFinalized = false;

        #endregion
        #region Forms

        private ConfirmClear formClear;

        #endregion
    }
    public class SelectedChangeArgs:EventArgs
    {
        private ObjectProto newItem;
        public ObjectProto NewItem
        {
            get { return newItem; }
        }
        public SelectedChangeArgs(ObjectProto newItem)
        {
            this.newItem = newItem;
        }
    }
}
