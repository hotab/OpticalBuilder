using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using OpticalBuilder;
using OpticalBuilderLib.EventArguments;
namespace OpticalBuilderLib.Configuration
{
    
    public class Config
    {
        static bool loaded = false;
        public static string StdConfigPattern;
        static Dictionary<string, string> ConfigurationInfo = new Dictionary<string,string>();
        public static event EventHandler<ConfigurationChangeArgs> NewConfig;
        public static void Read()
        {
            if (loaded == true) return;
            try
            {
                FileStream Configu;
                try
                {
                    Configu = new FileStream(Application.StartupPath + "/Configuration.obc", FileMode.Open);
                }
#pragma warning disable 0168
                catch (System.IO.FileNotFoundException e)
                {
                    Config.GenStandartFile();
                    return;
                }
#pragma warning restore 0168
                StreamReader ConfigReader = new StreamReader(Configu);
                string param;
                while (ConfigReader.EndOfStream == false)
                {
                    param = ConfigReader.ReadLine();
                    param.Trim();
                    if (param.Length < 2) continue;
                    if (param[0] == '/' && param[1] == '/') continue;
                    string[] read = param.Split('=');
                    if (read.Length == 2)
                    {
                        if (ConfigurationInfo.ContainsKey(read[0]) == false)
                            ConfigurationInfo.Add(read[0], read[1]);
                    }
                }
                ConfigReader.Close();
                string h = " ";
                loaded = true;
                if (NewConfig != null) NewConfig.Invoke(h, new ConfigurationChangeArgs(true, "AllParams"));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void ReadMissing()
        {
            FileStream Configu;
            Configu = new FileStream(Application.StartupPath + "/tmp/Configuration.obc", FileMode.Open);
            StreamReader ConfigReader = new StreamReader(Configu);
            string param;
            while (ConfigReader.EndOfStream == false)
            {
                param = ConfigReader.ReadLine();
                param.Trim();
                if (param[0] == '/' && param[1] == '/') continue;
                string[] read = param.Split('=');
                if (read.Length == 2)
                {
                    string value;
                    if(ConfigurationInfo.TryGetValue(read[0], out value) == false) ConfigurationInfo.Add(read[0], read[1]);
                }
            }
            ConfigReader.Close();
            File.Delete(Application.StartupPath + "/tmp/Configuration.obc");
            Directory.Delete(Application.StartupPath + "/tmp");
        }
        public static void Save()
            {
                FileStream save = new FileStream(Application.StartupPath+"/Configuration.obc", FileMode.Create);
                StreamWriter saver = new StreamWriter(save);
                foreach (var pair in ConfigurationInfo)
                    saver.WriteLine(pair.Key + "=" + pair.Value);
                saver.Close();
            }
        public static void AddLanguage(string FilePath)
        {
            string fname = "lng_";
            int id = 1;
            while(File.Exists(Application.StartupPath + "\\Languages\\" + fname + id.ToString() + ".obl"))
                ++id;
            if(!(File.Exists(FilePath)))
            {
                MessageBox.Show(STranslation.T["LangFileNotFound"]);
                return;
            }
            string shortName = fname + id.ToString();
            string fullName;
            try
            {
                File.Copy(FilePath, Application.StartupPath + "\\Languages\\" + fname + id.ToString() + ".obl");
                FileStream a = new FileStream(Application.StartupPath + "\\Languages\\" + fname + id.ToString() + ".obl", FileMode.Open, FileAccess.Read);
                StreamReader b = new StreamReader(a);
                fullName = b.ReadLine();
                b.Close();
            }
            catch 
            {
                MessageBox.Show(STranslation.T["FSError"]);
                return;
            }
            
            string __enum = GetKeyValue("language.enum");
            __enum = __enum+','+shortName;
            string __key = "language." + shortName;
            SetKeyValue("language.enum",__enum, true);
            SetKeyValue(__key,fullName, true);
            Translation.ConfigChangeHandle(new String(' ',1), new ConfigurationChangeArgs(true,"null"));
            Form1.form.NewConfiguration(new String(' ', 1), new ConfigurationChangeArgs(true, "null"));
            MessageBox.Show(STranslation.T["LangAdded"]);
        }

        public static string GetKeyValue(string Key)
        {
            string returnstr;
            if (loaded == false)
                Read();
            if (ConfigurationInfo.TryGetValue(Key, out returnstr))
                return returnstr;
            else
            {
                if (Key == "dev_menu") return "0";
                string FilePattern = StdConfigPattern;
                Directory.CreateDirectory(Application.StartupPath + "/tmp");
                FileStream save = new FileStream(Application.StartupPath + "/tmp/Configuration.obc", FileMode.Create);
                StreamWriter saver = new StreamWriter(save);
                saver.WriteLine(FilePattern);
                saver.Close();
                ReadMissing();
                if (ConfigurationInfo.TryGetValue(Key, out returnstr))
                    return returnstr;
                else
                    return null;
            }
        }
        public static void SetKeyValue(string Key, string NewValue, bool lng = false)
        {
            if(lng)
            {
                string oldValue2;
                if (ConfigurationInfo.TryGetValue(Key, out oldValue2) == false)
                    ConfigurationInfo.Add(Key, NewValue);
                else
                {
                    ConfigurationInfo.Remove(Key);
                    ConfigurationInfo.Add(Key, NewValue);
                }
                return;
            }
            string oldValue;
            if (Key.Split('.')[0] == "language" && Key != "language") return;
            if (ConfigurationInfo.TryGetValue(Key, out oldValue) == false)
                ConfigurationInfo.Add(Key, NewValue);
            else
            {
                ConfigurationInfo.Remove(Key);
                ConfigurationInfo.Add(Key, NewValue);
            }
            if (Key == "language") return;
            string H = " ";
            if (NewConfig != null)
            NewConfig.Invoke(H,new ConfigurationChangeArgs(false,Key));
        }
        public static void GenStandartFile()
        {
            string FilePattern = StdConfigPattern;
            FileStream save = new FileStream(Application.StartupPath + "/Configuration.obc", FileMode.Create);
            StreamWriter saver = new StreamWriter(save);
            saver.WriteLine(FilePattern);
            saver.Close();
            Read();
        }
        public string this[string Key]
        {
            get
            {
                return Config.GetKeyValue(Key);
            }
            set
            {
                Config.SetKeyValue(Key, value);
            }
        }
        public static string[] GetKeys()
        {
            string[] ToReturn;
            if (Config.GetKeyValue("dev_menu") == "1")
            {
                ToReturn = new string[ConfigurationInfo.Keys.Count];
                int nxt = 0;
                foreach (string x in ConfigurationInfo.Keys)
                {
                    ToReturn[nxt] = x;
                    nxt++;
                }
                return ToReturn;
            }
            else
                throw new Exception("This feature is only in dev mode");
        }
        public static bool KeyExists(string Key)
        {
            string returnstr;
            return ConfigurationInfo.TryGetValue(Key, out returnstr);
        }
    }
    
}
