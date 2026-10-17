namespace MiniPhotoshop
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
            btnBrowseImg = new Button();
            lblSelectImg = new Label();
            pbOrig = new PictureBox();
            pbResult = new PictureBox();
            btnNegate = new Button();
            chkParallel = new CheckBox();
            numGamma = new NumericUpDown();
            btnGamma = new Button();
            btnReset = new Button();
            lstHistory = new ListBox();
            btnLog = new Button();
            btnGrayscale = new Button();
            btnEqualize = new Button();
            btnBox = new Button();
            btnGauss = new Button();
            pbHistogram = new PictureBox();
            btnHistogram = new Button();
            btnSobel = new Button();
            btnLaplace = new Button();
            btnHarris = new Button();
            ((System.ComponentModel.ISupportInitialize)pbOrig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbResult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGamma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbHistogram).BeginInit();
            SuspendLayout();
            // 
            // btnBrowseImg
            // 
            btnBrowseImg.Location = new Point(0, 0);
            btnBrowseImg.Name = "btnBrowseImg";
            btnBrowseImg.Size = new Size(151, 23);
            btnBrowseImg.TabIndex = 0;
            btnBrowseImg.Text = "Kép tallózása";
            btnBrowseImg.UseVisualStyleBackColor = true;
            btnBrowseImg.Click += btnBrowseImg_Click;
            // 
            // lblSelectImg
            // 
            lblSelectImg.AutoSize = true;
            lblSelectImg.Location = new Point(12, 26);
            lblSelectImg.Name = "lblSelectImg";
            lblSelectImg.Size = new Size(110, 15);
            lblSelectImg.TabIndex = 1;
            lblSelectImg.Text = "Tallózzon egy képet";
            // 
            // pbOrig
            // 
            pbOrig.Location = new Point(43, 90);
            pbOrig.Name = "pbOrig";
            pbOrig.Size = new Size(477, 287);
            pbOrig.SizeMode = PictureBoxSizeMode.Zoom;
            pbOrig.TabIndex = 2;
            pbOrig.TabStop = false;
            // 
            // pbResult
            // 
            pbResult.Location = new Point(614, 90);
            pbResult.Name = "pbResult";
            pbResult.Size = new Size(477, 287);
            pbResult.SizeMode = PictureBoxSizeMode.Zoom;
            pbResult.TabIndex = 3;
            pbResult.TabStop = false;
            // 
            // btnNegate
            // 
            btnNegate.Location = new Point(43, 417);
            btnNegate.Name = "btnNegate";
            btnNegate.Size = new Size(157, 23);
            btnNegate.TabIndex = 4;
            btnNegate.Text = "Negálás";
            btnNegate.UseVisualStyleBackColor = true;
            btnNegate.Click += btnNegate_Click;
            // 
            // chkParallel
            // 
            chkParallel.AutoSize = true;
            chkParallel.Location = new Point(157, 4);
            chkParallel.Name = "chkParallel";
            chkParallel.Size = new Size(91, 19);
            chkParallel.TabIndex = 6;
            chkParallel.Text = "Párhuzamos";
            chkParallel.UseMnemonic = false;
            chkParallel.UseVisualStyleBackColor = true;
            chkParallel.CheckedChanged += chkParallel_CheckedChanged;
            // 
            // numGamma
            // 
            numGamma.DecimalPlaces = 2;
            numGamma.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numGamma.Location = new Point(206, 448);
            numGamma.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numGamma.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numGamma.Name = "numGamma";
            numGamma.Size = new Size(157, 23);
            numGamma.TabIndex = 7;
            numGamma.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // btnGamma
            // 
            btnGamma.Location = new Point(43, 446);
            btnGamma.Name = "btnGamma";
            btnGamma.Size = new Size(157, 23);
            btnGamma.TabIndex = 8;
            btnGamma.Text = "Gamma";
            btnGamma.UseVisualStyleBackColor = true;
            btnGamma.Click += btnGamma_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(269, 0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(79, 23);
            btnReset.TabIndex = 9;
            btnReset.Text = "Visszaállítás";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // lstHistory
            // 
            lstHistory.FormattingEnabled = true;
            lstHistory.ItemHeight = 15;
            lstHistory.Location = new Point(711, 416);
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new Size(257, 169);
            lstHistory.TabIndex = 10;
            // 
            // btnLog
            // 
            btnLog.Location = new Point(43, 475);
            btnLog.Name = "btnLog";
            btnLog.Size = new Size(157, 23);
            btnLog.TabIndex = 11;
            btnLog.Text = "Logaritmikus";
            btnLog.UseVisualStyleBackColor = true;
            btnLog.Click += btnLog_Click;
            // 
            // btnGrayscale
            // 
            btnGrayscale.Location = new Point(44, 504);
            btnGrayscale.Name = "btnGrayscale";
            btnGrayscale.Size = new Size(157, 23);
            btnGrayscale.TabIndex = 12;
            btnGrayscale.Text = "Szürkítés";
            btnGrayscale.UseVisualStyleBackColor = true;
            btnGrayscale.Click += btnGrayscale_Click;
            // 
            // btnEqualize
            // 
            btnEqualize.Location = new Point(44, 533);
            btnEqualize.Name = "btnEqualize";
            btnEqualize.Size = new Size(157, 23);
            btnEqualize.TabIndex = 13;
            btnEqualize.Text = "Hisztogram kiegyenlítés";
            btnEqualize.UseVisualStyleBackColor = true;
            btnEqualize.Click += btnEqualize_Click;
            // 
            // btnBox
            // 
            btnBox.Location = new Point(206, 533);
            btnBox.Name = "btnBox";
            btnBox.Size = new Size(157, 23);
            btnBox.TabIndex = 14;
            btnBox.Text = "Box szűrő";
            btnBox.UseVisualStyleBackColor = true;
            btnBox.Click += btnBox_Click;
            // 
            // btnGauss
            // 
            btnGauss.Location = new Point(206, 504);
            btnGauss.Name = "btnGauss";
            btnGauss.Size = new Size(157, 23);
            btnGauss.TabIndex = 15;
            btnGauss.Text = "Gauss szűrő";
            btnGauss.UseVisualStyleBackColor = true;
            btnGauss.Click += btnGauss_Click;
            // 
            // pbHistogram
            // 
            pbHistogram.Location = new Point(382, 417);
            pbHistogram.Name = "pbHistogram";
            pbHistogram.Size = new Size(299, 168);
            pbHistogram.TabIndex = 16;
            pbHistogram.TabStop = false;
            // 
            // btnHistogram
            // 
            btnHistogram.Location = new Point(44, 562);
            btnHistogram.Name = "btnHistogram";
            btnHistogram.Size = new Size(157, 23);
            btnHistogram.TabIndex = 17;
            btnHistogram.Text = "Hisztogram";
            btnHistogram.UseVisualStyleBackColor = true;
            btnHistogram.Click += btnHistogram_Click;
            // 
            // btnSobel
            // 
            btnSobel.Location = new Point(206, 562);
            btnSobel.Name = "btnSobel";
            btnSobel.Size = new Size(157, 23);
            btnSobel.TabIndex = 18;
            btnSobel.Text = "Sobel";
            btnSobel.UseVisualStyleBackColor = true;
            btnSobel.Click += btnSobel_Click;
            // 
            // btnLaplace
            // 
            btnLaplace.Location = new Point(206, 417);
            btnLaplace.Name = "btnLaplace";
            btnLaplace.Size = new Size(157, 23);
            btnLaplace.TabIndex = 19;
            btnLaplace.Text = "Laplace";
            btnLaplace.UseVisualStyleBackColor = true;
            btnLaplace.Click += btnLaplace_Click;
            // 
            // btnHarris
            // 
            btnHarris.Location = new Point(206, 475);
            btnHarris.Name = "btnHarris";
            btnHarris.Size = new Size(157, 23);
            btnHarris.TabIndex = 20;
            btnHarris.Text = "Jellemzőpontok";
            btnHarris.UseVisualStyleBackColor = true;
            btnHarris.Click += btnHarris_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 623);
            Controls.Add(btnHarris);
            Controls.Add(btnLaplace);
            Controls.Add(btnSobel);
            Controls.Add(btnHistogram);
            Controls.Add(pbHistogram);
            Controls.Add(btnGauss);
            Controls.Add(btnBox);
            Controls.Add(btnEqualize);
            Controls.Add(btnGrayscale);
            Controls.Add(btnLog);
            Controls.Add(lstHistory);
            Controls.Add(btnReset);
            Controls.Add(btnGamma);
            Controls.Add(numGamma);
            Controls.Add(chkParallel);
            Controls.Add(btnNegate);
            Controls.Add(pbResult);
            Controls.Add(pbOrig);
            Controls.Add(lblSelectImg);
            Controls.Add(btnBrowseImg);
            ForeColor = Color.Black;
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbOrig).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbResult).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGamma).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbHistogram).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBrowseImg;
        private Label lblSelectImg;
        private PictureBox pbOrig;
        private PictureBox pbResult;
        private Button btnNegate;
        private CheckBox chkParallel;
        private NumericUpDown numGamma;
        private Button btnGamma;
        private Button btnReset;
        private ListBox lstHistory;
        private Button btnLog;
        private Button btnGrayscale;
        private Button btnEqualize;
        private Button btnBox;
        private Button btnGauss;
        private PictureBox pbHistogram;
        private Button btnHistogram;
        private Button btnSobel;
        private Button btnLaplace;
        private Button btnHarris;
    }
}
