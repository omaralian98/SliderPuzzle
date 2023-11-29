```

BenchmarkDotNet v0.13.10, Windows 10 (10.0.19045.3693/22H2/2022Update)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


```
| Method | Mean     | Error     | StdDev    | Median   | Gen0     | Gen1     | Allocated |
|------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
| Test   | 4.134 ms | 0.2608 ms | 0.7689 ms | 3.846 ms | 492.1875 | 335.9375 |   2.76 MB |
