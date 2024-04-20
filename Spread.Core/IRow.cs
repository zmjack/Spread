namespace Spread;

public interface IRow
{
    int Index { get; }
    string Name { get; }
    ICell this[int index] { get; }
    IEnumerable<ICell> Cells { get; }
}
