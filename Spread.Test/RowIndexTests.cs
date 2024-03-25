namespace Spread.Test;

public class RowIndexTests
{
    [Fact]
    public void Test1()
    {
        var index = new ColumnIndex("ABC");
        Assert.Equal("ABC", index.Name);
        Assert.Equal(730, index.Value);
    }

    [Fact]
    public void Test2()
    {
        var index = new ColumnIndex(730);
        Assert.Equal(730, index.Value);
        Assert.Equal("ABC", index.Name);
    }
}
