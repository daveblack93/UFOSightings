using BenchmarkDotNet.Attributes;

namespace UfoOriginal;

public class SliceBenchmarks
{
    private const string Source = "abcderghijklmnopqrstuvwxyz";

    [Benchmark(Baseline = true)]
    public int Slice()
    {
        ReadOnlySpan<char> span = Source.AsSpan();
        int index = Source.IndexOf('m');
        return span.Slice(0, index).Length;
    }

    [Benchmark]
    public int Range()
    {
        ReadOnlySpan<char> span = Source.AsSpan();
        int index = Source.IndexOf('m');
        return span[..index].Length;
    }
}