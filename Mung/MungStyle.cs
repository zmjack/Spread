namespace Mung;

public record MungStyle
{
    public static readonly MungStyle Default = new();
    public static readonly MungStyle Table = Default with
    {
        Border = true,
        Center = true,
    };
    public static readonly MungStyle Form = Default with
    {
        Padding = 6,
    };

    public MungStyle() { }

    public bool IsHeadCell { get; set; }

    public IRgbaColor? BackgroundColor { get; set; }
    public IRgbaColor? Color { get; set; }
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
            return HoriAlign == Mung.HoriAlign.Center
                && VertAlign == Mung.VertAlign.Middle;
        }
        set
        {
            HoriAlign = Mung.HoriAlign.Center;
            VertAlign = Mung.VertAlign.Middle;
        }
    }

    public bool? BorderTop { get; set; }
    public bool? BorderBottom { get; set; }
    public bool? BorderLeft { get; set; }
    public bool? BorderRight { get; set; }

    public bool Border
    {
        get
        {
            return BorderTop == true
                && BorderBottom == true
                && BorderLeft == true
                && BorderRight == true;
        }
        set
        {
            BorderTop = true;
            BorderBottom = true;
            BorderLeft = true;
            BorderRight = true;
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
