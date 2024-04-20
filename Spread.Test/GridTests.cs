using NStandard;

namespace Spread.Test;

public class GridTests
{
    internal static readonly SpreadStyle Header = new()
    {
        FontFamily = "Microsoft YaHei",
        FontSize = 12,
        Bold = true,
        BackgroundColor = new RgbaColor(0xBDD7EE),
        Border = true,
        Center = true,
    };

    internal static readonly SpreadStyle Content = new()
    {
        FontFamily = "Microsoft YaHei",
        FontSize = 12,
        Border = true,
        Center = true,
    };

    [Fact]
    public void NumberTest()
    {
        var sheet = new SpreadSheet("Sheet1")
        {
            new Hori(SpreadStyle.Default with
            {
                Format = "#,###.00",
                Padding = 4,
            })
            {
                1, 2
            }
        };
        var ss = (ISpreadSheet)sheet;

        var s2s = ss.GetType().AssemblyQualifiedName;
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void Test()
    {
        var sheet = new SpreadSheet("Sheet1")
        {
            new Vert("main", [
                SpreadStyle.Table with { BackgroundColor = new RgbaColor(0xcccccc) },
                SpreadStyle.Table with { BackgroundColor = new RgbaColor(0x999999) },
            ])
            {
                new Hori(SpreadStyle.Table with { BackgroundColor = new RgbaColor(0xcc66cc) })
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
                        SpreadStyle.Table with { BackgroundColor = new RgbaColor(0xff0000) },
                        SpreadStyle.Table with { BackgroundColor = new RgbaColor(0x0000ff) },
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
        var sheet = new SpreadSheet(nameof(SimpleSheet.Grid))
        {
            SimpleSheet.Grid,
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void HoriMergeTest()
    {
        var sheet = new SpreadSheet(nameof(SimpleSheet.HoriGrid))
        {
            SimpleSheet.HoriGrid,
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void VertMergeTest()
    {
        var sheet = new SpreadSheet(nameof(SimpleSheet.VertGrid))
        {
            SimpleSheet.VertGrid,
        };
        var html = new HtmlTable(sheet).ToHtml();
    }

    [Fact]
    public void CenterTest()
    {
        var sheet = new SpreadSheet("test")
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
