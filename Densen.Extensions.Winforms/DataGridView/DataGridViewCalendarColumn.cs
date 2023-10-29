// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Windows.Forms;

namespace Extensions.Winforms;

public class DataGridViewCalendarColumn : DataGridViewColumn
{
    public DataGridViewCalendarColumn() : base(new DataGridViewCalendarCell())
    {
    }
    public override DataGridViewCell CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            // Ensure that the cell used for the template is a CalendarCell.
            if (value != null &&
            !value.GetType().IsAssignableFrom(typeof(DataGridViewCalendarCell)))
            {
                throw new InvalidCastException("Must be a CalendarCell");
            }
            base.CellTemplate = value;
        }
    }
}

public class DataGridViewCalendarCell : DataGridViewTextBoxCell
{
    public DataGridViewCalendarCell()
        : base()
    {
        // Use the short date format.
        // this.Style.Format = "d";
    }
    public override void InitializeEditingControl(int rowIndex, object
        initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        // Set the value of the editing control to the current cell value.
        base.InitializeEditingControl(rowIndex, initialFormattedValue,
            dataGridViewCellStyle);
        // Use the default row value when Value property is null.
        if (DataGridView?.EditingControl is DataGridViewCalendarEditingControl ctl)
        {
            if (Value == null)
            {
                ctl.Value = (DateTime)DefaultNewRowValue;
            }
            else
            {
                ctl.Value = (DateTime)Value;
            }
        }
    }

    public override Type EditType
    {
        get
        {
            // Return the type of the editing control that CalendarCell uses.
            return typeof(DataGridViewCalendarEditingControl);
        }
    }

    public override Type ValueType
    {
        get
        {
            // Return the type of the value that CalendarCell contains.

            return typeof(DateTime);
        }
    }

    public override object DefaultNewRowValue
    {
        get
        {
            // Use the current date and time as the default value.
            return DateTime.Now;
        }
    }
}


public class DataGridViewCalendarEditingControl : DateTimePicker,
    IDataGridViewEditingControl
{
    private bool? valueChanged = false;
    private int? rowIndex;

    public DataGridViewCalendarEditingControl()
    {
        //this.Format = DateTimePickerFormat.Short;
    }

    // Implements the IDataGridViewEditingControl.EditingControlFormattedValue
    // property.
    public object EditingControlFormattedValue
    {
        get
        {
            return Value.ToShortDateString();
        }
        set
        {
            if (value is String)
            {
                try
                {
                    // This will throw an exception of the string is
                    // null, empty, or not in the format of a date.
                    Value = DateTime.Parse((String)value);
                }
                catch
                {
                    // In the case of an exception, just use the
                    // default value so we're not left with a null
                    // value.
                    Value = DateTime.Now;
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
        CalendarForeColor = dataGridViewCellStyle.ForeColor;
        CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        if (!string.IsNullOrEmpty(dataGridViewCellStyle.Format))
        {
            Format = DateTimePickerFormat.Custom;
            CustomFormat = dataGridViewCellStyle.Format;
        }
        else
        {
            Format = DateTimePickerFormat.Short;
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
    public bool EditingControlValueChanged
    {
        get
        {
            return valueChanged ?? false;
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
        EditingControlDataGridView?.NotifyCurrentCellDirty(true);
        base.OnValueChanged(eventargs);
    }
}