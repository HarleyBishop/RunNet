using RunApp.Core.Models;

namespace RunApp.Core.Tests.Models;

public class GeoCoordinateTests
{
    // ---------- Constructor ----------

    // Each [InlineData] runs this test once, passing its values in as latitude and longitude.
    [Theory]
    [InlineData(91, 0)]     // latitude just too big
    [InlineData(-91, 0)]    // latitude just too small
    [InlineData(0, 181)]    // longitude just too big
    [InlineData(0, -181)]   // longitude just too small
    public void Constructor_OutOfRange_Throws(double latitude, double longitude)
    {
        // The () => ... is a tiny function. xUnit runs it and checks that it throws.
        Assert.Throws<ArgumentOutOfRangeException>(() => new GeoCoordinate(latitude, longitude));
    }

    [Theory]
    [InlineData(90, 0)]     // North Pole
    [InlineData(-90, 0)]    // South Pole
    [InlineData(0, 180)]    // edge of the map
    [InlineData(0, -180)]   // other edge
    public void Constructor_OnTheEdge_DoesNotThrow(double latitude, double longitude)
    {
        // No Assert needed: if this line throws, xUnit fails the test automatically.
        var point = new GeoCoordinate(latitude, longitude);

        // Check the values were actually saved.
        Assert.Equal(latitude, point.Latitude);
        Assert.Equal(longitude, point.Longitude);
    }

    // ---------- DistanceTo ----------

    [Fact]
    public void DistanceTo_SamePoint_ReturnsZero()
    {
        // Arrange
        var point = new GeoCoordinate(-27.4774, 153.1257);

        // Act
        double distance = point.DistanceTo(point);

        // Assert
        Assert.Equal(0, distance);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 111_195)]     // 1 degree north
    [InlineData(0, 0, 0, 1, 111_195)]     // 1 degree east, on the equator
    [InlineData(60, 0, 60, 1, 55_597)]    // 1 degree east, but up north the lines are closer together
    public void DistanceTo_KnownPoints_ReturnsExpectedMeters(
        double fromLat, double fromLon, double toLat, double toLon, double expectedMeters)
    {
        // Arrange
        var from = new GeoCoordinate(fromLat, fromLon);
        var to = new GeoCoordinate(toLat, toLon);

        // Act
        double distance = from.DistanceTo(to);

        // Assert: within 1 metre is close enough (decimal maths is never exact)
        Assert.Equal(expectedMeters, distance, tolerance: 1);
    }

    [Fact]
    public void DistanceTo_IsSameInBothDirections()
    {
        // Arrange
        var brisbane = new GeoCoordinate(-27.4698, 153.0251);
        var sydney = new GeoCoordinate(-33.8688, 151.2093);

        // Act
        double there = brisbane.DistanceTo(sydney);
        double back = sydney.DistanceTo(brisbane);

        // Assert
        Assert.Equal(there, back, tolerance: 0.001);
    }
}
