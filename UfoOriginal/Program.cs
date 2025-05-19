using BenchmarkDotNet.Running;
using UfoOriginal;

//BenchmarkRunner.Run<UfoBenchmarks>();

//BenchmarkRunner.Run<SplitBenchmarks>();

BenchmarkSwitcher.FromAssembly(typeof(FewerStringsImpl).Assembly).Run();