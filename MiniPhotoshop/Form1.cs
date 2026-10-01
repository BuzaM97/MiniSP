namespace MiniPhotoshop
{
    public partial class btnBrwsImg : Form
    {
        public btnBrwsImg()
        {
            InitializeComponent();
        }

        private void lblSelectImg_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblSelectImg.Visible = false;

            PictureBox pb1 = new PictureBox();
            pb1.ImageLocation = @"C:\Users\buzam\Documents\MiniSP\MiniPhotoshop\examplePhoto.jpg";  
            pb1.SizeMode = PictureBoxSizeMode.Zoom;
            pb1.Size = new Size(400, 300);
            pb1.Location = new Point(20, 60);
            this.Controls.Add(pb1);
        }



    }
}
