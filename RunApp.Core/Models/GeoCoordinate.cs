namespace RunApp.Core.Models;

// A spot on Earth: how far north/south (latitude) and east/west (longitude).
public readonly record struct GeoCoordinate
{
    public double Latitude {get;}
    public double Longitude{get;}

    public GeoCoordinate(double latitude, double longitude)
    {
        
        // Check longitude and latitude values are Correct
        if (latitude < -90 || latitude > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90.");
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude must be between -180 and 180.");
        }

        Latitude = latitude;
        Longitude = longitude;
    }

    public double DistanceTo(GeoCoordinate other)
    {
        // Haversine formula: distance in metres between two lat/lon points on a sphere.
        //   a = sin²(Δφ / 2) + cos(φ1) · cos(φ2) · sin²(Δλ / 2)
        //   c = 2 · atan2(√a, √(1 − a))
        //   d = R · c
        double lat1Rad = ToRadians(Latitude);
        double lat2Rad = ToRadians(other.Latitude);
        double deltaLatRad = ToRadians(other.Latitude - Latitude);
        double deltaLonRad = ToRadians(other.Longitude - Longitude);

        double sinHalfDeltaLat = Math.Sin(deltaLatRad / 2);
        double sinHalfDeltaLon = Math.Sin(deltaLonRad / 2);

        double a = sinHalfDeltaLat * sinHalfDeltaLat
                 + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * sinHalfDeltaLon * sinHalfDeltaLon;
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusMeters * c;
    }

    private const double EarthRadiusMeters = 6_371_000;

    private static double ToRadians(double degrees) => degrees * Math.PI / 180;

}