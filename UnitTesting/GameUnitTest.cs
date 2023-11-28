namespace UnitTesting;

public class GameUnitTest
{
    const int n = 3;
    private SliderPuzzleGame game = new SliderPuzzleGame(n);
    [Fact]
    public void IsGameOverTest1()
    {
        int[,] bor =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
        };
        game.InitializeBoard(bor);
        Assert.True(game.IsGameOver());
    }
    [Fact]
    public void IsGameOverTest2()
    {
        int[,] bor =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 0, 8 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.IsGameOver());
    }
    [Fact]
    public void IsGameOverTest3()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.IsGameOver());
    }
    [Fact]
    public void CoordinatesOfEmptyCellTest1()
    {
        int[,] bor =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
        };
        game.InitializeBoard(bor);
        Coordinates expectedValue = new Coordinates(2, 2);
        Assert.Equal(game.CoordinatesOfEmptyCell(), expectedValue);
    }
    [Fact]
    public void CoordinatesOfEmptyCellTest2()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Coordinates expectedValue = new Coordinates(0, 0);
        Assert.Equal(game.CoordinatesOfEmptyCell(), expectedValue);
    }
    [Fact]
    public void CoordinatesOfEmptyCellTest3()
    {
        int[,] bor =
        {
            { 9, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.Throws<Exception>(() => { game.CoordinatesOfEmptyCell(); });
    }
    [Fact]
    public void CanMoveTest1()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.True(game.CanMove(new Coordinates(0, 1)));
    }
    [Fact]
    public void CanMoveTest2()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.True(game.CanMove(new Coordinates(1, 0)));
    }
    [Fact]
    public void CanMoveTest3()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.CanMove(new Coordinates(0, 0)));
    }
    [Fact]
    public void CanMoveTest4()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.CanMove(new Coordinates(5, 2)));
    }
    [Fact]
    public void CanMoveTest5()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.CanMove(new Coordinates(2, 2)));
    }
    [Fact]
    public void MoveCellTest1()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.MoveCell(new Coordinates(2, 2)));
    }
    [Fact]
    public void MoveCellTest2()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.MoveCell(new Coordinates(5, 5)));
    }
    [Fact]
    public void MoveCellTest3()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.MoveCell(new Coordinates(1, 1)));
    }
    [Fact]
    public void MoveCellTest4()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.True(game.MoveCell(new Coordinates(1, 0)));
    }
}
