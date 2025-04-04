```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.5126/23H2/2023Update/SunValley3)
Intel Core i3-7020U CPU 2.30GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method                  | Mean       | Error     | StdDev     | Median     | Gen0   | Allocated |
|------------------------ |-----------:|----------:|-----------:|-----------:|-------:|----------:|
| KbrMapperSimple         |   930.9 ns |  69.66 ns |   205.4 ns |   959.2 ns | 0.3414 |     536 B |
| CachedMapperSimple      | 1,265.9 ns | 109.31 ns |   322.3 ns | 1,170.7 ns | 0.2232 |     352 B |
| KbrMapperComplex        |   675.7 ns |  41.63 ns |   121.4 ns |   643.6 ns | 0.3414 |     536 B |
| CachedMapperComplex     | 2,195.0 ns | 113.16 ns |   328.3 ns | 2,155.8 ns | 0.6294 |     992 B |
| KbrMapperListComplex    | 1,699.6 ns | 145.30 ns |   428.4 ns | 1,609.9 ns | 0.7744 |    1216 B |
| CachedMapperListComplex | 4,702.7 ns | 343.31 ns | 1,012.3 ns | 4,230.5 ns | 1.3962 |    2192 B |
| KbrMapperListSimple     | 1,269.5 ns |  48.26 ns |   134.5 ns | 1,245.1 ns | 0.7744 |    1216 B |
| CachedMapperListSimple  | 2,059.8 ns |  69.34 ns |   191.0 ns | 2,024.0 ns | 0.5798 |     912 B |
