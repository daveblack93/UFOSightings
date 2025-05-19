using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace UfoOriginal.LoopBenchmark;

[MemoryDiagnoser]
//[DisassemblyDiagnoser(printSource: true)]
public class LinqImpl
{
    private static readonly Random random = new Random(42);

    private static readonly List<int> NumberList =
        Enumerable.Range(0, 1000).Select(_ => random.Next(100)).ToList();

    private static readonly Func<int, bool> TenPredicate = n => n % 10 == 0;

    [Benchmark(Baseline = true)]
    public void LinqWhere() => NumberList.Count(n => n % 10 == 0);

    [Benchmark]
    public int Manual()
    {
        var predicate = TenPredicate;
        int count = 0;
        var span = CollectionsMarshal.AsSpan(NumberList);

        foreach (var n in span)
        {
            if (predicate(n)) count++;
        }

        return count;
    }
}