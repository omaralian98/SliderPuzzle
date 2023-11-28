using Game;

namespace Mr_Sure21;

class Program
{
    public static List<string> visited = new List<string>();
    static SliderPuzzleGame Solve(SliderPuzzleGame game)
    {
        Console.ReadLine();
        if (game.IsGameOver()) return game;

        Console.WriteLine("Current state:");
        game.PrintBoard();

        visited.Add(game.ToString());

        foreach (var move in game.GetAllPossibleMoves())
        {
            Console.WriteLine($"Trying move: {move}");

            var undo = game.CoordinatesOfEmptyCell();
            game.MoveCell(move);

            if (!IsVisited(game.ToString()))
            {
                Console.WriteLine("Exploring new state:");
                game.PrintBoard();
                var result = Solve(game);
                if (result.IsGameOver())
                    return result;
            }
            else
            {
                Console.WriteLine("Backtracking.");
                visited.Remove(game.ToString());  // Remove the state from visited
                game.MoveCell(undo);
            }
        }

        return game;
    }


    public static bool IsVisited(string x)
    {
        foreach (var game in visited)
        {
            if (x == game)
            {
                //Console.WriteLine($"{x} == {game} = True");
                return true;
            }
        }
        return false;
    }
    public static void Main()
    {
        SliderPuzzleGame game1 = new SliderPuzzleGame(3);
        int[,] boar =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 0, 8 },
        };
        game1.InitializeBoard(boar);
        game1 = Solve(game1);
        Console.WriteLine(visited.Count);
        game1.PrintBoard();
    }
}