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

    public Hori(params ISpreadStyle[] styles) : this()
    {
        Styles = styles;
    }

    public Hori(string scope, params ISpreadStyle[] styles) : this()
    {
        Scope = scope;
        Styles = styles;
    }
}
