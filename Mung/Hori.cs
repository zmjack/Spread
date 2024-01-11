namespace Mung;

public class Hori : Grid
{
    public Hori()
    {
        Direction = RenderDirection.LeftToRight;
    }

    public Hori(string scope) : this()
    {
        Scope = scope;
    }

    public Hori(params MungStyle[] styles) : this()
    {
        Styles = styles;
    }

    public Hori(string scope, params MungStyle[] styles) : this()
    {
        Scope = scope;
        Styles = styles;
    }
}
