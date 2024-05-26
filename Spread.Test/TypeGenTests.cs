using TypeSharp;

namespace Spread.Test;

public class TypeGenTests
{
    [Fact]
    public void Test()
    {
        var builder = new TypeScriptModelBuilder();
        builder.CacheType<ISpreadBook>();
        builder.CacheType<ISpreadSheet>();
        var code = builder.Compile();
    }
}
