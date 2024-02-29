using Mung.Form;
using System.Text.Json;

namespace Mung.Test;

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
        IMungBook book = new MungBook
        {
            new MungSheet("simple", "123")
            {
                SimpleSheet.Grid,
            }
        };
        var json = book.ToJson(new JsonSerializerOptions { WriteIndented = true });
    }

    [Fact]
    public void Test2()
    {

        var h = new MungFormHelper<Model>(new());
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
