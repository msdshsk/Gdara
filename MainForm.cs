using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace ToolApp
{
    public partial class MainForm : Form
    {
        HotKey hotKey;
        NotifyIcon notifyIcon;
        Dictionary<string, ImageFormat> dict;
        

        private Counter counter;
        private CreateExcelForm xlsxForm;

        public MainForm()
        {
            InitializeComponent();
            this.setComponents();
            // 初期時点ではフォームを表示しない
            //this.Visible = false;
            //WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            /*
            System.Configuration.Configuration config =
                System.Configuration.ConfigurationManager.OpenExeConfiguration(
                    System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal);
            */
        }
        
        private void setComponents()
        {
            this.Icon = Properties.Resources.icon;
            this.setContextMenu();

            Application.ApplicationExit += new EventHandler(ApplicationExit);

            // hotKey = new HotKey(MOD_KEY.ALT | MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.F);

            // SSして自動的にファイル名を決めて保存する処理
            hotKey = new HotKey(MOD_KEY.CONTROL, Keys.IMENonconvert);
            hotKey.HotKeyPush += new EventHandler(hotKey_ScreenShot);

            // SSしてファイル保存ダイアログが開く処理
            hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.IMENonconvert);
            hotKey.HotKeyPush += new EventHandler(hotKey_ScreenShotAndSaveAs);

            // 設定画面を開く処理
            hotKey = new HotKey(MOD_KEY.CONTROL, Keys.D1);
            hotKey.HotKeyPush += new EventHandler(hotKey_OpenSetting);

            // 保存フォルダを開く
            hotKey = new HotKey(MOD_KEY.CONTROL, Keys.D2);
            hotKey.HotKeyPush += new EventHandler(hotKey_OpenSavePath);

            InitSettings();
            InitCounter();
        }

        private void setContextMenu()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "NotifyIcon Test";

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            // 設定表示ボタン
            ToolStripMenuItem settingItem = new ToolStripMenuItem();
            settingItem.Text = "&設定";
            settingItem.Click += formVisibleEvent;
            contextMenuStrip.Items.Add(settingItem);

            // 終了ボタン
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "&終了";
            toolStripMenuItem.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem);


            notifyIcon.DoubleClick += formVisibleEvent;
            // notifyIcon.Click += NotifyIcon_Click;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // アプリケーションの終了
            Application.Exit();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void formVisibleEvent(object sender, EventArgs e)
        {
            if (this.Visible && Form.ActiveForm == null)
            {
                this.Activate();
            }
            else
            {
                this.Visible = !this.Visible;
            }

            if (this.Visible)
            {
                this.Activate();
            }
        }

        public void hotKey_ScreenShotAndSaveAs(object sender, EventArgs e)
        {
            ScreenShot shot = new ScreenShot();
            Bitmap bitmap = shot.execute();

            SaveFileDialog dialog = new SaveFileDialog();
            if (String.IsNullOrEmpty(savePath.Text))
            {
                dialog.InitialDirectory = savePath.Text;
                var date = DateTime.Now;
                dialog.FileName = $"ss_{date:yyyyMMdd_HHmmss}." + extension.SelectedItem.ToString();
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string saveFilePath = dialog.FileName;

                bitmap.Save(saveFilePath, dict[extension.SelectedItem.ToString()]);
                beforeSavePath.Text = saveFilePath;
            }
            bitmap.Dispose();
        }

        public void hotKey_ScreenShot(object sender, EventArgs e)
        {
            ScreenShot shot = new ScreenShot();
            Bitmap bitmap = shot.execute();

            if (Directory.Exists(savePath.Text))
            {
                string backupCurrent = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(this.savePath.Text);

                string ext = extension.SelectedItem.ToString();
                string savePath = this.savePath.Text;
                string caseNo = caseNum.Text;
                if (!String.IsNullOrEmpty(caseNo))
                {
                    savePath += "\\" + caseNo;
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                }

                var num = counter.up();
                var basename = padding(num, padSize.Text);
                var prefix = fileInitial.Text;

                string saveFilePath = string.Format(savePath + "\\{0}{1}.{2}", prefix, basename, ext);

                bitmap.Save(saveFilePath, dict[ext]);
                beforeSavePath.Text = saveFilePath;
                count.Text = num.ToString();
            }
            else
            {
                // バルーンヒントを表示する
                notifyIcon.BalloonTipTitle = "PrintScreenエラー";
                notifyIcon.BalloonTipText = "保存場所に指定したフォルダが存在しないため、ファイルを保存できませんでした。";
                notifyIcon.ShowBalloonTip(5000);
            }
            bitmap.Dispose();
        }

        public void hotKey_OpenSetting(object sender, EventArgs e)
        {
            formVisibleEvent(sender, e);
        }

        public void hotKey_OpenSavePath(object sender, EventArgs e)
        {
            string save = savePath.Text;
            if (!String.IsNullOrEmpty(caseNum.Text))
            {
                save += @"\" + caseNum.Text;
            }
            Process.Start(
                Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe",
                save
            );
        }

        private void ApplicationExit(object handler, EventArgs e)
        {
            // HotKeyの登録を解除する
            hotKey.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible == true && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            // 設定値を保存
            SaveSettings();
        }

        private void refButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "保存するフォルダを指定してください。";

            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            if (!String.IsNullOrEmpty(savePath.Text))
            {
                fbd.SelectedPath = savePath.Text;
            }
            else
            {
                // fbd.SelectedPath = Directory.GetCurrentDirectory();
                fbd.SelectedPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                savePath.Text = fbd.SelectedPath;
            }
        }

        private void saveFileOpen_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (!String.IsNullOrEmpty(beforeSavePath.Text) && File.Exists(beforeSavePath.Text))
            {
                try {
                    Process proc = new Process();
                    proc.StartInfo.FileName = beforeSavePath.Text;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                }
                finally
                {
                    isError = false;
                }

            }
            else
            {
                isError = true;
            }

            if (isError)
            {
                MessageBox.Show("ファイルが削除されたか移動されているため開くことが出来ません。\r\n" + beforeSavePath.Text);
            }
        }

        private void InitSettings()
        {
            Properties.Settings.Default.Reload();
            dict = new Dictionary<string, ImageFormat>()
            {
                {"png", ImageFormat.Png },
                {"gif", ImageFormat.Gif },
                {"jpg", ImageFormat.Jpeg },
            };
            foreach (string key in dict.Keys)
            {
                extension.Items.Add(key);
            }

            extension.SelectedItem = Properties.Settings.Default.extension;
            savePath.Text = Properties.Settings.Default.savePath;
            if (String.IsNullOrEmpty(savePath.Text))
            {
                savePath.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            beforeSavePath.Text = Properties.Settings.Default.beforeSavePath;
            fileInitial.Text = Properties.Settings.Default.fileInitial;
            padSize.Text = Properties.Settings.Default.padSize;
            caseNum.Text = Properties.Settings.Default.caseNo;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.savePath = savePath.Text;
            Properties.Settings.Default.fileInitial = fileInitial.Text;
            Properties.Settings.Default.padSize = padSize.Text;
            Properties.Settings.Default.beforeSavePath = beforeSavePath.Text;
            Properties.Settings.Default.caseNo = caseNum.Text;
            Properties.Settings.Default.Save();
        }

        private void InitCounter()
        {
            int initialNum = 0;
            string checkDir;
            if (String.IsNullOrEmpty(caseNum.Text))
            {
                checkDir = savePath.Text;
            }
            else
            {
                checkDir = savePath.Text + "\\" + caseNum.Text;
            }
            if (Directory.Exists(checkDir))
            {
                int max = 0;
                string[] files = Directory.GetFiles(checkDir);
                foreach (string file in files)
                {
                    if (file.Substring(file.Length - (extension.Text.Length + 1)) != "." + extension.Text)
                    {
                        continue;
                    }

                    string basename = new FileInfo(file).Name;
                    basename = basename.Substring(fileInitial.Text.Length, basename.Length - ((extension.SelectedItem.ToString().Length + 1) + fileInitial.Text.Length));
                    int current = suppress(basename);
                    if (max < current)
                    {
                        max = current;
                    }
                }
                count.Text = max.ToString();
                initialNum = max;
            }
            
            counter = new Counter(0);
            counter.set(initialNum);
            count.Text = initialNum.ToString();
        }

        private void count_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || '9' < e.KeyChar)
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }

        private void padSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || '9' < e.KeyChar)
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }

        private int suppress(string basename)
        {
            Match m = Regex.Match(basename, "^([0-9]+)$");
            if (m.Success)
            {
                return Int32.Parse(m.Groups[1].Value);
            }
            return 0;
        }

        private string padding(int number, string padSize)
        {
            if (String.IsNullOrEmpty(padSize))
            {
                padSize = "0";
            }
            return number.ToString().PadLeft(Int32.Parse(padSize), '0');
        }

        private string padding(int number, int padSize)
        {
            return number.ToString().PadLeft(padSize, '0');
        }

        private void initButton_Click(object sender, EventArgs e)
        {
            InitCounter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // アプリケーションの終了
            Application.Exit();
        }

        private void createExcelButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (xlsxForm == null)
            {
                xlsxForm = new CreateExcelForm();
            }

            xlsxForm.FormInitialize();

            if (!xlsxForm.Visible)
            {
                xlsxForm.Visible = true;
            }
            xlsxForm.Activate();
        }
    }
}
