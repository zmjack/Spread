using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace Mung.Test;

public class TypeGenTests
{
    [Fact]
    public void Test()
    {
        var builder = new TypeScriptModelBuilder();
        builder.CacheType<IMungBook>();
        builder.CacheType<IMungSheet>();
        var code = builder.Compile();
    }
}
