using RunApp.Core.Models;

namespace RunApp.Core.Calculations;

// WHAT IS THIS?
// Works out how far the whole run was.
//
// WHAT DO I DO?
// Add a method: public static double TotalMeters(IReadOnlyList<TrackPoint> points)
// Inside it:
//   1. Start with total = 0.
//   2. Look at each pair of neighbour dots: dot 0 and 1, then 1 and 2, then 2 and 3...
//   3. Add the distance between them to the total (use GeoCoordinate.DistanceTo).
//   4. Return the total.
//
// TEST IT: make 3 dots yourself where you know the gaps, and check the total is right.
//          Also check 0 dots and 1 dot both give 0.
public static class DistanceCalculator
{
}
