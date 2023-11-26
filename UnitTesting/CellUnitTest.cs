namespace UnitTesting;

public class CellUnitTest
{
    [Fact]
    public void MoveToTest()
    {
        Cell cell = new Cell(new Coordinates(1, 1), 5);
        cell.MoveTo(new Coordinates(2, 1));
        Coordinates expectedValue = new Coordinates(2, 1);
        Assert.Equal(cell.coordinates, expectedValue);
    }
}