using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DgvSample
{
    class TextAndButtonControl : UserControl

    {

        private TextBox textbox1;

        private Button button1;



        public TextAndButtonControl()

        {

            this.textbox1 = new TextBox();

            this.Controls.Add(this.textbox1);



            this.button1 = new Button();

            this.Controls.Add(this.button1);



            this.renderControl();

            this.button1.Click += new EventHandler(button1_Click);

        }



        void button1_Click(object sender, EventArgs e)

        {

            MessageBox.Show("Click! The value is:" + this.Text);

        }



        public override string Text

        {

            get { return this.textbox1.Text; }

            set { this.textbox1.Text = value; }

        }



        public string ButtonText

        {

            get { return this.button1.Text; }

            set { this.button1.Text = value; }

        }



        public void renderControl()

        {

            this.textbox1.Location = new Point(0, 0);

            this.textbox1.Width = 2 * this.Width / 3;

            this.textbox1.Height = this.Height;



            this.button1.Location = new Point(2 * this.Width / 3, 0);

            this.button1.Width = this.Width / 3;

            this.button1.Height = this.Height;

        }

    }
}
