using Pdf417EncoderLibrary;

namespace PDF417Maker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1_Click(null,null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //https://www.codeproject.com/Articles/1347529/PDF417-Barcode-Encoder-Class-Library-and-Demo-App

            // create PDF417 barcode object
            Pdf417Encoder Encoder = new Pdf417Encoder();

            // change default data columns
            Encoder.DefaultDataColumns = 4;

            // encode barcode data
            Encoder.Encode(textBox1.Text);

            var stream = new MemoryStream();
            
            Encoder.SaveBarcodeToPngFile(stream);  

            pictureBox1.Image = Image.FromStream(stream);

        }

    }
}