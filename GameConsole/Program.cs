using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EBFCalculator;
using Game;
using SearchAlgorithms.Algorithms;
using System.Linq;

namespace Mr_Sure21;
public class Program
{
    public static void Main()
    {
        //FirstAssignment();
        //Console.ReadLine();
        //SecondAssignment();
        //Console.ReadLine();
        //ThirdAssignment();
        SliderPuzzleGame initial = new(3);
        int[,] bor = initial.GenerateRandomBoard();
        initial.InitializeBoard(bor);
        initial.PrintBoard();
        AStarSearch<SliderPuzzleGame> aStarSearch = new();
        //var result1 = aStarSearch.FindPath(initial, SliderPuzzleGame.ManhattanDistance);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var result = aStarSearch.FindPath(initial, SliderPuzzleGame.ManhattanDistance);
        watch.Stop();
        var elapsedMs = (double)watch.ElapsedMilliseconds / 1000;
        Console.WriteLine("Took: {0} s", elapsedMs);
        Console.WriteLine("Depth: {0}", result.Item1.Count);
        Console.WriteLine("Discovered: {0}\nVisited: {1}", result.Item2, result.Item3);
        var rt = new Breadth_First_Search<SliderPuzzleGame>();
        var watch1 = System.Diagnostics.Stopwatch.StartNew();
        var res = rt.FindPath(initial);
        watch1.Stop();
        var elapsedMs1 = (double)watch1.ElapsedMilliseconds / 1000;
        Console.WriteLine("Took: {0} s", elapsedMs1);
        Console.WriteLine("Depth: {0}", res.Item1.Count);
        Console.WriteLine("Discovered: {0}\nVisited: {1}", res.Item2, res.Item3);

    }
    public static void FirstAssignment()
    {
        SliderPuzzleGame initial = new SliderPuzzleGame(3);
        int[,] bor = initial.GenerateRandomBoard();
        initial.InitializeBoard(bor);
        Console.WriteLine("Initial State:");
        initial.PrintBoard();
        PrintGoalState();
        var AStar = new AStarSearch<SliderPuzzleGame>();
        var resultMisplaced = AStar.FindPath(initial, SliderPuzzleGame.MisplacedTiles);
        var resultManhattan = AStar.FindPath(initial, SliderPuzzleGame.ManhattanDistance);

        Console.WriteLine("Moves to solve using misplaced tiles heuristic: {0}", resultMisplaced.Item1.Count);
        Console.WriteLine("Expanded nodes to solve using misplaced tiles heuristic: {0}", resultMisplaced.Item3);
        Console.WriteLine("Moves to solve using manhattan distance heuristic: {0}", resultManhattan.Item1.Count);
        Console.WriteLine("Expanded nodes to solve using manhattan distance heuristic: {0}", resultManhattan.Item3);
    }

    public static void SecondAssignment()
    {
        SliderPuzzleGame initial = new SliderPuzzleGame(3);
        int[,] bor = initial.GenerateRandomBoard();
        initial.InitializeBoard(bor);
        Console.WriteLine("Initial State:");
        initial.PrintBoard();
        Console.WriteLine("\nMoves to solve:\n");
        var AStar = new AStarSearch<SliderPuzzleGame>();
        var result = AStar.FindPath(initial, SliderPuzzleGame.ManhattanDistance);

        for (int i = 0; i < result.Item1.Count - 1; i++)
        {
            Console.WriteLine($"Move {i}");
            result.Item1[i].PrintBoard();
            Console.WriteLine("\n");
        }
        Console.WriteLine($"Move {result.Item1.Count - 1}");
        PrintGoalState();
    }

    public static void ThirdAssignment()
    {
        Console.WriteLine("\td\t\t EBF Misplaced Tiles\t\tEBF Manhattan Distance");
        SliderPuzzleGame initial = new(3);
        var Astar = new AStarSearch<SliderPuzzleGame>();
        for (int i = 0; i < 100; i++)
        {
            initial.InitializeBoard(initial.GenerateRandomBoard());
            var mis = Astar.FindPath(initial, SliderPuzzleGame.MisplacedTiles);
            var man = Astar.FindPath(initial, SliderPuzzleGame.ManhattanDistance);
            if (mis.Item1.Count != man.Item1.Count)
            {
                Console.WriteLine("Stopppppp");
                Console.ReadLine();
                initial.PrintBoard();
            }
            Console.WriteLine($"{i + 1}-\t{mis.Item1.Count - 1} \t\t {Math.Round(EBF.GetUsingBiSection(mis.Item1.Count, Convert.ToInt32(mis.Item3)), 4):f4}\t\t\t\t\t{Math.Round(EBF.GetUsingBiSection(man.Item1.Count, Convert.ToInt32(man.Item3)), 4):f4}");
        }
    }
    private static void PrintGoalState()
    {
        SliderPuzzleGame initial = new SliderPuzzleGame(3);
        int[,] bor =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 } 
        };
        initial.InitializeBoard(bor);
        Console.WriteLine("\nGoal State:");
        initial.PrintBoard();
    }
}