using RunApp.Core.Models;

namespace RunApp.Core.Calculations;

// WHAT IS THIS?
// Chops the run into 1 km pieces and times each one.
//
// WHAT DO I DO?
// Add a method: public static IReadOnlyList<Split> Calculate(IReadOnlyList<TrackPoint> points)
// Inside it:
//   1. Walk along the dots, adding up the distance as you go.
//   2. Every time the total goes past another 1000 m, make a Split:
//      how long since the last split ended?
//   3. Whatever is left over at the end becomes one last, shorter split.
//
// Build this AFTER DistanceCalculator works.
public static class SplitCalculator
{
}
