// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Extensions.Winforms;

public class DataGridViewNumericColumn : DataGridViewColumn
{
    public DataGridViewNumericColumn() : base(new DataGridViewNumericCell())
    {
        CellTemplate = new DataGridViewNumericCell();
    }
    public override DataGridViewCell CellTemplate
    {
        get
        {
            return (DataGridViewNumericCell)base.CellTemplate;
        }
        set
        {
            // Ensure that the cell used for the template is a NumericCell.
            if (value != null &&
            !value.GetType().IsAssignableFrom(typeof(DataGridViewNumericCell)))
            {
                throw new InvalidCastException("Must be a NumericCell");
            }
            base.CellTemplate = value;
        }
    }
    private DataGridViewNumericCell DataGridViewNumericCellTemplate
    {
        get { return (DataGridViewNumericCell)CellTemplate; }
    }

    public class DataGridViewNumericCell : DataGridViewTextBoxCell
    {

        public bool ThousandsSeparator { get; set; }

        public int DecimalPlaces { get; set; } = 2;

        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; } = int.MaxValue;

        public DataGridViewNumericCell()
            : base()
        {
        }
        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            // Use the default row value when Value property is null.
            if (DataGridView?.EditingControl is DataGridViewNumericEditingControl ctl)
            {
                if (Value == null)
                {
                    ctl.Value = decimal.Round(decimal.Parse(DefaultNewRowValue?.ToString() ?? "0"), DecimalPlaces);
                }
                else
                {
                    ctl.Value = decimal.Round(decimal.Parse(Value?.ToString() ?? "0"), DecimalPlaces);
                }
                ctl.ThousandsSeparator = ThousandsSeparator;
                ctl.DecimalPlaces = DecimalPlaces;
                ctl.Minimum = Minimum;
                ctl.Maximum = Maximum;
            }

        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that NumericCell uses.
                return typeof(DataGridViewNumericEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that NumericCell contains.

                return typeof(decimal);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return 0;
            }
        }


        public override object Clone()
        {
            var c = (DataGridViewNumericCell)base.Clone();
            c.ThousandsSeparator = ThousandsSeparator;
            c.DecimalPlaces = DecimalPlaces;
            c.Minimum = Minimum;
            c.Maximum = Maximum;
            return c;

        }
    }

    public bool ThousandsSeparator
    {
        get
        {
            return DataGridViewNumericCellTemplate.ThousandsSeparator;
        }
        set
        {
            if (DataGridViewNumericCellTemplate != null)
                DataGridViewNumericCellTemplate.ThousandsSeparator = value;
            if (DataGridView != null)
            {
                var dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is DataGridViewNumericCell dataGridViewCell)
                    {
                        dataGridViewCell.ThousandsSeparator = value;
                    }
                }
            }
        }
    }

    [DefaultValue(2)]
    public int DecimalPlaces
    {
        get
        {
            return DataGridViewNumericCellTemplate.DecimalPlaces;
        }
        set
        {
            if (DataGridViewNumericCellTemplate != null)
                DataGridViewNumericCellTemplate.DecimalPlaces = value;
            if (DataGridView != null)
            {
                var dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is DataGridViewNumericCell dataGridViewCell)
                    {
                        dataGridViewCell.DecimalPlaces = value;
                    }
                }
            }
        }
    }

    //public bool ThousandsSeparator { get; set; }

    //public int DecimalPlaces { get; set; }

    public decimal Minimum
    {
        get
        {
            return DataGridViewNumericCellTemplate.Minimum;
        }
        set
        {
            if (DataGridViewNumericCellTemplate != null)
                DataGridViewNumericCellTemplate.Minimum = value;
            if (DataGridView != null)
            {
                var dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is DataGridViewNumericCell dataGridViewCell)
                    {
                        dataGridViewCell.Minimum = value;
                    }
                }
            }
        }
    }



    [DefaultValue(int.MaxValue)]
    public decimal Maximum
    {
        get
        {
            return DataGridViewNumericCellTemplate.Maximum;
        }
        set
        {
            if (DataGridViewNumericCellTemplate != null)
                DataGridViewNumericCellTemplate.Maximum = value;
            if (DataGridView != null)
            {
                var dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    if (dataGridViewRow.Cells[Index] is DataGridViewNumericCell dataGridViewCell)
                    {
                        dataGridViewCell.Maximum = value;
                    }
                }
            }
        }
    }



    public class DataGridViewNumericEditingControl : NumericUpDown,
        IDataGridViewEditingControl
    {
        public DataGridViewNumericEditingControl()
        {
            ThousandsSeparator = false;
            DecimalPlaces = 2;
            Minimum = 0;
            Maximum = int.MaxValue;

        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return decimal.Round(Value, DecimalPlaces).ToString();
            }
            set
            {
                if (value is string)
                {
                    try
                    {
                        // This will throw an exception of the string is
                        // null, empty, or not in the format of a date.
                        Value = decimal.Parse((string)value);
                    }
                    catch
                    {
                        // In the case of an exception, just use the
                        // default value so we're not left with a null
                        // value.
                        Value = 0;
                    }
                }
            }
        }

        // Implements the
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex
        // property.
        public int EditingControlRowIndex { get; set; }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey
        // method.
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            return (key & Keys.KeyCode) switch
            {
                Keys.Left or Keys.Up or Keys.Down or Keys.Right or Keys.Home or Keys.End or Keys.PageDown or Keys.PageUp => true,
                _ => !dataGridViewWantsInputKey,
            };
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView? EditingControlDataGridView { get; set; }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged { get; set; } = false;

        // Implements the IDataGridViewEditingControl
        // .EditingPanelCursor property.
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            EditingControlValueChanged = true;
            EditingControlDataGridView?.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }

}