using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CommonFunction
{
    
    public static class ApplicationSetting
    {
        private static string[] ApplicationInfo = new string[2000];
        private static void TraverseControlsSave(Control parent, Configuration config1)
        {
            foreach (Control control in parent.Controls)
            {
                // 假設我們只儲存所有 TextBox 的 Text 屬性
                if (control is TextBox)
                {
                    // 移除舊的設定值（如果存在）
                    config1.AppSettings.Settings.Remove(control.Name);

                    // 添加新的設定值
                    config1.AppSettings.Settings.Add(control.Name, control.Text);
                }

                // 如果這個控件包含其他控件，遞歸遍歷它們
                if (control.HasChildren)
                {
                    TraverseControlsSave(control, config1);
                }
            }
        }
        private static void TraverseControlsUpdate<T>(Control parent, Configuration config1) where T : Enum
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    foreach (var value in Enum.GetValues(typeof(T)))
                    {
                        string eName = Enum.GetName(typeof(T), value);
                        if(eName == control.Name)
                        {
                            control.Text = ApplicationInfo[(int)value];
                        }
                    }
                }

                // 如果這個控件包含其他控件，遞歸遍歷它們
                if (control.HasChildren)
                {
                    TraverseControlsUpdate<T>(control, config1);
                }
            }
        }

        
        public static void SetRecipe(int element, string data)
        {
            ApplicationInfo[element] = data;
        }
        public static bool Get_Bool_Recipe(int element)
        {
            bool Flag = false;

            if (ApplicationInfo[element] == "0")
                return false;
            else if (ApplicationInfo[element] == "1")
                return true;

            if (bool.TryParse(ApplicationInfo[element], out Flag))
                return Flag;
            else
            {                
                return Flag;
            }
        }
        public static string Get_String_Recipe(int element)
        {
            return ApplicationInfo[element];
        }
        public static void SaveAllRecipe(Form form)
        {
            // 獲取當前應用程式的配置
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            TraverseControlsSave(form, config);

            // 儲存更改
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        public static void ReadAllRecipe<T>() where T:Enum
        {
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                string eName = Enum.GetName(typeof(T), value);

                var appSettings = ConfigurationManager.AppSettings;
                ApplicationInfo[(int)value] = appSettings[eName] ?? "Not Found";
            }
        }
        public static void UpdataRecipeToForm<T>(Form form) where T:Enum
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            TraverseControlsUpdate<T>(form, config);
        }
    }
}
