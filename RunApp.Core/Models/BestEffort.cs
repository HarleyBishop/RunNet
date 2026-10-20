namespace RunApp.Core.Models;

// WHAT IS THIS?
// Your fastest 1 km (or 5 km, or 10 km) hiding ANYWHERE inside a run.
// It doesn't have to start at the beginning of the run.
//
// WHAT DO I DO?
// Add these properties:
//   DistanceMeters  -> a double (e.g. 1000 or 5000)
//   Duration        -> a TimeSpan (how fast you did it)
//   StartIndex      -> an int (which dot it started at)
//   EndIndex        -> an int (which dot it ended at)
public sealed record BestEffort
{
}
