# SerializersBenchmark
The comparison of serializers with forward and backward compatibilities or tolerant to schema changes


BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17763.1577 (1809/October2018Update/Redstone5)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]        : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT


|                          Method |           Job |       Runtime |       Mean |     Error |      StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|-------------------------------- |-------------- |-------------- |-----------:|----------:|------------:|------:|--------:|-------:|----------:|
| NewtonsoftComplexObjectToString | .NET Core 3.1 | .NET Core 3.1 | 5,074.5 ns | 101.32 ns |   204.68 ns |  2.39 |    0.14 | 1.0757 |    4528 B |
|     DotNetComplexObjectToString | .NET Core 3.1 | .NET Core 3.1 | 2,931.3 ns |  52.23 ns |    48.85 ns |  1.41 |    0.03 | 0.4082 |    1720 B |
|  NewtonsoftSimpleObjectToString | .NET Core 3.1 | .NET Core 3.1 |   759.9 ns |  15.15 ns |    26.54 ns |  0.37 |    0.02 | 0.3414 |    1432 B |
|      DotNetSimpleObjectToString | .NET Core 3.1 | .NET Core 3.1 |   435.6 ns |   8.16 ns |     6.81 ns |  0.21 |    0.00 | 0.0706 |     296 B |
| NewtonsoftStringToComplexObject | .NET Core 3.1 | .NET Core 3.1 | 6,527.7 ns | 125.81 ns |   134.62 ns |  3.15 |    0.10 | 1.0300 |    4328 B |
|     DotNetStringToComplexObject | .NET Core 3.1 | .NET Core 3.1 | 5,252.6 ns |  85.80 ns |    80.25 ns |  2.54 |    0.05 | 0.4425 |    1856 B |
|  NewtonsoftStringToSimpleObject | .NET Core 3.1 | .NET Core 3.1 | 1,146.9 ns |  22.48 ns |    21.03 ns |  0.55 |    0.02 | 0.6495 |    2720 B |
|      DotNetStringToSimpleObject | .NET Core 3.1 | .NET Core 3.1 |   466.4 ns |   9.01 ns |    18.20 ns |  0.23 |    0.01 | 0.0315 |     136 B |
|                  TaskFromResult | .NET Core 3.1 | .NET Core 3.1 | 2,072.4 ns |  36.69 ns |    34.32 ns |  1.00 |    0.00 | 0.0916 |     392 B |
|                                 |               |               |            |           |             |       |         |        |           |
| NewtonsoftComplexObjectToString | .NET Core 5.0 | .NET Core 5.0 | 4,922.0 ns |  97.98 ns |   146.64 ns |  2.28 |    0.10 | 1.0757 |    4528 B |
|     DotNetComplexObjectToString | .NET Core 5.0 | .NET Core 5.0 | 3,341.1 ns | 341.29 ns | 1,006.31 ns |  1.35 |    0.40 | 0.3967 |    1664 B |
|  NewtonsoftSimpleObjectToString | .NET Core 5.0 | .NET Core 5.0 |   705.4 ns |  13.99 ns |    16.11 ns |  0.32 |    0.01 | 0.3414 |    1432 B |
|      DotNetSimpleObjectToString | .NET Core 5.0 | .NET Core 5.0 |   347.7 ns |   6.70 ns |     7.45 ns |  0.16 |    0.01 | 0.0706 |     296 B |
| NewtonsoftStringToComplexObject | .NET Core 5.0 | .NET Core 5.0 | 5,845.1 ns | 113.25 ns |   116.30 ns |  2.69 |    0.10 | 1.0300 |    4336 B |
|     DotNetStringToComplexObject | .NET Core 5.0 | .NET Core 5.0 | 3,474.8 ns |  52.05 ns |    46.14 ns |  1.60 |    0.05 | 0.4654 |    1960 B |
|  NewtonsoftStringToSimpleObject | .NET Core 5.0 | .NET Core 5.0 | 1,098.6 ns |  14.65 ns |    12.23 ns |  0.50 |    0.02 | 0.6495 |    2720 B |
|      DotNetStringToSimpleObject | .NET Core 5.0 | .NET Core 5.0 |   394.5 ns |   6.74 ns |    11.81 ns |  0.18 |    0.01 | 0.0324 |     136 B |
|                  TaskFromResult | .NET Core 5.0 | .NET Core 5.0 | 2,159.3 ns |  38.89 ns |    60.54 ns |  1.00 |    0.00 | 0.0916 |     392 B |
