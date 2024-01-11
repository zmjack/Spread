using System.Collections;
using System.Diagnostics;

namespace Mung;

public interface ICoRow
{
    int Index { get; }
    string Name { get; }
    ICoCell this[int index] { get; }
    IEnumerable<ICoCell> Cells { get; }
}

[DebuggerDisplay("{Name}")]
public class Row : ICoRow, IEnumerable<ICoCell>
{
    private readonly Dictionary<int, ICoCell> _innerCells = [];

    internal readonly MungSheet _sheet;
    private readonly int _rowIndex;

    public int Index => _rowIndex;
    public string Name => $"{_rowIndex + 1}";

    internal Row(MungSheet sheet, int index)
    {
        _sheet = sheet;
        _rowIndex = index;
    }

    public IEnumerable<ICoCell> Cells
    {
        get
        {
            var maxIndex = _sheet._maxColumnIndex;
            for (var i = 0; i <= maxIndex; i++)
            {
                yield return this[i];
            }
        }
    }

    public ICoCell this[int index]
    {
        get
        {
            if (_innerCells.TryGetValue(index, out ICoCell? value)) return value;
            else
            {
                var cell = new Cell(this, index);
                _innerCells.Add(index, cell);
                if (_sheet._maxColumnIndex < index) _sheet._maxColumnIndex = index;
                return cell;
            }
        }
    }

    public IEnumerator<ICoCell> GetEnumerator()
    {
        return Cells.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Cells.GetEnumerator();
    }
}
