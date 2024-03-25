using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Spread;

[DebuggerDisplay("R:{Red} G:{Green} B:{Blue} A:{Alpha}")]
[StructLayout(LayoutKind.Explicit)]
public struct RgbaColor : IRgbaColor
{
    [FieldOffset(3)] private byte _alpha;
    [FieldOffset(2)] private byte _red;
    [FieldOffset(1)] private byte _green;
    [FieldOffset(0)] private byte _blue;
    [FieldOffset(0)] private readonly uint _value;

    public byte Red { get => _red; set => _red = value; }
    public byte Green { get => _green; set => _green = value; }
    public byte Blue { get => _blue; set => _blue = value; }
    public byte Alpha { get => _alpha; set => _alpha = value; }
    public uint Value => _value;

    public RgbaColor(uint rgb, byte alpha = 255)
    {
        _value = rgb & 0x00ffffff;
        _alpha = alpha;
    }

    public RgbaColor(byte red, byte green, byte blue, byte alpha = 255)
    {
        _red = red;
        _green = green;
        _blue = blue;
        _alpha = alpha;
    }

    public static readonly RgbaColor Transparent = new(0x00000000);

    public static bool operator ==(RgbaColor left, RgbaColor right) => left.Value == right.Value;
    public static bool operator !=(RgbaColor left, RgbaColor right) => left.Value != right.Value;

    public override int GetHashCode() => (int)_value;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is IRgbaColor other) return Value == other.Value;
        return false;
    }

    public static implicit operator RgbaColor(int value) => new((uint)value);
    public static implicit operator RgbaColor(uint value) => new(value);

}
