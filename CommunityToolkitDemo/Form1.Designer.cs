namespace CommunityToolkitDemo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            消息ToolStripMenuItem = new ToolStripMenuItem();
            observablePropertyToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip1.ImageScalingSize = new Size(28, 28);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 消息ToolStripMenuItem, observablePropertyToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(960, 43);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 消息ToolStripMenuItem
            // 
            消息ToolStripMenuItem.Name = "消息ToolStripMenuItem";
            消息ToolStripMenuItem.Size = new Size(85, 39);
            消息ToolStripMenuItem.Text = "消息";
            消息ToolStripMenuItem.Click += 消息ToolStripMenuItem_Click;
            // 
            // observablePropertyToolStripMenuItem
            // 
            observablePropertyToolStripMenuItem.Name = "observablePropertyToolStripMenuItem";
            observablePropertyToolStripMenuItem.Size = new Size(284, 39);
            observablePropertyToolStripMenuItem.Text = "ObservableProperty";
            observablePropertyToolStripMenuItem.Click += observablePropertyToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 624);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 消息ToolStripMenuItem;
        private ToolStripMenuItem observablePropertyToolStripMenuItem;
    }
}