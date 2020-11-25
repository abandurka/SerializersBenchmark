using System;
using BenchmarkDotNet.Running;

namespace SerializersBenchmark.ConsoleAppBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SimpleBenchmarkJob>();
        }
    }
}
