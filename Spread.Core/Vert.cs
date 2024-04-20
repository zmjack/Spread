namespace Spread;

public class Vert : Grid
{
    public Vert()
    {
        Direction = RenderDirection.TopToBottom;
    }

    public Vert(string scope) : this()
    {
        Scope = scope;
    }

    public Vert(params ISpreadStyle[] styles) : this()
    {
        Styles = styles;
    }

    public Vert(string scope, params ISpreadStyle[] styles) : this()
    {
        Scope = scope;
        Styles = styles;
    }
}
