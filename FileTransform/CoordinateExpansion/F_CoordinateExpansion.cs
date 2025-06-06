using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using CommonFunction;

namespace FileTransform
{
    public partial class F_CoordinateExpansion : Form
    {
        #region parameter define 
        private bool isSelecting = false;
        private Point selectionStart;
        private Rectangle selectionRect = new Rectangle();
        #endregion

        #region public function
        public void SetF_CoordinateExpansion(Panel pnl, F_CoordinateExpansion form)
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

        #region private function
        private bool Expansion_Coordinate(List<Dictionary<string, string>> data)
        {           
            StreamWriter sw = null;
            String path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_SaveFilePath);

            try
            {
                string directoryPath = Path.GetDirectoryName(path);
                Tool.CreateFolder(directoryPath);
                sw = new StreamWriter(path);
            }
            catch(Exception)
            {
                Tool.SaveHistoryToFile("Expansion_Coordinate:創建檔案失敗");
                return false;
            }
            

            int type = ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_ExpansionType);

            int x_interval = ApplicationSetting.Get_Int_Recipe((int)FormItem.TxtBx_X_Interval);
            int y_interval = ApplicationSetting.Get_Int_Recipe((int)FormItem.TxtBx_Y_Interval);

            int x_expansion = ApplicationSetting.Get_Int_Recipe((int)FormItem.TxtBx_X_Expansion);
            int y_expansion = ApplicationSetting.Get_Int_Recipe((int)FormItem.TxtBx_Y_Expansion);
            int r_expansion = ApplicationSetting.Get_Int_Recipe((int)FormItem.TxtBx_RadiusExpansion);

            if (type == 0)
            {
                #region 方形擴展
                for (int i = 0; i < x_expansion; i++)
                {
                    for (int j = 0; j < y_expansion; j++)
                    {
                        for (int k = 0; k < data.Count; k++)
                        {
                            data[k].TryGetValue("Item0", out string PosX);
                            data[k].TryGetValue("Item1", out string PosY);

                            int iPosX = Tool.StringToInt(PosX);
                            int iPosY = Tool.StringToInt(PosY);

                            iPosX = iPosX + (i - x_expansion / 2) * x_interval;
                            iPosY = iPosY + (j - y_expansion / 2) * y_interval;

                            Tool.WriteFile(sw, iPosX.ToString() + "," + iPosY.ToString());
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 圓形擴展
                for (int i = -r_expansion + 1; i < r_expansion; i++)
                {
                    for (int j = -r_expansion + 1; j < r_expansion; j++)
                    {
                        double region = Math.Pow(i, 2) + Math.Pow(j, 2);

                        if (region > Math.Pow(r_expansion, 2))
                            continue;

                        for (int k = 0; k < data.Count; k++)
                        {
                            data[k].TryGetValue("Item0", out string PosX);
                            data[k].TryGetValue("Item1", out string PosY);

                            int iPosX = Tool.StringToInt(PosX);
                            int iPosY = Tool.StringToInt(PosY);

                            iPosX = iPosX + (i) * x_interval;
                            iPosY = iPosY + (j) * y_interval;

                            Tool.WriteFile(sw, iPosX.ToString() + "," + iPosY.ToString());
                        }
                    }
                }
                #endregion
            }

            Tool.CloseFile(sw);

            return true;
        }
        private void Draw_Coordinate(string path, Chart chart)
        {
            chart.Series[0].Points.AddXY(0, 0);

            chart.Series[0].Points.Clear();

            List<Dictionary<string, string>> CoordinateData = new List<Dictionary<string, string>>();

            CoordinateData = Tool.ReadCsvFile(path, false);

            // 禁用格線
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            //chart.Series.Clear();

            // 添加數據點
            for (int i = 0; i < CoordinateData.Count; i++)
            {
                CoordinateData[i].TryGetValue("Item0", out string sPosX);
                CoordinateData[i].TryGetValue("Item1", out string sPosY);

                int iPosX = Tool.StringToInt(sPosX);
                int iPosY = Tool.StringToInt(sPosY);

                chart.Series[0].Points.AddXY(iPosX, iPosY);
            }

            // 配置 X 軸
            //chart1.ChartAreas[0].AxisX.Title = "X 軸";
            //chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // 配置 Y 軸
            //chart1.ChartAreas[0].AxisY.Title = "Y 軸";
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // 配置散佈圖點的樣式
            chart.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart.Series[0].MarkerSize = 10;
            chart.Series[0].MarkerColor = System.Drawing.Color.Blue;
        }
        private List<Dictionary<string, string>> ReadCsvFile_AMIDA(String Path)
        {
            // 使用 Dictionary 來儲存資料
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            bool StartReadFile = false;
            string s_PosX = "X";// ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_X_KeyWord);
            int shift_row = 0;

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
                        string[] fields = line.Split(',','\t'); //AMIDA特別項


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
        }//20240607
        private List<Dictionary<string, string>> ReadCellInfo(string Path)  //20240607
        {
            List<Dictionary<string, string>> CellInfo = null;

            CellInfo = ReadCsvFile_AMIDA(Path);

            return CellInfo;
        }
        private bool SaveCsvFile(string[,] data, string path)
        {
            int rowCount = data.GetLength(1);
            int colCount = data.GetLength(0);

            using (StreamWriter file = new StreamWriter(path))
            {
                for (int i = 0; i < rowCount; i++)
                {
                    if (data[0, i] == null) break;

                    // 為當前行創建一個列表來存儲每一列的值
                    List<string> row = new List<string>();
                    for (int j = 0; j < colCount; j++)
                    {
                        if (j > 3 && data[j, i] == null) break;

                        // 添加當前元素到行列表，並在需要時處理可能的逗號（這裡假設數據是乾淨的，無需進一步處理）
                        row.Add(data[j, i]);
                    }
                    // 將列表轉換為用逗號分隔的字符串，並寫入檔案
                    file.WriteLine(string.Join(",", row));
                }

                return true;
            }
        }   //20240607
        void SaveCsvFile(List<Dictionary<string, string>> data, string filePath)    //20240607
        {
            // 確定CSV文件的標題，即字典中的鍵
            var headers = data.SelectMany(dict => dict.Keys).Distinct().ToList();

            using (var writer = new StreamWriter(filePath))
            {
                // 寫入標題行
                //writer.WriteLine(string.Join(",", headers));

                // 寫入資料行
                foreach (var dict in data)
                {
                    var row = headers.Select(header => dict.ContainsKey(header) ? dict[header] : string.Empty);
                    writer.WriteLine(string.Join("\t", row));
                }
            }
        }

        // scan wafer: 使用螢幕座標(X正方向:右, y正方向:下)
        // RecCol:相當於 X 方向第幾區(客戶定義:X正方向向右,0開始)
        // ChipCol:相當於 X 方向每區得第幾個晶粒(客戶定義:X正方向向右,0開始)
        // RecRow:相當於 Y 方向第幾區(客戶定義:Y正方向向下,0開始)
        // ChipRow:相當於 Y 方向每區得第幾個晶粒(客戶定義:Y正方向向下,0開始)    //20240607
        static int TranformPosX_RecCol(int iPosX,
                               int iMathOriginPointX,
                               int iShotOriginPointX_RecCol,
                               int iShotOriginPointX_ChipCol,
                               int iNumberOfXInOneShot
                              )
        {
            // iPosX: 待轉換的惠特座標
            // iMathOriginPointX: 對應點 X 座標(惠特座標)
            // iShotOriginPointX_RecCol: 對應點 X 座標(Rec座標)
            // iShotOriginPointX_ChipCol: 對應點 X 座標(Chip座標)
            // iNumberOfXInOneShot: 每區 X 方向的晶粒數

            // scan wafer: 使用螢幕座標(X正方向:右,y正方向:下)
            // RecCol:相當於 X 方向第幾區(客戶定義:X正方向向右,0開始)

            int Absolute_Coord = iShotOriginPointX_RecCol * iNumberOfXInOneShot + iShotOriginPointX_ChipCol; //對應點的絕對座標(由rec和chip組成)
                                                                                                             //int Absolute_Coord = iShotOriginPointX_RecCol * (-iNumberOfXInOneShot) + iShotOriginPointX_ChipCol; //對應點的絕對座標(由rec和chip組成)

            int relative_shift = (iPosX - iMathOriginPointX);  //相對位移(絕對座標與惠特座標同方向)
                                                               //int relative_shift = -(iPosX - iMathOriginPointX); //相對位移(絕對座標與惠特座標反方向)

            int Correspond_Absolute_Coord = relative_shift + Absolute_Coord;  // 換算我們要轉換的惠特座標，所對應的絕對座標(由rec和chip組成)

            int a = Correspond_Absolute_Coord % iNumberOfXInOneShot;
            //int a = Correspond_Absolute_Coord % (-iNumberOfXInOneShot);

            double b = (double)Correspond_Absolute_Coord / iNumberOfXInOneShot;
            //double b = (double)Correspond_Absolute_Coord / (-iNumberOfXInOneShot);

            double d_rec = Math.Floor(b);
            //double d_rec = Math.Ceiling(b);

            int i_rec = (int)d_rec;// 假設一區 n 個晶粒，號碼排序是 0 ~ (n-1)
            //int i_rec = a == 0 ? (int)d_rec - 1 : (int)d_rec; // 假設一區 n 個晶粒，號碼排序是 1 ~ n (等號狀況:餘數等於0時，也就是每區的最後一個點，商數會多算一區，所以扣回去)
            //int i_rec = a == 0 ? (int)d_rec + 1 : (int)d_rec; // 假設一區 n 個晶粒，號碼排序是 1 ~ n (等號狀況:餘數等於0時，也就是每區的最後一個點，商數會少算一區，所以加回去)

            return i_rec;
        }
        static int TranformPosX_ChipCol(int iPosX,
                                        int iMathOriginPointX,
                                        int iShotOriginPointX_RecCol,
                                        int iShotOriginPointX_ChipCol,
                                        int iNumberOfXInOneShot
                                      )
        {
            // iPosX: 待轉換的惠特座標
            // iMathOriginPointX: 對應點 X 座標(惠特座標)
            // iShotOriginPointX_RecCol: 對應點 X 座標(Rec座標)
            // iShotOriginPointX_ChipCol: 對應點 X 座標(Chip座標)
            // iNumberOfXInOneShot: 每區 X 方向的晶粒數

            // scan wafer: 使用螢幕座標(X正方向:右,y正方向:下)
            // ChipCol:相當於 X 方向每區得第幾個晶粒(客戶定義:X正方向向右,0開始)

            int Absolute_Coord = iShotOriginPointX_RecCol * iNumberOfXInOneShot + iShotOriginPointX_ChipCol; //對應點的絕對座標(由rec和chip組成)
            //int Absolute_Coord = iShotOriginPointX_RecCol * (-iNumberOfXInOneShot) + iShotOriginPointX_ChipCol; //對應點的絕對座標(由rec和chip組成)

            int relative_shift = (iPosX - iMathOriginPointX);  //相對位移(絕對座標與惠特座標同方向)
            //int relative_shift = -(iPosX - iMathOriginPointX); //相對位移(絕對座標與惠特座標反方向)

            int Correspond_Absolute_Coord = relative_shift + Absolute_Coord;  // 換算我們要轉換的惠特座標，所對應的絕對座標(由rec和chip組成)

            int a = Correspond_Absolute_Coord % (iNumberOfXInOneShot);
            //int a = Correspond_Absolute_Coord % (-iNumberOfXInOneShot);

            int ichip = a < 0 ? a + 1 * iNumberOfXInOneShot : a;  // 假設一區 n 個晶粒，號碼排序是 0 ~ (n-1)
            //int ichip = a <= 0 ? a + 1 * iNumberOfXInOneShot : a;  // 假設一區 n 個晶粒，號碼排序是 1 ~ n (等號狀況:餘數等於0時，也就是每區的最後一個點，要輸出n)
            return ichip;
        }
        static int TranformPosY_RecRow(int iPosY,
                               int iMathOriginPointY,
                               int iShotOriginPointY_RecRow,
                               int iShotOriginPointY_ChipRow,
                               int iNumberOfYInOneShot
                              )
        {
            // iPosY: 待轉換的惠特座標
            // iMathOriginPointY: 對應點 Y 座標(惠特座標)
            // iShotOriginPointY_RecRow: 對應點 Y 座標(Rec座標)
            // iShotOriginPointY_ChipRow: 對應點 Y 座標(Chip座標)
            // iNumberOfYInOneShot: 每區 Y 方向的晶粒數

            // scan wafer: 使用螢幕座標(X正方向:右,y正方向:下)
            // RecRow:相當於 Y 方向第幾區(客戶定義:Y正方向向上,0開始)

            int Absolute_Coord = iShotOriginPointY_RecRow * iNumberOfYInOneShot + iShotOriginPointY_ChipRow; //對應點的絕對座標(由rec和chip組成)
            //int Absolute_Coord = iShotOriginPointY_RecRow * (-iNumberOfYInOneShot) + iShotOriginPointY_ChipRow; //對應點的絕對座標(由rec和chip組成)

            //int relative_shift = (iPosY - iMathOriginPointY);  //相對位移(絕對座標與惠特座標同方向)
            int relative_shift = -(iPosY - iMathOriginPointY); //相對位移(絕對座標與惠特座標反方向)

            int Correspond_Absolute_Coord = relative_shift + Absolute_Coord;  // 換算我們要轉換的惠特座標，所對應的絕對座標(由rec和chip組成)

            int a = Correspond_Absolute_Coord % iNumberOfYInOneShot;
            //int a = Correspond_Absolute_Coord % (-iNumberOfYInOneShot);

            double b = (double)Correspond_Absolute_Coord / (iNumberOfYInOneShot);
            //double b = (double)Correspond_Absolute_Coord / (-iNumberOfYInOneShot);

            double d_rec;

            d_rec = Math.Floor(b);

            int i_rec = (int)d_rec;// 假設一區 n 個晶粒，號碼排序是 0 ~ (n-1)
            //int i_rec = a == 0 ? (int)d_rec - 1 : (int)d_rec; // 假設一區 n 個晶粒，號碼排序是 1 ~ n (等號狀況:餘數等於0時，也就是每區的最後一個點，商數會多算一區，所以扣回去)
            //int i_rec = a == 0 ? (int)d_rec + 1 : (int)d_rec; // 假設一區 n 個晶粒，號碼排序是 1 ~ n (等號狀況:餘數等於0時，也就是每區的最後一個點，商數會少算一區，所以加回去)

            return i_rec;
        }
        static int TranformPosY_ChipRow(int iPosY,
                                        int iMathOriginPointY,
                                        int iShotOriginPointY_RecRow,
                                        int iShotOriginPointY_ChipRow,
                                        int iNumberOfYInOneShot
                                      )
        {
            // iPosY: 待轉換的惠特座標
            // iMathOriginPointY: 對應點 Y 座標(惠特座標)
            // iShotOriginPointY_RecRow: 對應點 Y 座標(Rec座標)
            // iShotOriginPointY_ChipRow: 對應點 Y 座標(Chip座標)
            // iNumberOfYInOneShot: 每區 Y 方向的晶粒數

            // scan wafer: 使用螢幕座標(X正方向:右,y正方向:下)
            // RecRow:相當於 Y 方向每區得第幾個晶粒(客戶定義:Y正方向向下,0開始)

            int Absolute_Coord = iShotOriginPointY_RecRow * iNumberOfYInOneShot + iShotOriginPointY_ChipRow; //對應點的絕對座標(由rec和chip組成)
            //int Absolute_Coord = iShotOriginPointY_RecRow * (-iNumberOfYInOneShot) + iShotOriginPointY_ChipRow; //對應點的絕對座標(由rec和chip組成)

            //int relative_shift = (iPosY - iMathOriginPointY);  //相對位移(絕對座標與惠特座標同方向)
            int relative_shift = -(iPosY - iMathOriginPointY); //相對位移(絕對座標與惠特座標反方向)

            int Correspond_Absolute_Coord = relative_shift + Absolute_Coord;  // 換算我們要轉換的惠特座標，所對應的絕對座標(由rec和chip組成)

            int a = Correspond_Absolute_Coord % (iNumberOfYInOneShot);
            //int a = Correspond_Absolute_Coord % (-iNumberOfYInOneShot);

            int ichip = a < 0 ? a + 1 * iNumberOfYInOneShot : a;  // 假設一區 n 個晶粒，號碼排序是 0 ~ (n-1)
            //int ichip = a < 0 ? a + 1 * iNumberOfYInOneShot : a;  // 假設一區 n 個晶粒，號碼排序是 0 ~ (n-1)
            //int ichip = a <= 0 ? a + 1 * iNumberOfYInOneShot : a;  // 假設一區 n 個晶粒，號碼排序是 1 ~ n (等號狀況:餘數等於0時，也就是每區的最後一個點，要輸出n)

            return ichip;
        }
        // End
        //
        
        
        #endregion

        public F_CoordinateExpansion()
        {
            InitializeComponent();
        }

        private void Btn_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFileName = "";

            // 設置文件選擇對話框的屬性
            openFileDialog.Title = "Select TestData";
            openFileDialog.Filter = "TestData|*.txt|All|*.*";

            // 如果用戶選擇了文件，顯示文件名
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
            }

            TxtBx_FilePath.Text = selectedFileName;

            List<Dictionary<string, string>> CoordinateData = new List<Dictionary<string, string>>();

            CoordinateData = Tool.ReadCsvFile(selectedFileName, false);

            if (CoordinateData.Count == 0)
            {
                MessageBox.Show("Read File Error");
                Tool.SaveHistoryToFile("讀取檔案失敗");
                return;
            }

            if(!Expansion_Coordinate(CoordinateData))
            {
                MessageBox.Show("Expansion Coordinate Error");
                Tool.SaveHistoryToFile("座標擴展失敗");
                return;
            }

            Draw_Coordinate(ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_SaveFilePath), chart1);
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            /*if (e.Button == MouseButtons.Left)
            {
                isSelecting = true;
                selectionStart = e.Location;
                selectionRect = new Rectangle(e.Location, new Size(0, 0));
            }*/
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                // 更新選擇區域
                selectionRect = new Rectangle(
                    Math.Min(selectionStart.X, e.X),
                    Math.Min(selectionStart.Y, e.Y),
                    Math.Abs(e.X - selectionStart.X),
                    Math.Abs(e.Y - selectionStart.Y)
                );

                // 重新繪製 Chart 控件
                chart1.Invalidate();
            }

            // 獲取點擊位置的命中測試結果
            HitTestResult result = chart1.HitTest(e.X, e.Y);

            // 檢查點擊是否在數據點上
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                // 獲取點擊的數據點
                DataPoint point = chart1.Series[0].Points[result.PointIndex];

                // 顯示數據點的值
                toolTip1.Show($"X值: {point.XValue}, Y值: {point.YValues[0]}", chart1, e.X, e.Y - 15, 2000);
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            /*if (isSelecting)
            {
                isSelecting = false;

                // 獲取 ChartArea 的座標範圍
                ChartArea chartArea = chart1.ChartAreas[0];
                Axis xAxis = chartArea.AxisX;
                Axis yAxis = chartArea.AxisY;

                // 計算選擇區域對應的座標範圍
                double xMin = xAxis.PixelPositionToValue(selectionRect.Left);
                double xMax = xAxis.PixelPositionToValue(selectionRect.Right);
                double yMin = yAxis.PixelPositionToValue(selectionRect.Bottom);
                double yMax = yAxis.PixelPositionToValue(selectionRect.Top);

                // 設置座標範圍
                xAxis.ScaleView.Zoom(xMin, xMax);
                yAxis.ScaleView.Zoom(yMin, yMax);

                // 清除選擇區域
                selectionRect = Rectangle.Empty;
                chart1.Invalidate();
            }*/
        }

        private void Btn_Transform_Click(object sender, EventArgs e)    //20240607
        {
            // 美達轉檔用
            List<Dictionary<string, string>> CellInfo;

            CellInfo = ReadCellInfo(@"C:\Users\lankon\Desktop\AMIDA.csv");

            if (CellInfo.Count == 0)
            {
                MessageBox.Show("Read File Error");
                Tool.SaveHistoryToFile("讀取檔案失敗");
                return;
            }

            int[] X_RecCol = new int[500000];
            int[] X_ChipCol = new int[500000];
            int[] Y_RecCol = new int[500000];
            int[] Y_ChipCol = new int[500000];

            if (CellInfo.Count > 500000)
            {
                Tool.SaveHistoryToFile("數量超過500000");
                MessageBox.Show("File Size Over 500000");
                return;
            }

            for (int i = 1; i < CellInfo.Count; i++)
            {
                CellInfo[i].TryGetValue("X", out string sPosX);
                CellInfo[i].TryGetValue("Y", out string sPosY);

                int iPosX = Tool.StringToInt(sPosX);
                int iPosY = Tool.StringToInt(sPosY);

                X_RecCol[i - 1] = TranformPosX_RecCol(iPosX, 0, 0, 0, 20);
                X_ChipCol[i - 1] = TranformPosX_ChipCol(iPosX, 0, 0, 0, 20);

                Y_RecCol[i - 1] = TranformPosY_RecRow(iPosY, 2, 0, 0, 20);
                Y_ChipCol[i - 1] = TranformPosY_ChipRow(iPosY, 2, 0, 0, 20);
            }


            // 讀取CSV文件的路徑

            string csvFilePath = @"C:\Users\lankon\Desktop\AMIDA.csv";

            // 讀取CSV文件內容
            var lines = File.ReadAllLines(csvFilePath).ToList();
            int Start = 0;

            // 解析CSV文件的標題行
            var headers = lines[0].Split('\t', ',').Select(header => header.Trim()).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                headers = lines[i].Split('\t', ',').Select(header => header.Trim()).ToList();

                if (headers[0] == "Name")
                    break;

                Start++;
            }

            // 找到"Y"的索引位置
            int nameIndex = headers.IndexOf("Y");

            // 在"Y"後插入四象座標
            headers.Insert(nameIndex + 1, "X_Rec");
            headers.Insert(nameIndex + 2, "Y_Rec");
            headers.Insert(nameIndex + 3, "X_Chip");
            headers.Insert(nameIndex + 4, "Y_Chip");

            // 更新標題行
            lines[Start] = string.Join("\t", headers);

            // 在每一行的Name後插入一個新的空值或預設值
            for (int i = Start + 1; i < lines.Count; i++)
            {
                var columns = lines[i].Split('\t', ',').Select(column => column.Trim()).ToList();

                // 插入一個新的空值在Name後
                if (i < Start + 4)
                {
                    columns.Insert(nameIndex + 1, "N");
                    columns.Insert(nameIndex + 2, "N");
                    columns.Insert(nameIndex + 3, "N");
                    columns.Insert(nameIndex + 4, "N");
                }
                else
                {
                    columns.Insert(nameIndex + 1, X_RecCol[i - (Start + 4)].ToString());
                    columns.Insert(nameIndex + 2, Y_RecCol[i - (Start + 4)].ToString());
                    columns.Insert(nameIndex + 3, X_ChipCol[i - (Start + 4)].ToString());
                    columns.Insert(nameIndex + 4, Y_ChipCol[i - (Start + 4)].ToString());
                }

                // 更新行數據
                lines[i] = string.Join("\t", columns);
            }

            // 寫回CSV文件
            string outputCsvFilePath = @"C:\Users\lankon\Desktop\新增資料夾\FileTransform\bin\Debug\aa.txt";
            File.WriteAllLines(outputCsvFilePath, lines);

        }

        
    }
}
