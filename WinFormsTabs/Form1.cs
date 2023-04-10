// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace WinFormsTabs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // click tab page change page activate color
            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);

        }

        private void tabControl1_DrawItem(object? sender, DrawItemEventArgs e)
        {
            //e.Graphics.DrawString("❎", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            //e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            //e.DrawFocusRectangle();


            if (e.Index < 0)
                return;

            Rectangle rect = e.Bounds;
            //每一项的边框

            //确定显示的文字的颜色()
            Brush b2 = Brushes.Black;
            //绘制项如果被选中则显示高亮显示背景,否则用白色
            if (Convert.ToBoolean(e.State) & Convert.ToBoolean(DrawItemState.Selected))
            {
                e.Graphics.FillRectangle(SystemBrushes.Highlight, rect);
                b2 = SystemBrushes.Window;
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.Window, rect);
                b2 = SystemBrushes.Highlight;
            }


            string colorname = "green";
            SolidBrush b = new SolidBrush(System.Drawing.Color.FromName(colorname));


            ////缩小选定项区域()
            rect.Inflate(-16, -2);
            ////填充颜色(文字对应的颜色)
            //e.Graphics.FillRectangle(b, rect);
            ////绘制边框()
            //e.Graphics.DrawRectangle(Pens.Black, rect);
            //Brush b2= Brushes.Black;
            ////确定显示的文字的颜色()
            //if (Convert.ToInt32(b.Color.R) + Convert.ToInt32(b.Color.G) + Convert.ToInt32(b.Color.B) > 128 * 3)
            //{
            //    b2 = Brushes.Black;
            //}
            //else
            //{
            //    b2 = Brushes.White;
            //}
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, this.tabControl1.Font, b2, rect.X, rect.Y);
        }
    }
}
