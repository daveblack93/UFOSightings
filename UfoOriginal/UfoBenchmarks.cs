using BenchmarkDotNet.Attributes;

namespace UfoOriginal;

[MemoryDiagnoser]
public class UfoBenchmarks
{
    [Benchmark(Baseline = true)]
    public Dictionary<string, int> Basic() => BasicImpl.Run();

    // [Benchmark]
    // public Dictionary<string, int> FewerStrings() => FewerStringsImpl.Run();

    [Benchmark]
    public Dictionary<string, int> DictionaryHack() => DictionaryHackImpl.Run();

    [Benchmark]
    public Dictionary<string, int> Utf8() => Utf8Impl.Run();

    [Benchmark]
    public Dictionary<string, int> KeyHack() => KeyHackImpl.Run();
}