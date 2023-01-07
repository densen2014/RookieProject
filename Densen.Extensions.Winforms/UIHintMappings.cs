using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace Extensions.Winforms
{
    public class UIHintMappings
    {
        public static Dictionary<string, Func<DataGridViewColumn>> DataGridViewColumns
        {
            get;
        }
        static UIHintMappings()
        {
            DataGridViewColumns = new Dictionary<string, Func<DataGridViewColumn>>
        {
            {
                "TextBox",
                () => new DataGridViewTextBoxColumn()
            },
            {
                "CheckBox",
                () => new DataGridViewCheckBoxColumn(false)
            },
            {
                "TreeStateCheckBox",
                () => new DataGridViewCheckBoxColumn(true)
            },
            {
                "Link",
                () => new DataGridViewLinkColumn()
            },
            {
                "Calendar",
                () => new DataGridViewCalendarColumn()
            },
            {
                "Image",
                () => new DataGridViewImageColumn()
            },
            {
                "Button",
                () => new DataGridViewButtonColumn()
            },
            {
                "List",
                () => new DataGridViewComboBoxColumn()
            },
            {
                "Rollover",
                () => new DataGridViewRolloverCellColumn()
            },
            {
                "Password",
                () => new DataGridViewPasswordColumn()
            },
            {
                "Integer",
                () => new DataGridViewIntegerColumn()
            },
            {
                "Numeric",
                () => new DataGridViewNumericColumn()
            }
        };
        }
    }
}