namespace SandDuneFormation
{
    partial class ControlForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.windDirPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.windspeedBar = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.windSpeedBox = new System.Windows.Forms.TextBox();
            this.openBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.browseBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.maxHBox = new System.Windows.Forms.TextBox();
            this.cellHBox = new System.Windows.Forms.TextBox();
            this.avalancheHBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.blurBox1 = new System.Windows.Forms.TextBox();
            this.pBox = new System.Windows.Forms.TextBox();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.resumeBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.setBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.windDirPanel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 100);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // windDirPanel
            // 
            this.windDirPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.windDirPanel.Location = new System.Drawing.Point(43, 3);
            this.windDirPanel.Name = "windDirPanel";
            this.windDirPanel.Size = new System.Drawing.Size(10, 10);
            this.windDirPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Wind Direction";
            // 
            // windspeedBar
            // 
            this.windspeedBar.LargeChange = 100;
            this.windspeedBar.Location = new System.Drawing.Point(15, 152);
            this.windspeedBar.Maximum = 1000;
            this.windspeedBar.Name = "windspeedBar";
            this.windspeedBar.Size = new System.Drawing.Size(111, 17);
            this.windspeedBar.TabIndex = 2;
            this.windspeedBar.Value = 400;
            this.windspeedBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Wind Speed:";
            // 
            // windSpeedBox
            // 
            this.windSpeedBox.Location = new System.Drawing.Point(91, 176);
            this.windSpeedBox.Name = "windSpeedBox";
            this.windSpeedBox.Size = new System.Drawing.Size(35, 20);
            this.windSpeedBox.TabIndex = 4;
            this.windSpeedBox.Text = "4";
            this.windSpeedBox.TextChanged += new System.EventHandler(this.windSpeedBox_TextChanged);
            // 
            // openBtn
            // 
            this.openBtn.Location = new System.Drawing.Point(12, 274);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(100, 28);
            this.openBtn.TabIndex = 5;
            this.openBtn.Text = "Open HeightMap";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "FileName:";
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(72, 245);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.Size = new System.Drawing.Size(121, 20);
            this.fileNameBox.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(118, 274);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 28);
            this.browseBtn.TabIndex = 8;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Max visible height:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cell height";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(242, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 40);
            this.label6.TabIndex = 11;
            this.label6.Text = "Avalanche height in cells";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxHBox
            // 
            this.maxHBox.Location = new System.Drawing.Point(342, 20);
            this.maxHBox.Name = "maxHBox";
            this.maxHBox.Size = new System.Drawing.Size(100, 20);
            this.maxHBox.TabIndex = 12;
            this.maxHBox.Text = "16";
            this.maxHBox.TextChanged += new System.EventHandler(this.mxHBox_TextChanged);
            // 
            // cellHBox
            // 
            this.cellHBox.Location = new System.Drawing.Point(342, 49);
            this.cellHBox.Name = "cellHBox";
            this.cellHBox.Size = new System.Drawing.Size(100, 20);
            this.cellHBox.TabIndex = 13;
            this.cellHBox.Text = "8";
            this.cellHBox.TextChanged += new System.EventHandler(this.cellHBox_TextChanged);
            // 
            // avalancheHBox
            // 
            this.avalancheHBox.Location = new System.Drawing.Point(342, 83);
            this.avalancheHBox.Name = "avalancheHBox";
            this.avalancheHBox.Size = new System.Drawing.Size(100, 20);
            this.avalancheHBox.TabIndex = 14;
            this.avalancheHBox.Text = "2";
            this.avalancheHBox.TextChanged += new System.EventHandler(this.avalancheHBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Blur:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(242, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 36);
            this.label8.TabIndex = 16;
            this.label8.Text = "Chosing probability:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blurBox1
            // 
            this.blurBox1.Location = new System.Drawing.Point(342, 153);
            this.blurBox1.Name = "blurBox1";
            this.blurBox1.Size = new System.Drawing.Size(100, 20);
            this.blurBox1.TabIndex = 17;
            this.blurBox1.Text = "0";
            this.blurBox1.TextChanged += new System.EventHandler(this.blurBox1_TextChanged);
            // 
            // pBox
            // 
            this.pBox.Location = new System.Drawing.Point(342, 202);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(100, 20);
            this.pBox.TabIndex = 18;
            this.pBox.Text = "0.3";
            // 
            // pauseBtn
            // 
            this.pauseBtn.Location = new System.Drawing.Point(221, 311);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(75, 23);
            this.pauseBtn.TabIndex = 19;
            this.pauseBtn.Text = "Pause";
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // resumeBtn
            // 
            this.resumeBtn.Location = new System.Drawing.Point(302, 311);
            this.resumeBtn.Name = "resumeBtn";
            this.resumeBtn.Size = new System.Drawing.Size(75, 23);
            this.resumeBtn.TabIndex = 20;
            this.resumeBtn.Text = "Resume";
            this.resumeBtn.UseVisualStyleBackColor = true;
            this.resumeBtn.Click += new System.EventHandler(this.resumeBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(383, 311);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(75, 23);
            this.nextBtn.TabIndex = 21;
            this.nextBtn.Text = "Next";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(12, 308);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(100, 29);
            this.saveBtn.TabIndex = 22;
            this.saveBtn.Text = "Save heightmap";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // setBtn
            // 
            this.setBtn.Location = new System.Drawing.Point(245, 232);
            this.setBtn.Name = "setBtn";
            this.setBtn.Size = new System.Drawing.Size(75, 23);
            this.setBtn.TabIndex = 23;
            this.setBtn.Text = "set";
            this.setBtn.UseVisualStyleBackColor = true;
            this.setBtn.Click += new System.EventHandler(this.setBtn_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 349);
            this.Controls.Add(this.setBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.resumeBtn);
            this.Controls.Add(this.pauseBtn);
            this.Controls.Add(this.pBox);
            this.Controls.Add(this.blurBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.avalancheHBox);
            this.Controls.Add(this.cellHBox);
            this.Controls.Add(this.maxHBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.fileNameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.windSpeedBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.windspeedBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "ControlForm";
            this.Text = "ControlForm";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel windDirPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar windspeedBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox windSpeedBox;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox maxHBox;
        private System.Windows.Forms.TextBox cellHBox;
        private System.Windows.Forms.TextBox avalancheHBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox blurBox1;
        private System.Windows.Forms.TextBox pBox;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.Button resumeBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button setBtn;
    }
}