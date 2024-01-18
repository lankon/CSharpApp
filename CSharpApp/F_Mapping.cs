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

namespace CSharpApp
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
            public float GridSize;
            public List<Dictionary<string, string>> CellInfo;
        }

        private Dictionary<string, int> data;

        public F_Mapping()
        {
            InitializeComponent();

            //初始化数据
           data = new Dictionary<string, int>
           {
                {"1.75", 30},
                {"1.76", 50},
                {"1.77", 20},
                {"1.89", 200},
                {"1.90", 750},
                {"1.91", 208},
                {"2.00", 1000}
           };
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    // 获取绘图对象
        //    Graphics g = e.Graphics;

        //    // 设置绘图区域
        //    int chartWidth = 300;  // 图表宽度
        //    int chartHeight = 30 * data.Count; // 图表高度
        //    int startX = 50; // 图表起始横坐标
        //    int startY = 50; // 图表起始纵坐标

        //    // 绘制横向条形图
        //    foreach (var kvp in data)
        //    {
        //        int barWidth = kvp.Value * 5; // 条形宽度，根据数据值设置
        //        int barHeight = 20; // 条形高度

        //        // 绘制条形
        //        g.FillRectangle(Brushes.Blue, startX, startY, barWidth, barHeight);

        //        // 绘制文本
        //        g.DrawString($"{kvp.Key}: {kvp.Value}", Font, Brushes.Black, startX + barWidth + 5, startY);

        //        // 更新下一个条形的起始纵坐标
        //        startY += 30;
        //    }
        //}

        public void DrawColorbar(Panel Pnl, int[] ValueRegionCount, List<Color> ColorList)
        {
            //int chartWidth = 300;  
            //int chartHeight = 30 * data.Count; 
            float startX = 25; // 图表起始横坐标
            float startY = 20; // 图表起始纵坐标

            using (Graphics g = Pnl.CreateGraphics())
            {
                //double Max = 0;
                double Max = ValueRegionCount.Max();

                RectangleF drawRect1 = new RectangleF(0, 0, Pnl.Width, Pnl.Height);
                g.FillRectangle(Brushes.Black, drawRect1);

                //foreach (var kvp in data)
                //{
                //    if(kvp.Value > Max)
                //    {
                //        Max = kvp.Value;
                //    }
                //}

                int i = 0;
                Color CellColor;

                foreach (int kvp in ValueRegionCount)
                //foreach (var kvp in data)
                {
                    

                    //float barWidth = (float)(kvp.Value * (Pnl.Width - startX - 30)/Max); // 条形宽度，根据数据值设置
                    float barWidth = (float)(kvp * (Pnl.Width - startX - 30) / Max); // 条形宽度，根据数据值设置
                    float barHeight = 20; // 条形高度

                    // 绘制条形
                    CellColor = ColorList[i];
                    SolidBrush solidBrush = new SolidBrush(CellColor);
                    RectangleF drawRect = new RectangleF(startX, startY, barWidth, barHeight);
                    g.FillRectangle(solidBrush, drawRect);

                    //g.FillRectangle(Brushes.Blue, startX, startY, barWidth, barHeight);

                    // 绘制文本
                    //g.DrawString($"{kvp.Key}", Font, Brushes.White, startX, startY-15);
                    //g.DrawString($"{kvp.Key}", Font, Brushes.White, 0, startY-7);

                    // 更新下一个条形的起始纵坐标
                    //startY = startY + barHeight+20;
                    startY = startY + barHeight;

                    i++;
                }
            }            
        }

        public List<Dictionary<string, string>> ReadCsvFile(String Path)
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

                        if (fields[0].Trim() == "PosX")
                        {
                            for (int i = 0; i < fields.Count(); i++)
                            {
                                headers[i] = fields[i].Trim();
                            }

                            StartReadFile = true;
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
        public void InsertTestItemToCmbx(ComboBox Cmbx, Dictionary<string, string> dictTestItem)
        {
            foreach (var kvp in dictTestItem)
            {
                string key = kvp.Key;
                string value = kvp.Value;

                Cmbx.Items.Add(value.Trim());
            }

            Cmbx_TestItem.SelectedIndex = 0;
        }
        public Dictionary<string, object> FindMapInfo(int MapSize, List<Dictionary<string, string>> data)
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
        public List<Dictionary<string, string>> ReadCellInfo(string Path)
        {
            List<Dictionary<string, string>> CellInfo = null;
            CellInfo = ReadCsvFile(Path);
            return CellInfo;
        }
        public int HueToRGB(int hue)
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
        public Color ColorFromHue(int hue)
        {
            // 根据色相创建 RGB 颜色
            return Color.FromArgb(255, HueToRGB(hue + 120), HueToRGB(hue), HueToRGB(hue - 120));
        }     
        public List<Color> SetCellColor(double Start, double End, double Step)
        {
            int iStep = (int)((End - Start) / Step);

            List<Color> ColorList = new List<Color>();

            for (int i = 0; i < iStep+2; i++)
            {
                Color currentColor = ColorFromHue((i * 360 / iStep) % 360);

                ColorList.Add(currentColor);
            }

            return ColorList;
        }
        public double[] SetValueRegion(double Start, double Step, List<Color> ColorList)
        {
            double[] ValueRegion = new double[ColorList.Count() + 1];
            for (int i = 0; i < ValueRegion.Count(); i++)
            {
                ValueRegion[i] = Start + i * Step;
            }

            return ValueRegion;
        }
        public int[] DrawMapping(Panel Pnl, float GridSize, List<Dictionary<string,string>> CellInfo,int ShiftX, int ShiftY,
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
                            ValueRegionCount[ColorList.Count() - 2]++;
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
        public void DrawCell(Panel Pnl, float gridSize, int PosX, int PosY, Color CellColor)
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
        public void SetDrawSize(Panel Pnl, int MapSize)
        {
            Pnl.ClientSize = new Size(MapSize, MapSize);
        }      
        public void ClearMapping(Panel Pnl)
        {
            using (Graphics g = Pnl.CreateGraphics())
            {
                RectangleF drawRect1 = new RectangleF(0, 0, Pnl.Width, Pnl.Height);
                g.FillRectangle(Brushes.Black, drawRect1);
            }
        }
        public void DrawGrid(Panel Pnl, float gridSize)
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
                           
            List<Color> ColorList = new List<Color>();
            Dictionary<string, object> myDictionary = null;
            double[] ValueRegion = null;
            int[] ValueRegionCount = null;
            double Start = tool.StringToDouble(TxtBx_Start.Text);
            double End = tool.StringToDouble(TxtBx_End.Text);
            double Step = tool.StringToDouble(TxtBx_Step.Text);
            String TestItem = Cmbx_TestItem.Text;

            mapInformation.MapSize = 500;

            SetDrawSize(Pnl_Mapping, mapInformation.MapSize);

            ClearMapping(Pnl_Mapping);

            myDictionary = FindMapInfo(mapInformation.MapSize, mapInformation.CellInfo);
            mapInformation.ShiftX = (int)myDictionary["ShiftX"];
            mapInformation.ShiftY = (int)myDictionary["ShiftY"];
            mapInformation.CellCount = (int)myDictionary["CellCount"];
            mapInformation.GridSize = (float)myDictionary["GridSize"];

            ColorList = SetCellColor(Start, End, Step);
           
            ValueRegion = SetValueRegion(Start, Step, ColorList);

            ValueRegionCount = DrawMapping(Pnl_Mapping, mapInformation.GridSize, mapInformation.CellInfo, 
                                            mapInformation.ShiftX, mapInformation.ShiftY,
                                            TestItem, ColorList, ValueRegion);

            DrawColorbar(Pnl_Colorbar, ValueRegionCount, ColorList);

            tool.SaveHistoryToFile("繪圖完成");
        }

        

        private void F_Mapping_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DrawColorbar(Pnl_Colorbar);
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

            InsertTestItemToCmbx(Cmbx_TestItem, mapInformation.CellInfo[0]);
        }
    }
}
