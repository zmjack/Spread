namespace Spread.Test;

public class ColorTests
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(0xFF010203, new RgbaColor(1, 2, 3, 255).Value);
        Assert.Equal(0xFF010203, new RgbaColor(0x010203).Value);
        Assert.Equal(0xFF010203u, new RgbaColor(0x7F010203).Value);
        Assert.Equal(0x7F010203u, new RgbaColor(0x010203, 0x7F).Value);
    }
}
