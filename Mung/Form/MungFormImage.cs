namespace Mung.Form;

public class MungFormImage : IMungFormColumn
{
    /// <inheritdoc cref="IMungFormColumn.Type" />
    public required string Type { get; set; }

    /// <inheritdoc cref="IMungFormColumn.DataIndex" />
    public required string DataIndex { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Label" />
    public required string Label { get; set; }

    public required string Url { get; set; }

    /// <inheritdoc cref="IMungFormColumn.Rule" />
    public MungFormRule? Rule { get; set; }

    public override string ToString()
    {
        return $"<{Type}>:{DataIndex}";
    }
}
