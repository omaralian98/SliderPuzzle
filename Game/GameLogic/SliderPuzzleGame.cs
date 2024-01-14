using Game.GameLogic;
using SearchAlgorithms;
using System.Text;

namespace Game;

/// <summary>
/// This class represent a Slider Puzzle Game.
/// </summary>
/// <param name="size">The length and width of the board</param>
public class SliderPuzzleGame : ISearch
{
    public int[,] board { get; set; }
    public int[,] goal { get; set; }
    public int size { get; set; }
    private ISearch? _parent;
    public ISearch? Parent { get => _parent; set => _parent = value; }
    public SliderPuzzleGame(int size)
    {
        this.size = size;
        board = new int[size, size];
        goal = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board[i, j] = 1 + j + size * i;
                goal[i, j] = 1 + j + size * i;
            }
        }
        board[size - 1, size - 1] = 0;
        goal[size - 1, size - 1] = 0;
    }

    /// <summary>
    /// This Function initializes the board. 
    /// </summary>
    /// <param name="bor">The board you want.</param>
    /// <exception cref="EmptyCellNotFound"></exception>
    public void InitializeBoard(int[,] bor)
    {
        if (!IsValidForm(bor))
        {
            throw new EmptyCellNotFound("The board is not in a correct shape");
        }
        Array.Copy(bor, board, size * size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                goal[i, j] = 1 + j + size * i;
            }
        }
        goal[size - 1, size - 1] = 0;
    }

    /// <summary>
    /// This Function initializes the goal board.
    /// </summary>
    /// <param name="bor">The board you want.</param>
    /// <exception cref="EmptyCellNotFound"></exception>
    public void InitializeGoal(int[,] bor)
    {
        if (!IsValidForm(bor, board))
        {
            throw new EmptyCellNotFound("The board and the goal board are not in a correct shape");
        }
        Array.Copy(bor, goal, size * size);
    }

    /// <summary>
    /// This function determines if we reached the goal state or not.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsOver()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i + 1 == size && j + 1 == size) continue;
                if (board[i, j] != goal[i, j]) return false;
            }
        }
        return true;
    }

    /// <summary>
    /// This function determines if the given moves is valid.
    /// </summary>
    /// <param name="b">the move in coordinates.</param>
    /// <returns>bool</returns>
    /// <exception cref="EmptyCellNotFound"></exception>
    public bool CanMove(Coordinates b)
    {
        Coordinates emptyCell = CoordinatesOfEmptyCell(board);
        if (b == emptyCell) return false;
        if (emptyCell.Distance(b) != 1) return false;
        return b.x < size && b.x >= 0 && b.y < size && b.y >= 0;
    }

    /// <summary>
    /// This function apply the given move.
    /// </summary>
    /// <param name="from">coordinates of the move.</param>
    /// <returns>Whether the operation is done or not</returns>
    /// <exception cref="EmptyCellNotFound"></exception>
    public bool MoveCell(Coordinates from)
    {
        Coordinates to = CoordinatesOfEmptyCell(board);
        if (!CanMove(from)) return false;
        (board[from.x, from.y], board[to.x, to.y]) = (board[to.x, to.y], board[from.x, from.y]);
        return true;
    }

    /// <summary>
    /// This function gets all the possible moves of this game.
    /// </summary>
    /// <returns>List of coordinates of possible moves</returns>
    /// <exception cref="EmptyCellNotFound"></exception>
    public IEnumerable<Coordinates> GetAllPossibleMoves()
    {
        int[] xs = [0, -1, 0, 1];
        int[] ys = [-1, 0, 1, 0];
        var co = CoordinatesOfEmptyCell(board);

        for (int i = 0; i < 4; i++)
        {
            var coTest = new Coordinates(xs[i] + co.x, ys[i] + co.y);
            if (CanMove(coTest))
            {
                yield return coTest;
            }
        }
    }

    /// <summary>
    /// This function gets all the possible moves in a different state.
    /// </summary>
    /// <returns>List of all new states</returns>
    public IEnumerable<ISearch> GetAllPossibleStates()
    {
        foreach (var move in GetAllPossibleMoves())
        {
            SliderPuzzleGame next = this.Clone();
            next.MoveCell(move);
            yield return next;
        }
    }

    /// <summary>
    /// This function convert the board to string.
    /// </summary>
    /// <returns>string</returns>
    public string GetBoard() => GetBoard(board);

    /// <summary>
    /// This function convert the goal board to string.
    /// </summary>
    /// <returns>string</returns>
    public string GetGoalBoard() => GetBoard(goal);

    /// <summary>
    /// This function clones the game.
    /// </summary>
    /// <returns>New slider puzzle game</returns>
    public SliderPuzzleGame Clone()
    {
        var newr = new SliderPuzzleGame(size);
        newr.InitializeBoard(board);
        newr.InitializeGoal(goal);
        return newr;
    }

    public override string ToString()
    {
        StringBuilder result = new();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                result.Append(board[i, j]);
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// This function finds the coordinates of the empty cell.
    /// </summary>
    /// <param name="board">the board you want to get it's empty cell coordinates.</param>
    /// <returns>Coordinates of the empty cell</returns>
    /// <exception cref="EmptyCellNotFound"></exception>
    public static Coordinates CoordinatesOfEmptyCell(int[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == 0) return new Coordinates(i, j);
            }
        }
        throw new EmptyCellNotFound("There was no Empty Cell");
    }

    /// <summary>
    /// This function finds the manhattan distance between the board and the goal board
    /// </summary>
    /// <param name="a">The Game</param>
    /// <returns>The manhattan distance</returns>
    public static int ManhattanDistance(ISearch a)
    {
        var game = (SliderPuzzleGame)a;
        int manhattanDistance = 0;
        for (int i = 0; i < game.size; i++)
        {
            for (int j = 0; j < game.size; j++)
            {
                Coordinates target = new();
                for (int i1 = 0; i1 < game.goal.GetLength(0); i1++)
                {
                    for (int j1 = 0; j1 < game.goal.GetLength(1); j1++)
                    {
                        if (game.goal[i1, j1] == game.board[i, j]) target = new (i1, j1);
                    }
                }
                manhattanDistance += Math.Abs(i - target.x) + Math.Abs(j - target.y);
            }
        }
        return manhattanDistance;
    }

    /// <summary>
    /// This function finds the number of misplaced tiles.
    /// </summary>
    /// <param name="a">The Game</param>
    /// <returns>The number of misplaced tiles</returns>
    public static int MisplacedTiles(ISearch a)
    {
        var game = (SliderPuzzleGame)a;
        int totalOfMisplacedTiles = 0;

        for (int i = 0; i < game.size; i++)
        {
            for (int j = 0; j < game.size; j++)
            {
                totalOfMisplacedTiles += game.board[i, j] == (game.goal[i, j]) ? 0 : 1;
            }
        }
        return totalOfMisplacedTiles;
    }

    private static string GetBoard<T>(T[,] a)
    {
        StringBuilder result = new();
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                result.Append(string.Format("{0, -5}", a[i, j]));
            }
            result.AppendLine("");
        }
        result.AppendLine("");
        return result.ToString();
    }

    /// <summary>
    /// This function generates a random board.
    /// </summary>
    /// <returns>2d array that represents the board</returns>
    public static int[,] GenerateRandomBoard(int size)
    {
        int[,] board = new int[size, size];
        int[,] goal = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board[i, j] = 1 + j + size * i;
                goal[i, j] = 1 + j + size * i;
            }
        }
        board[size - 1, size - 1] = 0;
        goal[size - 1, size - 1] = 0;
        board = Shuffle(board);
        while (!IsSolvable(board, goal)) board = Shuffle(board);
        return board;
    }

    private static Random rand = new();

    /// <summary>
    /// This function shuffles a 2D array using Fisher–Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The type of the array</typeparam>
    /// <param name="array">The array</param>
    /// <returns>The same array but shuffled</returns>
    public static T[,] Shuffle<T>(T[,] array)
    {
        int lengthRow = array.GetLength(1);
        int i = array.Length - 1;
        while (i-- > 0)
        {
            int x = i / lengthRow;
            int y = i % lengthRow;

            int j = rand.Next(i + 1);
            int x1 = j / lengthRow;
            int y1 = j % lengthRow;

            (array[x1, y1], array[x, y]) = (array[x, y], array[x1, y1]);
        }
        return array;
    }
    /// <summary>
    /// This function shuffles an array using Fisher–Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The type of the array</typeparam>
    /// <param name="array">The array</param>
    /// <returns>The same array but shuffled</returns>
    public static T[] Shuffle<T>(T[] array)
    {
        for (int s = 0; s < array.Length - 1; s++)
        {
            int GenObj = rand.Next(s, array.Length);
            (array[GenObj], array[s]) = (array[s], array[GenObj]);
        }
        return array;
    }

    /// <summary>
    /// This function counts the inversions in an array.
    /// </summary>
    /// <param name="arr">The array</param>
    /// <returns>The number of inversions</returns>
    public static int CountInversions(int[] arr)
    {
        int counter = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] > 0 && arr[j] > 0 && arr[i] > arr[j]) counter++;
            }
        }
        return counter;
    }
    private static bool ContainsDuplicates(int[] board, int[]? goal = null)
    {
        HashSet<object> visited = [.. board];
        HashSet<object>? visitedGoal = null;
        if (goal is not null) visitedGoal = [.. goal];
        if (visited.Count != board.Length || visitedGoal?.Count != goal?.Length) return true;
        else if (visited.Union(visitedGoal ?? []).Count() != board.Length) return true;
        return false;
    }

    private static bool IsValid(int[] board, int[]? goal = null)
    {
        if (goal is not null && board.Length != goal?.Length) return false;
        else if (Math.Sqrt(board.Length) % 1 != 0) return false;
        else if (goal is null) return board.Any(x => x == 0);
        else return board.Any(x => x == 0) && goal.Any(x => x == 0);
    }

    public static bool IsValidForm(int[] board, int[]? goal = null)
    {
        if (!IsValid(board)) return false;
        else if (ContainsDuplicates(board, goal)) return false;
        return true;
    }

    private static bool ContainsDuplicates(int[,] board, int[,]? goal = null)
    {
        HashSet<object> visited = [.. board];
        HashSet<object>? visitedGoal = null;
        if (goal is not null) visitedGoal = [.. goal];
        if (visited.Count != board.Length || visitedGoal?.Count != goal?.Length) return true;
        else if (visited.Union(visitedGoal ?? []).Count() != board.Length) return true;
        return false;
    }

    private static bool IsValid(int[,] board, int[,]? goal = null)
    {
        if (goal is not null && board.Length != goal?.Length) return false;
        else if (Math.Sqrt(board.Length) % 1 != 0) return false;
        else if (goal is null)
        {
            var query = from int item in board where item == 0 select item;
            return query.Count() == 1;
        }
        else
        {
            var query = from int item in board where item == 0 select item;
            var queryGoal = from int item in goal where item == 0 select item;
            return query.Count() == 1 && queryGoal.Count() == 1;
        }
    }

    public static bool IsValidForm(int[,] board, int[,]? goal = null)
    {
        if (!IsValid(board)) return false;
        else if (ContainsDuplicates(board, goal)) return false;
        return true;
    }


    /// <summary>
    /// This function determines whether the given board is solvable.
    /// </summary>
    /// <param name="board"></param>
    /// <param name="goal"></param>
    /// <returns>Whether It's Solvable or not</returns>
    /// <exception cref="EmptyCellNotFound"></exception>
    public static bool IsSolvable(int[,] board, int[,] goal)
    {
        int[] result = board.ConvertTo1D();
        int[] goalResult = goal.ConvertTo1D();
        if (!IsValid(board) || !IsValid(goal) || !IsValid(board, goal))
        {
            return false;
        }
        int inversion = CountInversions(result);
        int goalInversion = CountInversions(goalResult);
        if (board.GetLength(0) % 2 == 0)
        {
            int x = CoordinatesOfEmptyCell(board).x;
            int xGoal = CoordinatesOfEmptyCell(goal).x;
            return (goalInversion % 2) == ((inversion + xGoal + x) % 2);
        }
        else
        {
            return (inversion % 2) == (goalInversion % 2);
        }
    }


}
public class EmptyCellNotFound : Exception
{
    public EmptyCellNotFound() { }

    public EmptyCellNotFound(string message): base(message) { }

    public EmptyCellNotFound(string message, Exception inner) : base(message, inner) { }
}