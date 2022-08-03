namespace DeterMouseIsInsideControl
{
    public partial class Form1 : Form
    {
        Graphics? g { get; set; }
        Point[] points =
        {
            new Point(20,30), 
            new Point(159,28),
            new Point(80,288),
            new Point(389,100)
        };

        public Form1()
        {
            InitializeComponent();
            this.groupBox1.MouseLeave  +=((s,e)=> this.dtpStart_MouseLeave());
            this.groupBox1.MouseMove += ((s, e) => this.dtpStart_MouseLeave());
            this.Paint  += new PaintEventHandler(this.Form1_Paint) ;
            this.MouseMove += ((s, e) => this.g_MouseLeave());
        }


        private void dtpStart_MouseLeave()
        {
            bool b = groupBox1.RectangleToScreen(groupBox1.ClientRectangle).Contains(MousePosition);            
            pictureBox1.Visible = b;
        }
        private void g_MouseLeave()
        {
            bool b = CheckPntInPoly(points,MousePosition);  
            if (b)
            {
                Pen pen = new Pen(Color.Green, 10);
                //e.Graphics.DrawCurve(pen, points, 1);
                g!.DrawLine(pen, MousePosition.X, MousePosition.Y, MousePosition.X-1, MousePosition.Y-1);
            }
            this.Text  = b?"is":"no";
        }
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Pen pen = new Pen(Color.Yellow, 20);
             //e.Graphics.DrawCurve(pen, points, 1);
             e.Graphics.DrawBeziers(pen, points );
        }

        private bool CheckPntInPoly(Point[] points, Point pnt)
        {
            if (points == null || points.Length == 0 || pnt == Point.Empty)
            {
                return false;
            }

            System.Drawing.Drawing2D.GraphicsPath myGraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            Region myRegion = new Region();
            myGraphicsPath.Reset();
            myGraphicsPath.AddPolygon(points);
            myRegion.MakeEmpty();
            myRegion.Union(myGraphicsPath);
            //返回判断点是否在多边形里
            return myRegion.IsVisible(pnt);
        }
    }
}