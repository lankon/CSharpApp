﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.IO;

namespace ToolFunction.Base
{
    /// <summary>
    /// Form Function
    /// </summary>
    public static partial class Tool
    {
        public static void SetForm(Panel pnl, System.Windows.Forms.Form form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);
        }

        public static void CloseFormOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is System.Windows.Forms.Form && control.Visible == true)
                {
                    ((System.Windows.Forms.Form)control).Close();
                    ((System.Windows.Forms.Form)control).Dispose();
                    break;
                }
            }
        }

        public static void HideElementOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is System.Windows.Forms.Form && control.Visible == true)
                {
                    ((System.Windows.Forms.Form)control).Hide();
                    //break; 
                }
                else if (control is Button && control.Visible == true)
                {
                    ((Button)control).Visible = false;
                }
                else if (control is Label && control.Visible == true)
                {
                    ((Label)control).Visible = false;
                }
            }
        }

        /// <summary>
        /// 顯示Form Name,Debug的時後使用,其他用途請勿使用,會持續占用記憶體
        /// </summary>
        /// <param name="form"></param>
        public static void ShowFormName(System.Windows.Forms.Form form, int pos = 0)
        {
            // 建立 Panel
            Panel namePanel = new Panel();
            namePanel.Name = "FormNamePanel";
            namePanel.BackColor = Color.FromArgb(0, 0, 0, 128); // 半透明黑底
            namePanel.Size = new Size(350, 30);

            if (pos == 0)
                namePanel.Location = new Point(0, 0); // 左上
            else if (pos == 1)
                namePanel.Location = new Point(form.Width - 350, 0); // 右上

            namePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            namePanel.BringToFront(); // 確保在最上層

            // 建立 Label
            Label nameLabel = new Label();
            nameLabel.Text = form.Name; // 或 form.Text 看你想顯示什麼
            nameLabel.ForeColor = Color.Red;
            nameLabel.AutoSize = false;
            nameLabel.Dock = DockStyle.Fill;
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            nameLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // 加入 Label 到 Panel
            namePanel.Controls.Add(nameLabel);

            // 加入 Panel 到 Form
            form.Controls.Add(namePanel);
            namePanel.BringToFront(); // 再次確保最上層
        }

        public static void ReleaseButtonImages(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Image?.Dispose();
                    btn.Image = null;

                    btn.BackgroundImage?.Dispose();
                    btn.BackgroundImage = null;
                }

                if (ctrl.HasChildren)
                    ReleaseButtonImages(ctrl);
            }
        }
    }

    /// <summary>
    /// Log Function
    /// </summary>
    public static partial class Tool
    {

        public static ILogger CreateLog(String Name = "History\\Log_", String Type = ".log")
        {
            String path;
            path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\" + Name;
            path += Type;

            if (Name == "History\\Log_")
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(a => a.File(path: path,
                                           rollingInterval: RollingInterval.Day,
                                           outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                               ).CreateLogger();

                return Log.Logger;
            }
            else
            {
                var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(a => a.File(path: path,
                                           rollingInterval: RollingInterval.Day,
                                           outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                               ).CreateLogger();

                return logger;
            }
        }

        public static void SaveLogToFile(String Msg, ILogger log = null, string level = "DBG")
        {
            if (log == null)
                log = Log.Logger;

            switch (level)
            {
                case "DBG": log.Debug(Msg); break;
                case "INF": log.Information(Msg); break;
                case "WRN": log.Warning(Msg); break;
                case "ERR": log.Error(Msg); break;
                default: log.Debug(Msg); break;
            }
        }

        public static void CloseLog()
        {
            Log.CloseAndFlush();
        }
    }

    /// <summary>
    /// Time Record
    /// </summary>
    public static partial class Tool
    {
        public static void ResetTimeCount(out long startTicks)
        {
            startTicks = Stopwatch.GetTimestamp();
        }
        public static int GetTime(long startTicks, string time = "ms")
        {
            long endTicks = Stopwatch.GetTimestamp();
            double tickFrequency = (double)Stopwatch.Frequency;

            double us = (endTicks - startTicks) * 1_000_000.0 / tickFrequency;

            double unit = 1;

            switch (time)
            {
                case "s": unit = 0.000001; break;
                case "ms": unit = 0.001; break;
                case "us": unit = 1; break;
            }

            return (int)(us * unit);
        }
        public static bool CheckTimeOverSec(int tick, int time)
        {
            var time_count = Environment.TickCount - tick;
            bool res = time_count > time * 1000;

            return res;
        }

    }

    /// <summary>
    /// DataGrid Function
    /// </summary>
    public static partial class Tool
    {
        public static void DataGrid_AddRow(DataGridView dataGridView, string[] context)
        {
            if (context.Length != dataGridView.ColumnCount)
            {
                SaveLogToFile("新增行數與DataGrid行數不一致");
                return;
            }

            // 取得目前選取列的 index，如果沒有選取則預設加在最後
            int insertIndex = dataGridView.CurrentCell?.RowIndex ?? dataGridView.Rows.Count;

            // 插入一列
            dataGridView.Rows.Insert(insertIndex, context);
        }
        public static void DataGrid_DeleteRow(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow != null && !dataGridView.CurrentRow.IsNewRow)
            {
                dataGridView.Rows.Remove(dataGridView.CurrentRow);
            }
        }
        public static void DataGrid_RowUp(DataGridView dataGridView)
        {
            // 確保選取的列不為 null 且不是第一列
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index > 0)
            {
                int currentIndex = dataGridView.CurrentRow.Index;
                int previousIndex = currentIndex - 1;

                // 交換當前列與上一列
                var currentRow = dataGridView.Rows[currentIndex];
                var previousRow = dataGridView.Rows[previousIndex];

                // 交換資料
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    var temp = currentRow.Cells[i].Value;
                    currentRow.Cells[i].Value = previousRow.Cells[i].Value;
                    previousRow.Cells[i].Value = temp;
                }

                // 更新選擇列
                dataGridView.CurrentCell = previousRow.Cells[dataGridView.CurrentCell.ColumnIndex];
            }
            else
            {
                return;
            }
        }
        public static bool DataGrid_DataSave(DataGridView dataGridView, string file_name)
        {
            bool res = false;

            SaveLogToFile($"{file_name}儲存Start");

            string FolderPath = Application.StartupPath + @"\Setting";
            CreateFolder(FolderPath);

            string file_path = FolderPath + @"\" + file_name;

            // 將 DataGridView 資料轉換為 DataTable
            DataTable dt = new DataTable("IOTable");

            // 假設 DataGridView 已經有資料
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dt.Columns.Add(column.Name);
            }

            // 把每一列資料加到 DataTable 中
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)  // 忽略最後一行空白列
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Value;
                    }
                    dt.Rows.Add(dataRow);
                }
            }

            // 儲存為 XML 檔案
            try
            {
                dt.WriteXml(file_path);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                return res;
            }

            SaveLogToFile($"{file_name}儲存End");

            return res;
        }
        public static bool DataGrid_DataLoad(DataGridView dataGridView, string file_name)
        {
            bool res = true;

            string FolderPath = Application.StartupPath + @"\Setting";

            string file_path = FolderPath + @"\" + file_name;

            if (!File.Exists(file_path))
                return false;

            DataSet ds = new DataSet();

            ds.ReadXml(file_path);

            DataTable dt = ds.Tables[0];

            dataGridView.Rows.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                int rowIndex = dataGridView.Rows.Add();

                //// 對應欄位名稱來設定資料（需與 DataGridView 欄位名稱一致）
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    if (dt.Columns.Contains(col.Name))
                    {
                        dataGridView.Rows[rowIndex].Cells[col.Name].Value = dr[col.Name];
                    }
                }
            }

            return res;
        }
        public static void DataGrid_RowDown(DataGridView dataGridView)
        {
            // 確保選取的列不為 null 且不是最後一列
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index < dataGridView.Rows.Count - 1)
            {
                int currentIndex = dataGridView.CurrentRow.Index;
                int previousIndex = currentIndex + 1;

                // 交換當前列與下一列
                var currentRow = dataGridView.Rows[currentIndex];
                var previousRow = dataGridView.Rows[previousIndex];

                // 交換資料
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    var temp = currentRow.Cells[i].Value;
                    currentRow.Cells[i].Value = previousRow.Cells[i].Value;
                    previousRow.Cells[i].Value = temp;
                }

                // 更新選擇列
                dataGridView.CurrentCell = previousRow.Cells[dataGridView.CurrentCell.ColumnIndex];
            }
            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// Type Conversion
    /// </summary>
    public static partial class Tool
    {
        public static int StringToInt(string str)
        {
            int result;

            if (Int32.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                SaveLogToFile("型別轉換錯誤");
                return -999;
            }
        }
    }

    public static partial class Tool
    {
        #region 寫檔
        public static StreamWriter CreateFile(String Name, String Type, bool ContinueWrite)
        {
            String path;
            StreamWriter File;
            path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\" + Name;
            path += Type;

            string directoryPath = Path.GetDirectoryName(path);

            CreateFolder(directoryPath);

            if (ContinueWrite)
                File = new StreamWriter(path, true, Encoding.Default);
            else
                File = new StreamWriter(path);

            return File;
        }

        public static void WriteFile(StreamWriter File, String Msg)
        {
            File.WriteLine(Msg);
        }

        public static void CloseFile(StreamWriter File)
        {
            File.Close();
        }
        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    SaveLogToFile("創建資料夾:" + folderPath);
                }
                catch (Exception ex)
                {
                    SaveLogToFile("無效的創建資料夾路徑");
                    SaveLogToFile($"Tool:CreateFolder Fail{ex}");
                }
            }
            else
            {
                //SaveLogToFile("資料夾已存在");
            }
        }
        #endregion
    }
}
