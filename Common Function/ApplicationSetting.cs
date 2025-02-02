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
                else if(control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    
                    // 移除舊的設定值（如果存在）
                    config1.AppSettings.Settings.Remove(comboBox.Name);

                    // 添加新的設定值                   
                    string sIndex = comboBox.SelectedIndex.ToString();
                    config1.AppSettings.Settings.Add(control.Name, sIndex);
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
                else if(control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;

                    foreach (var value in Enum.GetValues(typeof(T)))
                    {
                        string eName = Enum.GetName(typeof(T), value);
                        if (eName == comboBox.Name)
                        {
                            int Index;
                            
                            if(int.TryParse(ApplicationInfo[(int)value], out Index))
                            {
                                comboBox.SelectedIndex = Index;
                            }
                            else
                            {
                                comboBox.SelectedIndex = -1;
                            }
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
        public static int Get_Int_Recipe(int element)
        {
            int Flag = -1;

            if(int.TryParse(ApplicationInfo[element], out Flag))
            {
                return Flag;
            }
            else
            {
                return -1;
            }
        }
        public static double Get_Double_Recipe(int element)
        {
            double Flag = -1.0;

            if (double.TryParse(ApplicationInfo[element], out Flag))
            {
                return Flag;
            }
            else
            {
                return -1.0;
            }
        }
        public static void SaveAllRecipe(Form form, string save_path = "default")
        {
            Configuration config = null;

            if (save_path == "default")
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            else
            {
                string customConfigPath = save_path;

                // 設定自訂的配置檔路徑
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = customConfigPath
                };

                config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            }

            TraverseControlsSave(form, config);

            // 儲存更改
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        public static void ReadAllRecipe<T>(string read_path = "default") where T:Enum
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = read_path
            };

            foreach (var value in Enum.GetValues(typeof(T)))
            {
                string eName = Enum.GetName(typeof(T), value);

                if(read_path == "default")
                {
                    var appSettings = ConfigurationManager.AppSettings;
                    ApplicationInfo[(int)value] = appSettings[eName] ?? "Not Found";
                }
                else
                {
                    Configuration customConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                    KeyValueConfigurationCollection appSettings = customConfig.AppSettings.Settings;
                    ApplicationInfo[(int)value] = appSettings[eName]?.Value;
                }
            }
        }
        public static void UpdataRecipeToForm<T>(Form form, string save_path = "default") where T:Enum
        {
            Configuration config = null;

            if (save_path == "default")
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            else
            {
                string customConfigPath = save_path;

                // 設定自訂的配置檔路徑
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = customConfigPath
                };

                config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            }

            TraverseControlsUpdate<T>(form, config);
        }
    }
}
