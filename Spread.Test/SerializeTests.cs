using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var html = new HtmlTable(book.Sheets[0]).ToHtml();

            var _book = SpreadBook.Parse(json);
            var _html = new HtmlTable(_book.Sheets[0]).ToHtml();
        }

        [Fact]
        public void Test2()
        {
            var json = "{}";
            var book = JsonSerializer.Deserialize<SpreadBook>(json)!;
        }

    }
}
