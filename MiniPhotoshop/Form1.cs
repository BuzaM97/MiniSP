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
            lblSelectImg.Text = string.Empty;
        }
    }
}
