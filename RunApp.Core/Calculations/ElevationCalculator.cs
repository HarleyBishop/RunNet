using RunApp.Core.Models;

namespace RunApp.Core.Calculations;

// WHAT IS THIS?
// Works out how much uphill you climbed in total.
//
// WHAT DO I DO?
// Add a method: public static double GainMeters(IReadOnlyList<TrackPoint> points)
// Inside it, for each pair of neighbour dots:
//   1. If either dot has no elevation (null), skip the pair.
//   2. If the second dot is HIGHER, add the difference.
//   3. If it's lower, do nothing. Downhill doesn't count.
//
// TEST IT: heights 10 -> 15 -> 12 -> 20 should give 13 (5 up, then 8 up).
//
// LATER: GPS heights wobble up and down a lot, which makes the number too big.
//        Look up "moving average smoothing" once the simple version works.
public static class ElevationCalculator
{
}
