using NStandard.Design;
using System.Diagnostics;

namespace Spread;

public class SpreadBlankValue : ISingleton<SpreadBlankValue>
{
    public static SpreadBlankValue Instance => StaticSingleton<SpreadBlankValue>.Instance;

    public void OnInit()
    {
    }
}

[DebuggerDisplay("{Value}")]
public struct SpreadValue(object? value) : IRenderUnit
{
    public static SpreadBlankValue Blank => SpreadBlankValue.Instance;

    public object? Source { get; set; }
    public bool AutoSpan { get; set; } = true;

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
