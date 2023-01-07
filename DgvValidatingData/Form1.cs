using Extensions.Winforms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DgvValidatingData
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var list = new List<Person>
            {
                new Person
                {
                    FirstName = "Alex",
                    LastName = "Chow",
                    Password="12234",
                    BirthDate =  DateTime.Now.AddYears(-30).AddMonths(2).AddDays(5),
                    Url = "https://blazor.app1.es",
                    MemberLevel=10,
                    IsMember = true,
                    Prepaid =25.99m
                },
                new Person
                {
                    FirstName = "Juan",
                    LastName = "Calos",
                    Password="2233",
                    BirthDate = new DateTime(1991, 3, 4),
                    Url = "app1.es",
                    IsMember = false,
                }
            };

            //注销这句可以直接看设计器效果
            dataGridView1.Bind(list);

            this.dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;

            this.dataGridView1.CellValidating += dataGridView1_CellValidating;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValidating(object? sender,
            DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
                dataGridView1.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("CompanyName")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e?.FormattedValue?.ToString()))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText =
                    "Company Name must not be empty";
                e.Cancel = true;
            }
        }

        void dataGridView1_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty;
        }


        private void buttonADD_Click(object sender, EventArgs e)
        {

        }

        private void buttonSAVE_Click(object sender, EventArgs e)
        {

        }

        private void buttonDEL_Click(object sender, EventArgs e)
        {

        }

        private void buttonREFRESH_Click(object sender, EventArgs e)
        {

        }

        private void buttonLink_Click(object sender, EventArgs e)
        {
            AppLink.CreateAppLink();
        }

        private void buttonDelLink_Click(object sender, EventArgs e)
        {
            AppLink.CreateAppLink(Delete:true);
        }
    }
}

