using System.Buffers;

namespace UfoOriginal.Extensions;

public static class StringExtensions
{
    private static readonly ArrayPool<char> CharPool = ArrayPool<char>.Shared;
    public static string ToUpperTrimmed(this string str) => ToUpperTrimmed(str.AsSpan());

    public static string ToUpperTrimmed(in ReadOnlySpan<char> span)
    {
        var trimmed = span.Trim();
        if (trimmed.Length < 64)
        {
            Span<char> upper = stackalloc char[trimmed.Length];
            trimmed.ToUpperInvariant(upper);
            return upper.ToString();
        }
        else
        {
            var upper = CharPool.Rent(trimmed.Length);
            try
            {
                var upperSpan = upper.AsSpan().Slice(0, trimmed.Length);
                trimmed.ToUpperInvariant(upperSpan);
                return upperSpan.ToString();
            }
            finally
            {
                CharPool.Return(upper);
            }
        }
    }
}