using BenchmarkDotNet.Running;
using UfoOriginal.UfoOrderBenchmark;

//BenchmarkRunner.Run<UfoBenchmarks>();

//BenchmarkRunner.Run<SplitBenchmarks>();

//DictionaryHackImpl.Run();
//Utf8Impl.Run();
BenchmarkSwitcher.FromAssembly(typeof(BasicImpl).Assembly).Run();