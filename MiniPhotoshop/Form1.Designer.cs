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
            lblTime = new Label();
            chkParallel = new CheckBox();
            numGamma = new NumericUpDown();
            btnGamma = new Button();
            btnReset = new Button();
            lstHistory = new ListBox();
            btnLog = new Button();
            btnGrayscale = new Button();
            ((System.ComponentModel.ISupportInitialize)pbOrig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbResult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGamma).BeginInit();
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
            lblSelectImg.Location = new Point(41, 37);
            lblSelectImg.Name = "lblSelectImg";
            lblSelectImg.Size = new Size(110, 15);
            lblSelectImg.TabIndex = 1;
            lblSelectImg.Text = "Tallózzon egy képet";
            lblSelectImg.Click += lblSelectImg_Click;
            // 
            // pbOrig
            // 
            pbOrig.Location = new Point(43, 90);
            pbOrig.Name = "pbOrig";
            pbOrig.Size = new Size(290, 177);
            pbOrig.SizeMode = PictureBoxSizeMode.Zoom;
            pbOrig.TabIndex = 2;
            pbOrig.TabStop = false;
            // 
            // pbResult
            // 
            pbResult.Location = new Point(456, 90);
            pbResult.Name = "pbResult";
            pbResult.Size = new Size(290, 177);
            pbResult.SizeMode = PictureBoxSizeMode.Zoom;
            pbResult.TabIndex = 3;
            pbResult.TabStop = false;
            // 
            // btnNegate
            // 
            btnNegate.Location = new Point(43, 290);
            btnNegate.Name = "btnNegate";
            btnNegate.Size = new Size(76, 23);
            btnNegate.TabIndex = 4;
            btnNegate.Text = "Negálás";
            btnNegate.UseVisualStyleBackColor = true;
            btnNegate.Click += btnNegate_Click;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(456, 280);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(0, 15);
            lblTime.TabIndex = 5;
            // 
            // chkParallel
            // 
            chkParallel.AutoSize = true;
            chkParallel.Location = new Point(161, 5);
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
            numGamma.Location = new Point(126, 319);
            numGamma.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numGamma.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numGamma.Name = "numGamma";
            numGamma.Size = new Size(74, 23);
            numGamma.TabIndex = 7;
            numGamma.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // btnGamma
            // 
            btnGamma.Location = new Point(125, 290);
            btnGamma.Name = "btnGamma";
            btnGamma.Size = new Size(75, 23);
            btnGamma.TabIndex = 8;
            btnGamma.Text = "Gamma";
            btnGamma.UseVisualStyleBackColor = true;
            btnGamma.Click += btnGamma_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(254, 7);
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
            lstHistory.Location = new Point(459, 277);
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new Size(290, 109);
            lstHistory.TabIndex = 10;
            // 
            // btnLog
            // 
            btnLog.Location = new Point(206, 290);
            btnLog.Name = "btnLog";
            btnLog.Size = new Size(84, 23);
            btnLog.TabIndex = 11;
            btnLog.Text = "Logaritmikus";
            btnLog.UseVisualStyleBackColor = true;
            // 
            // btnGrayscale
            // 
            btnGrayscale.Location = new Point(297, 293);
            btnGrayscale.Name = "btnGrayscale";
            btnGrayscale.Size = new Size(75, 23);
            btnGrayscale.TabIndex = 12;
            btnGrayscale.Text = "Szürkítés";
            btnGrayscale.UseVisualStyleBackColor = true;
            btnGrayscale.Click += btnGrayscale_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGrayscale);
            Controls.Add(btnLog);
            Controls.Add(lstHistory);
            Controls.Add(btnReset);
            Controls.Add(btnGamma);
            Controls.Add(numGamma);
            Controls.Add(chkParallel);
            Controls.Add(lblTime);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBrowseImg;
        private Label lblSelectImg;
        private PictureBox pbOrig;
        private PictureBox pbResult;
        private Button btnNegate;
        private Label lblTime;
        private CheckBox chkParallel;
        private NumericUpDown numGamma;
        private Button btnGamma;
        private Button btnReset;
        private ListBox lstHistory;
        private Button btnLog;
        private Button btnGrayscale;
    }
}
