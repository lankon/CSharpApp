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
        Tool tool = new Tool();
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
                tool.CreateFolder(directoryPath);
                sw = new StreamWriter(path);
            }
            catch(Exception ex)
            {
                tool.SaveHistoryToFile("Expansion_Coordinate:創建檔案失敗");
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

                            int iPosX = tool.StringToInt(PosX);
                            int iPosY = tool.StringToInt(PosY);

                            iPosX = iPosX + (i - x_expansion / 2) * x_interval;
                            iPosY = iPosY + (j - y_expansion / 2) * y_interval;

                            tool.WriteFile(sw, iPosX.ToString() + "," + iPosY.ToString());
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

                            int iPosX = tool.StringToInt(PosX);
                            int iPosY = tool.StringToInt(PosY);

                            iPosX = iPosX + (i) * x_interval;
                            iPosY = iPosY + (j) * y_interval;

                            tool.WriteFile(sw, iPosX.ToString() + "," + iPosY.ToString());
                        }
                    }
                }
                #endregion
            }

            tool.CloseFile(sw);

            return true;
        }
        private void Draw_Coordinate(string path, Chart chart)
        {
            chart.Series[0].Points.AddXY(0, 0);

            chart.Series[0].Points.Clear();

            List<Dictionary<string, string>> CoordinateData = new List<Dictionary<string, string>>();

            CoordinateData = tool.ReadCsvFile(path, false);

            // 禁用格線
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            //chart.Series.Clear();

            // 添加數據點
            for (int i = 0; i < CoordinateData.Count; i++)
            {
                CoordinateData[i].TryGetValue("Item0", out string sPosX);
                CoordinateData[i].TryGetValue("Item1", out string sPosY);

                int iPosX = tool.StringToInt(sPosX);
                int iPosY = tool.StringToInt(sPosY);

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

            CoordinateData = tool.ReadCsvFile(selectedFileName, false);

            if (CoordinateData.Count == 0)
            {
                MessageBox.Show("Read File Error");
                tool.SaveHistoryToFile("讀取檔案失敗");
                return;
            }

            if(!Expansion_Coordinate(CoordinateData))
            {
                MessageBox.Show("Expansion Coordinate Error");
                tool.SaveHistoryToFile("座標擴展失敗");
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
    }
}
