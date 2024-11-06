namespace Spread.Test;

public class MergingTests
{
    [Fact]
    public void Test1()
    {
        var sheet = new SpreadSheet("Simple")
        {
            new Vert(SpreadStyle.Table)
            {
                new Hori
                {
                    "A", "B", "C", "D", "E",
                },

                new Hori(SpreadStyle.Table with
                {
                    Round = 2,
                })
                {
                    1,
                    1.2,
                    1.23,
                    1.234,
                    1.2345,
                },

                new Hori
                {
                    new Hori(SpreadStyle.Table with
                    {
                        Border = false,
                        BackgroundColor = new(0xff0000),
                    })
                    {
                        new SpreadValue("")
                        {
                            ColSpan = 1
                        },
                    },

                    (double?)null,
                    (double?)111,
                    (decimal?)222,
                }
            },
        };
        var html = new HtmlTable(sheet).ToHtml();
    }
}
