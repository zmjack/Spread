namespace Mung;

public interface IRgbaColor
{
    byte Red { get; set; }
    byte Green { get; set; }
    byte Blue { get; set; }
    byte Alpha { get; set; }
    uint Value { get; }
}