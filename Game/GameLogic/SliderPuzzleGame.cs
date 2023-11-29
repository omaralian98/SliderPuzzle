namespace Game;

public class SliderPuzzleGame(int n) : Search
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
    public IEnumerable<SliderPuzzleGame> GetAllPossibleStates()
    {
        foreach (var move in GetAllPossibleMoves())
        {
            SliderPuzzleGame next = this.Clone();
            next.MoveCell(move);
            yield return next;
        }
    }

    public void PrintBoard()
    {
        for (int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                Console.Write($"{board[i, j]} ");
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

        return Shuffle(board);
    }

    public static T[,] Shuffle<T>(T[,] array)
    {
        Random rand = new();
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
        string result = string.Empty;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                result += board[i, j];
            }
        }
        return result;
    }
}