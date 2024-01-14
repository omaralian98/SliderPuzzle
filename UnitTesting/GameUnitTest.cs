namespace UnitTesting;

public class GameUnitTest
{
    const int n = 3;
    private SliderPuzzleGame game = new SliderPuzzleGame(n);
    [Fact]
    public void IsOverTest1()
    {
        int[,] bor =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
        };
        game.InitializeBoard(bor);
        Assert.True(game.IsOver());
    }
    [Fact]
    public void IsOverTest2()
    {
        int[,] bor =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 0, 8 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.IsOver());
    }
    [Fact]
    public void IsOverTest3()
    {
        int[,] bor =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(bor);
        Assert.False(game.IsOver());
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
        Assert.Equal(SliderPuzzleGame.CoordinatesOfEmptyCell(bor), expectedValue);
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
        Assert.Equal(SliderPuzzleGame.CoordinatesOfEmptyCell(bor), expectedValue);
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
        Assert.Throws<EmptyCellNotFound>(() => { SliderPuzzleGame.CoordinatesOfEmptyCell(bor); });
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
    [Fact]
    public void IsValidFormTest1()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 8, 0];
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.True(result);
    }
    [Fact]
    public void IsValidFormTest2()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }

    [Fact]
    public void IsValidFormTest3()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 0, 0];
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest4()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 1, 0];
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest5()
    {
        int[,] board = 
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.True(result);
    }
    [Fact]
    public void IsValidFormTest6()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }

    [Fact]
    public void IsValidFormTest7()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 0, 0 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest8()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 1, 0 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest9()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 8, 0];
        int[] goal = [8, 0, 2, 4, 5, 6, 7, 1, 3];
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.True(result);
    }
    [Fact]
    public void IsValidFormTest10()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 0, 0];
        int[] goal = [8, 0, 2, 4, 5, 6, 7, 1, 3];
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest11()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 9, 0];
        int[] goal = [8, 0, 2, 4, 5, 6, 7, 1, 3];
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest12()
    {
        int[] board = [1, 2, 3, 9, 5, 6, 7, 10, 0];
        int[] goal = [8, 0, 2, 4, 5, 6, 7, 1, 3];
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest13()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 1, 0, 9];
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest14()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 1, 0, 9, 10];
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest15()
    {
        int[,] board =
        {
            { 1, 2, 3, 9 },
            { 4, 5, 6, 10 },
            { 7, 8, 0, 11 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest16()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
            { 9, 10, 11 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest17()
    {
        int[] board = [1, 2, 3, 4, 5, 6, 7, 8, 0, 9];
        int[] goal = [8, 0, 2, 4, 5, 6, 7, 1, 3, 9];
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest18()
    {
        int[,] board = 
        {
            { 1, 2, 3 }, 
            { 4, 5, 6 }, 
            { 7, 8, 0 },
        };
        int[,] goal =
        {
            { 8, 0, 2 },
            { 4, 5, 6 },
            { 7, 1, 3 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.True(result);
    }
    [Fact]
    public void IsValidFormTest19()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 0, 0 },
        };
        int[,] goal =
        {
            { 8, 0, 2 },
            { 4, 5, 6 },
            { 7, 1, 3 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest20()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 9, 0 },
        };
        int[,] goal =
        {
            { 8, 0, 2 },
            { 4, 5, 6 },
            { 7, 1, 3 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest21()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 9, 5, 6 },
            { 7, 10, 0 },
        };
        int[,] goal =
        {
            { 8, 0, 2 },
            { 4, 5, 6 },
            { 7, 1, 3 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest22()
    {
        int[,] board =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 },
            { 9, 10, 11 },
        };
        int[,] goal =
        {
            { 8, 0, 2 },
            { 4, 5, 6 },
            { 7, 1, 3 },
            { 9, 10, 11 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
    [Fact]
    public void IsValidFormTest23()
    {
        int[,] board =
        {
            { 1, 2, 3, 9 },
            { 4, 5, 6, 10 },
            { 7, 8, 0, 11 },
        };
        int[,] goal =
        {
            { 8, 0, 2, 9 },
            { 4, 5, 6, 10 },
            { 7, 1, 3, 11 },
        };
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.False(result);
    }
}
