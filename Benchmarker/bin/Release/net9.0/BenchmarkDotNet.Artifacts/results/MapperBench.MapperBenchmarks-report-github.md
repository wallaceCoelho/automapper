```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.5126/23H2/2023Update/SunValley3)
Intel Core i3-7020U CPU 2.30GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method       | Mean     | Error    | StdDev   | Median   | Gen0   | Allocated |
|------------- |---------:|---------:|---------:|---------:|-------:|----------:|
| KbrMapper    | 390.5 ns | 13.95 ns | 39.80 ns | 372.9 ns | 0.3262 |     512 B |
| CachedMapper | 112.8 ns |  2.24 ns |  5.36 ns | 110.4 ns | 0.0508 |      80 B |
