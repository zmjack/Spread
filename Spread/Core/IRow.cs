using NStandard.Text.Json;

namespace Spread;

public interface IRowSerializable
{
    int Index { get; }
    string Name { get; }
    IEnumerable<ICell> Cells { get; }
}

[JsonImpl<IRow, Row>]
public interface IRow
{
    int Index { get; }
    string Name { get; }
    IEnumerable<ICell> Cells { get; }

    ICell this[int index] { get; }
}
