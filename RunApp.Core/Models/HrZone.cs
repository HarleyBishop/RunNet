namespace RunApp.Core.Models;

// WHAT IS THIS?
// One heart-rate "band". Example: Zone 2 = 120 to 140 beats per minute.
//
// WHAT DO I DO?
// Add these properties:
//   Number  -> an int (1 to 5)
//   MinBpm  -> an int (the bottom of the band)
//   MaxBpm  -> an int (the top of the band)
//
// You won't need this until your runs have heart rate in them.
public sealed record HrZone
{
}
