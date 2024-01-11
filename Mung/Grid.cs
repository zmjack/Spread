using System.Collections;

namespace Mung;

public abstract class Grid : IRenderUnit, IEnumerable<object>
{
    private readonly List<IRenderUnit> _objectList = [];

    /// <summary>
    /// Elements of <see cref="Grid" /> or <see cref="MungValue" />.
    /// </summary>
    public IRenderUnit[] Children
    {
        get => _objectList.ToArray();
        protected set
        {
            _objectList.Clear();
            _objectList.AddRange(value);
        }
    }

    public string? Scope { get; set; }

    public MungStyle[]? Styles { get; protected set; }
    public RenderDirection Direction { get; protected set; }

    private int _span;
    public int Span
    {
        get => _span;
    }
    public bool AutoSpan
    {
        get => _span == 0;
        set => _span = value ? 0 : -1;
    }

    public IEnumerator<object> GetEnumerator()
    {
        return Children.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Children.GetEnumerator();
    }

    private void Add(Grid value)
    {
        _objectList.Add(value);
    }
    private void Add(MungValue value)
    {
        _objectList.Add(value);
    }
    private void Add(IEnumerable<Grid> value)
    {
        _objectList.AddRange(value);
    }
    private void Add(IEnumerable enumerable)
    {
        if (enumerable is string str)
        {
            _objectList.Add(new MungValue(str));
        }
        else
        {
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current is Grid grid)
                {
                    Add(grid);
                }
                else if (current is MungValue value)
                {
                    Add(value);
                }
                else
                {
                    Add(new MungValue(current));
                }
            }
        }
    }
    public void Add(object? obj)
    {
        if (obj is Grid grid)
        {
            Add(grid);
        }
        else if (obj is MungValue value)
        {
            Add(value);
        }
        else if (obj is not string && obj is IEnumerable enumerable)
        {
            Add(enumerable);
        }
        else
        {
            Add(new MungValue(obj));
        }
    }
}
