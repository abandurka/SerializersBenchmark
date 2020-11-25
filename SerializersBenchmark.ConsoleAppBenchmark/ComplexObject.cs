using System.Collections.Generic;

namespace SerializersBenchmark.ConsoleAppBenchmark
{
    public class ComplexObject
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public double DoubleProperty { get; set; }
        public Dictionary<string, string> Dictionary { get; set; }
        public List<SimpleObject> SimpleObjects { get; set; }
    }
}
