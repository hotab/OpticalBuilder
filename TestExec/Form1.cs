 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
 using System.Diagnostics;
 using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
 using OpticalBuilder.Forms;
 using OpticalBuilder.Properties;
using OpticalBuilderLib;
 using OpticalBuilderLib.MathLib;
 using OpticalBuilderLib.OpticalObjects;
 using OpticalBuilderLib.Configuration;
 using OpticalBuilderLib.EventArguments;
 using OpticalBuilderLib.SaveFiles;
 using OpticalBuilderLib.TypeExtentions;
 using OpticalBuilderLib.Forms;
 using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
namespace OpticalBuilder
{
    public partial class Form1 : Form
    {
        public static Form1 form;
        ObjectCollection X;
        private ConfigForm frm_cfg = new ConfigForm();
        FormHelp frm_hlp = new FormHelp();
        private ObjectProto PastleObj;
        private Point cursor_c;
        private bool key_down = false; //mouse left button is down
        private Point prevPoint; //prev_movement_point
        private bool prev_point_set = false;
        public ObjectProto SelectedItem;
        public PropertiesList z;
        private InstrumentBase activeInstrument;
        private List<InstrumentBase> instruments;
        private Dictionary<ThreePosBtn, InstrumentBase> instruments_bindage; 
        SaveBeforeClosing saveBeforeClosing;
        ToolStripMenuItem[] languages;
        string[] lenumarray;
        string[] FullNameArray;

        private int PictureSizeDiferenceFromFormX = 418;
        private int pictureSizeDiferenceFromFormY = 147;
        private int DistFromGroupBoxToPic = 20;

        #region Init Code - Instruments + OpenGL + Menu Translation + Constructors
        #region Instrument Code
        public void DeactivateInstrument(InstrumentBase x)
        {
            if(x == activeInstrument)
            {
                activeInstrument.Deselect();
                activeInstrument.Deactivate();
                activeInstrument = instruments_bindage[threePosBtn1];
                activeInstrument.Activate();
            }
        }
        public void CreateInstruments()
        {
            threePosBtn1.LoadImages(Application.StartupPath + "\\Images\\icon_select_move.png",
                                    Application.StartupPath + "\\Images\\icon_select_move_3.png",
                                    Application.StartupPath + "\\Images\\icon_select_move_2.png",
                                    Application.StartupPath + "\\Images\\icon_select_move_4.png");
            threePosBtn2.LoadImages(Application.StartupPath + "\\Images\\icon_luch.png",
                                    Application.StartupPath + "\\Images\\icon_luch_3.png",
                                    Application.StartupPath + "\\Images\\icon_luch_2.png",
                                    Application.StartupPath + "\\Images\\icon_luch_4.png");
            threePosBtn3.LoadImages(Application.StartupPath + "\\Images\\icon_mirror.png",
                                    Application.StartupPath + "\\Images\\icon_mirror_3.png",
                                    Application.StartupPath + "\\Images\\icon_mirror_2.png",
                                    Application.StartupPath + "\\Images\\icon_mirror_4.png");
            threePosBtn4.LoadImages(Application.StartupPath + "\\Images\\icon_point.png",
                                    Application.StartupPath + "\\Images\\icon_point_3.png",
                                    Application.StartupPath + "\\Images\\icon_point_2.png",
                                    Application.StartupPath + "\\Images\\icon_point_4.png");
            threePosBtn5.LoadImages(Application.StartupPath + "\\Images\\icon_circle.png",
                                    Application.StartupPath + "\\Images\\icon_circle_3.png",
                                    Application.StartupPath + "\\Images\\icon_circle_2.png",
                                    Application.StartupPath + "\\Images\\icon_circle_4.png");
            threePosBtn6.LoadImages(Application.StartupPath + "\\Images\\icon_linz_vip.png",
                                    Application.StartupPath + "\\Images\\icon_linz_vip_3.png",
                                    Application.StartupPath + "\\Images\\icon_linz_vip_2.png",
                                    Application.StartupPath + "\\Images\\icon_linz_vip_4.png");
            threePosBtn7.LoadImages(Application.StartupPath + "\\Images\\icon_lup.png",
                                    Application.StartupPath + "\\Images\\icon_lup_3.png",
                                    Application.StartupPath + "\\Images\\icon_lup_2.png",
                                    Application.StartupPath + "\\Images\\icon_lup_4.png");
            threePosBtn8.LoadImages(Application.StartupPath + "\\Images\\icon_linz_vp.png",
                                    Application.StartupPath + "\\Images\\icon_linz_vp_3.png",
                                    Application.StartupPath + "\\Images\\icon_linz_vp_2.png",
                                    Application.StartupPath + "\\Images\\icon_linz_vp_4.png");
            threePosBtn9.LoadImages(Application.StartupPath + "\\Images\\icon_hand.png",
                                    Application.StartupPath + "\\Images\\icon_hand_3.png",
                                    Application.StartupPath + "\\Images\\icon_hand_2.png",
                                    Application.StartupPath + "\\Images\\icon_hand_4.png");
            threePosBtn10.LoadImages(Application.StartupPath + "\\Images\\icon_mirrorc.png",
                                    Application.StartupPath + "\\Images\\icon_mirrorc_3.png",
                                    Application.StartupPath + "\\Images\\icon_mirrorc_2.png",
                                    Application.StartupPath + "\\Images\\icon_mirrorc_4.png");
            threePosBtn11.LoadImages(Application.StartupPath + "\\Images\\icon_polygon.png",
                                    Application.StartupPath + "\\Images\\icon_polygon_3.png",
                                    Application.StartupPath + "\\Images\\icon_polygon_2.png",
                                    Application.StartupPath + "\\Images\\icon_polygon_4.png");
            instruments = new List<InstrumentBase>();
            instruments_bindage = new Dictionary<ThreePosBtn, InstrumentBase>();
            InstrumentBase rayer = new InstrumentRay(threePosBtn2);
            instruments.Add(rayer);
            instruments_bindage[threePosBtn2] = rayer;
            InstrumentBase mirrorer = new InstrumentMirror(threePosBtn3);
            instruments.Add(mirrorer);
            instruments_bindage[threePosBtn3] = mirrorer;
            InstrumentBase pointer = new InstrumentBrightPoint(threePosBtn4);
            instruments.Add(pointer);
            instruments_bindage[threePosBtn4] = pointer;
            InstrumentBase spherer = new InstrumentSphere(threePosBtn5);
            instruments.Add(spherer);
            instruments_bindage[threePosBtn5] = spherer;
            InstrumentBase SelecterAndMover = new InstrumentSelectAndMove(threePosBtn1);
            instruments.Add(SelecterAndMover);
            instruments_bindage[threePosBtn1] = SelecterAndMover;
            InstrumentBase Lenser1 = new InstrumentLenseNoMenisksTwoOkrs(threePosBtn6);
            instruments.Add(Lenser1);
            instruments_bindage[threePosBtn6] = Lenser1;
            InstrumentBase Lupa = new InstrumentLupa(threePosBtn7);
            instruments.Add(Lupa);
            instruments_bindage[threePosBtn7] = Lupa;
            InstrumentBase Lenser2 = new InstrumentLenseMenisksTwoOkrs(threePosBtn8);
            instruments.Add(Lenser2);
            instruments_bindage[threePosBtn8] = Lenser2;
            InstrumentBase Hander = new InstrumentHand(threePosBtn9);
            instruments.Add(Hander);
            instruments_bindage[threePosBtn9] = Hander;
            InstrumentBase Hander2 = new InstrumentSphericalMirror(threePosBtn10);
            instruments.Add(Hander2);
            instruments_bindage[threePosBtn10] = Hander2;
            InstrumentBase polygoner = new InstrumentPolygon(threePosBtn11);
            instruments.Add(polygoner);
            instruments_bindage[threePosBtn11] = polygoner;
            activeInstrument = SelecterAndMover;
            activeInstrument.Activate();
        }
        public void SelectNewInstrument(InstrumentBase ins)
        {
            activeInstrument.Deselect();
            activeInstrument.Deactivate();
            activeInstrument = ins;
            activeInstrument.Activate();
        }
        public void SelectEvent(object sender, MouseEventArgs e)
        {
            if (sender is ThreePosBtn && activeInstrument != instruments_bindage[(ThreePosBtn)sender])
                SelectNewInstrument(instruments_bindage[(ThreePosBtn)sender]);
            else
            if (sender is ThreePosBtn && activeInstrument == instruments_bindage[(ThreePosBtn)sender])
                SelectNewInstrument(instruments_bindage[threePosBtn1]);
        }
        #region ToolTips

        private ToolTip Ins_Mover;
        private ToolTip Ins_Ray;
        private ToolTip Ins_Point;
        private ToolTip Ins_Mirror;
        private ToolTip Ins_Sphere;
        private ToolTip Ins_LenseType1;
        private ToolTip Ins_LenseType2;
        private ToolTip Ins_SphericalMirror;
        private ToolTip Ins_Polygon;
        private ToolTip Ins_Magnifier;
        private ToolTip Ins_Hand;
        #endregion
        public void GenerateToolTips()
        {
            if (Ins_Mover == null) Ins_Mover = new ToolTip();
            if (Ins_Ray == null) Ins_Ray = new ToolTip();
            if (Ins_Point == null) Ins_Point = new ToolTip();
            if (Ins_Mirror == null) Ins_Mirror = new ToolTip();
            if (Ins_Sphere == null) Ins_Sphere = new ToolTip();
            if (Ins_LenseType1 == null) Ins_LenseType1 = new ToolTip();
            if (Ins_LenseType2 == null) Ins_LenseType2 = new ToolTip();
            if (Ins_SphericalMirror == null) Ins_SphericalMirror = new ToolTip();
            if (Ins_Polygon == null) Ins_Polygon = new ToolTip();
            if (Ins_Magnifier == null) Ins_Magnifier = new ToolTip();
            if (Ins_Hand == null) Ins_Hand = new ToolTip();
            Ins_Mover.RemoveAll();
            Ins_Ray.RemoveAll();
            Ins_Point.RemoveAll();
            Ins_Mirror.RemoveAll();
            Ins_Sphere.RemoveAll();
            Ins_LenseType1.RemoveAll();
            Ins_LenseType2.RemoveAll();
            Ins_SphericalMirror.RemoveAll();
            Ins_Polygon.RemoveAll();
            Ins_Magnifier.RemoveAll();
            Ins_Hand.RemoveAll();
            Ins_Mover.SetToolTip(threePosBtn1, STranslation.T["Ins_Mover"]);
            Ins_Ray.SetToolTip(threePosBtn2, STranslation.T["Ins_Ray"]);
            Ins_Point.SetToolTip(threePosBtn4, STranslation.T["Ins_Point"]);
            Ins_Mirror.SetToolTip(threePosBtn3, STranslation.T["Ins_Mirror"]);
            Ins_Sphere.SetToolTip(threePosBtn5, STranslation.T["Ins_Sphere"]);
            Ins_LenseType1.SetToolTip(threePosBtn6, STranslation.T["Ins_LenseType1"]);
            Ins_LenseType2.SetToolTip(threePosBtn8, STranslation.T["Ins_LenseType2"]);
            Ins_SphericalMirror.SetToolTip(threePosBtn10, STranslation.T["Ins_SphericalMirror"]);
            Ins_Polygon.SetToolTip(threePosBtn11, STranslation.T["Ins_Polygon"]);
            Ins_Magnifier.SetToolTip(threePosBtn7, STranslation.T["Ins_Magnifier"]);
            Ins_Hand.SetToolTip(threePosBtn9, STranslation.T["Ins_Hand"]);
        }
        #endregion

        #region OpenGL Code
        public void InitOGL()
        {
            Gl.glClearColor(1, 1, 1, 1);
            Gl.glViewport(0, 0, simpleGL1.Width, simpleGL1.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, (float)simpleGL1.Width, (float)simpleGL1.Height ,0.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glEnable(Gl.GL_BLEND);
            //Gl.glEnable(Gl.GL_DEPTH_TEST);
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }
        #endregion

        #region Menu Translation Code
        void AddMainMenuItems()
        {
            menuStrip1.Items[0].Text = STranslation.T["File"];
            loadToolStripMenuItem.Text = STranslation.T["Load"]+"...";
            saveToolStripMenuItem.Text = STranslation.T["Save"];
            saveAsToolStripMenuItem.Text = STranslation.T["SaveAs"] + "...";
            editToolStripMenuItem.Text = STranslation.T["Edit"]; 
            languageToolStripMenuItem.Text = STranslation.T["Language"];
            exitToolStripMenuItem.Text = STranslation.T["Exit"];
            closeToolStripMenuItem.Text = STranslation.T["Close"];
            clearToolStripMenuItem.Text = STranslation.T["ClearProject"];
            pastleToolStripMenuItem.Text = STranslation.T["Pastle"];
            copyToolStripMenuItem.Text = STranslation.T["Copy"];
            helpToolStripMenuItem1.Text = STranslation.T["Help"];
            configToolStripMenuItem.Text = STranslation.T["Configform"];

        }
        #endregion

        #region Constructors

        private string load_str;
        private bool load = false;
        public Form1()
        {
            #region SplashScreen
            #endregion
            
            #region Initialization
            Config.StdConfigPattern = Resources.Configuration;
            CodeFunctions.Init();
            X = ObjectCollection.Instance;
            saveBeforeClosing = new SaveBeforeClosing();
            #endregion
            #region Event Signatures
            this.KeyDown+=new KeyEventHandler(OpticalBuilderLib.SaveFiles.SaveLoad.Instance.HookManager_KeyDown);
            this.KeyUp+=new KeyEventHandler(OpticalBuilderLib.SaveFiles.SaveLoad.Instance.HookManager_KeyUp);
            ObjectCollection.Instance.OnSelectedChange += new EventHandler<SelectedChangeArgs>(Instance_OnSelectedChange);
            Config.NewConfig += new EventHandler<ConfigurationChangeArgs>(Translation.ConfigChangeHandle);
            Config.NewConfig += new EventHandler<ConfigurationChangeArgs>(NewConfiguration);
            Translation.LanguageChange += new EventHandler<LanguageChangeArgs>(Translation_LanguageChange);
            #endregion
            InitializeComponent();
            
            CreateInstruments();
            simpleGL1.InitializeContexts();
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            simpleGL1.Size = new Size(this.Size.Width - PictureSizeDiferenceFromFormX, this.Size.Height - pictureSizeDiferenceFromFormY);
            groupBox1.Location = new Point(this.Size.Width - PictureSizeDiferenceFromFormX + DistFromGroupBoxToPic, groupBox1.Location.Y);
            groupBox1.Height = simpleGL1.Height;
            groupBox1.HookEvents();
            form = this;
        }
        public Form1(string [] args)
        {
                #region SplashScreen

                #endregion

                #region Initialization

                Config.StdConfigPattern = Resources.Configuration;
                CodeFunctions.Init();
                X = ObjectCollection.Instance;
                saveBeforeClosing = new SaveBeforeClosing();

                #endregion

                #region Event Signatures

                this.KeyDown += new KeyEventHandler(OpticalBuilderLib.SaveFiles.SaveLoad.Instance.HookManager_KeyDown);
                this.KeyUp += new KeyEventHandler(OpticalBuilderLib.SaveFiles.SaveLoad.Instance.HookManager_KeyUp);
                ObjectCollection.Instance.OnSelectedChange += new EventHandler<SelectedChangeArgs>(Instance_OnSelectedChange);
                Config.NewConfig += new EventHandler<ConfigurationChangeArgs>(Translation.ConfigChangeHandle);
                Config.NewConfig += new EventHandler<ConfigurationChangeArgs>(NewConfiguration);
                Translation.LanguageChange += new EventHandler<LanguageChangeArgs>(Translation_LanguageChange);

                #endregion

                InitializeComponent();
                CreateInstruments();
                simpleGL1.Size = new Size(this.Size.Width - PictureSizeDiferenceFromFormX,
                                          this.Size.Height - pictureSizeDiferenceFromFormY);
                groupBox1.Location = new Point(this.Size.Width - PictureSizeDiferenceFromFormX + DistFromGroupBoxToPic,
                                               groupBox1.Location.Y);
                simpleGL1.InitializeContexts();
                Glut.glutInit();
                Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
                load = true;
                load_str = args[0];
                groupBox1.Height = simpleGL1.Height;
                groupBox1.HookEvents();
                form = this;
        }

        #endregion

        #endregion

        #region Outer Events

        void SelectLanguageFromList(object sender, MouseEventArgs e)
        {

            int i;
            for (i = 0; i < languages.Length; i++)
                languages[i].Checked = false;
            for (i = 0; i < lenumarray.Length; i++)
            {
                if (FullNameArray[i] == ((ToolStripMenuItem) sender).Text)
                {
                    Translation.InitTranslations(lenumarray[i]);
                    ((ToolStripMenuItem) sender).Checked = true;
                }
            }
        }

        void Translation_LanguageChange(object sender, LanguageChangeArgs e)
        {
            AddMainMenuItems();
            Text = STranslation.T["OpticalBuilder"];
            GenerateToolTips();
        }
        
        public void NewConfiguration(object sender, ConfigurationChangeArgs e)
        {
            if (e.Param == "Picture.Size" || e.FullReload == true)
            {
                string newSize = SConfig.C["Picture.Size"];
                if (newSize == null)
                {
                    Config.GenStandartFile();
                    return;
                }
                if (newSize.Split(',').Length != 2)
                {
                    Config.GenStandartFile();
                    return;
                }
                try
                {
                    int width = Convert.ToInt32(newSize.Split(',')[0]);
                    int height = Convert.ToInt32(newSize.Split(',')[1]);
                    //pictureBox1.Size = new Size(width, height);
                    simpleGL1.Size = new Size(width, height);
                    
                    InitOGL();
                    //12; 41
                    Size = new Size(simpleGL1.Size.Width + PictureSizeDiferenceFromFormX, simpleGL1.Size.Height + pictureSizeDiferenceFromFormY);
                    groupBox1.Location = new Point(this.Size.Width - PictureSizeDiferenceFromFormX + DistFromGroupBoxToPic, groupBox1.Location.Y);
                    groupBox1.Height = simpleGL1.Height;
                    Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
                    Gl.glLoadIdentity();
                    ObjectCollection.Instance.Redraw();
                    Point x = simpleGL1.PointToClient(Cursor.Position);
                    if (x.X < 0 || x.Y < 0 || x.X > simpleGL1.Width || x.Y > simpleGL1.Height) return;
                    else activeInstrument.Draw(x);
                    Gl.glFlush();
                    simpleGL1.Invalidate();
                    X.InitDrawing(ref simpleGL1);
                }
#pragma warning disable 0168
                catch (Exception ex)
                {
                    Config.GenStandartFile();
                    return;
                }
#pragma warning restore 0168
            }
            #region Load Language
            if (e.FullReload == false) return;
            string lenum = SConfig.C["language.enum"];
            if (lenum == null)
            {
                Config.GenStandartFile();
                return;
            }
            string[] newlenumarray = lenum.Split(',');
            if (lenumarray != null)
                if (lenumarray.Length == newlenumarray.Length)
                {
                    int u = 0;
                    for (u = 0; u < lenumarray.Length; u++)
                    {
                        if (lenumarray[u] != newlenumarray[u])
                        {
                            u = -1;
                            break;
                        }
                    }
                    if (u != -1) return;
                }
            lenumarray = lenum.Split(',');
            FullNameArray = new string[lenumarray.Length];
            int i = 0;
            for (i = 0; i < lenumarray.Length; i++)
            {
                FullNameArray[i] = SConfig.C["language." + lenumarray[i]];
                if (FullNameArray[i] == null)
                {
                    Config.GenStandartFile();
                    return;
                }
            }
            languages = new ToolStripMenuItem[FullNameArray.Length];
            for (int al = 0; al < FullNameArray.Length; al++)
            {
                languages[al] = new ToolStripMenuItem(FullNameArray[al]);
                if (lenumarray[al] == SConfig.C["language"])
                    languages[al].Checked = true;
                languages[al].MouseUp += new MouseEventHandler(SelectLanguageFromList);
            }
            menuStrip1.Items.Remove(languageToolStripMenuItem);
            languageToolStripMenuItem = new ToolStripMenuItem(STranslation.T["Language"], null, languages);
            languageToolStripMenuItem.Owner = menuStrip1;
            menuStrip1.Items.Insert(2, languageToolStripMenuItem);
            //menuStrip1.Items[1] = new ToolStripMenuItem("Language", null, languages);
            #endregion
        }
        

        void Instance_OnSelectedChange(object sender, SelectedChangeArgs e)
        {
            //groupBox1.Controls.Clear();
            SelectedItem = e.NewItem;
            if(e.NewItem == null)
            {
                return;
            }
            if(e.NewItem is Mirror)
            {
            }
        }
        #endregion

        #region Inner Events
        private void Form1_Load(object sender, EventArgs e)
        {
            Config.Read();
            X.InitDrawing(ref simpleGL1, Convert.ToInt32(SConfig.C["System.Scaling"]));
            InitOGL();
            groupBox1.OnUserSelect += new EventHandler<EventArgs>(groupBox1_OnUserSelect);
            z = groupBox1;
            
        }

        void groupBox1_OnUserSelect(object sender, EventArgs e)
        {
            if (groupBox1.selected_name != null)
            {
                if (groupBox1.is_ray)
                    SelectedItem = ObjectCollection.Instance.Select(X.GetRayByName(groupBox1.selected_name), false);
                else
                    SelectedItem = ObjectCollection.Instance.Select(X.GetObjectByName(groupBox1.selected_name), false);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SelectNewInstrument(instruments_bindage[threePosBtn1]);
            DialogResult result = saveBeforeClosing.ShowDialog();
            if (result == DialogResult.OK)
            {
                if(!(SaveLoad.Instance.SaveNoDialog()))
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            groupBox1.UnHook();
            Config.Save();
            SaveLoad.Instance.StopAutosaveThread();
            Application.ExitThread();
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            if (load) SaveLoad.Instance.LoadNoDialog(load_str);
        }
        
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            SConfig.C["Picture.Size"] = (Size.Width - PictureSizeDiferenceFromFormX).ToString() + ',' + (Size.Height - pictureSizeDiferenceFromFormY).ToString();
        }

        #endregion

        #region Keyboard Code
        private bool ctrl = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            SaveLoad.Instance.HookManager_KeyDown(sender, e);
            if (e.KeyCode == System.Windows.Forms.Keys.LControlKey || e.KeyCode == System.Windows.Forms.Keys.RControlKey || e.KeyCode == System.Windows.Forms.Keys.ControlKey)
                ctrl = true;
            /*if (e.KeyCode == System.Windows.Forms.Keys.LControlKey || e.KeyCode == System.Windows.Forms.Keys.RControlKey || e.KeyCode == System.Windows.Forms.Keys.ControlKey)
                ((InstrumentMirror)instruments_bindage[pictureBox3]).ControlPressed(true);*/
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            SaveLoad.Instance.HookManager_KeyUp(sender, e);
            if(e.KeyCode == Keys.Escape)
                if (!(activeInstrument is InstrumentSelectAndMove))
                    SelectNewInstrument(InstrumentSelectAndMove.Instance);
            if (e.KeyCode == System.Windows.Forms.Keys.LControlKey || e.KeyCode == System.Windows.Forms.Keys.RControlKey || e.KeyCode == System.Windows.Forms.Keys.ControlKey)
                ctrl = false;
            if(ctrl && e.KeyCode == Keys.C && SelectedItem != null)
            {
                PastleObj = SelectedItem;
                string h = PastleObj.GenerateSaveString();
                PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), h);
            }
            if (ctrl && e.KeyCode == Keys.V && PastleObj != null)
            {
                string h = PastleObj.GenerateSaveString();
                PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), h);
                PastleObj.Name = ObjectProto.GenName(PastleObj.GetTypeSpecifier(),
                                                     PastleObj.GetTypeSpecifier() ==
                                                     ObjectProto.GetSpec(ObjectTypes.Ray));
                int id = -1;
                if (!(PastleObj is Ray))
                    ObjectCollection.Instance.AddObject(PastleObj);
                else
                {
                    ((Ray)PastleObj).Angle = new Angle(((Ray)PastleObj).Angle.GetInDegrees() + 30, false);
                    id = ObjectCollection.Instance.AddRay(PastleObj.Name, PastleObj.Coordinates, ((Ray) PastleObj).Angle,
                                                     ((Ray) PastleObj).BoundPoint, true);
                }
                if (PastleObj is Ray)
                    SelectedItem = ObjectCollection.Instance.Select(ObjectCollection.Instance.GetRayByID(id));
                else
                    SelectedItem = ObjectCollection.Instance.Select(PastleObj);
                if (!(PastleObj is Ray))
                {
                    SystemCoordinates tmp = new SystemCoordinates(PastleObj.Coordinates);
                    Point X = tmp;
                    X.X += 10;
                    X.Y += 10;
                    PastleObj.Coordinates = new SystemCoordinates(X);
                }
                PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), PastleObj.GenerateSaveString());
            }
            if(SelectedItem!=null && e.KeyCode == Keys.Delete)
            {
                X.DeleteObjectRay(SelectedItem);
            }
            /*if (e.KeyCode == System.Windows.Forms.Keys.LControlKey || e.KeyCode == System.Windows.Forms.Keys.RControlKey || e.KeyCode == System.Windows.Forms.Keys.ControlKey)
                ((InstrumentMirror)instruments_bindage[pictureBox3]).ControlPressed(false);*/
        }
        #endregion

        #region Tool Strip Code
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLoad.Instance.LoadDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLoad.Instance.SaveNoDialog();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLoad.Instance.SaveDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLoad.Instance.CloseSave();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            X.Reset(true);
        }

        #endregion

        #region Context Menu Code

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //TODO: Generate context menu for the main display
            if(!(activeInstrument is InstrumentSelectAndMove))
            {
                e.Cancel = true;
                return;
            }
            SelectedItem = X.Select(simpleGL1.PointToClient(MousePosition));
            if (SelectedItem != null)
            {
                contextMenuStrip1.Items.Clear();
                ToolStripItem x = contextMenuStrip1.Items.Add(SelectedItem.Name);
                x.Enabled = false;
                ToolStripItem z = contextMenuStrip1.Items.Add(STranslation.T["RemoveObject"],null, x_MouseUp);
                x.Click += new EventHandler(x_MouseUp);
                z = contextMenuStrip1.Items.Add(STranslation.T["Copy"], null, z_MouseUp);
            }
            else
            {
                contextMenuStrip1.Items.Clear();
                ToolStripItem z = contextMenuStrip1.Items.Add(STranslation.T["Pastle"], null, p_MouseUp);
                if (PastleObj == null) z.Enabled = false;
                //ToolStripItem x = contextMenuStrip1.Items.Add("null");
                //x.Enabled = false;
            }
        }

        void x_MouseUp(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            X.DeleteObjectRay(SelectedItem);
        }
        void z_MouseUp(object sender, EventArgs e)
        {
            PastleObj = SelectedItem;
            string h = PastleObj.GenerateSaveString();
            PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), h);
        }
        void p_MouseUp(object sender, EventArgs e)
        {
            string h = PastleObj.GenerateSaveString();
            PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), h);
            PastleObj.Name = ObjectProto.GenName(PastleObj.GetTypeSpecifier(),
                                                 PastleObj.GetTypeSpecifier() ==
                                                 ObjectProto.GetSpec(ObjectTypes.Ray));
            int id = -1;
            if (!(PastleObj is Ray))
                ObjectCollection.Instance.AddObject(PastleObj);
            else
            {
                ((Ray)PastleObj).Angle = new Angle(((Ray)PastleObj).Angle.GetInDegrees() + 30, false);
                id = ObjectCollection.Instance.AddRay(PastleObj.Name, PastleObj.Coordinates, ((Ray)PastleObj).Angle,
                                                 ((Ray)PastleObj).BoundPoint, true);
            }
            if (PastleObj is Ray)
                SelectedItem = ObjectCollection.Instance.Select(ObjectCollection.Instance.GetRayByID(id));
            else
                SelectedItem = ObjectCollection.Instance.Select(PastleObj);
            if (!(PastleObj is Ray))
            {
                SystemCoordinates tmp = new SystemCoordinates(PastleObj.Coordinates);
                Point X = tmp;
                X.X += 10;
                X.Y += 10;
                PastleObj.Coordinates = new SystemCoordinates(X);
            }
            PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), PastleObj.GenerateSaveString());
        }
        #endregion

        #region Mouse Code
        private void simpleGL1_MouseUp(object sender, MouseEventArgs e)
        {
            key_down = false;
            if (e.Button == MouseButtons.Left)
            {
                activeInstrument.Action(new Point(e.X, e.Y));
                if (activeInstrument is InstrumentSelectAndMove)
                    SelectedItem = X.Select(new Point(e.X, e.Y));
                prev_point_set = false;
            }
            cursor_c = new Point(e.X, e.Y);
        }

        private void simpleGL1_MouseMove(object sender, MouseEventArgs e)
        {
            if (key_down && SelectedItem != null && activeInstrument is InstrumentSelectAndMove)
            {
                if (!prev_point_set)
                {
                    prevPoint = e.Location;
                    prev_point_set = true;
                    return;
                }
                if (SelectedItem is Ray)
                {
                    ObjectCollection.Instance.MoveTo(SelectedItem.Name, prevPoint, e.Location, true);
                }
                else
                    ObjectCollection.Instance.MoveTo(SelectedItem.Name, prevPoint, e.Location);
                prevPoint = e.Location;
            }
            if (key_down && activeInstrument is InstrumentHand)
            {
                if (!prev_point_set)
                {
                    prevPoint = e.Location;
                    prev_point_set = true;
                    return;
                }
                CoordinateSystem.Instance.DifferenceX = CoordinateSystem.Instance.DifferenceX +
                                                        (DoubleExtention) ((prevPoint.X - e.Location.X))/
                                                        CoordinateSystem.Instance.Scale;
                CoordinateSystem.Instance.DifferenceY = CoordinateSystem.Instance.DifferenceY -
                                                        (DoubleExtention) ((prevPoint.Y - e.Location.Y))/
                                                        CoordinateSystem.Instance.Scale;
                prevPoint = e.Location;
            }
        }

        private void simpleGL1_MouseDown(object sender, MouseEventArgs e)
        {
            if (activeInstrument is InstrumentHand && e.Button == MouseButtons.Left)
            {
                key_down = true;
            }
            if (activeInstrument is InstrumentSelectAndMove && e.Button == MouseButtons.Left)
            {
                SelectedItem = ObjectCollection.Instance.Select(e.Location);
                key_down = true;
            }
        }
        #endregion

        #region Timer Code
        private void timer2_Tick(object sender, EventArgs e)
        {
            SaveLoad.Instance.AutoSave();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            simpleGL1.Invalidate();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();
            ObjectCollection.Instance.Redraw();
            Point x = simpleGL1.PointToClient(Cursor.Position);
            if (x.X < 0 || x.Y < 0 || x.X > simpleGL1.Width || x.Y > simpleGL1.Height) return;
            else activeInstrument.Draw(x);
            Gl.glFlush();
            simpleGL1.Invalidate();
            foreach(var ins in instruments)
            {
                ins.DrawInstrumentPic();
                ins.ControlPressed((Control.ModifierKeys & Keys.Control) == Keys.Control);
                ins.AltPressed((Control.ModifierKeys & Keys.Alt) == Keys.Alt);
                ins.ShiftPressed((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
            }
        }
        #endregion

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                PastleObj = SelectedItem;
                string h = PastleObj.GenerateSaveString();
                PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), h);
            }
        }

        private void pastleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PastleObj != null)
            {
                string h = PastleObj.GenerateSaveString();
                PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), h);
                PastleObj.Name = ObjectProto.GenName(PastleObj.GetTypeSpecifier(),
                                                     PastleObj.GetTypeSpecifier() ==
                                                     ObjectProto.GetSpec(ObjectTypes.Ray));
                int id = -1;
                if (!(PastleObj is Ray))
                    ObjectCollection.Instance.AddObject(PastleObj);
                else
                {
                    ((Ray)PastleObj).Angle = new Angle(((Ray)PastleObj).Angle.GetInDegrees() + 30, false);
                    id = ObjectCollection.Instance.AddRay(PastleObj.Name, PastleObj.Coordinates, ((Ray)PastleObj).Angle,
                                                     ((Ray)PastleObj).BoundPoint, true);
                }
                if (PastleObj is Ray)
                    SelectedItem = ObjectCollection.Instance.Select(ObjectCollection.Instance.GetRayByID(id));
                else
                    SelectedItem = ObjectCollection.Instance.Select(PastleObj);
                if (!(PastleObj is Ray))
                {
                    SystemCoordinates tmp = new SystemCoordinates(PastleObj.Coordinates);
                    Point X = tmp;
                    X.X += 10;
                    X.Y += 10;
                    PastleObj.Coordinates = new SystemCoordinates(X);
                }
                PastleObj = SaveLoad.Instance.ConstructObject(PastleObj.GetTypeSpecifier(), PastleObj.GenerateSaveString());
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frm_hlp.Owner = this;
            frm_hlp.Show();
            //Help.ShowHelp(this, Application.StartupPath + "\\Документация.chm");
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_cfg.TopMost = true;
            frm_cfg.Show();
        }


    }
}
