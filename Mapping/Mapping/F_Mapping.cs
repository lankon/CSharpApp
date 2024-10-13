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
using ClosedXML.Excel;

namespace Mapping
{
    
    public partial class F_Mapping : Form
    {
        #region parameter define 
        Tool tool = new Tool();
        Xlsx xlsx = new Xlsx();
        XLWorkbook file_xlsx;

        MapInformation mapInformation = new MapInformation();
        MapInformation SmallMap = new MapInformation();
        XY_Coord xy_record = new XY_Coord();    //記錄起始與結束的xy座標

        private Point startPoint;  // 滑鼠起始點
        private Point currentPoint;  // 滑鼠當前點
        private bool isDrawing = false;  // 是否在繪製中
        private bool isLargeImg = false;    //是否為放大圖
        private bool isMouseMove = false;   //判斷滑鼠是否有拖曳
        
        private struct XY_Coord
        {
            public int x_start;
            public int x_end;
            public int y_start;
            public int y_end;
        }
        private struct MapInformation
        {
            public int MapSize;
            public int ShiftX;
            public int ShiftY;
            public int MaxPosX;
            public int MinPosX;
            public int MaxPosY;
            public int MinPosY;
            //public int CellCount;
            public int[] ValueRegionCount;

            public float GridSize;

            public double[] ValueRegion;

            public List<Dictionary<string, string>> CellInfo;

            public List<Color> ColorList;
        }

        List<Dictionary<string, string>> TestItemCondition = new List<Dictionary<string, string>>();
        #endregion

        #region public function
        public void SetF_Mapping(Panel pnl, F_Mapping form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);
        }
        public void SavePicture()
        {
            Control ctrl = Pnl_Colorbar;

            // 創建一個與 Panel 大小相同的 Bitmap
            Bitmap bitmap = new Bitmap(730, 500);

            // 使用 Bitmap 的 Graphics 對象
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 定義 Panel 在螢幕上的位置
                Point panelLocationOnScreen = ctrl.PointToScreen(Point.Empty);

                // 將 Panel 當前的螢幕畫面複製到 Bitmap 上
                g.CopyFromScreen(panelLocationOnScreen, Point.Empty, bitmap.Size);
                //g.CopyFromScreen(panelLocationOnScreen, Point.Empty, ctrl.Size);
            }

            // 儲存 Bitmap 到檔案
            string path = Application.StartupPath + @"\Picture\" + TxtBx_ShowItem.Text + ".png";
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            // 釋放 Bitmap 資源
            bitmap.Dispose();
        }
        public void SaveMappingXlsx()
        {
            String TestItem = Cmbx_TestItem.Text;

            int[] xy_dir = new int[2];

            if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc) == 0)
                xy_dir[0] = 1;
            else
                xy_dir[0] = -1;

            if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc) == 0)
                xy_dir[1] = 1;
            else
                xy_dir[1] = -1;

            SaveAsXlsxMapping(mapInformation, TestItem, xy_dir);
        }
        #endregion

        #region private function
        private void InitialApplication()
        {
            tool.SaveHistoryToFile("開啟應用程式");

            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);

            LoadTestItemCondition();

            ShowHint();

        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Pnl_FormHint, "F_Mapping");
        }
        private void AddTestItemCondition()
        {
            Dictionary<string, string> row;

            for (int i = 0; i < TestItemCondition.Count; i++)
            {
                TestItemCondition[i].TryGetValue("Item", out string item);

                if (item == Cmbx_TestItem.Text)
                {
                    TestItemCondition.RemoveAt(i);
                    break;
                }
            }

            row = new Dictionary<string, string>();

            row["Item"] = Cmbx_TestItem.Text;
            row["Start"] = TxtBx_Start.Text;
            row["End"] = TxtBx_End.Text;
            row["Step"] = TxtBx_Step.Text;

            TestItemCondition.Add(row);

        }
        private void SaveTestItemCondition()
        {
            tool.SaveHistoryToFile("Save Test Item Condition Start");

            Dictionary<string, string> row = new Dictionary<string, string>();

            //寫檔案標題
            row["Item"] = "Item";
            row["Start"] = "Start";
            row["End"] = "End";
            row["Step"] = "Step";

            TestItemCondition[0].TryGetValue("Item", out string item);
            
            if (item != "Item")
                TestItemCondition.Insert(0, row);

            StreamWriter writer = tool.CreateFile("TestData\\TestItemCondition", ".txt", false);

            //寫入資料
            string context = "";
            int ii = 0;
            int jj = 0;
            foreach (var dict in TestItemCondition)
            {
                jj++; 

                foreach (var kvp in dict)
                {
                    ii++;

                    if (ii < dict.Count)
                        context = context + kvp.Value + ",";
                    else
                        context = context + kvp.Value;
                }

                tool.WriteFile(writer, context);

                context = "";
                ii = 0;
            }

            tool.CloseFile(writer);

            tool.SaveHistoryToFile("Save Test Item Condition End");
        }
        private void LoadTestItemCondition()
        {
            tool.SaveHistoryToFile("LoadTsetItemCodition Start");
            
            string path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\" + "TestData\\TestItemCondition.txt";

            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            TestItemCondition = tool.ReadCsvFile(path, true);

            tool.SaveHistoryToFile("LoadTsetItemCodition End");
        }
        private void UpdateTestItemConditionToForm()
        {
            for(int i=0; i<TestItemCondition.Count; i++)
            {
                TestItemCondition[i].TryGetValue("Item", out string item);

                if(item == Cmbx_TestItem.Text)
                {
                    TestItemCondition[i].TryGetValue("Start", out string start);
                    TestItemCondition[i].TryGetValue("End", out string end);
                    TestItemCondition[i].TryGetValue("Step", out string step);

                    TxtBx_Start.Text = start;
                    TxtBx_End.Text = end;
                    TxtBx_Step.Text = step;
                }
            }
        }
        private void DrawColorbar(Panel Pnl, int[] ValueRegionCount, List<Color> ColorList, double[] ValueRegion)
        {
            float startX = 25; // 图表起始横坐标
            float startY = 10; // 图表起始纵坐标

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
                    g.DrawString($"{kvp}", Font, Brushes.White, startX + barWidth + 5, startY + 5);

                    if (i <= ValueRegionCount.Count() - 2)
                        g.DrawString($"{ValueRegion[i]}", Font, Brushes.White, 0, startY + 15);

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
            string s_PosX = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);

            try
            {
                // 使用 StreamReader 來讀取檔案
                using (StreamReader sr = new StreamReader(Path))
                {
                    if (sr == null) return data;

                    string[] headers = new string[2000];// = sr.ReadLine().Split(',');

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
        private List<Dictionary<string, string>> ReadCsvFile_AMIDA(String Path)
        {
            // 使用 Dictionary 來儲存資料
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            bool StartReadFile = false;
            string s_PosX = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);
            int shift_row = 0;

            try
            {
                // 使用 StreamReader 來讀取檔案
                using (StreamReader sr = new StreamReader(Path))
                {
                    if (sr == null) return data;

                    string[] headers = new string[2000];// = sr.ReadLine().Split(',');

                    // 逐行讀取 CSV 檔案
                    while (!sr.EndOfStream)
                    {
                        // 讀取一行
                        string line = sr.ReadLine();

                        // 使用逗號分隔解析欄位
                        string[] fields = line.Split('\t'); //AMIDA特別項


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
                            if (shift_row < 1 || shift_row > 3)  //AMIDA特別項
                                data.Add(row);

                            shift_row++;
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
            int i = 0;

            foreach (var kvp in dictTestItem)
            {
                string key = kvp.Key;
                string value = kvp.Value;

                i++;
                if (i > 500)
                {
                    tool.SaveHistoryToFile("TestItemt超過500項");
                    break;
                }

                Cmbx.Items.Add(value.Trim());
            }

            Cmbx.SelectedIndex = 0;
        }
        private Dictionary<string, object> FindMapInfo(int MapSize, List<Dictionary<string, string>> data, int[] xy_direc)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();

            #region 排序陣列
            int[] ArrayX = new int[data.Count - 1];
            int[] ArrayY = new int[data.Count - 1];
            string sPosX = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);
            string sPosY = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_Y_KeyWord);

            for (int i = 1; i < data.Count; i++)
            {
                data[i].TryGetValue(sPosX, out String value);
                data[i].TryGetValue(sPosY, out String value1);

                if (value == "" || value1 == "")
                    break;

                ArrayX[i - 1] = Int32.Parse(value);
                ArrayY[i - 1] = Int32.Parse(value1);
            }

            Array.Sort(ArrayX);
            Array.Sort(ArrayY);
            #endregion

            #region 尋找長邊大小
            int MinXPos = ArrayX.Min();
            int MaxXPos = ArrayX.Max();
            int MinYPos = ArrayY.Min();
            int MaxYPos = ArrayY.Max();

            myDictionary.Add("MinPosX", MinXPos);
            myDictionary.Add("MinPosY", MinYPos);
            myDictionary.Add("MaxPosX", MaxXPos);
            myDictionary.Add("MaxPosY", MaxYPos);

            int X_Len = MaxXPos - MinXPos;
            int Y_Len = MaxYPos - MinYPos;

            int MapCellCount;
            if (X_Len > Y_Len)
                MapCellCount = X_Len;
            else
                MapCellCount = Y_Len;

            //myDictionary.Add("CellCount", MapCellCount);
            #endregion

            #region 決定Map GridSize大小
            float[] GridSizeArray = { 50f, 25f, 10f, 5f, 4f, 2f, 1f, 0.8f };
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
            int ShiftX = (int)(MapSize / GridSize / 2) - (MaxXPos + MinXPos) / 2 * xy_direc[0];
            int ShiftY = (int)(MapSize / GridSize / 2) - (MaxYPos + MinYPos) / 2 * xy_direc[1];

            ShiftX = ShiftX * xy_direc[0];
            ShiftY = ShiftY * xy_direc[1];

            myDictionary.Add("ShiftX", ShiftX);
            myDictionary.Add("ShiftY", ShiftY);
            #endregion

            return myDictionary;
        }
        private List<Dictionary<string, string>> ReadCellInfo(string Path)
        {
            List<Dictionary<string, string>> CellInfo = null;
            
            if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Customer) == 1)
            {
                CellInfo = ReadCsvFile_AMIDA(Path);
            }
            else
            {
                CellInfo = ReadCsvFile(Path);
            }

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

            for (int i = 0; i < iStep + 2; i++)
            {
                Color currentColor = ColorFromHue((i * 360 / (iStep + 2) % 360));

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
        private int[] DrawMapping(Panel Pnl, float GridSize, List<Dictionary<string, string>> CellInfo, int ShiftX, int ShiftY,
                                String TestItem, List<Color> ColorList, double[] ValueRegion, int[] xy_direc)
        {
            int[] ValueRegionCount = new int[ColorList.Count()];
            string s_PosX = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);
            string s_PosY = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_Y_KeyWord);


            for (int i = 0; i < ValueRegionCount.Count(); i++)
            {
                ValueRegionCount[i] = 0;
            }

            for (int i = 1; i < CellInfo.Count(); i++)
            {
                Color CellColor = Color.Black;

                CellInfo[i].TryGetValue(s_PosX, out String sPosX);
                CellInfo[i].TryGetValue(s_PosY, out String sPosY);
                CellInfo[i].TryGetValue(TestItem, out String sValue);

                for (int j = 0; j < ColorList.Count() - 2; j++)
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
                            CellColor = ColorList[ColorList.Count() - 1];
                            ValueRegionCount[ColorList.Count() - 1]++;
                            break;
                        }
                        else if (ValueRegion[j] <= dVale && dVale <= ValueRegion[j + 1] &&
                                 ColorList.Count() - 3 == j)
                        {
                            CellColor = ColorList[j + 1];
                            ValueRegionCount[j + 1]++;
                            break;
                        }
                        else if (ValueRegion[j] <= dVale && dVale < ValueRegion[j + 1])
                        {
                            CellColor = ColorList[j + 1];
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
                    dPosX = (dPosX + ShiftX) * xy_direc[0];
                    dPosY = (dPosY + ShiftY) * xy_direc[1];

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

                float X = gridSize + PosX * gridSize;
                float Y = Pnl.Height - 2 * gridSize - PosY * gridSize;

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
                for (int j = 1; j <= gridCountY - 1; j++)
                {
                    float y = j * gridSize;
                    g.DrawLine(pen, 0 + gridSize, y, Pnl.ClientSize.Width - gridSize, y);
                }

                // 釋放畫筆資源
                pen.Dispose();
            }
        }
        private bool CheckLoadFileCondition()
        {
            bool flag = true;

            if (ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord) == "")
            {
                flag = false;
                MessageBox.Show("Please Enter X Coordinate Key Word");
                tool.SaveHistoryToFile("X座標關鍵字未輸入");
            }

            if (ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_Y_KeyWord) == "")
            {
                flag = false;
                MessageBox.Show("Please Enter Y Coordinate Key Word");
                tool.SaveHistoryToFile("Y座標關鍵字未輸入");
            }

            return flag;
        }
        private void OpenXlsx()
        {
            //開檔太慢了用Thread先讀
            string path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Sample.xlsx";
            file_xlsx = xlsx.Open(path);

            if (file_xlsx == null)
                tool.SaveHistoryToFile("OpenXlsx開啟失敗");
        }
        private void SaveAsXlsxMapping(MapInformation map, String TestItem, int[] xy_dir)
        {
            tool.SaveHistoryToFile("儲存xlsx mapping start");

            if (file_xlsx == null)
            {
                MessageBox.Show("Save xlsx error");
                tool.SaveHistoryToFile("xlsx存檔失敗");
                return;
            }

            XlsxType type = new XlsxType();
            type = xlsx.DefaultType(type);

            string s_PosX = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);
            string s_PosY = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_Y_KeyWord);

            int offset_x = 6;
            int offset_y = 10;

            //輸出X座標
            for (int i = map.MinPosX; i <= map.MaxPosX; i++)
            {
                int pos_x;

                if (xy_dir[0] == 1)
                    pos_x = i - map.MinPosX + offset_x;
                else
                    pos_x = -i + map.MaxPosX + offset_x;

                //上側
                xlsx.SetType(file_xlsx, "Result", i - map.MinPosX + offset_x, offset_y - 1, type);
                xlsx.WriteValue(file_xlsx, "Result", pos_x, offset_y - 1, i);

                //下側
                xlsx.SetType(file_xlsx, "Result", i - map.MinPosX + offset_x,
                             offset_y + (map.MaxPosY - map.MinPosY) + 1, type);
                xlsx.WriteValue(file_xlsx, "Result", pos_x,
                                offset_y + (map.MaxPosY - map.MinPosY) + 1, i);
            }

            //輸出Y座標
            for (int i = map.MinPosY; i <= map.MaxPosY; i++)
            {
                int pos_y;

                if (xy_dir[1] == 1)
                    pos_y = i * -1 + map.MaxPosY + offset_y;
                else
                    pos_y = i - map.MinPosY + offset_y;

                //左側
                xlsx.SetType(file_xlsx, "Result", offset_x - 1, i - map.MinPosY + offset_y, type);
                xlsx.WriteValue(file_xlsx, "Result", offset_x - 1, pos_y, i);

                //右側
                xlsx.SetType(file_xlsx, "Result", offset_x + (map.MaxPosX - map.MinPosX) + 1,
                             i - map.MinPosY + offset_y, type);
                xlsx.WriteValue(file_xlsx, "Result", offset_x + (map.MaxPosX - map.MinPosX) + 1,
                                pos_y, i);
            }

            type.IsBold = false;    //取消粗體
            type.IsBackgroundColor = true;  //添加欄位底色

            //輸出測試值
            for (int i = 1; i < map.CellInfo.Count; i++)
            {
                map.CellInfo[i].TryGetValue(s_PosX, out String sPosX);
                map.CellInfo[i].TryGetValue(s_PosY, out String sPosY);
                map.CellInfo[i].TryGetValue(TestItem, out String sValue);

                int iPosX = tool.StringToInt(sPosX);
                int iPosY = tool.StringToInt(sPosY);
                double dValue = tool.StringToDouble(sValue);

                if (xy_dir[0] == 1)
                    iPosX = iPosX - map.MinPosX + offset_x;
                else
                    iPosX = -iPosX + map.MaxPosX + offset_x;

                if (xy_dir[1] == 1)
                    iPosY = iPosY * -1 + map.MaxPosY + offset_y;
                else
                    iPosY = iPosY - map.MinPosY + offset_y;

                xlsx.SetType(file_xlsx, "Result", iPosX, iPosY, type);
                xlsx.WriteValue(file_xlsx, "Result", iPosX, iPosY, dValue);
            }

            string path = System.IO.Directory.GetCurrentDirectory() + "\\" + $"TestData\\Mapping_{TestItem}.xlsx";
            bool res = xlsx.SaveAs(file_xlsx, path);

            if (!res)
            {
                MessageBox.Show("Save xlsx error");
                tool.SaveHistoryToFile("xlsx存檔失敗");
                return;
            }

            tool.SaveHistoryToFile("儲存xlsx mapping end");
            MessageBox.Show("Finish");
        }
        private List<Dictionary<string, string>> PickupMapInformation(List<Dictionary<string, string>> small_map,
                                                                      int x_start, int x_end, int y_start, int y_end)
        {
            #region 確保 end > start
            if (x_end < x_start)
            {
                int temp = x_start;
                x_start = x_end;
                x_end = temp;
            }

            if (y_end < y_start)
            {
                int temp = y_start;
                y_start = y_end;
                y_end = temp;
            }
            #endregion

            string s_PosX = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);
            string s_PosY = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_Y_KeyWord);

            for (int i = 1; i < small_map.Count; i++)
            {
                small_map[i].TryGetValue(s_PosX, out String sPosX);
                small_map[i].TryGetValue(s_PosY, out String sPosY);

                if (!Int32.TryParse(sPosX, out int dPosX) ||
                    !Int32.TryParse(sPosY, out int dPosY))
                {
                    tool.SaveHistoryToFile("繪圖失敗,讀取座標型態轉換錯誤");
                    break;
                }

                if (dPosX > x_end || dPosX < x_start ||
                   dPosY > y_end || dPosY < y_start)
                {
                    small_map.RemoveAt(i);  //刪除超出座標範圍的資料
                    i--;
                }
            }

            return small_map;
        }
        #endregion

        public F_Mapping()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_DrawMap_Click(object sender, EventArgs e)
        {
            if (mapInformation.CellInfo.Count == 0)
            {
                MessageBox.Show("Please Load Wafer Data");
                tool.SaveHistoryToFile("未載入晶圓資料");
                return;
            }

            if (TxtBx_Start.Text == "" || TxtBx_End.Text == "" || TxtBx_Step.Text == "")
            {
                MessageBox.Show("Please Enter Draw Condition");
                tool.SaveHistoryToFile("未輸入晶圓繪圖條件");
                return;
            }

            #region draw mapping
            PicBx_Colorbar.Visible = false;
            PicBx_Mapping.Visible = false;
            Labl_ShowCellValue.Visible = false;

            TxtBx_ShowItem.Text = Cmbx_TestItem.Text;

            Dictionary<string, object> myDictionary = null;
            double Start = tool.StringToDouble(TxtBx_Start.Text);
            double End = tool.StringToDouble(TxtBx_End.Text);
            double Step = tool.StringToDouble(TxtBx_Step.Text);
            String TestItem = Cmbx_TestItem.Text;
            int[] XY_Direc = new int[2];

            if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc) == 1)
                XY_Direc[0] = -1;
            else
                XY_Direc[0] = 1;

            if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc) == 1)
                XY_Direc[1] = -1;
            else
                XY_Direc[1] = 1;


            mapInformation.MapSize = 500;

            if (Start > End)
            {
                MessageBox.Show("Start Large Than End");
                tool.SaveHistoryToFile("起始值比結束值大");
                return;
            }

            SetDrawSize(Pnl_Mapping, mapInformation.MapSize);

            ClearMapping(Pnl_Mapping);

            myDictionary = FindMapInfo(mapInformation.MapSize, mapInformation.CellInfo, XY_Direc);
            mapInformation.ShiftX = (int)myDictionary["ShiftX"];
            mapInformation.ShiftY = (int)myDictionary["ShiftY"];
            mapInformation.MinPosX = (int)myDictionary["MinPosX"];
            mapInformation.MinPosY = (int)myDictionary["MinPosY"];
            mapInformation.MaxPosX = (int)myDictionary["MaxPosX"];
            mapInformation.MaxPosY = (int)myDictionary["MaxPosY"];
            //mapInformation.CellCount = (int)myDictionary["CellCount"];
            mapInformation.GridSize = (float)myDictionary["GridSize"];

            mapInformation.ColorList = SetCellColor(Start, End, Step);

            mapInformation.ValueRegion = SetValueRegion(Start, Step, mapInformation.ColorList);

            mapInformation.ValueRegionCount = DrawMapping(Pnl_Mapping, mapInformation.GridSize, mapInformation.CellInfo,
                                                          mapInformation.ShiftX, mapInformation.ShiftY,
                                                          TestItem, mapInformation.ColorList, mapInformation.ValueRegion, XY_Direc);

            DrawColorbar(Pnl_Colorbar, mapInformation.ValueRegionCount,
                         mapInformation.ColorList, mapInformation.ValueRegion);
                     
            tool.CreateFolder(Application.StartupPath + @"\Temp");
            tool.CaptureImage(Pnl_Colorbar, Application.StartupPath + @"\Temp\Pnl_Colorbar.png");
            tool.LoadImageToPicBx(PicBx_Colorbar, Application.StartupPath + @"\Temp\Pnl_Colorbar.png");
            tool.CaptureImage(Pnl_Mapping, Application.StartupPath + @"\Temp\Pnl_Mapping.png");
            tool.LoadImageToPicBx(PicBx_Mapping, Application.StartupPath + @"\Temp\Pnl_Mapping.png");

            isLargeImg = false;
            PicBx_Colorbar.Visible = true;
            PicBx_Mapping.Visible = true;
            
            tool.SaveHistoryToFile("繪圖完成");
            #endregion

            AddTestItemCondition();
            SaveTestItemCondition();

            #region copy cell data
            SmallMap.CellInfo = new List<Dictionary<string, string>>();

            foreach (var dictionary in mapInformation.CellInfo)
            {
                // 複製字典
                Dictionary<string, string> newDict = new Dictionary<string, string>(dictionary);
                SmallMap.CellInfo.Add(newDict);
            }
            #endregion

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        private void F_Mapping_Load(object sender, EventArgs e)
        {
            
        }

        private void Btn_LoadFile_Click(object sender, EventArgs e)
        {           
            Licence licence = new Licence();
            if (!licence.CheckLicence())
            {
                MessageBox.Show("Licence Error");
                Application.Exit();
                return;
            }

            if (CheckLoadFileCondition() == false)
                return;

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

            Task task = Task.Run(() => OpenXlsx());
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

        private void Cmbx_TestItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTestItemConditionToForm();
        }

        private int[] GetCoordinate(Point mouse_pos, Panel map_panel, 
                                    float gridSize, int x_dir, int y_dir,
                                    int shift_x, int shift_y)
        {
            //獲取點擊位置的座標
            int x = mouse_pos.X;
            int y = mouse_pos.Y;

            //float gridSize = mapInformation.GridSize;
            int[] XY_Direc = new int[2];
            float f_x, f_y;

            //將點擊位置X座標轉換成晶粒X座標
            if (x_dir == 1)
            {
                XY_Direc[0] = -1;
                f_x = (x - gridSize) / gridSize * XY_Direc[0] - shift_x + 1;
            }
            else
            {
                XY_Direc[0] = 1;
                f_x = (x - gridSize) / gridSize * XY_Direc[0] - shift_x;
            }

            if (f_x < 0)
                f_x--;

            //將點擊位置Y座標轉換成晶粒Y座標
            if (y_dir == 1)
            {
                XY_Direc[1] = -1;
                f_y = ((500 - 2 * gridSize) - y) / gridSize * XY_Direc[1] - shift_y;
            }
            else
            {
                XY_Direc[1] = 1;
                f_y = ((500 - 2 * gridSize) - y) / gridSize * XY_Direc[1] - shift_y+1;
            }

            if (f_y < 0)
                f_y--;

            x = (int)f_x;
            y = (int)f_y;

            int[] xy_pos = new int[2];
            xy_pos[0] = x;
            xy_pos[1] = y;

            return xy_pos;
        }

        private void PicBx_Mapping_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Labl_ShowCellValue.Visible = false;
                return;
            }

            int[] xy = { 0, 0 };
            if (isLargeImg)
            {
                xy = GetCoordinate(e.Location, Pnl_Mapping, SmallMap.GridSize,
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc),
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc),
                                    SmallMap.ShiftX, SmallMap.ShiftY);
            }
            else
            {
                xy = GetCoordinate(e.Location, Pnl_Mapping, mapInformation.GridSize,
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc),
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc),
                                    mapInformation.ShiftX, mapInformation.ShiftY);
            }
                
            Labl_ShowCellValue.Text = $"({xy[0]}, {xy[1]})";

            // 設置 Label 的位置在滑鼠點擊的右側
            if(e.X > PicBx_Mapping.Width/2)
                Labl_ShowCellValue.Location = new System.Drawing.Point(e.X - 90, e.Y);
            else
                Labl_ShowCellValue.Location = new System.Drawing.Point(e.X + 20, e.Y);

            // 確保 Label 可見
            Labl_ShowCellValue.Visible = true;
        }

        private void PicBx_Mapping_MouseDown(object sender, MouseEventArgs e)
        {
            // 當滑鼠按下時，開始繪製
            isDrawing = true;
            startPoint = e.Location;

            int[] xy = { 0, 0 };
            if(isLargeImg)
            {
                xy = GetCoordinate(e.Location, Pnl_Mapping, SmallMap.GridSize,
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc),
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc),
                                    SmallMap.ShiftX, SmallMap.ShiftY);
            }
            else
            {
                xy = GetCoordinate(e.Location, Pnl_Mapping, mapInformation.GridSize,
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc),
                                    ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc),
                                    mapInformation.ShiftX, mapInformation.ShiftY);
            }

            xy_record.x_start = xy[0];
            xy_record.y_start = xy[1];
        }

        private void PicBx_Mapping_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // 動態更新當前滑鼠位置
                currentPoint = e.Location;
                PicBx_Mapping.Invalidate();  // 重新繪製 Panel
                isMouseMove = true;
            }
        }

        private void PicBx_Mapping_MouseUp(object sender, MouseEventArgs e)
        {
            if(isDrawing && isMouseMove &&
                Math.Abs(currentPoint.X - startPoint.X) > 15 && 
                Math.Abs(currentPoint.Y - startPoint.Y) > 15)
            {
                isMouseMove = false;

                int[] xy = { 0, 0 };

                if (isLargeImg)
                {
                    xy = GetCoordinate(currentPoint, Pnl_Mapping, SmallMap.GridSize,
                                        ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc),
                                        ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc),
                                        SmallMap.ShiftX, SmallMap.ShiftY);
                }
                else
                {
                    xy = GetCoordinate(currentPoint, Pnl_Mapping, mapInformation.GridSize,
                                        ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc),
                                        ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc),
                                        mapInformation.ShiftX, mapInformation.ShiftY);
                }

                xy_record.x_end = xy[0];
                xy_record.y_end = xy[1];

                PickupMapInformation(SmallMap.CellInfo, 
                                        xy_record.x_start, xy_record.x_end,
                                        xy_record.y_start, xy_record.y_end);

                PicBx_Colorbar.Visible = false;
                PicBx_Mapping.Visible = false;
                Labl_ShowCellValue.Visible = false;

                Dictionary<string, object> myDictionary = null;
                double Start = tool.StringToDouble(TxtBx_Start.Text);
                double End = tool.StringToDouble(TxtBx_End.Text);
                double Step = tool.StringToDouble(TxtBx_Step.Text);
                String TestItem = Cmbx_TestItem.Text;
                int[] XY_Direc = new int[2];

                if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc) == 1)
                    XY_Direc[0] = -1;
                else
                    XY_Direc[0] = 1;

                if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc) == 1)
                    XY_Direc[1] = -1;
                else
                    XY_Direc[1] = 1;


                SmallMap.MapSize = 500;

                if (Start > End)
                {
                    MessageBox.Show("Start Large Than End");
                    tool.SaveHistoryToFile("起始值比結束值大");
                    return;
                }

                SetDrawSize(Pnl_Mapping, SmallMap.MapSize);

                ClearMapping(Pnl_Mapping);

                if (SmallMap.CellInfo.Count <= 1)
                {
                    // 結束繪製
                    isDrawing = false;
                    PicBx_Mapping.Invalidate();  // 最後一次繪製
                    return;
                }
                    

                myDictionary = FindMapInfo(SmallMap.MapSize, SmallMap.CellInfo, XY_Direc);
                SmallMap.ShiftX = (int)myDictionary["ShiftX"];
                SmallMap.ShiftY = (int)myDictionary["ShiftY"];
                //mapInformation.CellCount = (int)myDictionary["CellCount"];
                SmallMap.GridSize = (float)myDictionary["GridSize"];

                SmallMap.ColorList = SetCellColor(Start, End, Step);

                SmallMap.ValueRegion = SetValueRegion(Start, Step, mapInformation.ColorList);

                DrawMapping(Pnl_Mapping, SmallMap.GridSize, SmallMap.CellInfo,
                            SmallMap.ShiftX, SmallMap.ShiftY,
                            TestItem, SmallMap.ColorList, SmallMap.ValueRegion, XY_Direc);

                tool.CaptureImage(Pnl_Mapping, Application.StartupPath + @"\Temp\Pnl_Mapping.png");
                tool.LoadImageToPicBx(PicBx_Mapping, Application.StartupPath + @"\Temp\Pnl_Mapping.png");

                PicBx_Colorbar.Visible = true;
                PicBx_Mapping.Visible = true;

                isLargeImg = true;
            }

            // 結束繪製
            isDrawing = false;
            PicBx_Mapping.Invalidate();  // 最後一次繪製
        }

        private void PicBx_Mapping_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing)
            {
                // 計算矩形的左上角和寬高
                int x = Math.Min(startPoint.X, currentPoint.X);
                int y = Math.Min(startPoint.Y, currentPoint.Y);
                int width = Math.Abs(startPoint.X - currentPoint.X);
                int height = Math.Abs(startPoint.Y - currentPoint.Y);

                // 繪製矩形
                using (Pen customPen = new Pen(Color.White, 3))
                {
                    e.Graphics.DrawRectangle(customPen, x, y, width, height);
                }
            }
        }
    }
}
