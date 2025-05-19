using BenchmarkDotNet.Attributes;

namespace UfoOriginal;

[MemoryDiagnoser]
public class SplitBenchmarks
{
    private static readonly string Source = "foo, bar, quux";

    [Benchmark(Baseline = true)] public string StringSplit() => Source.Split(",").First();

    [Benchmark]
    public string SpanSplit()
    {
        var fields = Source.AsSpan().Split(',');
        if (fields.MoveNext()) return fields.Current.ToString();
        return "";
    }

    [Benchmark]
    public string SpanSplit8()
    {
        //Range[] ranges = new Range[3];

        //with Span we do not need to mark the method as unusafe using stackalloc
        Span<Range> ranges = stackalloc Range[3];
        Source.AsSpan().Split(ranges, ",");
        return Source[ranges[0]];
    }
}