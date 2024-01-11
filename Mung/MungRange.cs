namespace Mung;

public interface IMungRange
{
    IMungSheet Sheet { get; }
    Cursor Start { get; }
    Cursor End { get; }

    IEnumerable<ICoCell> Cells { get; }
    void Merge();
}

public class MungRange : IMungRange
{
    public IMungSheet Sheet { get; private set; }

    internal Cursor _start;
    internal Cursor _end;
    public Cursor Start => _start;
    public Cursor End => _end;

    internal MungRange(IMungSheet sheet, Cursor start, Cursor end)
    {
        Sheet = sheet;
        _start = start;
        _end = end;
    }

    public IEnumerable<ICoCell> Cells
    {
        get
        {
            for (var row = _start.Row; row <= _end.Row; row++)
            {
                for (var col = _start.Col; col <= _end.Col; col++)
                {
                    yield return Sheet[(row, col)];
                }
            }
        }
    }

    public void Merge() => Sheet.Merge(_start, _end);

    public MungRange Row(int index)
    {
        var row = Start.Row + index;
        return new MungRange(Sheet, (row, Start.Col), (row, End.Col));
    }

    public MungRange Column(int index)
    {
        var col = Start.Col + index;
        return new MungRange(Sheet, (Start.Row, col), (End.Row, col));
    }
}
