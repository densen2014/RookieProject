using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSAPI_test
{
    public partial class Form1 : Form
    {
        private DSAPI.摄像头.摄像头_DirectShow _cam;
        private Bitmap _bitmap;


        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            摄像头ToolStripMenuItem.Enabled = DSAPI.摄像头.有摄像头();
        }

        private void 预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _cam = new DSAPI.摄像头.摄像头_DirectShow();
                _cam.在指定控件显示摄像头画面(pictureBox1, 0);
            }
            catch (Exception ex)
            {
                lberror.Text = ex.Message;
            }
        }

        private void 获取CPU序列号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                lbmsg.Text = DSAPI.硬件信息.获取CPU序列号();
            }
            catch (Exception ex)
            {
                lberror.Text = ex.Message;
            }

        }

        private void 获取所有USB设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.DataSource = DSAPI.硬件信息.获取所有USB设备();
            }
            catch (Exception ex)
            {
                lberror.Text = ex.Message;
            }
        }

        private void 获取硬件信息cpuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.DataSource = DSAPI.硬件信息.获取硬件信息(DSAPI.硬件信息.硬件选择.硬盘序列号);
            }
            catch (Exception ex)
            {
                lberror.Text = ex.Message;
            }
        }

        private void 获取硬件详细信息cpuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.DataSource = DSAPI.硬件信息.获取硬件详细信息(DSAPI.硬件信息.硬件选择.CPU处理器, false);
            }
            catch (Exception ex)
            {
                lberror.Text = ex.Message;
            }
        }

        private void 从网址获取图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            pictureBox1.Image = _bitmap= DSAPI.图形图像.从网址获取图片("https://www.baidu.com/img/PCtm_d9c8750bed0b3c7d089fa7d55720d6cf.png");
        }
        private void 检查图像对象()
        {
            if (_bitmap==null) _bitmap = DSAPI.图形图像.从网址获取图片("https://www.baidu.com/img/PCtm_d9c8750bed0b3c7d089fa7d55720d6cf.png");
        }
        private void 反色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            检查图像对象();
            DSAPI.图形图像.反色(ref _bitmap);
            pictureBox1.Image = _bitmap;

        }

        private void 剪成圆形图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            检查图像对象();
            _bitmap= DSAPI.图形图像.剪成圆形图像( _bitmap);
             pictureBox1.Image = _bitmap;

        }

        private void 将指定图像区域填充指定颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            检查图像对象();
            _bitmap= DSAPI.图形图像.将指定图像区域填充指定颜色(_bitmap, new Rectangle(0, 0, 200, 200), Color.Brown);
            pictureBox1.Image = _bitmap;
        }

        private void 文字描边ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSAPI.图形图像.彩色文字图像 DSL = new DSAPI.图形图像.彩色文字图像();
             DSL.画布尺寸 = label1.Size;
            DSL.绘制描边 = true;
            DSL.描边颜色 = Color.LightPink;
            DSL.左上角偏移量 = new Point(9, 9);
            //.绘制阴影 = True 
            DSL.文字清晰度 = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            DSL.字体 = new Font("Arial", 16);
            //ProductName.Font 
            DSL.文字颜色 = Color.RoyalBlue;
            DSL.set_代码文本(value : "我的主打歌");

            label1.Image = DSL.输出图像;

         }
    } 
}
