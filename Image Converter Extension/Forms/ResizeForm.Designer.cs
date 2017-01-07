namespace GuusBeltman.Image_Converter_Extension.Forms
{
    partial class ResizeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResizeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblQual = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.quality = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCustomHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCustomWidth = new System.Windows.Forms.TextBox();
            this.cbProportions = new System.Windows.Forms.CheckBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbCreateBackup = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbNewPostFix = new System.Windows.Forms.TextBox();
            this.cbCreateNewFile = new System.Windows.Forms.CheckBox();
            this.cbAddToSol = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quality)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Resize Images";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(455, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "You have selected one or more images to resize. Please select the desired size (w" +
    "idth x height).";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(332, 307);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "Resize";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(413, 307);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblQual);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.quality);
            this.groupBox1.Location = new System.Drawing.Point(15, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 105);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "QualityLevel";
            // 
            // lblQual
            // 
            this.lblQual.AutoSize = true;
            this.lblQual.Location = new System.Drawing.Point(126, 29);
            this.lblQual.Name = "lblQual";
            this.lblQual.Size = new System.Drawing.Size(27, 13);
            this.lblQual.TabIndex = 22;
            this.lblQual.Text = "85%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(166, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "100%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "10%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Select the image quality:";
            // 
            // quality
            // 
            this.quality.Location = new System.Drawing.Point(5, 45);
            this.quality.Maximum = 100;
            this.quality.Minimum = 10;
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(194, 45);
            this.quality.TabIndex = 18;
            this.quality.Value = 85;
            this.quality.Scroll += new System.EventHandler(this.quality_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbCustomHeight);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbCustomWidth);
            this.groupBox2.Controls.Add(this.cbProportions);
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(15, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(473, 135);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "pixels  (0 = auto)";
            // 
            // tbCustomHeight
            // 
            this.tbCustomHeight.Enabled = false;
            this.tbCustomHeight.Location = new System.Drawing.Point(144, 88);
            this.tbCustomHeight.Name = "tbCustomHeight";
            this.tbCustomHeight.Size = new System.Drawing.Size(45, 20);
            this.tbCustomHeight.TabIndex = 22;
            this.tbCustomHeight.Text = "1024";
            this.tbCustomHeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCustomHeight_PreChange);
            this.tbCustomHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCustomHeight_Changed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "x";
            // 
            // tbCustomWidth
            // 
            this.tbCustomWidth.Enabled = false;
            this.tbCustomWidth.Location = new System.Drawing.Point(75, 88);
            this.tbCustomWidth.Name = "tbCustomWidth";
            this.tbCustomWidth.Size = new System.Drawing.Size(45, 20);
            this.tbCustomWidth.TabIndex = 20;
            this.tbCustomWidth.Text = "1024";
            this.tbCustomWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCustomWidth_PreChange);
            this.tbCustomWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCustomWidth_Changed);
            // 
            // cbProportions
            // 
            this.cbProportions.AutoSize = true;
            this.cbProportions.Location = new System.Drawing.Point(25, 112);
            this.cbProportions.Name = "cbProportions";
            this.cbProportions.Size = new System.Drawing.Size(106, 17);
            this.cbProportions.TabIndex = 19;
            this.cbProportions.Text = "Keep proportions";
            this.cbProportions.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(6, 88);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(63, 17);
            this.radioButton6.TabIndex = 18;
            this.radioButton6.Text = "Custom:";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 65);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(191, 17);
            this.radioButton5.TabIndex = 17;
            this.radioButton5.Text = "Large (resize to 1680 x 1050 pixels)";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(232, 42);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(179, 17);
            this.radioButton4.TabIndex = 16;
            this.radioButton4.Text = "Ipad (resize to 1024 x 768 pixels)";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(177, 17);
            this.radioButton3.TabIndex = 15;
            this.radioButton3.Text = "Small (resize to 800 x 600 pixels)";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(232, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(183, 17);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.Text = "Mobile (resize to 320 x 480 pixels)";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(201, 17);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Thumbnail (resize to 100 x 100 pixels)";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbCreateBackup);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tbNewPostFix);
            this.groupBox3.Controls.Add(this.cbCreateNewFile);
            this.groupBox3.Controls.Add(this.cbAddToSol);
            this.groupBox3.Location = new System.Drawing.Point(258, 196);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 105);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File output options";
            // 
            // cbCreateBackup
            // 
            this.cbCreateBackup.AutoSize = true;
            this.cbCreateBackup.Location = new System.Drawing.Point(6, 82);
            this.cbCreateBackup.Name = "cbCreateBackup";
            this.cbCreateBackup.Size = new System.Drawing.Size(194, 17);
            this.cbCreateBackup.TabIndex = 25;
            this.cbCreateBackup.Text = "Create backup (original-backup.ext)";
            this.cbCreateBackup.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "original-filename";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(164, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = ".ext";
            // 
            // tbNewPostFix
            // 
            this.tbNewPostFix.Enabled = false;
            this.tbNewPostFix.Location = new System.Drawing.Point(119, 35);
            this.tbNewPostFix.Name = "tbNewPostFix";
            this.tbNewPostFix.Size = new System.Drawing.Size(44, 20);
            this.tbNewPostFix.TabIndex = 22;
            this.tbNewPostFix.Text = "-resized";
            this.tbNewPostFix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbCreateNewFile
            // 
            this.cbCreateNewFile.AutoSize = true;
            this.cbCreateNewFile.Location = new System.Drawing.Point(6, 19);
            this.cbCreateNewFile.Name = "cbCreateNewFile";
            this.cbCreateNewFile.Size = new System.Drawing.Size(181, 17);
            this.cbCreateNewFile.TabIndex = 21;
            this.cbCreateNewFile.Text = "Keep orignal and create new file:";
            this.cbCreateNewFile.UseVisualStyleBackColor = true;
            this.cbCreateNewFile.CheckedChanged += new System.EventHandler(this.cbCreateNewFile_CheckedChanged);
            // 
            // cbAddToSol
            // 
            this.cbAddToSol.AutoSize = true;
            this.cbAddToSol.Enabled = false;
            this.cbAddToSol.Location = new System.Drawing.Point(6, 61);
            this.cbAddToSol.Name = "cbAddToSol";
            this.cbAddToSol.Size = new System.Drawing.Size(135, 17);
            this.cbAddToSol.TabIndex = 20;
            this.cbAddToSol.Text = "Add new file to solution";
            this.cbAddToSol.UseVisualStyleBackColor = true;
            // 
            // ResizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 342);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResizeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Tools Image Resize";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quality)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblQual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar quality;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCustomHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCustomWidth;
        private System.Windows.Forms.CheckBox cbProportions;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbCreateNewFile;
        private System.Windows.Forms.CheckBox cbAddToSol;
        private System.Windows.Forms.CheckBox cbCreateBackup;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbNewPostFix;
    }
}