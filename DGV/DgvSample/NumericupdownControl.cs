using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DgvSample
{
    class NumericupdownControl : UserControl

    {

        private NumericUpDown numericUpDown;

        private Button button1;



        public NumericupdownControl()

        {

            this.numericUpDown = new NumericUpDown();

            this.numericUpDown.BackColor = Color.Red;

            this.Controls.Add(this.numericUpDown);



            this.button1 = new Button();

            this.Controls.Add(this.button1);



            this.renderControl();

            this.button1.Click += new EventHandler(button1_Click);

        }



        void button1_Click(object sender, EventArgs e)

        {

            MessageBox.Show("Click! The value is:" + this.Text);

        }



        public decimal Value

        {

            get { return this.numericUpDown.Value; }

            set { this.numericUpDown.Value = value; }

        }



        public string ButtonText

        {

            get { return this.button1.Text; }

            set { this.button1.Text = value; }

        }



        public void renderControl()

        {

            this.numericUpDown.Location = new Point(0, 0);

            this.numericUpDown.Width = 2 * this.Width / 3;

            this.numericUpDown.Height = this.Height;



            this.button1.Location = new Point(2 * this.Width / 3, 0);

            this.button1.Width = this.Width / 3;

            this.button1.Height = this.Height;

        }

    }
}
