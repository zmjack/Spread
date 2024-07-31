namespace Spread.Form.Columns;

public class SpreadText : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Text;

    /// <inheritdoc />
    public string DataIndex { get; set; }

    /// <inheritdoc />
    public string Label { get; set; }

    /// <inheritdoc />
    public SpreadRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
