// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAddMoreCtrls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void Test()
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                Button[] ucList = new Button[50];//定义一个控件数组 用于存放多个控件
                for (int i = 0; i < ucList.Length; i++)
                {
                    Button c = new Button();//新建一个用户控件
                    c.Height = 50;
                    c.Text = i.ToString();
                    c.BackColor = Color.OrangeRed;
                    ucList[i] = c;//将用户控件放入控件数组集合
                }
                if (InvokeRequired) { 
                    BeginInvoke(new Action(() => {
                        flowLayoutPanel1.SuspendLayout();
                        flowLayoutPanel1.Controls.AddRange(ucList);
                        flowLayoutPanel1.ResumeLayout();
                    }));
                }
                else
                {
                    flowLayoutPanel1.SuspendLayout();
                    flowLayoutPanel1.Controls.AddRange(ucList);
                    flowLayoutPanel1.ResumeLayout();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            sw.Stop();
            if (InvokeRequired)
                BeginInvoke(new Action(() => Text = $"{flowLayoutPanel1.Controls.Count}|{sw.ElapsedMilliseconds}"));
            else
                Text = $"{flowLayoutPanel1.Controls.Count}|{sw.ElapsedMilliseconds}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.DoubleBuffered = true;
            ///启用双缓冲 解决列表滑动闪烁问题
            flowLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic).SetValue(flowLayoutPanel1, true, null);
            //Test();

            Task.Run( () =>
            {
                //Task.Delay(3000).Wait();
                for (int i = 0; i < 20; i++)
                {
                    Debug.WriteLine(i.ToString());
                    Task.Delay(300).Wait();
                    Debug.WriteLine(i.ToString());
                    BeginInvoke(new Action(() => Text = $"ADD {i}"));
                    Test();
                }
            });

            Text = "SHOW";
        }
    }
}
