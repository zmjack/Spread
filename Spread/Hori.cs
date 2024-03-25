namespace Spread;

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

    public Hori(params SpreadStyle[] styles) : this()
    {
        Styles = styles;
    }

    public Hori(string scope, params SpreadStyle[] styles) : this()
    {
        Scope = scope;
        Styles = styles;
    }
}
