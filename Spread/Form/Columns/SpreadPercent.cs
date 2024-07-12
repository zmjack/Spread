namespace Spread.Form.Columns;

public class SpreadPercent : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Percent;

    /// <inheritdoc />
    public string DataIndex { get; set; }

    /// <inheritdoc />
    public string Label { get; set; }

    /// <inheritdoc />
    public ISpreadRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
