
namespace ToolApp
{
    partial class CreateExcelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateExcelForm));
            this.targetDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.subDirList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maxWidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maxHeight = new System.Windows.Forms.TextBox();
            this.createExcel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.spanHeight = new System.Windows.Forms.TextBox();
            this.delSubFolderButton = new System.Windows.Forms.Button();
            this.changeTarget = new System.Windows.Forms.CheckBox();
            this.saveFileName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // targetDir
            // 
            this.targetDir.Location = new System.Drawing.Point(87, 17);
            this.targetDir.Name = "targetDir";
            this.targetDir.ReadOnly = true;
            this.targetDir.Size = new System.Drawing.Size(384, 23);
            this.targetDir.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "対象フォルダ:";
            // 
            // subDirList
            // 
            this.subDirList.FormattingEnabled = true;
            this.subDirList.ItemHeight = 15;
            this.subDirList.Location = new System.Drawing.Point(12, 70);
            this.subDirList.Name = "subDirList";
            this.subDirList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.subDirList.Size = new System.Drawing.Size(297, 79);
            this.subDirList.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "対象サブフォルダ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "最大幅(px):";
            // 
            // maxWidth
            // 
            this.maxWidth.Location = new System.Drawing.Point(392, 68);
            this.maxWidth.Name = "maxWidth";
            this.maxWidth.Size = new System.Drawing.Size(79, 23);
            this.maxWidth.TabIndex = 3;
            this.maxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "最大高さ(px):";
            // 
            // maxHeight
            // 
            this.maxHeight.Location = new System.Drawing.Point(392, 97);
            this.maxHeight.Name = "maxHeight";
            this.maxHeight.Size = new System.Drawing.Size(79, 23);
            this.maxHeight.TabIndex = 3;
            this.maxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // createExcel
            // 
            this.createExcel.Location = new System.Drawing.Point(11, 248);
            this.createExcel.Name = "createExcel";
            this.createExcel.Size = new System.Drawing.Size(460, 36);
            this.createExcel.TabIndex = 4;
            this.createExcel.Text = "作成する";
            this.createExcel.UseVisualStyleBackColor = true;
            this.createExcel.Click += new System.EventHandler(this.createExcel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(334, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "間隔(px):";
            // 
            // spanHeight
            // 
            this.spanHeight.Location = new System.Drawing.Point(392, 126);
            this.spanHeight.Name = "spanHeight";
            this.spanHeight.Size = new System.Drawing.Size(79, 23);
            this.spanHeight.TabIndex = 3;
            this.spanHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // delSubFolderButton
            // 
            this.delSubFolderButton.Location = new System.Drawing.Point(11, 156);
            this.delSubFolderButton.Name = "delSubFolderButton";
            this.delSubFolderButton.Size = new System.Drawing.Size(195, 33);
            this.delSubFolderButton.TabIndex = 5;
            this.delSubFolderButton.Text = "選択したサブフォルダを対象から外す";
            this.delSubFolderButton.UseVisualStyleBackColor = true;
            this.delSubFolderButton.Click += new System.EventHandler(this.delSubFolderButton_Click);
            // 
            // changeTarget
            // 
            this.changeTarget.AutoSize = true;
            this.changeTarget.Location = new System.Drawing.Point(226, 164);
            this.changeTarget.Name = "changeTarget";
            this.changeTarget.Size = new System.Drawing.Size(134, 19);
            this.changeTarget.TabIndex = 6;
            this.changeTarget.Text = "親フォルダを対象とする";
            this.changeTarget.UseVisualStyleBackColor = true;
            this.changeTarget.CheckedChanged += new System.EventHandler(this.changeTarget_CheckedChanged);
            // 
            // saveFileName
            // 
            this.saveFileName.Location = new System.Drawing.Point(97, 195);
            this.saveFileName.Name = "saveFileName";
            this.saveFileName.Size = new System.Drawing.Size(374, 23);
            this.saveFileName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "保存ファイル名:";
            // 
            // CreateExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 296);
            this.Controls.Add(this.changeTarget);
            this.Controls.Add(this.delSubFolderButton);
            this.Controls.Add(this.createExcel);
            this.Controls.Add(this.spanHeight);
            this.Controls.Add(this.maxHeight);
            this.Controls.Add(this.maxWidth);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.subDirList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveFileName);
            this.Controls.Add(this.targetDir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateExcelForm";
            this.Text = "Excelに張り付ける";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateExcelForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox targetDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox subDirList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox maxHeight;
        private System.Windows.Forms.Button createExcel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox spanHeight;
        private System.Windows.Forms.Button delSubFolderButton;
        private System.Windows.Forms.CheckBox changeTarget;
        private System.Windows.Forms.TextBox saveFileName;
        private System.Windows.Forms.Label label6;
    }
}