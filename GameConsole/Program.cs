using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EBFCalculator;
using Game;
using SearchAlgorithms.Algorithms;
using System.Linq;

namespace Mr_Sure21;
public class Program
{

    const int N = 4;

    // A utility function to count inversions in given
    // array 'arr[]'. Note that this function can be
    // optimized to work in O(n Log n) time. The idea
    // here is to keep code small and simple.
    static int getInvCount(int[] arr)
    {
        int inv_count = 0;
        for (int i = 0; i < N * N - 1; i++)
        {
            for (int j = i + 1; j < N * N; j++)
            {
                // count pairs(arr[i], arr[j]) such that
                // i < j but arr[i] > arr[j]
                if (arr[j] != 0 && arr[i] != 0
                    && arr[i] > arr[j])
                    inv_count++;
            }
        }
        return inv_count;
    }

    // find Position of blank from bottom
    static int findXPosition(int[,] puzzle)
    {
        // start from bottom-right corner of matrix
        for (int i = N - 1; i >= 0; i--)
        {
            for (int j = N - 1; j >= 0; j--)
            {
                if (puzzle[i, j] == 0)
                    return N - i;
            }
        }
        return -1;
    }

    // This function returns true if given
    // instance of N*N - 1 puzzle is solvable
    static bool isSolvable(int[,] puzzle)
    {
        int[] arr = new int[N * N];
        int k = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                arr[k++] = puzzle[i, j];
            }
        }

        // Count inversions in given puzzle
        int invCount = getInvCount(arr);


        int pos = findXPosition(puzzle);
        if (pos % 2 == 1)
            return invCount % 2 == 0;
        else
            return invCount % 2 == 1;
    }

    public static void Main()
    {
        //FirstAssignment();
        //Console.ReadLine();
        //SecondAssignment();
        //Console.ReadLine();
        //ThirdAssignment();
        SliderPuzzleGame initial = new(4);
        int[,] bor =
        {
            { 2, 3, 4, 8 },
            { 1, 6, 0, 12 },
            { 5, 10, 7, 11 },
            { 9, 13, 14, 15}
        };
        initial.InitializeBoard(initial.GenerateRandomBoard());
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