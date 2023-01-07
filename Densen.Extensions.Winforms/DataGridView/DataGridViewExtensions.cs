using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Extensions.Winforms;

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
                Type = p.PropertyType,
                UIHint = p.Attributes.OfType<UIHintAttribute>()
                    .FirstOrDefault()?.UIHint
            });
            var columns = metedata.OrderBy(m => m.Order).Select(m =>
            {
                DataGridViewColumn c;
                if (!string.IsNullOrEmpty(m.UIHint) &&
                UIHintMappings.DataGridViewColumns.ContainsKey(m.UIHint))
                {
                    c = UIHintMappings.DataGridViewColumns[m.UIHint].Invoke();
                }
                else
                {
                    c = new DataGridViewTextBoxColumn();
                }
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
    }
}