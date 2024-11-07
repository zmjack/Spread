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

    private int? _round;
    public int? Round
    {
        get => _round;
        set
        {
            if (value is null) _round = value;
            else
            {
                if (value < 0) throw new ArgumentException("The value can not be less then 0.", nameof(Round));
                if (value > 30) throw new ArgumentException("The value can not be greater then 30.", nameof(Round));
                _round = value;
            }
        }
    }
    public bool? ThousandSeparator { get; set; }
    public string? Format { get; set; }
}
