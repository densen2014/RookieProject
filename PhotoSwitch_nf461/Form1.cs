using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoSwitch_nf461
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Task.Run(async ()=> await switchPhoto());
        }

        async Task switchPhoto() { 
        string[] url =
            {
                "https://hblive-img.huabanimg.com/412f3dd4056aca5345f35c814ad4a9f46ff3f8d8_/both/472x316", 
                "http://t14.baidu.com/it/u=789371821,2028710836&fm=224&app=112&f=JPEG?w=500&h=313&s=B5B23E9F55C2F4FCC09880EE03007033",
                "https://gd2.alicdn.com/imgextra/i4/0/O1CN01Y9XaVS1NTYs4viTFA_!!0-item_pic.jpg_400x400.jpg"
            };
            
            var i = 0;
            do
            {
                this.Invoke(() => { this.Text = i.ToString(); });
                this.BackgroundImage = Image.FromStream(System.Net.WebRequest.Create(url[i]).GetResponse().GetResponseStream());
                pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(url[i++]).GetResponse().GetResponseStream());
                await Task.Delay(1000); 
                if (i == url.Length) i=0;
            } while (true);

        }
        }

 public static class Formxxx { 
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke((Delegate)action);
        }
        public static void BeginInvoke(this Control control, Action action)
        {
            control.BeginInvoke((Delegate)action);
        }
    }
}
