using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace UfoOriginal.LoopBenchmark;

[MemoryDiagnoser]
public class LoopClassImpl
{
    private static readonly List<MyModel> MyList = new();

    public LoopClassImpl()
    {
        for (int i = 0; i < 1000000; i++)
        {
            MyList.Add(new MyModel()
            {
                Name = $"Name {i}",
                Value = i
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int ForeachList()
    {
        int acc = 0;
        foreach (var m in MyList)
        {
            acc += m.Value;
        }

        return acc;
    }

    [Benchmark]
    public int ForeachSpan()
    {
        int acc = 0;
        var span = CollectionsMarshal.AsSpan(MyList);
        foreach (var ms in span)
        {
            acc += ms.Value;
        }

        return acc;
    }
}


public class MyModel
{
    public string Name { get; set; }
    public int Value { get; set; }
}