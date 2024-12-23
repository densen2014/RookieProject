﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.Diagnostics;

namespace DGV虚拟模式;

#nullable disable 

public partial class Form1 : Form
{
    private DataGridView dataGridView1 = new DataGridView();

    // Declare an ArrayList to serve as the data store.
    private System.Collections.ArrayList customers =
        new System.Collections.ArrayList();

    // Declare a Customer object to store data for a row being edited.
    private Customer customerInEdit;

    // Declare a variable to store the index of a row being edited.
    // A value of -1 indicates that there is no row currently in edit.
    private int rowInEdit = -1;

    // Declare a variable to indicate the commit scope.
    // Set this value to false to use cell-level commit scope.
    private bool rowScopeCommit = true;


    public Form1()
    {
        InitializeComponent();

        // Initialize the form.
        this.dataGridView1.Dock = DockStyle.Fill;
        this.Controls.Add(this.dataGridView1);
        this.Load += new EventHandler(Form1_Load);
        this.Text = "DataGridView virtual-mode demo (row-level commit scope)";
    }
    private void Form1_Load(object sender, EventArgs e)
    {
        var sw = new Stopwatch();
        sw.Start();

        // Enable virtual mode.
        this.dataGridView1.VirtualMode = true;

        // Connect the virtual-mode events to event handlers.
        this.dataGridView1.CellValueNeeded += new
            DataGridViewCellValueEventHandler(dataGridView1_CellValueNeeded);
        this.dataGridView1.CellValuePushed += new
            DataGridViewCellValueEventHandler(dataGridView1_CellValuePushed);
        this.dataGridView1.NewRowNeeded += new
            DataGridViewRowEventHandler(dataGridView1_NewRowNeeded);
        this.dataGridView1.RowValidated += new
            DataGridViewCellEventHandler(dataGridView1_RowValidated);
        this.dataGridView1.RowDirtyStateNeeded += new
            QuestionEventHandler(dataGridView1_RowDirtyStateNeeded);
        this.dataGridView1.CancelRowEdit += new
            QuestionEventHandler(dataGridView1_CancelRowEdit);
        this.dataGridView1.UserDeletingRow += new
            DataGridViewRowCancelEventHandler(dataGridView1_UserDeletingRow);

        // Add columns to the DataGridView.
        DataGridViewTextBoxColumn companyNameColumn = new
            DataGridViewTextBoxColumn();
        companyNameColumn.HeaderText = "Company Name";
        companyNameColumn.Name = "Company Name";
        DataGridViewTextBoxColumn contactNameColumn = new
            DataGridViewTextBoxColumn();
        contactNameColumn.HeaderText = "Contact Name";
        contactNameColumn.Name = "Contact Name";
        this.dataGridView1.Columns.Add(companyNameColumn);
        this.dataGridView1.Columns.Add(contactNameColumn);
        this.dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.DisplayedCells;

        // Add some sample entries to the data store.
        //this.customers.Add(new Customer(
        //    "Bon app'", "Laurence Lebihan"));
        //this.customers.Add(new Customer(
        //    "Bottom-Dollar Markets", "Elizabeth Lincoln"));
        //this.customers.Add(new Customer(
        //    "B's Beverages", "Victoria Ashworth"));

        // Set the row count, including the row for new records.
        //1000000 是100万
        //10000000 是一千万。
        var count = 1000000;

        for (int i = 0; i <= count; i++)
        {
            this.customers.Add(new Customer(
                               $"Tailspin Toys {count - i}", "Michael Suyama"));
        }

        this.dataGridView1.RowCount = count;

        sw.Stop();
        Text=sw.Elapsed.TotalSeconds.ToString();
    }
    private void dataGridView1_CellValueNeeded(object sender, System.Windows.Forms.DataGridViewCellValueEventArgs e)
    {
        // If this is the row for new records, no values are needed.
        if (e.RowIndex == this.dataGridView1.RowCount - 1) return;

        Customer customerTmp = null;

        // Store a reference to the Customer object for the row being painted.
        if (e.RowIndex == rowInEdit)
        {
            customerTmp = this.customerInEdit;
        }
        else
        {
            customerTmp = (Customer)this.customers[e.RowIndex];
        }

        // Set the cell value to paint using the Customer object retrieved.
        switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
        {
            case "Company Name":
                e.Value = customerTmp.CompanyName;
                break;

            case "Contact Name":
                e.Value = customerTmp.ContactName;
                break;
        }
    }
    private void dataGridView1_CellValuePushed(object sender, System.Windows.Forms.DataGridViewCellValueEventArgs e)
    {
        Customer customerTmp = null;

        // Store a reference to the Customer object for the row being edited.
        if (e.RowIndex < this.customers.Count)
        {
            // If the user is editing a new row, create a new Customer object.
            this.customerInEdit ??= new Customer(
                ((Customer)this.customers[e.RowIndex]).CompanyName,
                ((Customer)this.customers[e.RowIndex]).ContactName);
            customerTmp = this.customerInEdit;
            this.rowInEdit = e.RowIndex;
        }
        else
        {
            customerTmp = this.customerInEdit;
        }

        // Set the appropriate Customer property to the cell value entered.
        String newValue = e.Value as String;
        switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
        {
            case "Company Name":
                customerTmp.CompanyName = newValue;
                break;

            case "Contact Name":
                customerTmp.ContactName = newValue;
                break;
        }
    }

    private void dataGridView1_NewRowNeeded(object sender, System.Windows.Forms.DataGridViewRowEventArgs e)
    {
        // Create a new Customer object when the user edits
        // the row for new records.
        this.customerInEdit = new Customer();
        this.rowInEdit = this.dataGridView1.Rows.Count - 1;
    }

    private void dataGridView1_RowValidated(object sender,
    System.Windows.Forms.DataGridViewCellEventArgs e)
    {
        // Save row changes if any were made and release the edited
        // Customer object if there is one.
        if (e.RowIndex >= this.customers.Count &&
            e.RowIndex != this.dataGridView1.Rows.Count - 1)
        {
            // Add the new Customer object to the data store.
            this.customers.Add(this.customerInEdit);
            this.customerInEdit = null;
            this.rowInEdit = -1;
        }
        else if (this.customerInEdit != null &&
            e.RowIndex < this.customers.Count)
        {
            // Save the modified Customer object in the data store.
            this.customers[e.RowIndex] = this.customerInEdit;
            this.customerInEdit = null;
            this.rowInEdit = -1;
        }
        else if (this.dataGridView1.ContainsFocus)
        {
            this.customerInEdit = null;
            this.rowInEdit = -1;
        }
    }

    private void dataGridView1_RowDirtyStateNeeded(object sender, System.Windows.Forms.QuestionEventArgs e)
    {
        if (!rowScopeCommit)
        {
            // In cell-level commit scope, indicate whether the value
            // of the current cell has been modified.
            e.Response = this.dataGridView1.IsCurrentCellDirty;
        }
    }
    private void dataGridView1_CancelRowEdit(object sender, System.Windows.Forms.QuestionEventArgs e)
    {
        if (this.rowInEdit == this.dataGridView1.Rows.Count - 2 &&
            this.rowInEdit == this.customers.Count)
        {
            // If the user has canceled the edit of a newly created row,
            // replace the corresponding Customer object with a new, empty one.
            this.customerInEdit = new Customer();
        }
        else
        {
            // If the user has canceled the edit of an existing row,
            // release the corresponding Customer object.
            this.customerInEdit = null;
            this.rowInEdit = -1;
        }
    }

    private void dataGridView1_UserDeletingRow(object sender, System.Windows.Forms.DataGridViewRowCancelEventArgs e)
    {
        if (e.Row.Index < this.customers.Count)
        {
            // If the user has deleted an existing row, remove the
            // corresponding Customer object from the data store.
            this.customers.RemoveAt(e.Row.Index);
        }

        if (e.Row.Index == this.rowInEdit)
        {
            // If the user has deleted a newly created row, release
            // the corresponding Customer object.
            this.rowInEdit = -1;
            this.customerInEdit = null;
        }
    }
    public class Customer
    {
        private String companyNameValue;
        private String contactNameValue;

        public Customer()
        {
            // Leave fields empty.
        }

        public Customer(String companyName, String contactName)
        {
            companyNameValue = companyName;
            contactNameValue = contactName;
        }

        public String CompanyName
        {
            get
            {
                return companyNameValue;
            }
            set
            {
                companyNameValue = value;
            }
        }

        public String ContactName
        {
            get
            {
                return contactNameValue;
            }
            set
            {
                contactNameValue = value;
            }
        }
    }

}
