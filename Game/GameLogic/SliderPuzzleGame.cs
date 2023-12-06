using SearchAlgorithms;
using System.Text;

namespace Game;

public class SliderPuzzleGame(int n) : ISearch
{
    public int[,] board = new int[n, n];
    public int size = n;
    public void InitializeBoard(int[,] bor) => Array.Copy(bor, board, size*size);
    public bool IsOver()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i + 1 == n && j + 1 == n) continue;
                if (board[i, j] != 1 + j + n * i) return false;
            }
        }
        return true;
    }
    public Coordinates CoordinatesOfEmptyCell()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (board[i, j] == 0) return new Coordinates(i, j);
            }
        }
        throw new Exception("There was no Empty Cell");
    }
    public bool CanMove(Coordinates b)
    {
        Coordinates emptyCell = CoordinatesOfEmptyCell();
        if (b == emptyCell) return false;
        if (emptyCell.Distance(b) != 1) return false;
        return b.x < n && b.x >= 0 && b.y < n && b.y >= 0;
    }
    public bool MoveCell(Coordinates from)
    {
        Coordinates to = CoordinatesOfEmptyCell();
        if (!CanMove(from)) return false;
        (board[from.x, from.y], board[to.x, to.y]) = (board[to.x, to.y], board[from.x, from.y]);
        return true;
    }

    public IEnumerable<Coordinates> GetAllPossibleMoves()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var coordinates = new Coordinates(i, j);
                if (CanMove(coordinates)) yield return coordinates;
            }
        }
    }
    public IEnumerable<ISearch> GetAllPossibleStates()
    {
        foreach (var move in GetAllPossibleMoves())
        {
            SliderPuzzleGame next = this.Clone();
            next.MoveCell(move);
            yield return next;
        }
    }

    public static int ManhattanDistance(ISearch a)
    {
        var game = (SliderPuzzleGame)a;
        int manhattanDistance = 0;

        for (int i = 0; i < game.size; i++)
        {
            for (int j = 0; j < game.size; j++)
            {
                int value = game.board[i, j];
                if (value != 0)
                {
                    var target = Coordinates._1dTo2d(game.board[i, j], game.size);
                    manhattanDistance += Math.Abs(i - target.x) + Math.Abs(j - target.y);
                }
                else
                {
                    manhattanDistance += Math.Abs(i - 2) + Math.Abs(j - 2);
                }
            }
        }

        return manhattanDistance;
    }

    public static int MisplacedTiles(ISearch a)
    {
        var game = (SliderPuzzleGame)a;
        int totalOfMisplacedTiles = 0;

        for (int i = 0; i < game.size; i++)
        {
            for (int j = 0; j < game.size; j++)
            {
                int value = game.board[i, j] == 0 ? 9 : game.board[i, j];
                totalOfMisplacedTiles += value == (1 + j + game.size * i) ? 0 : 1;
            }
        }
        return totalOfMisplacedTiles;
    }

    public void PrintBoard()
    {
        for (int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                Console.Write("{0, -5}", board[i, j]);
            }
            Console.WriteLine("");
        }
        Console.WriteLine("");
    }

    public SliderPuzzleGame Clone()
    {
        var newr = new SliderPuzzleGame(n);
        newr.InitializeBoard(board);
        return newr;
    }

    public int[,] GenerateRandomBoard()
    {
        int[,] board = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                board[i, j] = 1 + j + n * i;
            }
        }
        board[n - 1, n - 1] = 0;
        board = Shuffle(board);
        while (!IsSolvable(board)) board = Shuffle(board);
        return board;
    }
    private static Random rand = new();
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
    public static T[] Shuffle<T>(T[] array)
    {
        for (int s = 0; s < array.Length - 1; s++)
        {
            int GenObj = rand.Next(s, array.Length); // pleace, note the range

            // swap procedure: note, var h to store initial array[s] value
            (array[GenObj], array[s]) = (array[s], array[GenObj]);
        }

        return array;
    }

    public int CountInversions(int[] arr)
    {
        int counter = 0;
        for (int i = 0; i < size*size; i++)
        {
            for (int j = i + 1; j < size*size; j++)
            {
                if (arr[i] > 0 && arr[j] > 0 && arr[i] > arr[j]) counter++;
            }
        }
        return counter;
    }

    public bool IsSolvable(int[,] board)
    {
        int[] result = board.Cast<int>().Select(c => c).ToArray();
        int inversionCounter = CountInversions(result);
        return inversionCounter % 2 == 0;
    }

    public static bool operator ==(SliderPuzzleGame a, SliderPuzzleGame b)
    {
        for (int i = 0; i < a.board.GetLength(0); i++)
        {
            for (int j = 0; j < a.board.GetLength(0); j++)
            {
                if (a.board[i, j] != b.board[i, j]) return false;
            }
        }
        return true;
    }
    public static bool operator !=(SliderPuzzleGame a, SliderPuzzleGame b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        else return (SliderPuzzleGame)obj == this;
    }
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                result.Append(board[i, j]);
            }
        }

        return result.ToString();
    }
}