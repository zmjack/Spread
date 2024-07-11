using NStandard.Text.Json;

namespace Spread;

[JsonImpl<IRow, Row>]
public interface IRow
{
    int Index { get; }
    string Name { get; }
    ICell this[int index] { get; }
    IEnumerable<ICell> Cells { get; }
}
