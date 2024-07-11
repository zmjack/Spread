using Spread.Form;
using System.Text.Json;

namespace Spread.Test;

public class JsonTests
{
    public class Item
    {
        public SubItem[] SubItems { get; set; }
    }

    public class SubItem
    {
        public string Value { get; set; }
    }

    public class Model
    {
        public Item[] Items { get; set; }
    }

    [Fact]
    public void Test()
    {
        SpreadBook book = new SpreadBook
        {
            //new SpreadSheet("simple", "123")
            //{
            //    SimpleSheet.Grid,
            //}
        };
        var json = book.ToJson();
    }

    [Fact]
    public void Test2()
    {
        var h = new SpreadFormHelper<Model>();
        var aa =
            h.Table(x => x.Items!, p =>
            [
                p.Table(x => x.SubItems!, pp =>
                [
                    pp.Text(x => x.Value),
                ]),
            ]);
        var json = JsonSerializer.Serialize(aa);
    }
}
