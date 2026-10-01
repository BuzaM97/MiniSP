namespace MiniPhotoshop
{
    partial class btnBrwsImg
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
            button1 = new Button();
            lblSelectImg = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(151, 23);
            button1.TabIndex = 0;
            button1.Text = "Kép tallózása";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblSelectImg
            // 
            lblSelectImg.AutoSize = true;
            lblSelectImg.Location = new Point(236, 117);
            lblSelectImg.Name = "lblSelectImg";
            lblSelectImg.Size = new Size(110, 15);
            lblSelectImg.TabIndex = 1;
            lblSelectImg.Text = "Tallózzon egy képet";
            lblSelectImg.Click += lblSelectImg_Click;
            // 
            // btnBrwsImg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblSelectImg);
            Controls.Add(button1);
            Name = "btnBrwsImg";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label lblSelectImg;
    }
}
