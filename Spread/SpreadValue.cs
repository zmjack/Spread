using System.Diagnostics;

namespace Spread;

[DebuggerDisplay("{Value}")]
public struct SpreadValue(object? value) : IRenderUnit
{
    public object? Source { get; set; }
    public bool AutoSpan { get; set; } = true;

    public (int RowSpan, int ColSpan) Span
    {
        get => (RowSpan, ColSpan);
        set
        {
            RowSpan = value.RowSpan;
            ColSpan = value.ColSpan;
        }
    }

    private int _rowSpan = 1;
    public int RowSpan
    {
        get => _rowSpan;
        set
        {
            if (value <= 0) throw new ArgumentException("The span value must be greater than 0.", nameof(value));
            AutoSpan = false;
            _rowSpan = value;
        }
    }

    private int _colSpan = 1;
    public int ColSpan
    {
        get => _colSpan;
        set
        {
            if (value <= 0) throw new ArgumentException("The span value must be greater than 0.", nameof(value));
            AutoSpan = false;
            _colSpan = value;
        }
    }

    public object? Value { get; } = value;
    public string? Comment { get; set; }
}
