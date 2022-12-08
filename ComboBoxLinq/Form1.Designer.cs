namespace ComboBoxLinq
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(381, 691);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "OFF\r\n69.2\r\n67.0\r\nD023N\r\nD024I\r\nD025N";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 145);
            this.label1.TabIndex = 1;
            this.label1.Text = "OFF，69.2，67.0，D023N，D024I，D025N，\r\n排序后要得到\r\nOFF，67.0，69.2，D023N，D025N，D024I，\r\n模拟亚音范" +
    "围，67.0-254.1，数字亚音范围：D023N-D754I";
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(407, 691);
            this.comboBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 872);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(800, 59);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 181);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 691);
            this.splitContainer1.SplitterDistance = 381;
            this.splitContainer1.SplitterWidth = 12;
            this.splitContainer1.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据1ToolStripMenuItem,
            this.数据2ToolStripMenuItem,
            this.数据3ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 36);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据1ToolStripMenuItem
            // 
            this.数据1ToolStripMenuItem.Name = "数据1ToolStripMenuItem";
            this.数据1ToolStripMenuItem.Size = new System.Drawing.Size(84, 32);
            this.数据1ToolStripMenuItem.Text = "数据1";
            this.数据1ToolStripMenuItem.Click += new System.EventHandler(this.数据1ToolStripMenuItem_Click);
            // 
            // 数据2ToolStripMenuItem
            // 
            this.数据2ToolStripMenuItem.Name = "数据2ToolStripMenuItem";
            this.数据2ToolStripMenuItem.Size = new System.Drawing.Size(84, 32);
            this.数据2ToolStripMenuItem.Text = "数据2";
            this.数据2ToolStripMenuItem.Click += new System.EventHandler(this.数据2ToolStripMenuItem_Click);
            // 
            // 数据3ToolStripMenuItem
            // 
            this.数据3ToolStripMenuItem.Name = "数据3ToolStripMenuItem";
            this.数据3ToolStripMenuItem.Size = new System.Drawing.Size(84, 32);
            this.数据3ToolStripMenuItem.Text = "数据3";
            this.数据3ToolStripMenuItem.Click += new System.EventHandler(this.数据3ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 931);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
        private SplitContainer splitContainer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 数据1ToolStripMenuItem;
        private ToolStripMenuItem 数据2ToolStripMenuItem;
        private ToolStripMenuItem 数据3ToolStripMenuItem;
    }
}