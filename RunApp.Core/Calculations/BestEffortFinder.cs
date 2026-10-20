using RunApp.Core.Models;

namespace RunApp.Core.Calculations;

// WHAT IS THIS?
// Finds your fastest 1 km (or 5 km...) ANYWHERE in the run, not just from the start.
//
// WHAT DO I DO?
// Add a method: public static BestEffort? Find(IReadOnlyList<TrackPoint> points, double distanceMeters)
// Imagine putting two fingers on the line of dots: a "start" finger and an "end" finger.
//   1. Move the end finger forward until the gap between the fingers is at least distanceMeters.
//   2. Write down how long that took. Is it the fastest so far? Keep it.
//   3. Move the start finger forward one dot, and repeat.
//   4. Return the fastest one (or null if the run is shorter than distanceMeters).
//
// This trick is called a "sliding window". It's the hardest calculator, so do it last.
public static class BestEffortFinder
{
}
