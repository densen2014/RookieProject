using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DgvSample
{
    public partial class DgvWithUC : Form
    {
        DataGridView dataGridView1;
        
        List<ItemTest> ItemTests = new List<ItemTest>();

        TextAndButtonControl txbtnControl;

        ComboboxAndButtonControl cmbbtnControl;

        PictureBoxAndButtonControl picbtnControl;

        NumericupdownControl numericupdownbtnControl;

        public DgvWithUC()

        {

            InitializeComponent();
            Application.VisualStyleState = VisualStyleState.NonClientAreaEnabled;

            //OpenFileDialog dlg = new OpenFileDialog(); 
            //// Show all files
            //dlg.Filter = null;

            //dlg.ShowDialog();
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
            this.dataGridView1.EditingControlShowing += (s, e) => dataGridView1_EditingControlShowing(s, e);
        }

        void init()
        {
            for (int i = 0; i < 10; i++)
            {
                var ctrlType = i < 3 ? CtrlType.TextAndButton : i % 2 == 0 ? CtrlType.ComboboxAndButton : CtrlType.PictureBoxAndButton;
                ItemTests.Add(new ItemTest(i, "col1" + i.ToString(), "col2" + i.ToString(), ctrlType));
            }
            for (int i = 10; i < 15; i++)
            {
                var ctrlType = CtrlType.TextBox;
                ItemTests.Add(new ItemTest(i, "col1" + i.ToString(), "col2" + i.ToString(), ctrlType));
            }
            for (int i = 16; i < 18; i++)
            {
                var ctrlType = CtrlType.NumericupdownButton;
                ItemTests.Add(new ItemTest(i, "col1" + i.ToString(), "col2" + i.ToString(), ctrlType));
            }
            this.dataGridView1.DataSource = ItemTests;

            this.dataGridView1.Columns[0].Width = 200;

        }

        private void DgvTextAndButton_Load(object sender, EventArgs e)

        {
            init();

            //DataTable dt = new DataTable();

            //dt.Columns.Add("col1");

            //dt.Columns.Add("col2");

            //dt.Columns.Add("col3");

            //for (int j = 0; j < 20; j++)

            //{

            //    dt.Rows.Add("col1" + j.ToString(), "col2" + j.ToString(), "col3" + j.ToString());

            //}

            //this.dataGridView1.DataSource = dt;

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



            this.numericupdownbtnControl = new NumericupdownControl();

            this.numericupdownbtnControl.Visible = false;

            this.dataGridView1.Controls.Add(this.numericupdownbtnControl);



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


        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)

        {

            if (this.dataGridView1.CurrentCell.ColumnIndex == 0)

            {

                this.BeginInvoke(new MethodInvoker(setfocus));

            }

        }

        private void setfocus()

        {

            this.txbtnControl.Focus();

        }
        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)

        {

            if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

            {
                var cellStyle = ItemTests[e.RowIndex].CtrlType;

                Rectangle textRect = e.CellBounds;

                textRect.Width -= e.CellBounds.Width / 3;

                Rectangle btnRect = e.CellBounds;

                btnRect.X += textRect.Width;

                btnRect.Width = e.CellBounds.Width / 3;

                e.Paint(textRect, DataGridViewPaintParts.All);

                ControlPaint.DrawButton(e.Graphics, btnRect, ButtonState.Normal);

                StringFormat formater = new StringFormat();

                formater.Alignment = StringAlignment.Center;

                e.Graphics.DrawString(cellStyle ==
                    CtrlType.PictureBoxAndButton ? "图片框" :
                    cellStyle == CtrlType.TextAndButton ? "文本框" :
                    cellStyle == CtrlType.ComboboxAndButton ? "combo框" :
                    cellStyle == CtrlType.NumericupdownButton ? "Numeric框" :
                    cellStyle.ToString(),
                    e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), btnRect, formater);

                e.Handled = true;

            }

        }



        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)

        {

            if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

            {

                Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                var cellStyle = ItemTests[e.RowIndex].CtrlType;

                if (cellStyle == CtrlType.PictureBoxAndButton)
                {
                    this.picbtnControl.Location = rect.Location;

                    this.picbtnControl.Size = rect.Size;

                    this.picbtnControl.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    this.picbtnControl.ButtonText = "click";

                    this.picbtnControl.renderControl();

                    this.picbtnControl.Visible = true;

                }
                else if (cellStyle == CtrlType.TextAndButton)
                {
                    this.txbtnControl.Location = rect.Location;

                    this.txbtnControl.Size = rect.Size;

                    this.txbtnControl.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    this.txbtnControl.ButtonText = "click";

                    this.txbtnControl.renderControl();

                    this.txbtnControl.Visible = true;

                }
                else if (cellStyle == CtrlType.ComboboxAndButton)
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
                else if (cellStyle == CtrlType.NumericupdownButton)
                {
                    this.numericupdownbtnControl.Location = rect.Location;

                    this.numericupdownbtnControl.Size = rect.Size;

                    this.numericupdownbtnControl.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    this.numericupdownbtnControl.ButtonText = "click";

                    this.numericupdownbtnControl.renderControl();

                    this.numericupdownbtnControl.Visible = true;

                }
            }

        }



        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)

        {

            if (e.ColumnIndex == 0 && e.RowIndex > -1 && e.RowIndex != this.dataGridView1.NewRowIndex)

            {
                var cellStyle = ItemTests[e.RowIndex].CtrlType;
                if (cellStyle == CtrlType.PictureBoxAndButton)
                {

                    this.dataGridView1.CurrentCell.Value = this.picbtnControl.Text;

                    this.picbtnControl.Visible = false;

                }
                else if (cellStyle == CtrlType.TextAndButton)
                {

                    this.dataGridView1.CurrentCell.Value = this.txbtnControl.Text;

                    this.txbtnControl.Visible = false;
                }
                else if (cellStyle == CtrlType.ComboboxAndButton)
                {

                    this.dataGridView1.CurrentCell.Value = this.cmbbtnControl.Text;

                    this.cmbbtnControl.Visible = false;
                }
                else if (cellStyle == CtrlType.NumericupdownButton)
                {

                    this.dataGridView1.CurrentCell.Value = this.numericupdownbtnControl.Value;

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

            else if (this.numericupdownbtnControl.Visible == true)

            {

                Rectangle r = this.dataGridView1.GetCellDisplayRectangle(

                    this.dataGridView1.CurrentCell.ColumnIndex,

                    this.dataGridView1.CurrentCell.RowIndex,

                    true);

                this.numericupdownbtnControl.Location = r.Location;

                this.numericupdownbtnControl.Size = r.Size;

            }

        }

    }




}

