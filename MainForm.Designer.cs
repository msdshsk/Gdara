
namespace ToolApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.savePath = new System.Windows.Forms.TextBox();
            this.refButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.extension = new System.Windows.Forms.ComboBox();
            this.fileNameInitial = new System.Windows.Forms.Label();
            this.fileInitial = new System.Windows.Forms.TextBox();
            this.initButton = new System.Windows.Forms.Button();
            this.beforeSavePath = new System.Windows.Forms.TextBox();
            this.padSize = new System.Windows.Forms.TextBox();
            this.count = new System.Windows.Forms.TextBox();
            this.caseNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.saveFileOpen = new System.Windows.Forms.Button();
            this.createExcelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // savePath
            // 
            this.savePath.Location = new System.Drawing.Point(12, 28);
            this.savePath.Name = "savePath";
            this.savePath.Size = new System.Drawing.Size(304, 23);
            this.savePath.TabIndex = 0;
            // 
            // refButton
            // 
            this.refButton.Location = new System.Drawing.Point(322, 27);
            this.refButton.Name = "refButton";
            this.refButton.Size = new System.Drawing.Size(65, 22);
            this.refButton.TabIndex = 1;
            this.refButton.Text = "参照";
            this.refButton.UseVisualStyleBackColor = true;
            this.refButton.Click += new System.EventHandler(this.refButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "保存場所";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "拡張子:";
            // 
            // extension
            // 
            this.extension.FormattingEnabled = true;
            this.extension.Location = new System.Drawing.Point(90, 57);
            this.extension.Name = "extension";
            this.extension.Size = new System.Drawing.Size(65, 23);
            this.extension.TabIndex = 5;
            // 
            // fileNameInitial
            // 
            this.fileNameInitial.AutoSize = true;
            this.fileNameInitial.Location = new System.Drawing.Point(178, 62);
            this.fileNameInitial.Name = "fileNameInitial";
            this.fileNameInitial.Size = new System.Drawing.Size(46, 15);
            this.fileNameInitial.TabIndex = 6;
            this.fileNameInitial.Text = "頭文字:";
            // 
            // fileInitial
            // 
            this.fileInitial.Location = new System.Drawing.Point(230, 59);
            this.fileInitial.Name = "fileInitial";
            this.fileInitial.Size = new System.Drawing.Size(157, 23);
            this.fileInitial.TabIndex = 7;
            // 
            // initButton
            // 
            this.initButton.Location = new System.Drawing.Point(16, 210);
            this.initButton.Name = "initButton";
            this.initButton.Size = new System.Drawing.Size(371, 29);
            this.initButton.TabIndex = 15;
            this.initButton.Text = "枝番カウンタを更新する";
            this.initButton.UseVisualStyleBackColor = true;
            this.initButton.Click += new System.EventHandler(this.initButton_Click);
            // 
            // beforeSavePath
            // 
            this.beforeSavePath.Location = new System.Drawing.Point(16, 181);
            this.beforeSavePath.Name = "beforeSavePath";
            this.beforeSavePath.Size = new System.Drawing.Size(300, 23);
            this.beforeSavePath.TabIndex = 14;
            // 
            // padSize
            // 
            this.padSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.padSize.Location = new System.Drawing.Point(317, 119);
            this.padSize.Name = "padSize";
            this.padSize.Size = new System.Drawing.Size(69, 23);
            this.padSize.TabIndex = 11;
            // 
            // count
            // 
            this.count.Location = new System.Drawing.Point(317, 90);
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Size = new System.Drawing.Size(69, 23);
            this.count.TabIndex = 12;
            // 
            // caseNum
            // 
            this.caseNum.Location = new System.Drawing.Point(90, 90);
            this.caseNum.Name = "caseNum";
            this.caseNum.Size = new System.Drawing.Size(100, 23);
            this.caseNum.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "ゼロ埋め桁数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "枝番カウンタ:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "ケースナンバー:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.OrangeRed;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(241, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 32);
            this.button1.TabIndex = 16;
            this.button1.Text = "アプリケーション完全終了";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "最後に保存したファイル";
            // 
            // saveFileOpen
            // 
            this.saveFileOpen.Location = new System.Drawing.Point(322, 180);
            this.saveFileOpen.Name = "saveFileOpen";
            this.saveFileOpen.Size = new System.Drawing.Size(65, 22);
            this.saveFileOpen.TabIndex = 1;
            this.saveFileOpen.Text = "開く";
            this.saveFileOpen.UseVisualStyleBackColor = true;
            this.saveFileOpen.Click += new System.EventHandler(this.saveFileOpen_Click);
            // 
            // createExcelButton
            // 
            this.createExcelButton.Location = new System.Drawing.Point(16, 245);
            this.createExcelButton.Name = "createExcelButton";
            this.createExcelButton.Size = new System.Drawing.Size(125, 32);
            this.createExcelButton.TabIndex = 17;
            this.createExcelButton.Text = "Excelに張り付ける";
            this.createExcelButton.UseVisualStyleBackColor = true;
            this.createExcelButton.Click += new System.EventHandler(this.createExcelButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 289);
            this.Controls.Add(this.createExcelButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.initButton);
            this.Controls.Add(this.beforeSavePath);
            this.Controls.Add(this.padSize);
            this.Controls.Add(this.count);
            this.Controls.Add(this.caseNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fileInitial);
            this.Controls.Add(this.fileNameInitial);
            this.Controls.Add(this.extension);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveFileOpen);
            this.Controls.Add(this.refButton);
            this.Controls.Add(this.savePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox savePath;
        private System.Windows.Forms.Button refButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox extension;
        private System.Windows.Forms.Label fileNameInitial;
        private System.Windows.Forms.TextBox fileInitial;
        private System.Windows.Forms.Button initButton;
        private System.Windows.Forms.TextBox beforeSavePath;
        private System.Windows.Forms.TextBox padSize;
        private System.Windows.Forms.TextBox count;
        private System.Windows.Forms.TextBox caseNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button saveFileOpen;
        private System.Windows.Forms.Button createExcelButton;
    }
}

