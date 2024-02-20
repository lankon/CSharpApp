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
using System.Runtime.InteropServices;

namespace Mapping
{
    public partial class F_Mapping : Form
    {
        
        Tool tool = new Tool();
        MapInformation mapInformation = new MapInformation();
        private struct MapInformation
        {
            public int MapSize;
            public int ShiftX;
            public int ShiftY;
            public int CellCount;
            public int[] ValueRegionCount;

            public float GridSize;

            public double[] ValueRegion;

            public List<Dictionary<string, string>> CellInfo;

            public List<Color> ColorList;
        }

        public F_Mapping()
        {
            InitializeComponent();            
        
        }

        public void SetF_Mapping(Panel pnl, F_Mapping form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);
        }
        
        
        private void DrawColorbar(Panel Pnl, int[] ValueRegionCount, List<Color> ColorList, double[] ValueRegion)
        {
            float startX = 25; // 图表起始横坐标
            float startY = 20; // 图表起始纵坐标

            using (Graphics g = Pnl.CreateGraphics())
            {
                double Max = ValueRegionCount.Max();

                RectangleF drawRect1 = new RectangleF(0, 0, Pnl.Width, Pnl.Height);
                g.FillRectangle(Brushes.Black, drawRect1);

                int i = 0;
                Color CellColor;

                foreach (int kvp in ValueRegionCount)
                {                                        
                    float barWidth = (float)(kvp * (Pnl.Width - startX - 50) / Max); // 条形宽度，根据数据值设置
                    float barHeight = 22; // 条形高度

                    // 绘制条形
                    CellColor = ColorList[i];
                    SolidBrush solidBrush = new SolidBrush(CellColor);
                    RectangleF drawRect = new RectangleF(startX, startY, barWidth, barHeight);
                    g.FillRectangle(solidBrush, drawRect);
                   
                    // 绘制文本
                    g.DrawString($"{kvp}", Font, Brushes.White, startX + barWidth + 5, startY+5);
                    
                    if(i <= ValueRegionCount.Count()-2)
                        g.DrawString($"{ValueRegion[i]}", Font, Brushes.White, 0, startY+15);

                    // 更新下一个条形的起始纵坐标
                    startY = startY + barHeight;

                    i++;
                }
            }            
        }
        private List<Dictionary<string, string>> ReadCsvFile(String Path)
        {
            // 使用 Dictionary 來儲存資料
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            bool StartReadFile = false;
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

                        
                        if(StartReadFile == false)
                        {
                            for (int i = 0; i < fields.Count(); i++)
                            {
                                headers[i] = fields[i].Trim();
                                if (fields[i].Trim() == "PosX")
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
        private void InsertTestItemToCmbx(ComboBox Cmbx, Dictionary<string, string> dictTestItem)
        {
            foreach (var kvp in dictTestItem)
            {
                string key = kvp.Key;
                string value = kvp.Value;

                Cmbx.Items.Add(value.Trim());
            }

            Cmbx_TestItem.SelectedIndex = 0;
        }
        private Dictionary<string, object> FindMapInfo(int MapSize, List<Dictionary<string, string>> data)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();

            #region 排序陣列
            int[] ArrayX = new int[data.Count-1];
            int[] ArrayY = new int[data.Count-1];

            for(int i = 1; i < data.Count; i++)
            {
                data[i].TryGetValue("PosX", out String value);                
                ArrayX[i-1] = Int32.Parse(value);
                data[i].TryGetValue("PosY", out String value1);
                ArrayY[i-1] = Int32.Parse(value1);
            }

            Array.Sort(ArrayX);
            Array.Sort(ArrayY);
            #endregion
           
            #region 尋找長邊大小
            int MinXPos = ArrayX.Min();
            int MaxXPos = ArrayX.Max();
            int MinYPos = ArrayY.Min();
            int MaxYPos = ArrayY.Max();

            int X_Len = MaxXPos - MinXPos;
            int Y_Len = MaxYPos - MinYPos;

            int MapCellCount;
            if (X_Len > Y_Len)
                MapCellCount = X_Len;
            else
                MapCellCount = Y_Len;

            myDictionary.Add("CellCount", MapCellCount);
            #endregion

            #region 決定Map GridSize大小
            float[] GridSizeArray = { 10f, 5f, 4f, 2f, 1f, 0.8f };
            float GridSize = 0f;

            for (int i = 0; i < GridSizeArray.Count(); i++)
            {
                if (MapSize / GridSizeArray[i] > MapCellCount)
                {
                    GridSize = GridSizeArray[i];
                    break;
                }
            }

            myDictionary.Add("GridSize", GridSize);
            #endregion

            #region 找中心點位移量
            int middleIndex = ArrayX.Length / 2;
            int ShiftX = (int)(MapSize / GridSize / 2) - ArrayX[middleIndex];
            int ShiftY = (int)(MapSize / GridSize / 2) - ArrayY[middleIndex];

            myDictionary.Add("ShiftX", ShiftX);
            myDictionary.Add("ShiftY", ShiftY);
            #endregion

            return myDictionary;
        }
        private List<Dictionary<string, string>> ReadCellInfo(string Path)
        {
            List<Dictionary<string, string>> CellInfo = null;
            CellInfo = ReadCsvFile(Path);
            return CellInfo;
        }
        private int HueToRGB(int hue)
        {
            // 辅助方法：将色相转换为 RGB 分量
            hue = (hue % 360 + 360) % 360; // 转换到 [0, 359] 范围

            int max = 255;
            int min = 0;

            if (hue >= 0 && hue < 60)
                return min + (int)((max - min) * hue / 60);
            else if (hue < 180)
                return max;
            else if (hue < 240)
                return min + (int)((max - min) * (240 - hue) / 60);
            else
                return min;
        }
        private Color ColorFromHue(int hue)
        {
            // 根据色相创建 RGB 颜色
            return Color.FromArgb(255, HueToRGB(hue + 120), HueToRGB(hue), HueToRGB(hue - 120));
        }
        private List<Color> SetCellColor(double Start, double End, double Step)
        {
            int iStep = (int)((End - Start) / Step);

            List<Color> ColorList = new List<Color>();

            for (int i = 0; i < iStep+2; i++)
            {
                Color currentColor = ColorFromHue((i * 360 / (iStep+2) % 360));

                ColorList.Add(currentColor);
            }

            return ColorList;
        }
        private double[] SetValueRegion(double Start, double Step, List<Color> ColorList)
        {
            double[] ValueRegion = new double[ColorList.Count() + 1];
            for (int i = 0; i < ValueRegion.Count(); i++)
            {
                ValueRegion[i] = Start + i * Step;
            }

            return ValueRegion;
        }
        private int[] DrawMapping(Panel Pnl, float GridSize, List<Dictionary<string,string>> CellInfo,int ShiftX, int ShiftY,
                                String TestItem, List<Color> ColorList, double[] ValueRegion)
        {         
            int[] ValueRegionCount = new int[ColorList.Count()];

            for(int i=0; i<ValueRegionCount.Count(); i++)
            {
                ValueRegionCount[i] = 0;
            }
            
            for (int i = 1; i < mapInformation.CellInfo.Count(); i++)
            {
                Color CellColor = Color.Black;

                CellInfo[i].TryGetValue("PosX", out String sPosX);
                CellInfo[i].TryGetValue("PosY", out String sPosY);
                CellInfo[i].TryGetValue(TestItem, out String sValue);

                for (int j = 0; j < ColorList.Count()-2; j++)
                {
                    if (Double.TryParse(sValue, out double dVale))
                    {
                        if (dVale < ValueRegion[0])
                        {
                            CellColor = ColorList[0];
                            ValueRegionCount[0]++;
                            break;
                        }
                        else if (ValueRegion[ColorList.Count() - 2] < dVale)
                        {
                            CellColor = ColorList[ColorList.Count()-1];
                            ValueRegionCount[ColorList.Count() - 1]++;
                            break;
                        }
                        else if (ValueRegion[j] <= dVale && dVale <= ValueRegion[j + 1] &&
                                 ColorList.Count() - 3 == j)
                        {
                            CellColor = ColorList[j+1];
                            ValueRegionCount[j + 1]++;
                            break;
                        }                        
                        else if (ValueRegion[j] <= dVale && dVale < ValueRegion[j + 1])
                        {
                            CellColor = ColorList[j+1];
                            ValueRegionCount[j + 1]++;                           
                            break;
                        }    
                    }
                    else
                    {
                        tool.SaveHistoryToFile("繪圖失敗,讀取測試值型態轉換錯誤");
                        break;
                    }
                }

                if (Int32.TryParse(sPosX, out int dPosX) &&
                    Int32.TryParse(sPosY, out int dPosY))
                {
                    dPosX = dPosX + ShiftX;
                    dPosY = dPosY + ShiftY;

                    DrawCell(Pnl, GridSize, dPosX, dPosY, CellColor);
                }
                else
                {
                    tool.SaveHistoryToFile("繪圖失敗,讀取座標型態轉換錯誤");
                    break;
                }
            }

            return ValueRegionCount;
        }
        private void DrawCell(Panel Pnl, float gridSize, int PosX, int PosY, Color CellColor)
        {
            using (Graphics g = Pnl.CreateGraphics())
            {
                SolidBrush solidBrush = new SolidBrush(CellColor);
                
                float X = gridSize + PosX*gridSize;
                float Y = Pnl.Height - 2*gridSize - PosY*gridSize;

                RectangleF drawRect = new RectangleF(X, Y, gridSize, gridSize);
                g.FillRectangle(solidBrush, drawRect);
            }
        }
        private void SetDrawSize(Panel Pnl, int MapSize)
        {
            Pnl.ClientSize = new Size(MapSize, MapSize);
        }
        private void ClearMapping(Panel Pnl)
        {
            using (Graphics g = Pnl.CreateGraphics())
            {
                RectangleF drawRect1 = new RectangleF(0, 0, Pnl.Width, Pnl.Height);
                g.FillRectangle(Brushes.Black, drawRect1);
            }
        }
        private void DrawGrid(Panel Pnl, float gridSize)
        {
            int gridCountX, gridCountY; // 網格數量
           
            // 計算網格數量
            gridCountX = (int)(Pnl.Width / gridSize);
            gridCountY = (int)(Pnl.Height / gridSize);

            using (Graphics g = Pnl.CreateGraphics())
            {
                // 使用黑色畫筆
                Pen pen = new Pen(Color.White);

                // 繪製垂直網格線
                for (int i = 1; i <= gridCountX - 1; i++)
                {
                    float x = i * gridSize;

                    g.DrawLine(pen, x, 0 + gridSize, x, Pnl.ClientSize.Height - gridSize);
                }

                // 繪製水平網格線
                for (int j = 1; j <= gridCountY-1; j++)
                {
                    float y = j * gridSize;
                    g.DrawLine(pen, 0 + gridSize, y, Pnl.ClientSize.Width-gridSize, y);
                }

                // 釋放畫筆資源
                pen.Dispose();
            }
        }
        private void Btn_DrawMap_Click(object sender, EventArgs e)
        {
            if (mapInformation.CellInfo == null)
            {
                MessageBox.Show("Please Load Wafer Data");
                tool.SaveHistoryToFile("未載入晶圓資料");
                return;
            }

            if(TxtBx_Start.Text == "" || TxtBx_End.Text == "" || TxtBx_Step.Text == "")
            {
                MessageBox.Show("Please Enter Draw Condition");
                tool.SaveHistoryToFile("未輸入晶圓繪圖條件");
                return;
            }            

            PicBx_Colorbar.Visible = false;
            PicBx_Mapping.Visible = false;

            Dictionary<string, object> myDictionary = null;
            double Start = tool.StringToDouble(TxtBx_Start.Text);
            double End = tool.StringToDouble(TxtBx_End.Text);
            double Step = tool.StringToDouble(TxtBx_Step.Text);
            String TestItem = Cmbx_TestItem.Text;

            mapInformation.MapSize = 500;

            if(Start > End)
            {
                MessageBox.Show("Start Large Than End");
                tool.SaveHistoryToFile("起始值比結束值大");
                return;
            }

            SetDrawSize(Pnl_Mapping, mapInformation.MapSize);

            ClearMapping(Pnl_Mapping);

            myDictionary = FindMapInfo(mapInformation.MapSize, mapInformation.CellInfo);
            mapInformation.ShiftX = (int)myDictionary["ShiftX"];
            mapInformation.ShiftY = (int)myDictionary["ShiftY"];
            mapInformation.CellCount = (int)myDictionary["CellCount"];
            mapInformation.GridSize = (float)myDictionary["GridSize"];
            
            mapInformation.ColorList = SetCellColor(Start, End, Step);

            mapInformation.ValueRegion =  SetValueRegion(Start, Step, mapInformation.ColorList);

            mapInformation.ValueRegionCount = DrawMapping(Pnl_Mapping, mapInformation.GridSize, mapInformation.CellInfo,
                                                          mapInformation.ShiftX, mapInformation.ShiftY,
                                                          TestItem, mapInformation.ColorList, mapInformation.ValueRegion);

            DrawColorbar(Pnl_Colorbar, mapInformation.ValueRegionCount,
                         mapInformation.ColorList, mapInformation.ValueRegion);
            
            tool.CaptureImage(Pnl_Colorbar, Application.StartupPath + @"\Pnl_Colorbar.png");
            tool.LoadImageToPicBx(PicBx_Colorbar, Application.StartupPath + @"\Pnl_Colorbar.png");
            tool.CaptureImage(Pnl_Mapping, Application.StartupPath + @"\Pnl_Mapping.png");
            tool.LoadImageToPicBx(PicBx_Mapping, Application.StartupPath + @"\Pnl_Mapping.png");

            PicBx_Colorbar.Visible = true;
            PicBx_Mapping.Visible = true;

            tool.SaveHistoryToFile("繪圖完成");
        }

        

        private void F_Mapping_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DrawColorbar(Pnl_Colorbar);
            string startupPath = Application.StartupPath;
            //button2.Text = startupPath;
            TxtBx_FilePath.Text = startupPath;
        }    

        private void Btn_LoadFile_Click(object sender, EventArgs e)
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

            TxtBx_FilePath.Text = selectedFileName;

            mapInformation.CellInfo = ReadCellInfo(selectedFileName);

            if (mapInformation.CellInfo.Count == 0)
            {
                MessageBox.Show("Read File Error");
                tool.SaveHistoryToFile("讀取檔案失敗");
                return;
            }

            Cmbx_TestItem.Items.Clear();

            InsertTestItemToCmbx(Cmbx_TestItem, mapInformation.CellInfo[0]);
        }

        private void Btn_CloseApp_Click(object sender, EventArgs e)
        {
            // 顯示確認對話框
            DialogResult dialogResult = MessageBox.Show("Close Application ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 根據用戶的選擇返回布爾值
            if (dialogResult == DialogResult.Yes)
            {
                ApplicationSetting.SaveAllRecipe(this);
                
                Application.Exit();
                tool.SaveHistoryToFile("關閉應用程式");
            }
            else
            {
                
            }
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            F_Setting f_Setting = new F_Setting();

            this.Hide();

            f_Setting.Show();

            f_Setting.Closed += (s, args) => this.Show();
        }
    }
}
