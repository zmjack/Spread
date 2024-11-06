namespace Spread.Test;

public static class SimpleSheet
{
    public class Model
    {
        public string Book { get; set; }
        public string Chapter1 { get; set; }
        public string Chapter2 { get; set; }
        public string Chapter3 { get; set; }
        public int Words { get; set; }
    }

    public class Style
    {
        public static readonly SpreadStyle Base = SpreadStyle.Default with
        {
            Border = true,
            Center = true,
        };
        public static readonly SpreadStyle Title = Base with { BackgroundColor = new RgbaColor(0xFFFF00) };
        public static readonly SpreadStyle Book = Base with { BackgroundColor = new RgbaColor(0xDDEBF7) };
        public static readonly SpreadStyle Chapter1 = Base with { BackgroundColor = new RgbaColor(0xFCE4D6) };
        public static readonly SpreadStyle Chapter2 = Base with { BackgroundColor = new RgbaColor(0xEDEDED) };
        public static readonly SpreadStyle Chapter3 = Base with { BackgroundColor = new RgbaColor(0xFFF2CC) };
        public static readonly SpreadStyle Words = Base with { Format = "#,##0" };
    }

    public static readonly Grid Grid = new Vert(Style.Base)
    {
        new Hori(Style.Title)
        {
            "Book",
            new SpreadValue("Chapter") { ColSpan = 3 },
            new SpreadValue("Words") { ColSpan = 2 },
        },
        new Hori(Style.Book)
        {
            "Book1",
            new Vert
            {
                new Vert(Style.Chapter1)
                {
                    new Hori
                    {
                        "A",
                        new Vert(Style.Chapter2)
                        {
                            new Hori
                            {
                                "A-1",
                                new Vert(Style.Chapter3)
                                {
                                    new Hori
                                    {
                                        "A-1-1",
                                        new Hori(Style.Chapter1, Style.Chapter2) { 1011, 1012 },
                                    },
                                    new Hori("data", Style.Words)
                                    {
                                        "A-1-2",
                                        new Hori { 1011, 1012 },
                                    },
                                }
                            }
                        }
                    },
                    new Hori
                    {
                        new SpreadValue("Total Words") { ColSpan = 3 },
                        2022,
                        2024,
                    }
                },

                new Vert(Style.Chapter1)
                {
                    new Hori
                    {
                        "B",
                        new Vert(Style.Chapter2)
                        {
                            new Hori
                            {
                                "B-1",
                                new Vert(Style.Chapter3)
                                {
                                    new Hori(Style.Words)
                                    {
                                        "B-1-1",
                                        new Hori { 1011, null },
                                    },
                                    new Hori(Style.Words)
                                    {
                                        "B-1-2",
                                        new Hori { 1011, 1012 } ,
                                    }
                                }
                            }
                        }
                    },
                    new Hori
                    {
                        new SpreadValue("Total Words") { ColSpan = 3 },
                        2022,
                        1012,
                    }
                },

                new Hori
                {
                    new SpreadValue ("Total Words") { ColSpan = 3 },
                    4044,
                    3036,
                }
            }
        }
    };

    public static readonly Grid HoriGrid = new Vert
    {
        "Center",
        new Hori { 1, 2, 3, 4, 5 },
        new Hori
        {
            new SpreadValue ("0 Words") { ColSpan = 2 },
            new Hori { 3, 4, 5 },
        },
        new Hori
        {
            new Hori
            {
                new SpreadValue ("0 Words") { ColSpan = 2 },
                new Hori { 3, 4, 5 },
            }
        },
        new Vert
        {
            new SpreadValue(7),
        }
    };

    public static readonly Grid VertGrid = new Hori
    {
        "Center",
        new Vert { 1, 2, 3, 4, 5 },
        new Vert
        {
            new SpreadValue("0 Words") { ColSpan = 2 },
            new Vert { 3, 4, 5 },
        },
        new Vert
        {
            new Vert
            {
                new SpreadValue("0 Words") { ColSpan = 2 },
                new Vert { 3, 4, 5 },
            }
        },
        new Hori
        {
            new SpreadValue(7)
        }
    };
}
