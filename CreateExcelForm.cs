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
using WorkbookUtility;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Diagnostics;

namespace ToolApp
{
    public partial class CreateExcelForm : Form
    {
        private string[] subDirs;
        public CreateExcelForm()
        {
            InitializeComponent();
            
        }

        public void FormInitialize()
        {
            LoadSettings();
            if (Directory.Exists(targetDir.Text))
            {
                subDirList.Items.Clear();
                subDirs = Directory.GetDirectories(targetDir.Text);
                foreach (string dir in subDirs)
                {
                    DirectoryInfo info = new DirectoryInfo(dir);
                    subDirList.Items.Add(info.Name);
                }
                createExcel.Enabled = true;
            }
            else
            {
                createExcel.Enabled = false;
            }
        }

        private void CreateExcelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible == true && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            // 設定値を保存
            SaveSettings();
        }

        private void LoadSettings()
        {
            Properties.Settings.Default.Reload();
            targetDir.Text = Properties.Settings.Default.savePath;
            maxWidth.Text = Properties.Settings.Default.xlMaxWidth;
            maxHeight.Text = Properties.Settings.Default.xlMaxHeight;
            spanHeight.Text = Properties.Settings.Default.xlSpanHeight;
            saveFileName.Text = Properties.Settings.Default.xlSaveFileName;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.xlMaxWidth = maxWidth.Text;
            Properties.Settings.Default.xlMaxHeight = maxHeight.Text;
            Properties.Settings.Default.xlSpanHeight = spanHeight.Text;
            Properties.Settings.Default.xlSaveFileName = saveFileName.Text;
            Properties.Settings.Default.Save();
        }

        private void delSubFolderButton_Click(object sender, EventArgs e)
        {
            if (subDirList.SelectedItems.Count > 0)
            {
                object[] items;
                List<object> list = new List<object>();
                foreach (object item in subDirList.SelectedItems)
                {
                    list.Add(item);
                }

                items = list.ToArray();
                foreach (var item in items)
                {
                    subDirList.Items.Remove(item);
                }
            }
        }

        private void createExcel_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            int maxWidth = Int32.Parse(this.maxWidth.Text);
            int maxHeight = Int32.Parse(this.maxHeight.Text);
            int spanHeight = Int32.Parse(this.spanHeight.Text);
            string saveFileName = this.saveFileName.Text;
            if (String.IsNullOrEmpty(saveFileName))
            {
                saveFileName = "Book1.xlsx";
            }

            string saveFilePath = targetDir.Text + "\\" + saveFileName;

            List<string> dirs = new List<string>();

            if (changeTarget.Checked == true)
            {
                dirs.Add(targetDir.Text);
            }
            else
            {
                foreach (object item in subDirList.Items)
                {
                    dirs.Add(targetDir.Text + "\\" + item.ToString());
                }
            }

            string tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tmp);
            string tmpFilePath = tmp + @"\tmp.xlsx";
            if (File.Exists(tmpFilePath))
            {
                File.Delete(tmpFilePath);
            }

            // FileStream fs = new FileStream(tmpFilePath, FileMode.Open);
            MemoryStream fs = new MemoryStream();
            Creator creator = new Creator();
            creator.Create(fs);

            foreach (string path in dirs)
            {
                DirectoryInfo info = new DirectoryInfo(path);
                string sheetName = info.Name;

                List<string> imageFiles = new List<string>();
                string[] allFiles = Directory.GetFiles(path);
                foreach (string file in allFiles)
                {
                    if (ImageProperties.IsImage(file))
                    {
                        imageFiles.Add(file);
                    }
                }

                if (imageFiles.Count > 0)
                {
                    Sheet sheet = creator.AddSheet(sheetName);
                    int Y = 0;
                    foreach (string file in imageFiles)
                    {
                        FileInfo f = new FileInfo(file);
                        ImageProperties prop = new ImageProperties();
                        prop.Path = file;
                        prop.OffsetX = 0;
                        prop.OffsetY = Y;
                        prop.MaxWidth = maxWidth;
                        prop.MaxHeight = maxHeight;
                        prop.Name = f.Name;
                        prop.Description = f.Name;
                        ImageSizeProperties results = creator.AddImage(sheet, prop);
                        Y += results.Height + spanHeight;
                    }
                }
            }
            SpreadsheetDocument document = creator.getDocument();
            // チェーンして、SaveAs->Closeとしないと握りこんで開けなくなる
            document.SaveAs(saveFilePath).Close();
            creator.Dispose();
            fs.Dispose();
            if (Directory.Exists(tmp))
            {
                Directory.Delete(tmp, true);
            }
            this.Enabled = true;
            MessageBox.Show("作成が完了しました\r\n" + saveFilePath);
        }

        private void changeTarget_CheckedChanged(object sender, EventArgs e)
        {
            if (changeTarget.Checked == true)
            {
                subDirList.Enabled = false;
            }
            else
            {
                subDirList.Enabled = true;
            }
        }
    }
}
