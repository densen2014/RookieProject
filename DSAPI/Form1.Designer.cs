namespace DSAPI_test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.串口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图形图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文字描边ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪成圆形图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.将指定图像区域填充指定颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反射ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从网址获取图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.键盘鼠标钩子ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.硬件信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取CPU序列号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取所有USB设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取硬件信息cpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取硬件详细信息cpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.摄像头ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbmsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.lberror = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.串口ToolStripMenuItem,
            this.图形图像ToolStripMenuItem,
            this.反射ToolStripMenuItem,
            this.网络ToolStripMenuItem,
            this.键盘鼠标钩子ToolStripMenuItem,
            this.文件ToolStripMenuItem,
            this.硬件信息ToolStripMenuItem,
            this.摄像头ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1172, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 串口ToolStripMenuItem
            // 
            this.串口ToolStripMenuItem.Name = "串口ToolStripMenuItem";
            this.串口ToolStripMenuItem.Size = new System.Drawing.Size(72, 32);
            this.串口ToolStripMenuItem.Text = "串口";
            // 
            // 图形图像ToolStripMenuItem
            // 
            this.图形图像ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文字描边ToolStripMenuItem,
            this.反色ToolStripMenuItem,
            this.剪成圆形图像ToolStripMenuItem,
            this.将指定图像区域填充指定颜色ToolStripMenuItem});
            this.图形图像ToolStripMenuItem.Name = "图形图像ToolStripMenuItem";
            this.图形图像ToolStripMenuItem.Size = new System.Drawing.Size(114, 33);
            this.图形图像ToolStripMenuItem.Text = "图形图像";
            // 
            // 文字描边ToolStripMenuItem
            // 
            this.文字描边ToolStripMenuItem.Name = "文字描边ToolStripMenuItem";
            this.文字描边ToolStripMenuItem.Size = new System.Drawing.Size(402, 40);
            this.文字描边ToolStripMenuItem.Text = "文字描边";
            this.文字描边ToolStripMenuItem.Click += new System.EventHandler(this.文字描边ToolStripMenuItem_Click);
            // 
            // 反色ToolStripMenuItem
            // 
            this.反色ToolStripMenuItem.Name = "反色ToolStripMenuItem";
            this.反色ToolStripMenuItem.Size = new System.Drawing.Size(402, 40);
            this.反色ToolStripMenuItem.Text = "反色";
            this.反色ToolStripMenuItem.Click += new System.EventHandler(this.反色ToolStripMenuItem_Click);
            // 
            // 剪成圆形图像ToolStripMenuItem
            // 
            this.剪成圆形图像ToolStripMenuItem.Name = "剪成圆形图像ToolStripMenuItem";
            this.剪成圆形图像ToolStripMenuItem.Size = new System.Drawing.Size(402, 40);
            this.剪成圆形图像ToolStripMenuItem.Text = "剪成圆形图像";
            this.剪成圆形图像ToolStripMenuItem.Click += new System.EventHandler(this.剪成圆形图像ToolStripMenuItem_Click);
            // 
            // 将指定图像区域填充指定颜色ToolStripMenuItem
            // 
            this.将指定图像区域填充指定颜色ToolStripMenuItem.Name = "将指定图像区域填充指定颜色ToolStripMenuItem";
            this.将指定图像区域填充指定颜色ToolStripMenuItem.Size = new System.Drawing.Size(402, 40);
            this.将指定图像区域填充指定颜色ToolStripMenuItem.Text = "将指定图像区域填充指定颜色";
            this.将指定图像区域填充指定颜色ToolStripMenuItem.Click += new System.EventHandler(this.将指定图像区域填充指定颜色ToolStripMenuItem_Click);
            // 
            // 反射ToolStripMenuItem
            // 
            this.反射ToolStripMenuItem.Name = "反射ToolStripMenuItem";
            this.反射ToolStripMenuItem.Size = new System.Drawing.Size(72, 32);
            this.反射ToolStripMenuItem.Text = "反射";
            // 
            // 网络ToolStripMenuItem
            // 
            this.网络ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.从网址获取图片ToolStripMenuItem});
            this.网络ToolStripMenuItem.Name = "网络ToolStripMenuItem";
            this.网络ToolStripMenuItem.Size = new System.Drawing.Size(72, 32);
            this.网络ToolStripMenuItem.Text = "网络";
            // 
            // 从网址获取图片ToolStripMenuItem
            // 
            this.从网址获取图片ToolStripMenuItem.Name = "从网址获取图片ToolStripMenuItem";
            this.从网址获取图片ToolStripMenuItem.Size = new System.Drawing.Size(276, 40);
            this.从网址获取图片ToolStripMenuItem.Text = "从网址获取图片";
            this.从网址获取图片ToolStripMenuItem.Click += new System.EventHandler(this.从网址获取图片ToolStripMenuItem_Click);
            // 
            // 键盘鼠标钩子ToolStripMenuItem
            // 
            this.键盘鼠标钩子ToolStripMenuItem.Name = "键盘鼠标钩子ToolStripMenuItem";
            this.键盘鼠标钩子ToolStripMenuItem.Size = new System.Drawing.Size(156, 32);
            this.键盘鼠标钩子ToolStripMenuItem.Text = "键盘鼠标钩子";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(72, 32);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 硬件信息ToolStripMenuItem
            // 
            this.硬件信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取CPU序列号ToolStripMenuItem,
            this.获取所有USB设备ToolStripMenuItem,
            this.获取硬件信息cpuToolStripMenuItem,
            this.获取硬件详细信息cpuToolStripMenuItem});
            this.硬件信息ToolStripMenuItem.Name = "硬件信息ToolStripMenuItem";
            this.硬件信息ToolStripMenuItem.Size = new System.Drawing.Size(114, 32);
            this.硬件信息ToolStripMenuItem.Text = "硬件信息";
            // 
            // 获取CPU序列号ToolStripMenuItem
            // 
            this.获取CPU序列号ToolStripMenuItem.Name = "获取CPU序列号ToolStripMenuItem";
            this.获取CPU序列号ToolStripMenuItem.Size = new System.Drawing.Size(369, 40);
            this.获取CPU序列号ToolStripMenuItem.Text = "获取CPU序列号";
            this.获取CPU序列号ToolStripMenuItem.Click += new System.EventHandler(this.获取CPU序列号ToolStripMenuItem_Click);
            // 
            // 获取所有USB设备ToolStripMenuItem
            // 
            this.获取所有USB设备ToolStripMenuItem.Name = "获取所有USB设备ToolStripMenuItem";
            this.获取所有USB设备ToolStripMenuItem.Size = new System.Drawing.Size(369, 40);
            this.获取所有USB设备ToolStripMenuItem.Text = "获取所有USB设备";
            this.获取所有USB设备ToolStripMenuItem.Click += new System.EventHandler(this.获取所有USB设备ToolStripMenuItem_Click);
            // 
            // 获取硬件信息cpuToolStripMenuItem
            // 
            this.获取硬件信息cpuToolStripMenuItem.Name = "获取硬件信息cpuToolStripMenuItem";
            this.获取硬件信息cpuToolStripMenuItem.Size = new System.Drawing.Size(369, 40);
            this.获取硬件信息cpuToolStripMenuItem.Text = "获取硬件信息-硬盘序列号";
            this.获取硬件信息cpuToolStripMenuItem.Click += new System.EventHandler(this.获取硬件信息cpuToolStripMenuItem_Click);
            // 
            // 获取硬件详细信息cpuToolStripMenuItem
            // 
            this.获取硬件详细信息cpuToolStripMenuItem.Name = "获取硬件详细信息cpuToolStripMenuItem";
            this.获取硬件详细信息cpuToolStripMenuItem.Size = new System.Drawing.Size(369, 40);
            this.获取硬件详细信息cpuToolStripMenuItem.Text = "获取硬件详细信息-cpu";
            this.获取硬件详细信息cpuToolStripMenuItem.Click += new System.EventHandler(this.获取硬件详细信息cpuToolStripMenuItem_Click);
            // 
            // 摄像头ToolStripMenuItem
            // 
            this.摄像头ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.预览ToolStripMenuItem});
            this.摄像头ToolStripMenuItem.Name = "摄像头ToolStripMenuItem";
            this.摄像头ToolStripMenuItem.Size = new System.Drawing.Size(93, 32);
            this.摄像头ToolStripMenuItem.Text = "摄像头";
            // 
            // 预览ToolStripMenuItem
            // 
            this.预览ToolStripMenuItem.Name = "预览ToolStripMenuItem";
            this.预览ToolStripMenuItem.Size = new System.Drawing.Size(171, 40);
            this.预览ToolStripMenuItem.Text = "预览";
            this.预览ToolStripMenuItem.Click += new System.EventHandler(this.预览ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbmsg,
            this.lberror});
            this.statusStrip1.Location = new System.Drawing.Point(0, 769);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1172, 54);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbmsg
            // 
            this.lbmsg.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbmsg.Name = "lbmsg";
            this.lbmsg.Size = new System.Drawing.Size(98, 45);
            this.lbmsg.Text = "MSG";
            // 
            // lberror
            // 
            this.lberror.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lberror.ForeColor = System.Drawing.Color.Red;
            this.lberror.Name = "lberror";
            this.lberror.Size = new System.Drawing.Size(133, 45);
            this.lberror.Text = "ERROR";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(67, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(586, 489);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1172, 733);
            this.splitContainer1.SplitterDistance = 390;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(390, 733);
            this.listBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("宋体", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(106, 578);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 120);
            this.label1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 823);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DSAPI_test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 串口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图形图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反射ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 键盘鼠标钩子ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 硬件信息ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem 摄像头ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取CPU序列号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lberror;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem 获取所有USB设备ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取硬件信息cpuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取硬件详细信息cpuToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lbmsg;
        private System.Windows.Forms.ToolStripMenuItem 从网址获取图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪成圆形图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 将指定图像区域填充指定颜色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文字描边ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

