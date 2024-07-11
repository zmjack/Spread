namespace Spread;

public class SpreadRange : ISpreadRange
{
    public ISpreadSheet Sheet { get; private set; }

    internal Cursor _start;
    internal Cursor _end;
    public Cursor Start => _start;
    public Cursor End => _end;

    internal SpreadRange(ISpreadSheet sheet, Cursor start, Cursor end)
    {
        Sheet = sheet;
        _start = start;
        _end = end;
    }

    public IEnumerable<ICell> Cells
    {
        get
        {
            for (var row = _start.Row; row <= _end.Row; row++)
            {
                for (var col = _start.Col; col <= _end.Col; col++)
                {
                    yield return Sheet[new Cursor(row, col)];
                }
            }
        }
    }

    public void Merge() => Sheet.Merge(_start, _end);

    public SpreadRange Row(int index)
    {
        var row = Start.Row + index;
        return new SpreadRange(Sheet, new(row, Start.Col), new(row, End.Col));
    }

    public SpreadRange Column(int index)
    {
        var col = Start.Col + index;
        return new SpreadRange(Sheet, new(Start.Row, col), new(End.Row, col));
    }
}
