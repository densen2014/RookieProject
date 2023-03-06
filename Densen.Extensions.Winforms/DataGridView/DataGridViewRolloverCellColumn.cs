using System.Drawing;
using System.Windows.Forms;

namespace Extensions.Winforms;

public class DataGridViewRolloverCellColumn : DataGridViewColumn
{
    public DataGridViewRolloverCellColumn()
    {
        this.CellTemplate = new DataGridViewRolloverCell();
    }
}

public class DataGridViewRolloverCell : DataGridViewTextBoxCell
{
    protected override void Paint(
        Graphics graphics,
        Rectangle clipBounds,
        Rectangle cellBounds,
        int rowIndex,
        DataGridViewElementStates cellState,
        object value,
        object formattedValue,
        string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
    {
        // Call the base class method to paint the default cell appearance.
        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
            value, formattedValue, errorText, cellStyle,
            advancedBorderStyle, paintParts);

        // Retrieve the client location of the mouse pointer.
        Point cursorPosition =
            this.DataGridView?.PointToClient(Cursor.Position)??new Point(0);

        // If the mouse pointer is over the current cell, draw a custom border.
        if (cellBounds.Contains(cursorPosition))
        {
            Rectangle newRect = new Rectangle(cellBounds.X + 1,
                cellBounds.Y + 1, cellBounds.Width - 4,
                cellBounds.Height - 4);
            graphics.DrawRectangle(Pens.Red, newRect);
        }
    }

    // Force the cell to repaint itself when the mouse pointer enters it.
    protected override void OnMouseEnter(int rowIndex)
    {
        this.DataGridView?.InvalidateCell(this);
    }

    // Force the cell to repaint itself when the mouse pointer leaves it.
    protected override void OnMouseLeave(int rowIndex)
    {
        this.DataGridView?.InvalidateCell(this);
    }

}