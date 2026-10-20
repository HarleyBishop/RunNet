using System.ComponentModel;

namespace RunApp.Core.Models;

// WHAT IS THIS?
// One dot on the map. Your watch drops a dot about every second.
// A run is just a long line of these dots.
//
// WHAT DO I DO?
// Add these properties:
//   Coordinate       -> a GeoCoordinate (where the dot is)
//   Time             -> a DateTimeOffset (when the dot was dropped)
//   ElevationMeters  -> a double?  (how high up)
//   HeartRate        -> an int?    (beats per minute)
//   Cadence          -> an int?    (steps per minute)
//
// The ? means "this might be missing". Your GPX files have no heart rate, so it will be null.
//
// TIP: Write each property like this: public double? ElevationMeters { get; init; }
//      "init" means you can set it once when you make the dot, and never change it after.


// GPX Trackpoint
//<trkpt lat="-27.4774190" lon="153.1257400">
//    <ele>4.7</ele>
//    <time>2026-04-12T05:58:10Z</time>
//   </trkpt>
public sealed record TrackPoint

{
    public GeoCoordinate Coordinate {get; init; }
    public DateTimeOffset Time {get; init; }
    public double? ElevationMeters {get; init; }
    public int? HeartRate {get; init; }
    public int? StepPerMin {get; init; }

}
