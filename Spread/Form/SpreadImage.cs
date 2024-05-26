namespace Spread.Form;

public class SpreadImage : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Image;

    /// <inheritdoc />
    public required string DataIndex { get; set; }

    /// <inheritdoc />
    public required string Label { get; set; }

    public required string Url { get; set; }

    /// <inheritdoc />
    public ISpreadRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
