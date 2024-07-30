using NStandard.Text.Json;
using System.Collections;

namespace Spread;

[JsonAs<SpreadSheet, ISpreadSheetSerializable>]
public class SpreadSheet : ISpreadSheet, IEnumerable, ISpreadSheetSerializable
{
    public class RowAgent(SpreadSheet sheet) : IEnumerable<Row>
    {
        public Row this[int index]
        {
            get
            {
                if (sheet._rows.TryGetValue(index, out Row? value)) return value;
                else
                {
                    var row = new Row(sheet, index);
                    sheet._rows.Add(index, row);
                    if (sheet._maxRowIndex < index) sheet._maxRowIndex = index;
                    return row;
                }
            }
        }

        public IEnumerator<Row> GetEnumerator()
        {
            return sheet._rows.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sheet._rows.Values.GetEnumerator();
        }
    }

    private readonly Dictionary<int, Row> _rows = [];
    internal int _maxRowIndex = -1;
    internal int _maxColumnIndex = -1;

    public int RowLength
    {
        get => _maxRowIndex + 1;
        set => _maxRowIndex = value - 1;
    }
    public int ColumnLength
    {
        get => _maxColumnIndex + 1;
        set => _maxColumnIndex = value - 1;
    }

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

    public IEnumerable<Row> Rows
    {
        get => _rows.Values;
        set
        {
            _rows.Clear();
            foreach (var item in value.Pairs())
            {
                _rows.Add(item.Index, item.Value);
            }
        }
    }
    public IEnumerable<Cell> GetCells()
    {
        foreach (var row in _rows.Values)
        {
            foreach (var cell in row.Cells) yield return cell;
        }
    }

    public void Add(Grid grid)
    {
        this[new Cursor(RowLength, 0)].Grid = grid;
    }

    public Row this[int row] => _rowSelector[row];
    public Cell this[Cursor cursor] => _rowSelector[cursor.Row][cursor.Col];
    public SpreadRange this[Cursor start, Cursor end] => new SpreadRange(this, start, end);

    public Grid this[int row, int col]
    {
        set => this[new Cursor(row, col)].Grid = value;
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
                var cell = this[new Cursor(row, col)];
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
            true => new(cursor.Row - cursorCell.RowOffset, cursor.Col - cursorCell.ColOffset),
        };
        Cursor end = cursorCell.Ignored switch
        {
            false => new(cursor.Row + cursorCell.RowSpan - 1, cursor.Col + cursorCell.ColSpan - 1),
            true => new(cursor.Row + cursorCell.RowSpan - 1 - cursorCell.RowOffset, cursor.Col + cursorCell.ColSpan - 1 - cursorCell.ColOffset),
        };

        for (int row = start.Row; row <= end.Row; row++)
        {
            for (int col = start.Col; col <= end.Col; col++)
            {
                var cell = this[new Cursor(row, col)];
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
}
