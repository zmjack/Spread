namespace Spread.Form;

public class SpreadOption : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Option;

    /// <inheritdoc />
    public string DataIndex { get; set; }

    /// <inheritdoc />
    public string Label { get; set; }

    /// <inheritdoc />
    public ISpreadRule? Rule { get; set; }

    public SpreadOptionValue[]? Options { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
