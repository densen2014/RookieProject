using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DgvSample
{
    public partial class DgvTextAndButton : Form
    {
        DataGridView dataGridView1;

        public DgvTextAndButton()

        {

            InitializeComponent();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(620, 392);
            this.Controls.Add(this.dataGridView1);

            this.Load += (s, e) => DgvTextAndButton_Load(s, e);
            this.dataGridView1.CellPainting += (s, e) => dataGridView1_CellPainting(s, e);
            this.dataGridView1.CellEndEdit += (s, e) => dataGridView1_CellEndEdit(s, e);
            this.dataGridView1.CellBeginEdit += (s, e) => dataGridView1_CellBeginEdit(s, e);
            this.dataGridView1.Scroll += (s, e) => dataGridView1_Scroll(s, e);
        }



        private void DgvTextAndButton_Load(object sender, EventArgs e)

        {

            DataTable dt = new DataTable();

            dt.Columns.Add("col1");

            dt.Columns.Add("col2");

            dt.Columns.Add("col3");

            for (int j = 0; j < 20; j++)

            {

                dt.Rows.Add("col1" + j.ToString(), "col2" + j.ToString(), "col3" + j.ToString());

            }

            this.dataGridView1.DataSource = dt;

            this.dataGridView1.Columns[0].Width = 150;



            this.txbtnControl = new TextAndButtonControl();

            this.txbtnControl.Visible = false;

            this.dataGridView1.Controls.Add(this.txbtnControl);


            this.cmbbtnControl = new ComboboxAndButtonControl();

            this.cmbbtnControl.Visible = false;

            this.dataGridView1.Controls.Add(this.cmbbtnControl);



            this.picbtnControl = new PictureBoxAndButtonControl();

            this.picbtnControl.Visible = false;

            this.dataGridView1.Controls.Add(this.picbtnControl);



            //Handle this event to paint a textbox and button style in the cell,

            //this painting avoid using amount of usercontrols, we just need one

            this.dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);



            //Handle the cellbeginEdit event to show the usercontrol in the cell while editing

            this.dataGridView1.CellBeginEdit += new DataGridViewCellCancelEventHandler(dataGridView1_CellBeginEdit);

            //Handle the cellEndEdit event to update the cell value

            this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);



            //Handle the scroll event to reset the location and size of the usercontrol while scrolling

            this.dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);

        }



        TextAndButtonControl txbtnControl;

        ComboboxAndButtonControl cmbbtnControl;

        PictureBoxAndButtonControl picbtnControl;



        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)

        {

            if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

            {

                Rectangle textRect = e.CellBounds;

                textRect.Width -= e.CellBounds.Width / 3;

                Rectangle btnRect = e.CellBounds;

                btnRect.X += textRect.Width;

                btnRect.Width = e.CellBounds.Width / 3;

                e.Paint(textRect, DataGridViewPaintParts.All);

                ControlPaint.DrawButton(e.Graphics, btnRect, ButtonState.Normal);

                StringFormat formater = new StringFormat();

                formater.Alignment = StringAlignment.Center;

                e.Graphics.DrawString(e.RowIndex % 2 == 0 ? "文本框" : "combo框", e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), btnRect, formater);

                e.Handled = true;

            }

        }



        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)

        {

            if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

            {

                Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                if (e.RowIndex <3)
                {
                    this.picbtnControl.Location = rect.Location;

                    this.picbtnControl.Size = rect.Size;

                    this.picbtnControl.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    this.picbtnControl.ButtonText = "click";

                    this.picbtnControl.renderControl();

                    this.picbtnControl.Visible = true;

                }
                else if (e.RowIndex % 2 == 0)
                {
                    this.txbtnControl.Location = rect.Location;

                    this.txbtnControl.Size = rect.Size;

                    this.txbtnControl.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    this.txbtnControl.ButtonText = "click";

                    this.txbtnControl.renderControl();

                    this.txbtnControl.Visible = true;

                }
                else
                {
                    this.cmbbtnControl.comboBox1.Items.Clear();
                    for (int i = 0; i < e.RowIndex; i++)
                    {
                        this.cmbbtnControl.comboBox1.Items.Add(i.ToString());
                    }
                    //this.cmbbtnControl.DataSource = new int[] { 1, 2, 3, 4, 5, 6, 100 };

                    this.cmbbtnControl.Location = rect.Location;

                    this.cmbbtnControl.Size = rect.Size;

                    this.cmbbtnControl.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    this.cmbbtnControl.ButtonText = "click";

                    this.cmbbtnControl.renderControl();

                    this.cmbbtnControl.Visible = true;
                }
            }

        }



        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)

        {

            if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

            {
                if (e.RowIndex < 3)
                {

                    this.dataGridView1.CurrentCell.Value = this.picbtnControl.Text;

                    this.picbtnControl.Visible = false;

                }
                else if (e.RowIndex % 2 == 0)
                {

                    this.dataGridView1.CurrentCell.Value = this.txbtnControl.Text;

                    this.txbtnControl.Visible = false;
                }
                else
                {

                    this.dataGridView1.CurrentCell.Value = this.cmbbtnControl.Text;

                    this.cmbbtnControl.Visible = false;
                }
            }

        }



        void dataGridView1_Scroll(object sender, ScrollEventArgs e)

        {

            if (this.picbtnControl.Visible == true)

            {

                Rectangle r = this.dataGridView1.GetCellDisplayRectangle(

                    this.dataGridView1.CurrentCell.ColumnIndex,

                    this.dataGridView1.CurrentCell.RowIndex,

                    true);

                this.picbtnControl.Location = r.Location;

                this.picbtnControl.Size = r.Size;

            }


            else if (this.txbtnControl.Visible == true)

            {

                Rectangle r = this.dataGridView1.GetCellDisplayRectangle(

                    this.dataGridView1.CurrentCell.ColumnIndex,

                    this.dataGridView1.CurrentCell.RowIndex,

                    true);

                this.txbtnControl.Location = r.Location;

                this.txbtnControl.Size = r.Size;

            }

            else if (this.cmbbtnControl.Visible == true)

            {

                Rectangle r = this.dataGridView1.GetCellDisplayRectangle(

                    this.dataGridView1.CurrentCell.ColumnIndex,

                    this.dataGridView1.CurrentCell.RowIndex,

                    true);

                this.cmbbtnControl.Location = r.Location;

                this.cmbbtnControl.Size = r.Size;

            }

        }

    }



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



        public string Text

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
    class ComboboxAndButtonControl : UserControl

    {

        public ComboBox comboBox1;

        private Button button1;

        public object DataSource;



        public ComboboxAndButtonControl()

        {

            this.comboBox1 = new ComboBox();

            //this.comboBox1.DataSource = DataSource ?? new int[] { 1, 2, 3, 4, 5, 6, 100 };

            this.Controls.Add(this.comboBox1);



            this.button1 = new Button();

            this.Controls.Add(this.button1);



            this.renderControl();

            this.button1.Click += new EventHandler(button1_Click);

        }



        void button1_Click(object sender, EventArgs e)

        {

            MessageBox.Show("Click! The value is:" + this.Text);

        }



        public string Text

        {

            get { return this.comboBox1.Text; }

            set { this.comboBox1.Text = value; }

        }



        public string ButtonText

        {

            get { return this.button1.Text; }

            set { this.button1.Text = value; }

        }



        public void renderControl()

        {

            this.comboBox1.Location = new Point(0, 0);

            this.comboBox1.Width = 2 * this.Width / 3;

            this.comboBox1.Height = this.Height;



            this.button1.Location = new Point(2 * this.Width / 3, 0);

            this.button1.Width = this.Width / 3;

            this.button1.Height = this.Height;

        }

    }
    class PictureBoxAndButtonControl : UserControl

    {

        private PictureBox pictureBox1;

        private Button button1;



        public PictureBoxAndButtonControl()

        {

            this.pictureBox1 = new PictureBox();

            this.pictureBox1.BackColor = Color.Red;

            this.Controls.Add(this.pictureBox1);



            this.button1 = new Button();

            this.Controls.Add(this.button1);



            this.renderControl();

            this.button1.Click += new EventHandler(button1_Click);

        }



        void button1_Click(object sender, EventArgs e)

        {

            MessageBox.Show("Click! The value is:" + this.Text);

        }



        public string Text

        {

            get { return this.pictureBox1.Text; }

            set { this.pictureBox1.Text = value; }

        }



        public string ButtonText

        {

            get { return this.button1.Text; }

            set { this.button1.Text = value; }

        }



        public void renderControl()

        {

            this.pictureBox1.Location = new Point(0, 0);

            this.pictureBox1.Width = 2 * this.Width / 3;

            this.pictureBox1.Height = this.Height *2 ;



            this.button1.Location = new Point(2 * this.Width / 3, 0);

            this.button1.Width = this.Width / 3;

            this.button1.Height = this.Height;

        }

    }

}

