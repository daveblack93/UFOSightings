using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace UfoOriginal.LoopBenchmark;

[MemoryDiagnoser]
public class LoopImpl
{
    private static readonly List<int> Numbers = Enumerable.Range(0, 1000).ToList();
    private static readonly int[] NumbersArray = Enumerable.Range(0, 1000).ToArray();

    [Benchmark(Baseline = true)]
    public int ForeachList()
    {
        int acc = 0;
        foreach (var n in Numbers)
        {
            acc += n;
        }

        return acc;
    }

    [Benchmark]
    public int ForList()
    {
        int acc = 0;

        for (int i = 0, c = Numbers.Count; i < c ; i++)
        {
            acc += Numbers[i];
        }

        return acc;
    }

    [Benchmark]
    public int ListOfSpan()
    {
        int acc = 0;
        var span = CollectionsMarshal.AsSpan(Numbers);
        foreach (var n in span)
        {
            acc += n;
        }

        return acc;
    }

    [Benchmark]
    public int Linq() => Numbers.Sum();

    // [Benchmark]
    // public int ForeachArray()
    // {
    //     int acc = 0;
    //     foreach (var n in NumbersArray)
    //     {
    //         acc += n;
    //     }
    //
    //     return acc;
    // }
    //
    // [Benchmark]
    // public int ForArray()
    // {
    //     int acc = 0;
    //
    //     for (int i = 0, c = NumbersArray.Length; i < c ; i++)
    //     {
    //         acc += NumbersArray[i];
    //     }
    //
    //     return acc;
    // }
}