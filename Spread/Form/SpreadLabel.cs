namespace Spread.Form;

public class SpreadLabel : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Label;

    /// <inheritdoc />
    public string? DataIndex { get; set; }

    /// <inheritdoc />
    public string? Label { get; set; }

    /// <inheritdoc />
    public ISpreadRule? Rule { get; set; }

    public required string Content { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
