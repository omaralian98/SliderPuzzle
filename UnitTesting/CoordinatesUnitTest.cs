namespace UnitTesting;

public class CoordinatesUnitTest
{
    [Fact]
    public void EqualTest()
    {
        Coordinates co1 = new Coordinates(1, 1);
        Coordinates co2 = new Coordinates(1, 1);
        Assert.True(co1 == co2);
        Assert.False(co1 != co2);
    }

    [Fact]
    public void DistanceTest1()
    {
        Coordinates co1 = new Coordinates(1, 1);
        Coordinates co2 = new Coordinates(1, 1);
        Assert.True(co1.Distance());
    }
}