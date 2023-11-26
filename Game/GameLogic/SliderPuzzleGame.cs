namespace Game;

public class SliderPuzzleGame(int n)
{
    Cell[,] board = new Cell[n, n];
    public void InitializeBoard(int[,] bor)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                board[i, j] = new Cell(new Coordinates(i, j), bor[i, j]);
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
    public bool IsValidMove(Coordinates b)
    {
        Coordinates emptyCell = CoordinatesOfEmptyCell();
        if (b == emptyCell) return false;
        if (emptyCell.Distance(b) != 1) return false;
        return b.x < n && b.x >= 0 && b.y < n && b.y >= 0;
    }
    public bool MoveCell(Coordinates from, Coordinates to)
    {
        Coordinates emptyCell = CoordinatesOfEmptyCell();
        if (to != emptyCell) return false;
        if (!IsValidMove(from)) return false;
        board[from.x, from.y].MoveTo(to);
        board[to.x, to.y].MoveTo(from);
        (board[from.x, from.y], board[to.x, to.y]) = (board[to.x, to.y], board[from.x, from.y]);
        return true;
    }
}