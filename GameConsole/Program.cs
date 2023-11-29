using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Game;
using SearchAlgorithms;

namespace Mr_Sure21;

[MemoryDiagnoser]
public class Program
{

    public static void Main()
    {
        var test = BenchmarkRunner.Run<Program>();
    }
    [Benchmark]
    public void Test()
    {
        SliderPuzzleGame game1 = new SliderPuzzleGame(3);
        int[,] bor = 
        {
            { 0, 2, 3 },
            { 4, 5, 6 },
            { 7, 1, 8 },
        };
        game1.InitializeBoard(bor);
        Breadth_First_Search.FindPath(game1);
    }
}