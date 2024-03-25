namespace Spread;

public struct ColumnIndex
{
    private const int RADIX = 26;

    public int Value { get; }
    public string Name { get; }

    /// <summary>
    /// Gets the number of letter. It's start at 0. (For example, "A" is 0).
    /// </summary>
    /// <param name="letter"></param>
    /// <returns></returns>
    public static int GetIndex(string name)
    {
        int row = 0;
        foreach (int ch in name)
        {
            if (ch is < 'A' or > 'Z') throw new ArgumentException("Invalid row text.", nameof(name));
            row = row * RADIX + ch - 'A' + 1;
        }
        return row - 1;
    }

    /// <summary>
    /// Gets the letter of number. It's start at "A". (For example, 0 is "A").
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string GetName(int index)
    {
        var stack = new Stack<char>(10);

        if (index < 0) throw new ArgumentException("Invalid row index.", nameof(index));

        stack.Push((char)(index % RADIX + 'A'));
        index /= RADIX;

        while (index > 0)
        {
            index -= 1;
            stack.Push((char)(index % RADIX + 'A'));
            index /= RADIX;
        }

        return new string(stack.ToArray());
    }

    public ColumnIndex(int index)
    {
        Name = GetName(index);
        Value = index;
    }

    public ColumnIndex(string name)
    {
        Value = GetIndex(name);
        Name = name;
    }

    public static implicit operator ColumnIndex(int index) => new(index);
    public static implicit operator ColumnIndex(string name) => new(name);

}
