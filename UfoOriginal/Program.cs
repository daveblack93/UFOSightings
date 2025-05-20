using BenchmarkDotNet.Running;
using UfoOriginal.LoopBenchmark;
using UfoOriginal.UfoOrderBenchmark;

//BenchmarkRunner.Run<UfoBenchmarks>();

//BenchmarkRunner.Run<SplitBenchmarks>();

//BenchmarkRunner.Run<LoopClassImpl>();

//DictionaryHackImpl.Run();
//Utf8Impl.Run();
BenchmarkSwitcher.FromAssembly(typeof(BasicImpl).Assembly).Run();