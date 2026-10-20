namespace RunApp.Core.Models;

// WHAT IS THIS?
// One kilometre-sized piece of a run. A 5 km run has 5 splits.
//
// WHAT DO I DO?
// Add these properties:
//   Number          -> an int (1 for the first km, 2 for the second...)
//   DistanceMeters  -> a double (usually 1000, but the last one can be shorter)
//   Duration        -> a TimeSpan (how long this piece took)
//   Pace            -> a TimeSpan (how long 1 km would take at this speed)
public sealed record Split
{
}
