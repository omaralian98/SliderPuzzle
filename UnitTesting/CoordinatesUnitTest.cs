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
        decimal expectecValue = 0;
        Assert.Equal(expectecValue, co1.Distance(co2));
    }
    [Fact]
    public void DistanceTest2()
    {
        Coordinates co1 = new Coordinates(1, 1);
        Coordinates co2 = new Coordinates(1, 2);
        decimal expectecValue = 1;
        Assert.Equal(expectecValue, co1.Distance(co2));
    }
    [Fact]
    public void DistanceTest3()
    {
        Coordinates co1 = new Coordinates(1, 1);
        Coordinates co2 = new Coordinates(0, 1);
        decimal expectecValue = 1;
        Assert.Equal(expectecValue, co1.Distance(co2));
    }
    [Fact]
    public void DistanceTest4()
    {
        Coordinates co1 = new Coordinates(1, 1);
        Coordinates co2 = new Coordinates(2, 2);
        decimal expectecValue = 1;
        Assert.NotEqual(expectecValue, co1.Distance(co2));
    }
}