namespace Mung;

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

    public Vert(params MungStyle[] styles) : this()
    {
        Styles = styles;
    }

    public Vert(string scope, params MungStyle[] styles) : this()
    {
        Scope = scope;
        Styles = styles;
    }
}
