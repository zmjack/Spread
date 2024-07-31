namespace Spread.Form.Columns;

public class SpreadHide : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Hide;

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
