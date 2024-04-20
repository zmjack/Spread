using System.Collections;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Spread;

public class SpreadSheet : ISpreadSheet, IEnumerable, ISerializable
{
    public class RowAgent(SpreadSheet sheet) : IEnumerable<IRow>
    {
        public IRow this[int index]
        {
            get
            {
                if (sheet._rows.TryGetValue(index, out IRow? value)) return value;
                else
                {
                    var row = new Row(sheet, index);
                    sheet._rows.Add(index, row);
                    if (sheet._maxRowIndex < index) sheet._maxRowIndex = index;
                    return row;
                }
            }
        }

        public IEnumerator<IRow> GetEnumerator()
        {
            return sheet._rows.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sheet._rows.Values.GetEnumerator();
        }
    }

    private readonly Dictionary<int, IRow> _rows = [];
    internal int _maxRowIndex = -1;
    internal int _maxColumnIndex = -1;

    public int RowLength => _maxRowIndex + 1;
    public int ColumnLength => _maxColumnIndex + 1;

    private readonly RowAgent _rowSelector;
    public string Name { get; set; }
    public object? Source { get; set; }

    public SpreadSheet()
    {
        _rowSelector = new RowAgent(this);
    }
    public SpreadSheet(string name) : this()
    {
        Name = name;
    }
    public SpreadSheet(string name, object? source) : this(name)
    {
        Source = source;
    }

    public IEnumerable<IRow> Rows => _rows.Values;
    public IEnumerable<ICell> GetCells()
    {
        foreach (var row in _rows.Values)
        {
            foreach (var cell in row.Cells) yield return cell;
        }
    }

    public void Add(Grid grid)
    {
        this[(RowLength, 0)].Grid = grid;
    }

    public IRow this[int row] => _rowSelector[row];
    public ICell this[Cursor cursor] => _rowSelector[cursor.Row][cursor.Col];
    public ISpreadRange this[Cursor start, Cursor end] => new SpreadRange(this, start, end);

    public Grid this[int row, int col]
    {
        set => this[(row, col)].Grid = value;
    }

    public void Merge(Cursor start, Cursor end)
    {
        if (start == end) throw new InvalidOperationException("Can not merge a single cell.");

        var rowSpan = end.Row - start.Row + 1;
        var colSpan = end.Col - start.Col + 1;
        var startCell = this[start];

        for (int row = start.Row; row <= end.Row; row++)
        {
            for (int col = start.Col; col <= end.Col; col++)
            {
                var cell = this[(row, col)];
                if (cell.Merged) continue;

                cell.Ignored = true;
                cell.RowSpan = rowSpan;
                cell.ColSpan = colSpan;
                cell.RowOffset = row - start.Row;
                cell.ColOffset = col - start.Col;

                if (cell.Value is not null)
                {
                    cell.Style = startCell.Style;
                }
            }
        }

        startCell.Ignored = false;
    }

    public void UndoMerge(Cursor cursor)
    {
        var cursorCell = this[cursor];
        if (!cursorCell.Merged) return;

        Cursor start = cursorCell.Ignored switch
        {
            false => cursor,
            true => (cursor.Row - cursorCell.RowOffset, cursor.Col - cursorCell.ColOffset),
        };
        Cursor end = cursorCell.Ignored switch
        {
            false => (cursor.Row + cursorCell.RowSpan - 1, cursor.Col + cursorCell.ColSpan - 1),
            true => (cursor.Row + cursorCell.RowSpan - 1 - cursorCell.RowOffset, cursor.Col + cursorCell.ColSpan - 1 - cursorCell.ColOffset),
        };

        for (int row = start.Row; row <= end.Row; row++)
        {
            for (int col = start.Col; col <= end.Col; col++)
            {
                var cell = this[(row, col)];
                cell.Ignored = false;
                cell.RowSpan = 1;
                cell.ColSpan = 1;
                cell.RowOffset = 0;
                cell.ColOffset = 0;
                cell.Style = cursorCell.Style;
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return _rows.Values.GetEnumerator();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}
