namespace UfoOriginal.Extensions;

public static class SpanByteExtensions
{
    public static int Split(this Span<byte> span, Span<Range> ranges, byte separator)
    {
        return Split((ReadOnlySpan<byte>)span, ranges, separator);
    }
    public static int Split(this ReadOnlySpan<byte> span, Span<Range> ranges, byte separator)
    {
        int index;
        int r = 0;
        int start = 0;

        while ((index = span.IndexOf(separator)) >= 0)
        {
            ranges[r] = new Range(start, start + index);
            start = index + 1;
            span = span.Slice(start);

            if (++r >= ranges.Length) break;
        }

        return r;
    }
}