using NStandard.Text.Json;
using System.Collections;

namespace Spread;

public class SpreadSheet : ISpreadSheet, IEnumerable
{
    private readonly Dictionary<int, IRow> _rows = [];
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

    public string Name { get; set; }
    public object? Source { get; set; }

    public SpreadSheet()
    {
    }
    public SpreadSheet(string name) : this()
    {
        Name = name;
    }
    public SpreadSheet(string name, object? source) : this(name)
    {
        Source = source;
    }

    public IEnumerable<IRow> Rows
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

    public IEnumerable<ICell> GetCells()
    {
        foreach (var row in _rows.Values)
        {
            foreach (var cell in row.Cells)
            {
                yield return cell;
            }
        }
    }

    public void Add(Grid grid)
    {
        this[new Cursor(RowLength, 0)].Grid = grid;
    }

    private IRow GetRow(int index)
    {
        if (_rows.TryGetValue(index, out IRow? value)) return value;
        else
        {
            var row = new Row(this, index);
            _rows.Add(index, row);
            if (_maxRowIndex < index) _maxRowIndex = index;
            return row;
        }
    }

    public IRow this[int row] => GetRow(row);
    public ICell this[Cursor cursor] => GetRow(cursor.Row)[cursor.Col];
    public IRange this[Cursor start, Cursor end] => new Range(this, start, end);

    public Grid this[int row, int col]
    {
        set => this[new Cursor(row, col)].Grid = value;
    }

    public void Merge(Cursor start, Cursor end)
    {
        if (start == end) throw new InvalidOperationException("Unable to merge individual cells.");

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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _rows.Values.GetEnumerator();
    }
}
