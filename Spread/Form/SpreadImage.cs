namespace Spread.Form;

public class SpreadImage : ISpreadColumn
{
    /// <inheritdoc />
    public string Type => UIComponent.Image;

    /// <inheritdoc />
    public string DataIndex { get; set; }

    /// <inheritdoc />
    public string Label { get; set; }

    public string Url { get; set; }

    /// <inheritdoc />
    public ISpreadRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
