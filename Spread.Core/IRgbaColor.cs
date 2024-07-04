namespace Spread;

public interface IRgbaColor
{
    byte Red { get; set; }
    byte Green { get; set; }
    byte Blue { get; set; }
    byte Alpha { get; set; }
    uint Value { get; }
}

public static class IRgbaColorExtensions
{
    public static string GetHtmlColor(this IRgbaColor @this)
    {
        return $"rgba({@this.Red},{@this.Green},{@this.Blue},{@this.Alpha / 255})";
    }
}
