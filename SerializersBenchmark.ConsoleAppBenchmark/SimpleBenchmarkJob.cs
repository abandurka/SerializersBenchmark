using System;
using System.Threading.Tasks;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Newtonsoft.Json;

namespace SerializersBenchmark.ConsoleAppBenchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [RPlotExporter, MarkdownExporter]
    [MemoryDiagnoser]
    public class SimpleBenchmarkJob
    {
        private ComplexObject _complexObject;
        private string _complexObjectStrings;
        private SimpleObject _simpleObject;
        private string _simpleObjectStrings;
        private int _salt;

        [GlobalSetup]
        public void Setup()
        {
            var fixture = new Fixture();
            _complexObject = fixture.Create<ComplexObject>();
            _complexObjectStrings = Newtonsoft.Json.JsonConvert.SerializeObject(_complexObject);
            _simpleObject = fixture.Create<SimpleObject>();
            _simpleObjectStrings = Newtonsoft.Json.JsonConvert.SerializeObject(_simpleObject);
            _salt = new Random().Next();
        }


        #region Serialization

        [Benchmark]
        public string NewtonsoftComplexObjectToString()
        {
            return JsonConvert.SerializeObject(_complexObject);
        }

        [Benchmark]
        public string DotNetComplexObjectToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(_complexObject);
        }

        [Benchmark]
        public string NewtonsoftSimpleObjectToString()
        {
            return JsonConvert.SerializeObject(_simpleObject);
        }

        [Benchmark]
        public string DotNetSimpleObjectToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(_simpleObject);
        }

        #endregion

        #region Deserialization

        [Benchmark]
        public ComplexObject NewtonsoftStringToComplexObject()
        {
            return JsonConvert.DeserializeObject<ComplexObject>(_complexObjectStrings);
        }

        [Benchmark]
        public ComplexObject DotNetStringToComplexObject()
        {
            return System.Text.Json.JsonSerializer.Deserialize<ComplexObject>(_complexObjectStrings);
        }

        [Benchmark]
        public SimpleObject NewtonsoftStringToSimpleObject()
        {
            return JsonConvert.DeserializeObject<SimpleObject>(_simpleObjectStrings);
        }

        [Benchmark]
        public SimpleObject DotNetStringToSimpleObject()
        {
            return System.Text.Json.JsonSerializer.Deserialize<SimpleObject>(_simpleObjectStrings);
        }

        #endregion

        [Benchmark(Baseline = true)]
        public async Task<int> TaskFromResult()
        {
            return await TaskWork(_salt);

            async Task<int> TaskWork(int i)
            {
                return await Task.Run(() => i * i);
            }
        }
    }
}
