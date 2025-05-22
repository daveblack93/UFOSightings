using BenchmarkDotNet.Running;
using UfoOriginal;
using UfoOriginal.LoopBenchmark;
using UfoOriginal.UfoOrderBenchmark;

//BenchmarkRunner.Run<UfoBenchmarks>();

//BenchmarkRunner.Run<SplitBenchmarks>();

BenchmarkRunner.Run<LoopClassImpl>();

//BenchmarkRunner.Run<ArrayPoolImpl>();

//DictionaryHackImpl.Run();
//Utf8Impl.Run();
//BenchmarkSwitcher.FromAssembly(typeof(BasicImpl).Assembly).Run();

// var personAge = new MagicPerson().CalculateTotalAge();
// Console.WriteLine($"Person age: {personAge}");