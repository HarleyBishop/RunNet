using RunApp.Core.Models;

namespace RunApp.Core.Calculations;

// WHAT IS THIS?
// Works out how long you were actually moving, NOT counting standing at traffic lights.
//
// WHAT DO I DO?
// Add a method: public static TimeSpan Calculate(IReadOnlyList<TrackPoint> points)
// Inside it, for each pair of neighbour dots:
//   1. How much time passed between them?
//   2. How far apart are they?
//   3. Speed = distance / seconds.
//   4. If the speed is more than a tiny bit (try 0.5 metres per second), add that time.
//      If not, you were standing still, so skip it.
//
// FUN FACT: Chill.gpx starts with two dots in the exact same place. You were standing still!
public static class MovingTimeCalculator
{
}
