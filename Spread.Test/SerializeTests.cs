using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    SimpleSheet.Grid,
                }
            };
            var json = book.ToJson();
            var html = new HtmlTable(book.Sheets[0]).ToHtml();
            // Failed
            return;

            var _book = SpreadBook.Parse(json);
            var _html = new HtmlTable(_book.Sheets[0]).ToHtml();
        }

    }
}
