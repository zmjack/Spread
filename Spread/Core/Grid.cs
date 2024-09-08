using Spread.Form;
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
        get => [.. _objectList];
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

    public void Add(object? obj)
    {
        if (obj is SpreadBlankValue) return;

        if (obj is Grid grid)
        {
            _objectList.Add(grid);
        }
        else if (obj is SpreadValue spvalue)
        {
            _objectList.Add(spvalue);
        }
        else if (obj is ISpreadObject spobj)
        {
            foreach (var value in spobj.Values)
            {
                _objectList.Add(new SpreadValue(value));
            }
        }
        else if (obj is ISpreadColumn column)
        {
            _objectList.Add(new Hori(":input")
            {
                new SpreadValue(column)
            });
        }
        else if (obj is not string && obj is IEnumerable enumerable)
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
                    Add(enumerator.Current);
                }
            }
        }
        else
        {
            _objectList.Add(new SpreadValue(obj));
        }
    }
}
