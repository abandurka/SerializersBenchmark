using System.Collections.Immutable;
using System.Linq;
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
    public class BenchmarkJob
    {
        [Params(20)]
        public int N;
        
        private IImmutableList<ComplexObject> _complexObjects;
        private IImmutableList<string> _complexObjectStrings;
        private IImmutableList<SimpleObject> _simpleObjects;
        private IImmutableList<string> _simpleObjectStrings;

        [GlobalSetup]
        public void Setup()
        {
            var fixture = new Fixture();
            _complexObjects = fixture.CreateMany<ComplexObject>(N).ToImmutableList();
            _complexObjectStrings =  _complexObjects.Select(Newtonsoft.Json.JsonConvert.SerializeObject).ToImmutableList();
            _simpleObjects = fixture.CreateMany<SimpleObject>(N).ToImmutableList();
            _simpleObjectStrings =_simpleObjects.Select(Newtonsoft.Json.JsonConvert.SerializeObject).ToImmutableList();
        }


        #region Serialization

        [Benchmark]
        public void NewtonsoftComplexObjectToString()
        {
            var arr = new string[_complexObjects.Count];
            for (int i = 0; i < _complexObjects.Count; i++)
            {
                arr[i] = JsonConvert.SerializeObject(_complexObjects[i]);
            }
        }

        [Benchmark]
        public void DotNetComplexObjectToString()
        {
            var arr = new string[_complexObjects.Count];
            for (int i = 0; i < _complexObjects.Count; i++)
            {
                arr[i] = System.Text.Json.JsonSerializer.Serialize(_complexObjects[i]);
            }
        }
        
        [Benchmark]
        public void NewtonsoftSimpleObjectToString()
        {
            var arr = new string[_complexObjects.Count];
            for (int i = 0; i < _simpleObjects.Count; i++)
            {
                arr[i] = JsonConvert.SerializeObject(_simpleObjects[i]);
            }
        }

        [Benchmark]
        public void DotNetSimpleObjectToString()
        {
            var arr = new string[_complexObjects.Count];
            for (int i = 0; i < _simpleObjects.Count; i++)
            {
                arr[i] = System.Text.Json.JsonSerializer.Serialize(_simpleObjects[i]);
            }
        }

        #endregion

        #region Deserialization

        [Benchmark]
        public void NewtonsoftStringToComplexObject()
        {
            var arr = new ComplexObject[_complexObjects.Count];
            for (int i = 0; i < _complexObjectStrings.Count; i++)
            {
                arr[i] = JsonConvert.DeserializeObject<ComplexObject>(_complexObjectStrings[i]);
            }
        }

        [Benchmark]
        public void DotNetStringToComplexObject()
        {
            var arr = new ComplexObject[_complexObjects.Count];
            for (int i = 0; i < _complexObjectStrings.Count; i++)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<ComplexObject>(_complexObjectStrings[i]);
            }
        }
        
        [Benchmark]
        public void NewtonsoftStringToSimpleObject()
        {
            var arr = new SimpleObject[_complexObjects.Count];
            for (int i = 0; i < _simpleObjectStrings.Count; i++)
            {
                arr[i] = JsonConvert.DeserializeObject<SimpleObject>(_simpleObjectStrings[i]);
            }
        }

        [Benchmark]
        public void DotNetStringToSimpleObject()
        {
            var arr = new SimpleObject[_complexObjects.Count];
            for (int i = 0; i < _simpleObjectStrings.Count; i++)
            {
                arr[i] = System.Text.Json.JsonSerializer.Deserialize<SimpleObject>(_simpleObjectStrings[i]);
            }
        }

        #endregion
        
        [Benchmark(Baseline = true)]
        public async Task TaskFromResult()
        {
            var res = 0;
            for (int i = 0; i < N; i++)
            {
                res += await TaskWork(i);
            }

            Task<int> TaskWork(int i)
            {
                return Task.FromResult(i * i);
            }            
        }

    }
}
