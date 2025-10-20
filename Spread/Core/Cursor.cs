using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Spread;

[DebuggerDisplay("{Row}, {Col}")]
public partial struct Cursor : IComparable<Cursor>
{
    [GeneratedRegex("^([A-Z]+)(\\d+)$")]
    private static partial Regex LetterRegex();

    private int _row;
    public int Row
    {
        get => _row;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Row), "The row value must be greater than 0.");
            _row = value;
        }
    }

    private int _col;
    public int Col
    {
        get => _col;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Col), "The row value must be greater than 0.");
            _col = value;
        }
    }

    public Cursor(string cellName)
    {
        var regex = LetterRegex();
        var match = regex.Match(cellName);
        if (match.Success)
        {
            Row = int.Parse(match.Groups[2].Value) - 1;
            Col = ColumnIndex.GetIndex(match.Groups[1].Value);
        }
        else throw new FormatException($"Illegal cell format：{cellName}。");
    }

    public Cursor(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public Cursor((int row, int col) cell)
    {
        Row = cell.row;
        Col = cell.col;
    }

    public static bool operator ==(Cursor left, Cursor right) => left.Row == right.Row && left.Col == right.Col;
    public static bool operator !=(Cursor left, Cursor right) => left.Row != right.Row || left.Col != right.Col;
    public override string ToString() => $"{ColumnIndex.GetName(Col)}{Row + 1}, ({Row}, {Col})";

    public static implicit operator Cursor(string cellName) => new(cellName);

    public static implicit operator Cursor((int row, int col) cell) => new(cell);

    public override int GetHashCode()
    {
        return Row << 16 | Col & 0x0000FFFF;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Cursor other) return Row == other.Row && Col == other.Col;
        return false;
    }

    public int CompareTo(Cursor other)
    {
        if (Row != other.Row) return Row.CompareTo(other.Row);
        return Col.CompareTo(other.Col);
    }
}
