using NStandard.Text.Json;

namespace Spread;

public record SpreadStyle : ISpreadStyle
{
    public static readonly SpreadStyle Default = new();
    public static readonly SpreadStyle Table = Default with
    {
        Border = true,
        Center = true,
    };
    public static readonly SpreadStyle Form = Default with
    {
        Padding = 6,
    };

    public SpreadStyle() { }

    public bool IsHeadCell { get; set; }

    public RgbaColor? BackgroundColor { get; set; }
    public RgbaColor? Color { get; set; }
    public int? FontSize { get; set; }
    public string? FontFamily { get; set; }
    public bool? Bold { get; set; }

    public HoriAlign? HoriAlign { get; set; }
    public VertAlign? VertAlign { get; set; }
    public int? Padding { get; set; }

    public bool Center
    {
        get
        {
            return HoriAlign == Spread.HoriAlign.Center
                && VertAlign == Spread.VertAlign.Middle;
        }
        set
        {
            HoriAlign = Spread.HoriAlign.Center;
            VertAlign = Spread.VertAlign.Middle;
        }
    }

    public bool? BorderTop { get; set; }
    public bool? BorderBottom { get; set; }
    public bool? BorderLeft { get; set; }
    public bool? BorderRight { get; set; }

    public bool? Border
    {
        get
        {
            if (BorderTop.HasValue)
            {
                var value = BorderTop.Value;
                if (BorderBottom == value && BorderLeft == value && BorderRight == value)
                {
                    return value;
                }
                else return null;
            }
            else return null;
        }
        set
        {
            BorderTop = value;
            BorderBottom = value;
            BorderLeft = value;
            BorderRight = value;
        }
    }

    public string? Format { get; set; }

    public string? FormatValue(object? value)
    {
        if (value is null) return null;

        if (Format is not null)
        {
            return value switch
            {
                short v => v.ToString(Format),
                int v => v.ToString(Format),
                long v => v.ToString(Format),
                ushort v => v.ToString(Format),
                uint v => v.ToString(Format),
                ulong v => v.ToString(Format),
                float v => v.ToString(Format),
                double v => v.ToString(Format),
                DateTime v => v.ToString(Format),
                decimal v => v.ToString(Format),
                _ => value.ToString(),
            };
        }
        else return value.ToString();
    }

}
