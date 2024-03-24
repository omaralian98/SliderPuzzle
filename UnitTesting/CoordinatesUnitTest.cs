namespace UnitTesting;

public class CoordinatesUnitTest
{

    public static TheoryData<Coordinates, Coordinates, decimal> DistanceTestData =>
        new()
        {
            { new Coordinates(1, 1), new Coordinates(1, 1), 0 },
            { new Coordinates(1, 1), new Coordinates(1, 2), 1 },
            { new Coordinates(1, 1), new Coordinates(0, 1), 1 },
            { new Coordinates(1, 1), new Coordinates(2, 2), (decimal)Math.Sqrt(2) }
        };

    public static TheoryData<Coordinates, Coordinates, bool> EqualityTestData =>
        new()
        {
            { new Coordinates(1, 1), new Coordinates(1, 1), true },
            { new Coordinates(1, 1), new Coordinates(1, 2), false },
            { new Coordinates(1, 1), new Coordinates(0, 1), false },
            { new Coordinates(1, 1), new Coordinates(2, 2), false }
        };

    [Theory]
    [MemberData(nameof(DistanceTestData))]
    public void DistanceTest(Coordinates co1, Coordinates co2, decimal expectedValue)
    {
        Assert.Equal(expectedValue, co1.Distance(co2));
    }

    [Theory]
    [MemberData(nameof(EqualityTestData))]
    public void EqualityTest(Coordinates co1, Coordinates co2, bool shouldBeEqual)
    {
        if (shouldBeEqual)
        {
            Assert.True(co1 == co2);
            Assert.False(co1 != co2);
        }
        else
        {
            Assert.False(co1 == co2);
            Assert.True(co1 != co2);
        }
    }

}