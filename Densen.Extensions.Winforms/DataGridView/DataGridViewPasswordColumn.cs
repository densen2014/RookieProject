// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.Drawing;
using System.Windows.Forms;

namespace Extensions.Winforms;

public class DataGridViewPasswordColumn : DataGridViewTextBoxColumn
{
    public DataGridViewPasswordColumn()
    {
        CellTemplate = new DataGriViewPasswordCell();
    }
    private DataGriViewPasswordCell PasswordCellTemplate
    {
        get { return (DataGriViewPasswordCell)CellTemplate; }
    }

    public bool UsePasswordCharWhenEditing
    {
        get
        {
            return PasswordCellTemplate.UsePasswordCharWhenEditing;
        }
        set
        {
            if (PasswordCellTemplate != null)
                PasswordCellTemplate.UsePasswordCharWhenEditing = value;
            if (DataGridView != null)
            {
                var dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is DataGriViewPasswordCell dataGridViewCell)
                    {
                        dataGridViewCell.UsePasswordCharWhenEditing = value;
                    }
                }
            }
        }
    }
    public class DataGriViewPasswordCell : DataGridViewTextBoxCell
    {
        public DataGriViewPasswordCell()
        {
            UsePasswordCharWhenEditing = true;
        }
        public bool UsePasswordCharWhenEditing { get; set; }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (DataGridView != null) ((TextBox)DataGridView.EditingControl).UseSystemPasswordChar = UsePasswordCharWhenEditing;
        }
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
            int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue,
            string errorText, DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            formattedValue = new string('●', $"{formattedValue}".Length);
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue,
                errorText, cellStyle, advancedBorderStyle, paintParts);
        }
        public override object Clone()
        {
            var c = (DataGriViewPasswordCell)base.Clone();
            c.UsePasswordCharWhenEditing = UsePasswordCharWhenEditing;
            return c;
        }
    }
}