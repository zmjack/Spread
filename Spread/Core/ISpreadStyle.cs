using System.Text.Json.Serialization;

namespace Spread;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(SpreadStyle), nameof(SpreadStyle))]
public interface ISpreadStyle
{
    RgbaColor? BackgroundColor { get; set; }
    bool? Bold { get; set; }
    bool? Border { get; set; }
    bool? BorderBottom { get; set; }
    bool? BorderLeft { get; set; }
    bool? BorderRight { get; set; }
    bool? BorderTop { get; set; }
    bool Center { get; set; }
    RgbaColor? Color { get; set; }
    string? FontFamily { get; set; }
    int? FontSize { get; set; }

    int? Round { get; set; }
    bool? ThousandSeparator { get; set; }
    string? Format { get; set; }

    HoriAlign? HoriAlign { get; set; }
    bool IsHeadCell { get; set; }
    int? Padding { get; set; }
    VertAlign? VertAlign { get; set; }
}
