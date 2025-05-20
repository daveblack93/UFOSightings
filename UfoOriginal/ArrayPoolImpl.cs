using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace UfoOriginal;

[MemoryDiagnoser]
public class ArrayPoolImpl
{
    private static readonly ArrayPool<byte> BytePool = ArrayPool<byte>.Shared;

    const int iterations = 1_000_000;
    // REMEMBER: the LOH threshold is 85,000 bytes (85KB) for .NET Framework and 100,000 bytes (100KB) for .NET Core and .NET 5+.

    //const int arraySize = 16384; // 16KB - below LOH threshold but still substantial
    const int arraySize = 114688; // 115KB - above LOH threshold

    [Benchmark(Baseline = true)]
    public int ByteArray()
    {
        for (int i = 0; i < iterations; i++)
        {
            byte[] buffer = new byte[arraySize];
            // Simulate brief usage
            buffer[0] = 42;
        }

        return 1;
    }

    [Benchmark]
    public int ByteArrayPool()
    {
        for (int i = 0; i < iterations; i++)
        {
            byte[] buffer = BytePool.Rent(arraySize);

            try
            {
                buffer[0] = 42;
            }
            finally
            {
                BytePool.Return(buffer);
            }
        }

        return 1;
    }
}