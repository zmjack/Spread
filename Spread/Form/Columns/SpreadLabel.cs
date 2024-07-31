namespace Spread.Form.Columns;

public class SpreadLabel : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Label;

    /// <inheritdoc />
    public string? DataIndex { get; set; }

    /// <inheritdoc />
    public string? Label { get; set; }

    /// <inheritdoc />
    public SpreadRule? Rule { get; set; }

    public string Content { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
