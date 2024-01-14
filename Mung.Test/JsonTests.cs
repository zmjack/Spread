using System.Text.Json;

namespace Mung.Test;

public class JsonTests
{
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
}
