namespace WinFormsApp1
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
            pictureBox1 = new PictureBox();
            button1 = new Button();
            comboBox1 = new ComboBox();
            button2 = new Button();
            vScrollBar1 = new VScrollBar();
            listBox1 = new ListBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Location = new Point(27, 103);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1024, 1024);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(314, 12);
            button1.Name = "button1";
            button1.Size = new Size(300, 60);
            button1.TabIndex = 1;
            button1.Text = "&Change Icon";
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.Enabled = false;
            comboBox1.Location = new Point(8, 8);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(300, 36);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "comboBox1";
            comboBox1.SelectedIndexChanged += indexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(665, 15);
            button2.Name = "button2";
            button2.Size = new Size(196, 60);
            button2.TabIndex = 3;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(1467, 128);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(73, 833);
            vScrollBar1.TabIndex = 4;
            vScrollBar1.Scroll += vScrollBar1_Scroll;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 28;
            listBox1.Location = new Point(12, 117);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(1433, 844);
            listBox1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1560, 1347);
            Controls.Add(listBox1);
            Controls.Add(vScrollBar1);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;

        #endregion

        private Button button2;
        private VScrollBar vScrollBar1;
        private ListBox listBox1;
    }
}