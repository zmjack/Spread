using NStandard;
using static System.Formats.Asn1.AsnWriter;

namespace Mung;

internal class GridRender
{
    public enum RenderMode
    {
        None = 0,
        Grid = 1,
        Value = 2,
    }

    private readonly Grid _grid;
    private readonly MungValue _value;

    public RenderMode Mode;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool AutoSpan { get; private set; }
    public MungValue Value => _value;
    public string Text => _value.Value?.ToString() ?? "";

    public RenderDirection Direction => _grid.Direction;
    public List<GridRender> _children = [];

    public GridRender(IRenderUnit unit)
    {
        if (unit is Grid grid)
        {
            _grid = grid;
            Mode = RenderMode.Grid;
            AutoSpan = grid.AutoSpan;

            foreach (var child in grid.Children)
            {
                if (Direction == RenderDirection.LeftToRight)
                {
                    var subRender = new GridRender(child);
                    if (subRender.Height > Height) Height = subRender.Height;
                    Width += subRender.Width;
                    _children.Add(subRender);
                }
                else if (Direction == RenderDirection.TopToBottom)
                {
                    var subRender = new GridRender(child);
                    if (subRender.Width > Width) Width = subRender.Width;
                    Height += subRender.Height;
                    _children.Add(subRender);
                }
                else throw new NotImplementedException();
            }
        }
        else if (unit is MungValue value)
        {
            _value = value;
            Mode = RenderMode.Value;
            AutoSpan = value.AutoSpan;
            Width = value.ColSpan;
            Height = value.RowSpan;
        }
        else throw new NotImplementedException();
    }

    public void Render(IMungSheet sheet, Cursor cursor)
    {
        Render(sheet, cursor, Width, Height, _grid.Scope, _grid.Styles ?? []);
    }

    private void Render(IMungSheet sheet, Cursor cursor, int width, int height, string? scope, MungStyle[] styles)
    {
        if (Direction == RenderDirection.LeftToRight)
        {
            RenderLR(sheet, cursor, width, height, scope, styles);
        }
        else if (Direction == RenderDirection.TopToBottom)
        {
            RenderTB(sheet, cursor, width, height, scope, styles);
        }
        else throw new NotImplementedException();
    }

    private void RenderLR(IMungSheet sheet, Cursor cursor, int width, int height, string? scope, MungStyle[] styles)
    {
        static Cursor EndCursor(Cursor cursor, int padValue, int axisPadValue)
        {
            return new(cursor.Row + axisPadValue, cursor.Col + padValue);
        }

        static Cursor NextCursor(Cursor cursor, int padValue)
        {
            return new(cursor.Row, cursor.Col + padValue + 1);
        }

        var totalValue = _children.Sum(x => x.Width);
        var padValue = width - totalValue;

        foreach (var (index, child) in _children.AsIndexValuePairs())
        {
            var style = styles.Any() ? styles[index % styles.Length] : null;

            if (child.Mode == RenderMode.Grid)
            {
                var axisTotalValue = child.Direction switch
                {
                    RenderDirection.LeftToRight => child._children.Any() ? child._children.Max(x => x.Height) : 0,
                    RenderDirection.TopToBottom => child._children.Sum(x => x.Height),
                    _ => throw new NotImplementedException(),
                };
                var axisPadValue = height - axisTotalValue;
                var padded = padValue == 0 && axisPadValue == 0;
                var _scope = child._grid.Scope ?? scope;
                MungStyle[] _styles = child._grid.Styles ?? (style is not null ? [style] : []);

                if (!padded && child.AutoSpan)
                {
                    var _width = child.Width + padValue;
                    var _height = child.Height + axisPadValue;

                    child.Render(sheet, cursor, _width, _height, _scope, _styles);
                    cursor = NextCursor(cursor, _width - 1);
                    padValue = 0;
                }
                else
                {
                    child.Render(sheet, cursor, child.Width, child.Height, _scope, _styles);
                    cursor = NextCursor(cursor, child.Width - 1);
                }
            }
            else if (child.Mode == RenderMode.Value)
            {
                var value = child.Value;

                sheet[cursor].CoValue = value;
                sheet[cursor].Scope = scope;
                sheet[cursor].Style = style;

                if (!child.AutoSpan)
                {
                    var merge = value.RowSpan > 1 || value.ColSpan > 1;
                    if (merge)
                    {
                        var end = new Cursor(cursor.Row + value.RowSpan - 1, cursor.Col + value.ColSpan - 1);
                        sheet.Merge(cursor, end);
                    }
                    cursor = new Cursor(cursor.Row, cursor.Col + value.ColSpan);
                }
                else
                {
                    var axisPadValue = height - child.Height;
                    var padded = padValue == 0 && axisPadValue == 0;

                    if (!padded)
                    {
                        var end = EndCursor(cursor, padValue, axisPadValue);
                        sheet.Merge(cursor, end);
                        cursor = NextCursor(cursor, padValue);
                        padValue = 0;
                    }
                    else
                    {
                        cursor = NextCursor(cursor, 0);
                    }
                }
            }
            else throw new NotImplementedException();
        }
    }

    private void RenderTB(IMungSheet sheet, Cursor cursor, int width, int height, string? scope, MungStyle[] styles)
    {
        static Cursor EndCursor(Cursor cursor, int padValue, int axisPadValue)
        {
            return new(cursor.Row + padValue, cursor.Col + axisPadValue);
        }

        static Cursor NextCursor(Cursor cursor, int padValue, int axisPadValue)
        {
            return new(cursor.Row + padValue + 1, cursor.Col);
        }

        var totalValue = _children.Sum(x => x.Height);
        var padValue = height - totalValue;

        foreach (var (index, child) in _children.AsIndexValuePairs())
        {
            var style = styles.Any() ? styles[index % styles.Length] : null;

            if (child.Mode == RenderMode.Grid)
            {
                var axisTotalValue = child.Direction switch
                {
                    RenderDirection.LeftToRight => child._children.Sum(x => x.Width),
                    RenderDirection.TopToBottom => child._children.Any() ? child._children.Max(x => x.Width) : 0,
                    _ => throw new NotImplementedException(),
                };
                var axisPadValue = width - axisTotalValue;
                var padded = padValue == 0 && axisPadValue == 0;
                var _scope = child._grid.Scope ?? scope;
                MungStyle[] _styles = child._grid.Styles ?? (style is not null ? [style] : []);

                if (!padded && child.AutoSpan)
                {
                    var _width = child.Width + axisPadValue;
                    var _height = child.Height + padValue;

                    child.Render(sheet, cursor, _width, _height, _scope, _styles);
                    cursor = NextCursor(cursor, _height - 1, axisPadValue);
                    padValue = 0;
                }
                else
                {
                    child.Render(sheet, cursor, child.Width, child.Height, _scope, _styles);
                    cursor = NextCursor(cursor, child.Height - 1, axisPadValue);
                }
            }
            else if (child.Mode == RenderMode.Value)
            {
                var value = child.Value;

                sheet[cursor].CoValue = value;
                sheet[cursor].Scope = scope;
                sheet[cursor].Style = style;

                if (!child.AutoSpan)
                {
                    var merge = value.RowSpan > 1 || value.ColSpan > 1;
                    if (merge)
                    {
                        var end = new Cursor(cursor.Row + value.RowSpan - 1, cursor.Col + value.ColSpan - 1);
                        sheet.Merge(cursor, end);
                    }
                    cursor = new Cursor(cursor.Row + value.RowSpan, cursor.Col);
                }
                else
                {
                    var axisPadValue = width - child.Width;
                    var padded = padValue == 0 && axisPadValue == 0;

                    if (!padded)
                    {
                        var end = EndCursor(cursor, padValue, axisPadValue);
                        sheet.Merge(cursor, end);
                        cursor = NextCursor(cursor, padValue, axisPadValue);
                        padValue = 0;
                    }
                    else
                    {
                        cursor = NextCursor(cursor, 0, axisPadValue);
                    }
                }
            }
            else throw new NotImplementedException();
        }
    }
}
