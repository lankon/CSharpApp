using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using CommonFunction;

namespace Mapping
{
    public partial class F_OneToOne : Form
    {
        #region parameter define 
        Tool tool = new Tool();
        
        FileInformation File1 = new FileInformation();
        FileInformation File2 = new FileInformation();     
        private struct FileInformation
        {
            //public int MapSize;
            //public int ShiftX;
            //public int ShiftY;
            //public int CellCount;
            //public int[] ValueRegionCount;

            //public float GridSize;

            //public double[] ValueRegion;

            public List<Dictionary<string, string>> CellInfo;

            //public List<Color> ColorList;
        }
        #endregion

        #region private function
        private List<Dictionary<string, string>> ReadCsvFile(String Path)
        {
            // 使用 Dictionary 來儲存資料
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            bool StartReadFile = false;
            string s_PosX = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);

            try
            {
                // 使用 StreamReader 來讀取檔案
                using (StreamReader sr = new StreamReader(Path))
                {
                    if (sr == null) return data;

                    string[] headers = new string[200];// = sr.ReadLine().Split(',');

                    // 逐行讀取 CSV 檔案
                    while (!sr.EndOfStream)
                    {
                        // 讀取一行
                        string line = sr.ReadLine();

                        // 使用逗號分隔解析欄位
                        string[] fields = line.Split(',');


                        if (StartReadFile == false)
                        {
                            for (int i = 0; i < fields.Count(); i++)
                            {
                                headers[i] = fields[i].Trim();
                                if (fields[i].Trim() == s_PosX)
                                {
                                    StartReadFile = true;
                                }
                            }
                        }

                        if (StartReadFile == true)
                        {
                            // 使用 Dictionary 來儲存每一行的資料
                            Dictionary<string, string> row = new Dictionary<string, string>();

                            // 將資料與欄位名稱對應起來
                            for (int i = 0; i < fields.Length; i++)
                            {
                                row[headers[i]] = fields[i];
                            }

                            // 將此行的資料加入到 List 中
                            data.Add(row);
                        }
                    }

                    return data;
                }
            }
            catch
            {
                return data;
            }
        }
        private string[] InsertTestItemToCmbx(ComboBox Cmbx, Dictionary<string, string> dictTestItem)
        {
            int i = 0;
            int len = 500;
            string[] Item = new string[len];

            foreach (var kvp in dictTestItem)
            {
                string key = kvp.Key;
                string value = kvp.Value;

                Item[i] = value;

                i++;
                if (i > len)
                {
                    tool.SaveHistoryToFile($"TestItemt超過{len}項");
                    break;
                }

                Cmbx.Items.Add(value.Trim());
            }

            Cmbx.SelectedIndex = 0;
            return Item;
        }
        private double[] CompareData(string key, List<Dictionary<string, string>> data1, List<Dictionary<string, string>> data2)
        {
            double[] array_diff = new double[500000];

            if (data1.Count != data2.Count)
            {
                tool.SaveHistoryToFile("相互比較的兩個檔案資料個數不同");
                return array_diff;
            }
                      
            for(int i=1; i<data1.Count; i++)
            {
                data1[i].TryGetValue(key, out string value1);
                data2[i].TryGetValue(key, out string value2);

                double d_value1 = tool.StringToDouble(value1);
                double d_value2 = tool.StringToDouble(value2);
                double difference = d_value1 - d_value2;

                array_diff[i] = difference;
            }

            return array_diff;
        }
        #endregion

        #region public function
        public void SetF_Setting(Panel pnl, F_OneToOne form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion

        public F_OneToOne()
        {
            InitializeComponent();
        }

        private void Btn_LoadFile1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFileName = "";

            // 設置文件選擇對話框的屬性
            openFileDialog.Title = "Select TestData";
            openFileDialog.Filter = "TestData|*.csv|All|*.*";

            // 如果用戶選擇了文件，顯示文件名
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
            }

            TxtBx_FilePath1.Text = selectedFileName;

            File1.CellInfo = ReadCsvFile(selectedFileName);

            if (File1.CellInfo.Count == 0)
            {
                MessageBox.Show("Read File Error");
                tool.SaveHistoryToFile("讀取檔案失敗");
                return;
            }

            InsertTestItemToCmbx(Cmbx_TestItem, File1.CellInfo[0]);
        }

        private void Btn_LoadFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFileName = "";

            // 設置文件選擇對話框的屬性
            openFileDialog.Title = "Select TestData";
            openFileDialog.Filter = "TestData|*.csv|All|*.*";

            // 如果用戶選擇了文件，顯示文件名
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
            }

            TxtBx_FilePath2.Text = selectedFileName;

            File2.CellInfo = ReadCsvFile(selectedFileName);

            if (File2.CellInfo.Count == 0)
            {
                MessageBox.Show("Read File Error");
                tool.SaveHistoryToFile("讀取檔案失敗");
                return;
            }
        }

        private void Btn_Compare_Click(object sender, EventArgs e)
        {
            if(File1.CellInfo == null || File2.CellInfo == null)
            {
                tool.SaveHistoryToFile("未載入資料");
                MessageBox.Show("Please Load Data");
                return;
            }
            
            CompareData("Ith", File1.CellInfo, File2.CellInfo);
        }
    }
}
