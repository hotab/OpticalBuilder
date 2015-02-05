using System;
using System.Collections.Generic;
 
using System.Text;
using System.IO;
using System.Windows.Forms;
using OpticalBuilderLib.EventArguments;
namespace OpticalBuilderLib.Configuration
{
    public class Translation
    {
        private static bool loaded = false;
        public static event EventHandler<LanguageChangeArgs> LanguageChange;
        private static Dictionary<string, string> Translations = new Dictionary<string, string>();
        private static string lang;
        public static string Lang { get { return lang; } }
        public static bool Loaded { get { return loaded; } }
        private static int ReadFile(string new_lang)
        {
            if (new_lang == lang)
            {
                return 1;
            }
            if (File.Exists(Application.StartupPath + "/Languages/" + new_lang + ".obl") && File.Exists(Application.StartupPath + "/Languages/keys.obl"))
            {
                FileStream ToRead = new FileStream(Application.StartupPath + "/Languages/" + new_lang + ".obl", FileMode.Open);
                StreamReader Reader = new StreamReader(ToRead);
                FileStream ToReadKeys = new FileStream(Application.StartupPath + "/Languages/keys.obl", FileMode.Open);
                StreamReader Keys = new StreamReader(ToReadKeys);
                Translations = new Dictionary<string, string>();
                while (Reader.EndOfStream == false && Keys.EndOfStream == false)
                    Translations.Add(Keys.ReadLine(), Reader.ReadLine());
                Reader.Close();
                Keys.Close();
                return 1;
            }
            else
            {
                if (lang == null)
                    throw new Exception("Error while loading translations");
                string H = " ";
                if (LanguageChange != null)
                    LanguageChange.Invoke(H, new LanguageChangeArgs(lang));
                return 0;
            }
        }
        public static void InitTranslations(string lng)
        {
            
            if (ReadFile(lng) == 1)
            {
                lang = lng;
                Configuration.SConfig.C["language"]=lng;
                string H = " ";
                loaded = true;
                if (LanguageChange != null)
                    LanguageChange.Invoke(H, new LanguageChangeArgs(lng));
            }
        }
        public static string GetTranslation(string Key)
        {
            if (loaded == false)
                throw new Exception("Translation not loaded");
            else
            {
                string toreturn;
                if (Translations.TryGetValue(Key, out toreturn))
                    return toreturn;
                else
                    return null;
            }
        }
        public string this[string Key]
        {
            get
            {
                return Translation.GetTranslation(Key);
            }
        }
        public static void ConfigChangeHandle(object s, ConfigurationChangeArgs e)
        {
            if (e.FullReload == false) return;
            string new_lang = OpticalBuilderLib.Configuration.SConfig.C["language"];
            InitTranslations(new_lang);
        }
    }
}
