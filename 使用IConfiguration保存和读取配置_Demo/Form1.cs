using Microsoft.Extensions.Configuration;

namespace IConfiguration_demo
{
    public partial class Form1 : Form
    {
        public class MySettings
        {
            public string? Text { get; set; }
            public Color BackColor { get; set; }
            public Size Size { get; set; }
        }
        
        public Form1()
        {
            InitializeComponent();
            var mySettings = Program.Config!.GetSection("MySettings").Get<MySettings>();
            this.Text = mySettings.Text;
            this.BackColor = mySettings.BackColor;
            this.Size = mySettings.Size;

        }
    }
}
