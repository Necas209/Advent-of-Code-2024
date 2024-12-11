using System.Numerics;

namespace SharedLib;

public static class NumberExtensions
{
    public static int NumberOfDigits<T>(this T number) where T : INumber<T>
    {
        if (number == T.Zero)
            return 1;

        if (number < T.Zero)
        {
            number = -number;
        }

        return (int)Math.Floor(Math.Log10(double.CreateChecked(number)) + 1);
    }

    public static (T Left, T Right) SplitNumber<T>(this T number) where T : INumber<T>
    {
        if (number < T.Zero)
            throw new ArgumentException("Number must be positive.", nameof(number));

        if (number < T.CreateChecked(10))
            return (T.Zero, number);

        var numDigits = number.NumberOfDigits();
        var halfDigits = numDigits / 2;
        var divisor = T.CreateChecked(Math.Pow(10, halfDigits));

        var leftPart = number / divisor;
        var rightPart = number % divisor;

        return (leftPart, rightPart);
    }
}