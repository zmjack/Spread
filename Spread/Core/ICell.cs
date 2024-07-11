namespace Spread;

public interface ICell
{
    int RowIndex { get; }
    int ColIndex { get; }
    string Name { get; }
    bool Merged { get; }

    bool Ignored { get; set; }
    int RowSpan { get; set; }
    int RowOffset { get; set; }
    int ColSpan { get; set; }
    int ColOffset { get; set; }

    ISpreadStyle? Style { get; set; }
    string? Comment { get; set; }

    public string? Scope { get; set; }
    public object? Source { get; set; }
    object? Value { get; set; }
    SpreadValue SpreadValue { set; }
    string? Text { get; }
    Grid Grid { set; }
}
