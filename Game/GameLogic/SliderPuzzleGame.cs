namespace Game;

public struct SliderPuzzleGame(int n)
{
    public Cell[,] board = new Cell[n, n];
    private int[,] secert = new int[n, n];
    public void InitializeBoard(int[,] bor)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                board[i, j] = new Cell(new Coordinates(i, j), bor[i, j]);
                secert[i, j] = bor[i, j];
            }
        }
    }
    public bool IsGameOver()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i + 1 == n && j + 1 == n) continue;
                if (board[i, j].content != 1 + j + n * i) return false;
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
                if (board[i, j].content == 0) return new Coordinates(i, j);
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
        if (to == from) return false;
        if (!CanMove(from)) return false;
        board[from.x, from.y].MoveTo(to);
        board[to.x, to.y].MoveTo(from);
        (board[from.x, from.y], board[to.x, to.y]) = (board[to.x, to.y], board[from.x, from.y]);
        return true;
    }

    public Coordinates[] GetAllPossibleMoves()
    {
        List<Coordinates> result = new List<Coordinates>();
        foreach (var it in board)
        {
            if (CanMove(it.coordinates)) result.Add(it.coordinates);
        }
        return result.ToArray();
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
        newr.InitializeBoard(secert);
        return newr;
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
                result += board[i, j].content;
            }
        }
        return result;
    }
}