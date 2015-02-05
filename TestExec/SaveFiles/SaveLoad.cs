using System;
using System.Collections.Generic;
 
using System.Net.Sockets;
using System.Text;
using System.IO;
using OpticalBuilder;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.OpticalObjects;
using OpticalBuilderLib.Exceptions;
using System.Windows.Forms;
using SaveBeforeClosing = OpticalBuilderLib.Forms.SaveBeforeClosing;

namespace OpticalBuilderLib.SaveFiles
{
    public class SaveLoad
    {
        public event EventHandler StartLoad;
        public event EventHandler FinishLoad;
        private static bool instance_generated = false;
        static SaveLoad instance;
        private SaveBeforeClosing formSave;
        bool ctrl;
        bool exiting = false;
        bool locked;
        string SavePath;
        public static SaveLoad Instance
        {
            get
            {
                if (instance_generated) return instance;
                else
                {
                    instance = new SaveLoad();
                    instance_generated = true;
                    OpticalObjects.ObjectCollection.Instance.SubscribeToSaveLoadEvents();
                    return instance;
                }
            }
        }
        private SaveLoad()
        {
            SavePath = null;
            formSave = new SaveBeforeClosing();
            if(!(Directory.Exists(Application.StartupPath + "/Saves")))
                Directory.CreateDirectory(Application.StartupPath + "/Saves");
            StartAutosaveThread();
            //Configuration.Translation.LanguageChange+=formSave.
            //Gma.UserActivityMonitor.HookManager.KeyUp += new System.Windows.Forms.KeyEventHandler(HookManager_KeyUp);
            //Gma.UserActivityMonitor.HookManager.KeyDown += new System.Windows.Forms.KeyEventHandler(HookManager_KeyDown);
        }
        public void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.LControlKey || e.KeyCode == System.Windows.Forms.Keys.RControlKey || e.KeyCode==System.Windows.Forms.Keys.ControlKey)
                ctrl = false;
            if (e.KeyCode == System.Windows.Forms.Keys.S && locked == true)
            {
                SaveDialog();
                locked = false;
            }
            else if(e.KeyCode == System.Windows.Forms.Keys.O && locked == true)
            {
                LoadDialog();
                locked = false;
            }
        }
        public void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.LControlKey || e.KeyCode == System.Windows.Forms.Keys.RControlKey || e.KeyCode == System.Windows.Forms.Keys.ControlKey)
                ctrl = true;
            if (ctrl == true && e.KeyCode == System.Windows.Forms.Keys.S)
                locked = true;
            if (ctrl == true && e.KeyCode == System.Windows.Forms.Keys.O)
                locked = true;
        }
        void Save(string file_path)
        {
            #region Open File
            if ((StartLoad == null) || (FinishLoad == null)) throw new InitializedListLookupFailed();
            if (file_path.Contains(".obs") == false)
                file_path = file_path + ".obs";
            StartLoad.Invoke(this, new EventArgs());
            FileStream save;
            StreamWriter saver;
            try
            {
                save = new FileStream(file_path, FileMode.Create);
                saver = new StreamWriter(save);
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
                return;
            }
            #endregion
            #region Write Data
            saver.WriteLine("[rays]");
            foreach (Ray x in ObjectCollection.Instance.rays)
                saver.WriteLine(x.GenerateSaveString());
            saver.WriteLine("[objects]");
            foreach (ObjectProto x in ObjectCollection.Instance.objects)
                saver.WriteLine(x.GenerateSaveString());
            saver.WriteLine("[EndSave]");
            saver.Close();
            FinishLoad.Invoke(this, new EventArgs());
            #endregion
        }
        public bool SaveNoDialog()
        {
            if (SavePath == null)
                return SaveDialog();
            else
            {
                Save(SavePath);
                return true;
            }
        }
        void Load(string file_path)
        {
            #region Open File
            if ((StartLoad == null) || (FinishLoad == null)) throw new InitializedListLookupFailed();
            StartLoad.Invoke(this, new EventArgs());
            List<Ray> Rays = new List<Ray>();
            List<ObjectProto> Objects = new List<ObjectProto>();
            if (File.Exists(file_path) == false)
            {
                System.Windows.Forms.MessageBox.Show(OpticalBuilderLib.Configuration.STranslation.T["FileNotFound"] + '!');
                FinishLoad.Invoke(this, new EventArgs());
            }
            FileStream read = new FileStream(file_path, FileMode.Open);
            StreamReader reader = new StreamReader(read);
            #endregion
            #region Read Data
            if (reader.ReadLine() != "[rays]")
            {
                Exception e = new ErrorWhileReading();
                System.Windows.Forms.MessageBox.Show((new ErrorWhileReading()).Message);
#pragma warning disable 0162
                FinishLoad.Invoke(this, new EventArgs());
#pragma warning restore 0162
                return;
            }
            string str = reader.ReadLine();
            while (str != "[objects]")
            {
                try
                {
                    Rays.Add(new Ray(str));
                }
                catch(Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                    FinishLoad.Invoke(this, new EventArgs());
                    return;
                }
                str = reader.ReadLine();
            }
            str = reader.ReadLine();
            while (str != "[EndSave]")
            {
                try
                {
                    Objects.Add(ConstructObject(str.Split('^')[0], str));
                }
                catch(Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                    if(e is ErrorWhileReading) FinishLoad.Invoke(this, new EventArgs());
                    return;
                }
                str = reader.ReadLine();
            }
            ObjectCollection.Instance.rays = Rays;
            ObjectCollection.Instance.objects = Objects;
            reader.Close();
            FinishLoad.Invoke(this, new EventArgs());
            ObjectCollection.Instance.RaiseObjectsChange();
            Form1.form.SelectedItem = null;
            Form1.form.groupBox1.ReloadList(this, new EventArgs());
            Form1.form.groupBox1.Instance_OnSelectedChange(this, new SelectedChangeArgs(null));
            #endregion
        }
        public void LoadDialog()
        {
            System.Windows.Forms.OpenFileDialog loader = new System.Windows.Forms.OpenFileDialog();
            loader.DefaultExt = "Optical Builder saves|*.obs";
            loader.Multiselect = false;
            loader.Filter = "Optical Builder saves|*.obs";
            loader.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\Saves";
            if (loader.ShowDialog() == System.Windows.Forms.DialogResult.OK)
               Load(loader.FileName);
            SavePath = loader.FileName;
        }
        public void LoadNoDialog(string Path)
        {
            /*System.Windows.Forms.OpenFileDialog loader = new System.Windows.Forms.OpenFileDialog();
            loader.DefaultExt = "Optical Builder saves|*.obs";
            loader.Multiselect = false;
            loader.Filter = "Optical Builder saves|*.obs";
            loader.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\Saves";
            if (loader.ShowDialog() == System.Windows.Forms.DialogResult.OK)*/
            Load(Path);
            SavePath = Path;
        }
        public bool SaveDialog()
        {
            bool ret = false;
            System.Windows.Forms.SaveFileDialog loader = new System.Windows.Forms.SaveFileDialog();
            loader.DefaultExt = "Optical Builder saves|*.obs";
            loader.Filter = "Optical Builder saves|*.obs";
            loader.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\Saves";
            if (loader.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Save(loader.FileName);
                SavePath = loader.FileName;
                return true;
            }
            return false;
        }
        public ObjectProto ConstructObject(string name,string SaveStr)
        {
            switch (name)
            {
                case "{Mirror}":
                    return new Mirror(SaveStr);
                case "{Ray}":
                    return new Ray(SaveStr);
                case "{BrightPoint}":
                    return new BrightPoint(SaveStr);
                case "{Sphere}":
                    return new Sphere(SaveStr);
                case "{Lense}":
                    return new Lense(SaveStr);
                case "{SphericalMirror}":
                    return new SphereMirror(SaveStr);
                case "{Polygon}":
                    return new OPolygon(SaveStr);
                default:
                    throw new ErrorWhileReading();
            }
        }
        public void CloseSave()
        {
            if(SavePath!=null)
            {
                DialogResult CloseNot = formSave.ShowDialog();
                if (CloseNot == DialogResult.OK)
                {
                    SaveNoDialog();
                    SavePath = null;
                    ObjectCollection.Instance.Reset(false);
                }
                else
                if (CloseNot == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    SavePath = null;
                    ObjectCollection.Instance.Reset(false);
                }
                return;
            }
            if(StartLoad!=null)
            {
                StartLoad.Invoke(this, new EventArgs());
                if(ObjectCollection.Instance.rays.Count + ObjectCollection.Instance.objects.Count != 0)
                {
                    DialogResult CloseNot = formSave.ShowDialog();
                    if (CloseNot == DialogResult.OK)
                    {
                        SaveNoDialog();
                        SavePath = null;
                        ObjectCollection.Instance.Reset(false);
                    }
                    else
                        if (CloseNot == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            SavePath = null;
                            ObjectCollection.Instance.Reset(false);
                        }
                    return;
                }
                FinishLoad.Invoke(this, new EventArgs());
            }
        }
        public void AutoSave()
        {
            if (!exiting)
            {
                if (SConfig.C["Autosave"] == "1")
                    if (SavePath == null)
                        Save(Application.StartupPath + "/Saves/AUTOSAVE.obs");
                    else
                        Save(SavePath);
            }
        }
        private void StartAutosaveThread()
        {
            exiting = false;
            /*System.Threading.Thread alpha = new System.Threading.Thread(AutoSave);
            alpha.Start();*/
        }
        public void StopAutosaveThread()
        {
            exiting = true;
        }
    }
}
