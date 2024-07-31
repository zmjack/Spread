using NStandard.Text.Json;
using System.Collections;
using System.Diagnostics;

namespace Spread;

[DebuggerDisplay("{Name}")]
[JsonAs<Row, IRowSerializable>]
public class Row : IRow, IEnumerable<Cell>, IRowSerializable
{
    private readonly Dictionary<int, Cell> _innerCells = [];

    internal readonly SpreadSheet _sheet;
    private readonly int _rowIndex;

    public int Index => _rowIndex;
    public string Name => $"{_rowIndex + 1}";

    public Row()
    {
        throw new NotImplementedException();
    }

    internal Row(SpreadSheet sheet, int index)
    {
        _sheet = sheet;
        _rowIndex = index;
    }

    public IEnumerable<Cell> Cells
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

    public Cell this[int index]
    {
        get
        {
            if (_innerCells.TryGetValue(index, out Cell? value)) return value;
            else
            {
                var cell = new Cell(this, index);
                _innerCells.Add(index, cell);
                if (_sheet._maxColumnIndex < index) _sheet._maxColumnIndex = index;
                return cell;
            }
        }
    }

    public IEnumerator<Cell> GetEnumerator()
    {
        return Cells.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Cells.GetEnumerator();
    }
}
