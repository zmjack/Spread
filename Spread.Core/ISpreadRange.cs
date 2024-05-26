using NStandard.Text.Json;

namespace Spread;

public interface ISpreadRange
{
    ISpreadSheet Sheet { get; }
    Cursor Start { get; }
    Cursor End { get; }

    IEnumerable<ICell> Cells { get; }
    void Merge();
}
