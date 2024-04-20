namespace Spread;

public interface ISpreadStyle
{
    IRgbaColor? BackgroundColor { get; set; }
    bool? Bold { get; set; }
    bool? Border { get; set; }
    bool? BorderBottom { get; set; }
    bool? BorderLeft { get; set; }
    bool? BorderRight { get; set; }
    bool? BorderTop { get; set; }
    bool Center { get; set; }
    IRgbaColor? Color { get; set; }
    string? FontFamily { get; set; }
    int? FontSize { get; set; }
    string? Format { get; set; }
    HoriAlign? HoriAlign { get; set; }
    bool IsHeadCell { get; set; }
    int? Padding { get; set; }
    VertAlign? VertAlign { get; set; }

    string? FormatValue(object? value);
}