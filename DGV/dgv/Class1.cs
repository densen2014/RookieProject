using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Text;

using System.Windows.Forms;


namespace dgv
{



 
        public partial class DgvTextAndButton : Form

        {

            public DgvTextAndButton()

            {

                InitializeComponent();

            }



            private void DgvTextAndButton_Load(object sender, EventArgs e)

            {

                DataTable dt = new DataTable();

                dt.Columns.Add("col1");

                dt.Columns.Add("col2");

                for (int j = 0; j < 20; j++)

                {

                    dt.Rows.Add("col1" + j.ToString(), "col2" + j.ToString());

                }

                this.dataGridView1.DataSource = dt;

                this.dataGridView1.Columns[0].Width = 150;



                this.txbtnControl = new TextAndButtonControl();

                this.txbtnControl.Visible = false;

                this.dataGridView1.Controls.Add(this.txbtnControl);



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

                    e.Graphics.DrawString("click", e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), btnRect, formater);

                    e.Handled = true;

                }

            }



            void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)

            {

                if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

                {

                    Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                    this.txbtnControl.Location = rect.Location;

                    this.txbtnControl.Size = rect.Size;

                    this.txbtnControl.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    this.txbtnControl.ButtonText = "click";

                    this.txbtnControl.renderControl();

                    this.txbtnControl.Visible = true;

                }

            }



            void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)

            {

                if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

                {

                    this.dataGridView1.CurrentCell.Value = this.txbtnControl.Text;

                    this.txbtnControl.Visible = false;

                }

            }



            void dataGridView1_Scroll(object sender, ScrollEventArgs e)

            {

                if (this.txbtnControl.Visible == true)

                {

                    Rectangle r = this.dataGridView1.GetCellDisplayRectangle(

                        this.dataGridView1.CurrentCell.ColumnIndex,

                        this.dataGridView1.CurrentCell.RowIndex,

                        true);

                    this.txbtnControl.Location = r.Location;

                    this.txbtnControl.Size = r.Size;

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

 

}
