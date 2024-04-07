using Spread.Form;
using NStandard;
using System.Collections;

namespace Spread;

public abstract class Grid : IRenderUnit, IEnumerable<object>
{
    private readonly List<IRenderUnit> _objectList = [];

    /// <summary>
    /// Elements of <see cref="Grid" /> or <see cref="SpreadValue" />.
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

    public SpreadStyle[]? Styles { get; protected set; }
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
    private void Add(SpreadValue value)
    {
        _objectList.Add(value);
    }
    private void Add(ISpreadObject obj)
    {
        foreach (var value in obj.Values)
        {
            _objectList.Add(new SpreadValue(value));
        }
    }
    private void Add(ISpreadFormColumn formColumn)
    {
        _objectList.Add(new Hori(":input")
        {
            new SpreadValue(formColumn)
        });
    }
    private void Add(IEnumerable enumerable)
    {
        if (enumerable is string str)
        {
            _objectList.Add(new SpreadValue(str));
        }
        else
        {
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current is null) continue;

                Add(current);
            }
        }
    }
    public void Add(object? obj)
    {
        if (obj is Grid grid)
        {
            Add(grid);
        }
        else if (obj is SpreadValue value)
        {
            Add(value);
        }
        else if (obj is ISpreadObject spreadObject)
        {
            Add(spreadObject);
        }
        else if (obj is ISpreadFormColumn column)
        {
            Add(column);
        }
        else if (obj is not string && obj is IEnumerable enumerable)
        {
            Add(enumerable);
        }
        else
        {
            Add(new SpreadValue(obj));
        }
    }
}
