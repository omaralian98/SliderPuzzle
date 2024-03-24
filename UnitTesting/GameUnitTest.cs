namespace UnitTesting;

public class GameUnitTest
{
    const int n = 3;
    private SliderPuzzleGame game = new SliderPuzzleGame(n);

    public static TheoryData<int[,], bool> IsOverTestData =>
            new()
            {
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 0 }
                    },
                    true
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 0, 8 }
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 0, 2, 7 },
                        { 3, 1, 6 },
                        { 4, 8, 5 }
                    },
                    false
                },
            };

    public static TheoryData<int[,], Coordinates> CoordinatesOfEmptyCellTestData =>
            new()
            {
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 0 }
                    },
                    new Coordinates(2, 2)
                },
                {
                    new int[,]
                    {
                        { 0, 2, 7 },
                        { 3, 1, 6 },
                        { 4, 8, 5 }
                    },
                    new Coordinates(0, 0)
                },
                {
                    new int[,]
                    {
                        { 9, 2, 0 },
                        { 3, 1, 6 },
                        { 4, 8, 5 }
                    },
                    new Coordinates(0, 2)
                },
            };
    public static TheoryData<int[,], Coordinates, bool> CanMoveTestData =>
            new()
            {
                {
                    new int[,]
                    {
                        { 0, 2, 7 },
                        { 3, 1, 6 },
                        { 4, 8, 5 },
                    },
                    new Coordinates(0, 1),
                    true
                },
                {
                    new int[,]
                    {
                        { 0, 2, 7 },
                        { 3, 1, 6 },
                        { 4, 8, 5 },
                    },
                    new Coordinates(1, 0),
                    true
                },
                {
                    new int[,]
                    {
                        { 0, 2, 7 },
                        { 3, 1, 6 },
                        { 4, 8, 5 },
                    },
                    new Coordinates(0, 0),
                    false
                },
                {
                    new int[,]
                    {
                        { 0, 2, 7 },
                        { 3, 1, 6 },
                        { 4, 8, 5 },
                    },
                    new Coordinates(5, 2),
                    false
                },
                {
                    new int[,]
                    {
                        { 0, 2, 7 },
                        { 3, 1, 6 },
                        { 4, 8, 5 },
                    },
                    new Coordinates(2, 2),
                    false
                }
            };

    public static TheoryData<int[], bool> IsValidFormTestData =>
            new()
            {
                {
                    [1, 2, 3, 4, 5, 6, 7, 8, 0],
                    true
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 8, 9],
                    false
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 0, 0],
                    false
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 1, 0],
                    false
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 1, 0, 9],
                    false
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 1, 0, 9, 10],
                    false
                },
            };

    public static TheoryData<int[,], bool> IsValidForm2DTestData =>
            new()
            {
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 0 },
                    },
                    true
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 9 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 0, 0 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 1, 0 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3, 9 },
                        { 4, 5, 6, 10 },
                        { 7, 8, 0, 11 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 0 },
                        { 9, 10, 11 },
                    },
                    false
                },
            };

    public static TheoryData<int[], int[], bool> IsValidFormWithGoalTestData =>
            new()
            {
                {
                    [1, 2, 3, 4, 5, 6, 7, 8, 0],
                    [8, 0, 2, 4, 5, 6, 7, 1, 3],
                    true
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 9, 0],
                    [8, 0, 2, 4, 5, 6, 7, 1, 3],
                    false
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 0, 0],
                    [8, 0, 2, 4, 5, 6, 7, 1, 3],
                    false
                },
                {
                    [1, 2, 3, 9, 5, 6, 7, 10, 0],
                    [8, 0, 2, 4, 5, 6, 7, 1, 3],
                    false
                },
                {
                    [1, 2, 3, 4, 5, 6, 7, 8, 0, 9],
                    [8, 0, 2, 4, 5, 6, 7, 1, 3, 9],
                    false
                },
            };

    public static TheoryData<int[,], int[,], bool> IsValidForm2DWithGoalTestData =>
        new()
        {
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 0 },
                    },
                    new int[,]
                    {
                        { 8, 0, 2 },
                        { 4, 5, 6 },
                        { 7, 1, 3 },
                    },
                    true
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 0, 0 },
                    },
                    new int[,]
                    {
                        { 8, 0, 2 },
                        { 4, 5, 6 },
                        { 7, 1, 3 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 9, 0 },
                    },
                    new int[,]
                    {
                        { 8, 0, 2 },
                        { 4, 5, 6 },
                        { 7, 1, 3 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 9, 5, 6 },
                        { 7, 10, 0 },
                    },
                    new int[,]
                    {
                        { 8, 0, 2 },
                        { 4, 5, 6 },
                        { 7, 1, 3 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 },
                        { 7, 8, 0 },
                        { 9, 10, 11 },
                    },
                    new int[,]
                    {
                        { 8, 0, 2 },
                        { 4, 5, 6 },
                        { 7, 1, 3 },
                        { 9, 10, 11 },
                    },
                    false
                },
                {
                    new int[,]
                    {
                        { 1, 2, 3, 9 },
                        { 4, 5, 6, 10 },
                        { 7, 8, 0, 11 },
                    },
                    new int[,]
                    {
                        { 8, 0, 2, 9 },
                        { 4, 5, 6, 10 },
                        { 7, 1, 3, 11 },
                    },
                    false
                },
        };


    [Theory]
    [MemberData(nameof(IsOverTestData))]
    public void IsOverTest(int[,] board, bool expected)
    {
        game.InitializeBoard(board);
        Assert.Equal(expected, game.IsOver());
    }

    [Theory]
    [MemberData(nameof(CoordinatesOfEmptyCellTestData))]
    public void CoordinatesOfEmptyCellTest(int[,] board, Coordinates expected)
    {
        game.InitializeBoard(board);
        Assert.Equal(expected, SliderPuzzleGame.CoordinatesOfEmptyCell(board));
    }

    [Theory]
    [MemberData(nameof(CanMoveTestData))]
    public void CanMoveTest(int[,] board, Coordinates move, bool expected)
    {
        game.InitializeBoard(board);
        bool result = game.CanMove(move);
        Assert.Equal(expected, result);
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
    public void MoveCell_InvalidCoordinates_ReturnsFalse()
    {
        int[,] board =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(board);

        Assert.False(game.MoveCell(new Coordinates(2, 2)));
        Assert.False(game.MoveCell(new Coordinates(5, 5)));
        Assert.False(game.MoveCell(new Coordinates(1, 1)));
    }

    [Fact]
    public void MoveCell_ValidCoordinates_ReturnsTrue()
    {
        int[,] board =
        {
            { 0, 2, 7 },
            { 3, 1, 6 },
            { 4, 8, 5 },
        };
        game.InitializeBoard(board);

        Assert.True(game.MoveCell(new Coordinates(1, 0)));
    }

    [Theory]
    [MemberData(nameof(IsValidFormTestData))]
    public void IsValidFormTest(int[] board, bool expected)
    {
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(IsValidForm2DTestData))]
    public void IsValidForm2DTest(int[,] board, bool expected)
    {
        bool result = SliderPuzzleGame.IsValidForm(board);
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(IsValidFormWithGoalTestData))]
    public void IsValidFormWithGoalTest(int[] board, int[] goal, bool expected)
    {
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(IsValidForm2DWithGoalTestData))]
    public void IsValidForm2DWithGoalTest(int[,] board, int[,] goal, bool expected)
    {
        bool result = SliderPuzzleGame.IsValidForm(board, goal);
        Assert.Equal(expected, result);
    }

}
