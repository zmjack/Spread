namespace Spread.Form;

public class SpreadPercent : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Percent;

    /// <inheritdoc />
    public required string DataIndex { get; set; }

    /// <inheritdoc />
    public required string Label { get; set; }

    /// <inheritdoc />
    public ISpreadRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
