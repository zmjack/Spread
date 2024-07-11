namespace Spread.Form;

public class SpreadNumber : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Number;

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
