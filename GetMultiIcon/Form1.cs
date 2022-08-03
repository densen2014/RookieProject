using System.Collections;
using System.Drawing.Imaging;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
 

        MultiIcon? readicon;
        public Form1()
        {
            InitializeComponent();
        }
        private void loadCombo(ArrayList iconInfo)
        {
            comboBox1.Items.Clear();

            foreach (MultiIcon.iconEntry thisIcon in iconInfo)
            {
                comboBox1.Items.Add(new Size(thisIcon.Width, thisIcon.Height).ToString());
            }

            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            comboBox1.Enabled = true;
        }


        private void indexChanged(object sender, System.EventArgs e)
        {
            pictureBox1.Image = readicon!.buildIcon(comboBox1.SelectedIndex).ToBitmap();
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            FileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                readicon = new MultiIcon(file.FileName);
                pictureBox1.Image = readicon.findIcon(MultiIcon.displayType.Largest).ToBitmap();
                loadCombo(readicon.iconsInfo);
            }
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png); 
        }
    }
}