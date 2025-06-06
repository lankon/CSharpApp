using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using CommonFunction;

namespace Mapping
{
    public partial class F_BinMapping : Form
    {
        #region parameter define
        F_Mapping f_Mapping = new F_Mapping();
        MapInformation mapInformation = new MapInformation();
        #endregion

        #region public function
        public void SetF_BinMapping_ButtonGroup(Panel pnl, F_BinMapping form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion



        public F_BinMapping()
        {
            InitializeComponent();
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

            if (f_Mapping.CheckLoadFileCondition() == false)
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

            mapInformation.CellInfo = f_Mapping.ReadCellInfo(selectedFileName);

            if (mapInformation.CellInfo.Count == 0)
            {
                MessageBox.Show("Read File Error");
                Tool.SaveHistoryToFile("讀取檔案失敗");
                return;
            }
        }

        private void Btn_DrawBinMap_Click(object sender, EventArgs e)
        {
            //if (mapInformation.CellInfo.Count == 0)
            //{
            //    MessageBox.Show("Please Load Wafer Data");
            //    Tool.SaveHistoryToFile("未載入晶圓資料");
            //    return;
            //}


            //#region draw mapping
            //PicBx_Mapping.Visible = false;
            //Labl_ShowCellValue.Visible = false;

            //Dictionary<string, object> myDictionary = null;
            //double Start = Tool.StringToDouble(TxtBx_Start.Text);
            //double End = Tool.StringToDouble(TxtBx_End.Text);
            //double Step = Tool.StringToDouble(TxtBx_Step.Text);
            //String TestItem = Cmbx_TestItem.Text;
            //int[] XY_Direc = new int[2];

            //if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_X_Direc) == 1)
            //    XY_Direc[0] = -1;
            //else
            //    XY_Direc[0] = 1;

            //if (ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_Y_Direc) == 1)
            //    XY_Direc[1] = -1;
            //else
            //    XY_Direc[1] = 1;


            //mapInformation.MapSize = 500;

            //if (Start > End)
            //{
            //    MessageBox.Show("Start Large Than End");
            //    Tool.SaveHistoryToFile("起始值比結束值大");
            //    return;
            //}

            //SetDrawSize(Pnl_Mapping, mapInformation.MapSize);

            //ClearMapping(Pnl_Mapping);

            //myDictionary = FindMapInfo(mapInformation.MapSize, mapInformation.CellInfo, XY_Direc);
            //mapInformation.ShiftX = (int)myDictionary["ShiftX"];
            //mapInformation.ShiftY = (int)myDictionary["ShiftY"];
            //mapInformation.MinPosX = (int)myDictionary["MinPosX"];
            //mapInformation.MinPosY = (int)myDictionary["MinPosY"];
            //mapInformation.MaxPosX = (int)myDictionary["MaxPosX"];
            //mapInformation.MaxPosY = (int)myDictionary["MaxPosY"];
            ////mapInformation.CellCount = (int)myDictionary["CellCount"];
            //mapInformation.GridSize = (float)myDictionary["GridSize"];

            //mapInformation.ColorList = SetCellColor(Start, End, Step);

            //mapInformation.ValueRegion = SetValueRegion(Start, Step, mapInformation.ColorList);

            //mapInformation.ValueRegionCount = DrawMapping(Pnl_Mapping, mapInformation.GridSize, mapInformation.CellInfo,
            //                                              mapInformation.ShiftX, mapInformation.ShiftY,
            //                                              TestItem, mapInformation.ColorList, mapInformation.ValueRegion, XY_Direc);

            //DrawColorbar(Pnl_Colorbar, mapInformation.ValueRegionCount,
            //             mapInformation.ColorList, mapInformation.ValueRegion);

            //Tool.CreateFolder(Application.StartupPath + @"\Temp");
            //Tool.CaptureImage(Pnl_Colorbar, Application.StartupPath + @"\Temp\Pnl_Colorbar.png");
            //Tool.LoadImageToPicBx(PicBx_Colorbar, Application.StartupPath + @"\Temp\Pnl_Colorbar.png");
            //Tool.CaptureImage(Pnl_Mapping, Application.StartupPath + @"\Temp\Pnl_Mapping.png");
            //Tool.LoadImageToPicBx(PicBx_Mapping, Application.StartupPath + @"\Temp\Pnl_Mapping.png");

            //isLargeImg = false;
            //PicBx_Colorbar.Visible = true;
            //PicBx_Mapping.Visible = true;

            //Tool.SaveHistoryToFile("繪圖完成");
            //#endregion

            //AddTestItemCondition();
            //SaveTestItemCondition();

            //#region copy cell data
            //SmallMap.CellInfo = new List<Dictionary<string, string>>();

            //foreach (var dictionary in mapInformation.CellInfo)
            //{
            //    // 複製字典
            //    Dictionary<string, string> newDict = new Dictionary<string, string>(dictionary);
            //    SmallMap.CellInfo.Add(newDict);
            //}
            //#endregion
        }
    }
}
