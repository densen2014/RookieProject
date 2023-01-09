using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Extensions.Winforms;

public class DataGridViewIntegerColumn : DataGridViewColumn
{
    public DataGridViewIntegerColumn() : base(new DataGridViewNumericCell())
    { 
    }
    public override DataGridViewCell CellTemplate
    {
        get
        {
            return (DataGridViewNumericCell) base.CellTemplate;
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
            DataGridViewNumericEditingControl ctl =
                DataGridView.EditingControl as DataGridViewNumericEditingControl;
            // Use the default row value when Value property is null.
            if (this.Value == null)
            {
                ctl.Value = (int)this.DefaultNewRowValue;
            }
            else
            {
                ctl.Value = (int)this.Value;
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

                return typeof(int);
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
         
    } 

    public class DataGridViewNumericEditingControl : NumericUpDown,
        IDataGridViewEditingControl
    {
        DataGridView? dataGridView;
        private bool? valueChanged = false;
        int? rowIndex;

        public DataGridViewNumericEditingControl()
        {
           this.Maximum = int.MaxValue;
            
        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return Convert.ToInt32(this.Value).ToString();
            }
            set
            {
                if (value is string)
                {
                    try
                    {
                        // This will throw an exception of the string is
                        // null, empty, or not in the format of a int.
                        this.Value = int.Parse((string)value);
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
            if (!string.IsNullOrEmpty(dataGridViewCellStyle.Format))
            {
                //this.Format = DateTimePickerFormat.Custom;
                //this.CustomFormat = dataGridViewCellStyle.Format;
            }
            else
            {
                //this.Format = DateTimePickerFormat.Short;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex ?? 0;
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
        public DataGridView EditingControlDataGridView
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
                return valueChanged?? false;
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
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }

}