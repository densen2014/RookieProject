using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System;
using System.Windows.Forms;
using System.Linq;

namespace DgvValidatingData
{

 
    public partial class Form1 : Form
    {
        BindingSource bindingSource1 = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            //this.bindingSource1.DataSource = typeof(Person);
            //this.sampleModelBindingNavigator.BindingSource = this.bindingSource1;
            //this.dataGridView1.DataSource = this.bindingSource1;

            var list = new List<Person>
            {
                new Person
                {
                    FirstName = "Alex",
                    LastName = "Chow",
                    BirthDate = DateTime.Now,
                    Url = "blazor.app1.es",
                    IsMember = true,
                },
                new Person
                {
                    FirstName = "Juan",
                    LastName = "Calos",
                    BirthDate = new DateTime(1991, 3, 4),
                    Url = "app1.es",
                    IsMember = false,
                }
            };
            dataGridView1.Bind(list);
            //dataGridView1.DataSource = sampleModelBindingSource;
            //sampleModelBindingSource.DataSource=list; 

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
    }
}

//[TypeDescriptionProvider(typeof(MetadataTypeTypeDescriptionProvider))]
public class Person : BaseModel
{
    [Display(Name = "Id")]
    [Browsable(false)]
    public int? Id { get; set; }

    [Display(Name = "First Name", Description = "First name.", Order = 1)]
    [Required(ErrorMessage = "{0}是必须的"), StringLength(10, ErrorMessage = "长度限制为{0}")]
    [RegularExpression("w+")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name", Description = "Last name", Order = 2)]
    public string? LastName { get; set; }

    [Display(Name = "Birth Date", Description = "Date of birth.", Order = 4)]
    [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Homepage", Description = "Url of homepage.", Order = 5)]
    [Required]
    [Url]
    public string? Url { get; set; }

    [Display(Name = "Member", Description = "Is member?", Order = 3)]
    [Required(ErrorMessage = "{0}是必须的")]
    public bool IsMember { get; set; }

    [Display(Order = 3, Name = "Level")]
    [Range(1, 99, ErrorMessage = "{0} 范围是 {1} - {2}")]
    [DataType(DataType.Currency)]
    public int MemberLevel { get; set; }

}

public class BaseModel : IDataErrorInfo
{
    [Browsable(false)]
    public string this[string property]
    {
        get
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(this)[property];
            if (propertyDescriptor == null)
                return string.Empty;

            var results = new List<ValidationResult>();
            var result = Validator.TryValidateProperty(
                                      propertyDescriptor.GetValue(this),
                                      new ValidationContext(this, null, null)
                                      { MemberName = property },
                                      results);
            if (!result)
                return results.First().ErrorMessage;
            return string.Empty;
        }
    }

    [Browsable(false)]
    public string Error
    {
        get
        {
            var results = new List<ValidationResult>();
            var result = Validator.TryValidateObject(this,
                new ValidationContext(this, null, null), results, true);
            if (!result)
                return string.Join("\n", results.Select(x => x.ErrorMessage));
            else
                return null;
        }
    }
}

public static class DataGridViewExtensions
{
    public static void Bind<T>(this DataGridView grid, IList<T> data,
        bool autoGenerateColumns = true)
    {
        if (autoGenerateColumns)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var metedata = properties.Cast<PropertyDescriptor>().Select(p => new
            {
                Name = p.Name,
                HeaderText = p.Attributes.OfType<DisplayAttribute>()
                    .FirstOrDefault()?.Name ?? p.DisplayName,
                ToolTipText = p.Attributes.OfType<DisplayAttribute>()
                    .FirstOrDefault()?.GetDescription() ?? p.Description,
                Order = p.Attributes.OfType<DisplayAttribute>()
                    .FirstOrDefault()?.GetOrder() ?? int.MaxValue,
                Visible = p.IsBrowsable,
                ReadOnly = p.IsReadOnly,
                Format = p.Attributes.OfType<DisplayFormatAttribute>()
                    .FirstOrDefault()?.DataFormatString,
                Type = p.PropertyType
            });
            var columns = metedata.OrderBy(m => m.Order).Select(m =>
            {
                DataGridViewColumn c;
                if (m.Type == typeof(bool))
                {
                    c = new DataGridViewCheckBoxColumn(false);
                }
                else if (m.Type == typeof(bool?))
                {
                    c = new DataGridViewCheckBoxColumn(true);
                }
                else { c = new DataGridViewTextBoxColumn(); }
                c.DataPropertyName = m.Name;
                c.Name = m.Name;
                c.HeaderText = m.HeaderText;
                c.ToolTipText = m.ToolTipText;
                c.DefaultCellStyle.Format = m.Format;
                c.ReadOnly = m.ReadOnly;
                c.Visible = m.Visible;
                return c;
            });
            grid.Columns.Clear();
            grid.Columns.AddRange(columns.ToArray());
        }
        grid.DataSource = data;
        //this.sampleModelBindingSource.DataSource = data;
    }

 }