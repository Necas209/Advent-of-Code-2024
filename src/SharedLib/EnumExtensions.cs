using System.Runtime.CompilerServices;

namespace SharedLib;

public static class EnumExtensions
{
    public static bool HasAnyFlag<TEnum>(this TEnum lhs, TEnum flag) where TEnum : unmanaged, Enum
    {
        return Unsafe.SizeOf<TEnum>() switch
        {
            sizeof(byte) => (Unsafe.As<TEnum, byte>(ref lhs) & Unsafe.As<TEnum, byte>(ref flag)) != 0,
            sizeof(ushort) => (Unsafe.As<TEnum, ushort>(ref lhs) & Unsafe.As<TEnum, ushort>(ref flag)) != 0,
            sizeof(int) => (Unsafe.As<TEnum, uint>(ref lhs) & Unsafe.As<TEnum, uint>(ref flag)) != 0,
            sizeof(long) => (Unsafe.As<TEnum, ulong>(ref lhs) & Unsafe.As<TEnum, ulong>(ref flag)) != 0,
            _ => throw new Exception("Size does not match a known Enum backing type.")
        };
    }
}