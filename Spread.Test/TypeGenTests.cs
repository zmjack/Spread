using TypeSharp;

namespace Spread.Test;

public class TypeGenTests
{
    [Fact]
    public void Test()
    {
        var builder = new TypeScriptModelBuilder();
        builder.CacheType<SpreadBook>();
        builder.CacheType<SpreadSheet>();
        var code = builder.Compile();
    }
}
