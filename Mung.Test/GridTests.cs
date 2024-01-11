using NStandard;

namespace Mung.Test;

public class GridTests
{
    internal static readonly MungStyle Header = new()
    {
        HoriAlign = HoriAlign.Center,
        VertAlign = VertAlign.Middle,
        FontFamily = "Microsoft YaHei",
        FontSize = 12,
        Bold = true,
        BackgroundColor = new RgbaColor(0xBDD7EE),
        BorderTop = true,
        BorderBottom = true,
        BorderLeft = true,
        BorderRight = true,
    };

    internal static readonly MungStyle Content = new()
    {
        HoriAlign = HoriAlign.Center,
        VertAlign = VertAlign.Middle,
        FontFamily = "Microsoft YaHei",
        FontSize = 12,
        BorderTop = true,
        BorderBottom = true,
        BorderLeft = true,
        BorderRight = true,
    };

    [Fact]
    public void NumberTest()
    {
        var sheet = new MungSheet("Sheet1")
        {
            new Hori(MungStyle.Default with
            {
                Format = "#,###.00",
                Padding = 4,
            })
            {
                1, 2
            }
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void Test()
    {
        var sheet = new MungSheet("Sheet1")
        {
            new Vert("main", [
                MungStyle.Table with { BackgroundColor = new RgbaColor(0xcccccc) },
                MungStyle.Table with { BackgroundColor = new RgbaColor(0x999999) },
            ])
            {
                new Hori(MungStyle.Table with { BackgroundColor = new RgbaColor(0xcc66cc) })
                {
                    new Vert
                    {
                        new Hori { "a" },
                        new Hori { "b", new Vert { 1, 2 } },
                    },
                    new Vert
                    {
                        "d",
                        "e",
                    },
                    new Vert
                    {
                        "e",
                    },
                    "f",
                    new Vert
                    {
                        "11",
                        "22",
                        "33",
                        "44",
                    },
                },
                new Hori()
                {
                    "A",
                    new Vert
                    {
                        "d",
                        "e",
                    },
                },
                new Hori()
                {
                    "A",
                    new Vert([
                        MungStyle.Table with { BackgroundColor = new RgbaColor(0xff0000) },
                        MungStyle.Table with { BackgroundColor = new RgbaColor(0x0000ff) },
                    ])
                    {
                        "d",
                        "e",
                    },
                },
            }
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void ConstTest()
    {
        var sheet = new MungSheet(nameof(SimpleSheet.Grid))
        {
            SimpleSheet.Grid,
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void HoriMergeTest()
    {
        var sheet = new MungSheet(nameof(SimpleSheet.HoriGrid))
        {
            SimpleSheet.HoriGrid,
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void VertMergeTest()
    {
        var sheet = new MungSheet(nameof(SimpleSheet.VertGrid))
        {
            SimpleSheet.VertGrid,
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void CenterTest()
    {
        var sheet = new MungSheet("test")
        {
            new Hori
            {
                "Center",
                new Vert { 1, 2, 3, 4, 5 },
            }
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

}
