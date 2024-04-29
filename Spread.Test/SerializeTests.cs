using System.Text.Json;

namespace Spread.Test
{
    public class SerializeTests
    {
        [Fact]
        public void Test()
        {
            var book = new SpreadBook
            {
                new SpreadSheet(nameof(SimpleSheet.Grid))
                {
                    //SimpleSheet.Grid,
                }
            };
            var json = book.ToJson();
            var html = new HtmlTable(book["Grid"]).ToHtml();

            var _book = SpreadBook.Parse(json);
            var _html = new HtmlTable(_book["Grid"]).ToHtml();
        }

        [Fact]
        public void Test2()
        {
            var json = "{}";
            var book = JsonSerializer.Deserialize<SpreadBook>(json)!;
        }

    }
}
