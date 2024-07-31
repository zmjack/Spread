using NStandard.Text.Json;

namespace Spread;

public interface IRowSerializable
{
    int Index { get; }
    string Name { get; }
    IEnumerable<Cell> Cells { get; }
}

public interface IRow
{
    int Index { get; }
    string Name { get; }
    IEnumerable<Cell> Cells { get; }

    Cell this[int index] { get; }
}
