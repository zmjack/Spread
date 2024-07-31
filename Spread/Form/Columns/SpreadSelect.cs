namespace Spread.Form.Columns;

public class SpreadSelect : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Select;

    /// <inheritdoc />
    public string DataIndex { get; set; }

    /// <inheritdoc />
    public string Label { get; set; }

    /// <inheritdoc />
    public SpreadRule? Rule { get; set; }

    public SpreadOptionValue[]? Options { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
