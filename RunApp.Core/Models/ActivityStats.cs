namespace RunApp.Core.Models;

// WHAT IS THIS?
// The big headline numbers at the top of a run page.
//
// WHAT DO I DO?
// Add these properties:
//   DistanceMeters       -> a double
//   ElapsedTime          -> a TimeSpan (first dot to last dot, including stops)
//   MovingTime           -> a TimeSpan (only the time you were actually moving)
//   AveragePace          -> a TimeSpan (time per km)
//   ElevationGainMeters  -> a double
//   AverageHeartRate     -> an int? (missing if the file has no heart rate)
public sealed record ActivityStats
{
}
