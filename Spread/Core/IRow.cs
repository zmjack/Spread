using NStandard.Text.Json;

namespace Spread;

public interface IRowSerializable
{
    int Index { get; }
    string Name { get; }
    IEnumerable<Cell> Cells { get; }
}

[JsonImpl<IRow, Row>]
public interface IRow
{
    int Index { get; }
    string Name { get; }
    IEnumerable<Cell> Cells { get; }

    Cell this[int index] { get; }
}
