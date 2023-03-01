using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Extensions.Winforms;

public class DataGridViewNumericColumn : DataGridViewColumn
{
    public DataGridViewNumericColumn() : base(new DataGridViewNumericCell())
    {
        this.CellTemplate = new DataGridViewNumericCell();
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
        get { return (DataGridViewNumericCell)this.CellTemplate; }
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
            DataGridViewNumericEditingControl? ctl =
                DataGridView?.EditingControl as DataGridViewNumericEditingControl;
            // Use the default row value when Value property is null.
            if (ctl != null)
            {
                if (this.Value == null)
                {
                    ctl.Value = decimal.Round(decimal.Parse(this.DefaultNewRowValue?.ToString()?? "0"), DecimalPlaces);
                }
                else
                {
                    ctl.Value = decimal.Round(decimal.Parse(this.Value?.ToString()??"0"), DecimalPlaces);
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
            c.ThousandsSeparator = this.ThousandsSeparator;
            c.DecimalPlaces = this.DecimalPlaces;
            c.Minimum = this.Minimum;
            c.Maximum = this.Maximum;
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
            if (this.DataGridView != null)
            {
                var dataGridViewRows = this.DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    var dataGridViewCell = dataGridViewRow.Cells[this.Index]
                        as DataGridViewNumericCell;
                    if (dataGridViewCell != null)
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
            if (this.DataGridView != null)
            {
                var dataGridViewRows = this.DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    var dataGridViewCell = dataGridViewRow.Cells[this.Index]
                        as DataGridViewNumericCell;
                    if (dataGridViewCell != null)
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
            if (this.DataGridView != null)
            {
                var dataGridViewRows = this.DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    var dataGridViewCell = dataGridViewRow.Cells[this.Index]
                        as DataGridViewNumericCell;
                    if (dataGridViewCell != null)
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
            if (this.DataGridView != null)
            {
                var dataGridViewRows = this.DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    var dataGridViewCell = dataGridViewRow.Cells[this.Index]
                        as DataGridViewNumericCell;
                    if (dataGridViewCell != null)
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
        DataGridView? dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public DataGridViewNumericEditingControl()
        {
            this.ThousandsSeparator = false;
            this.DecimalPlaces = 2;
            this.Minimum = 0;
            this.Maximum = int.MaxValue;

        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return decimal.Round(this.Value, DecimalPlaces).ToString();
            }
            set
            {
                if (value is string)
                {
                    try
                    {
                        // This will throw an exception of the string is
                        // null, empty, or not in the format of a date.
                        this.Value = decimal.Parse((string)value);
                    }
                    catch
                    {
                        // In the case of an exception, just use the
                        // default value so we're not left with a null
                        // value.
                        this.Value = 0;
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
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey
        // method.
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
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
        public DataGridView? EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

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
            valueChanged = true;
            this.EditingControlDataGridView?.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }

}