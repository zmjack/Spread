using System.Collections;
using System.Diagnostics;

namespace Spread;

[DebuggerDisplay("{Name}")]
public class Row : IRow, IEnumerable<ICell>
{
    private readonly Dictionary<int, ICell> _innerCells = [];

    internal readonly SpreadSheet _sheet;
    private readonly int _rowIndex;

    public int Index => _rowIndex;
    public string Name => $"{_rowIndex + 1}";

    internal Row(SpreadSheet sheet, int index)
    {
        _sheet = sheet;
        _rowIndex = index;
    }

    public IEnumerable<ICell> Cells
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

    public ICell this[int index]
    {
        get
        {
            if (_innerCells.TryGetValue(index, out ICell? value)) return value;
            else
            {
                var cell = new Cell(this, index);
                _innerCells.Add(index, cell);
                if (_sheet._maxColumnIndex < index) _sheet._maxColumnIndex = index;
                return cell;
            }
        }
    }

    public IEnumerator<ICell> GetEnumerator()
    {
        return Cells.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Cells.GetEnumerator();
    }
}
