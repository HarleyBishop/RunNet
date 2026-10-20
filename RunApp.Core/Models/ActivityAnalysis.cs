namespace RunApp.Core.Models;

// WHAT IS THIS?
// The finished report card: the run from the file, plus everything we worked out about it.
//
// WHAT DO I DO?
// Add these properties:
//   Activity     -> a ParsedActivity (what was in the file)
//   Stats        -> an ActivityStats (the headline numbers)
//   Splits       -> an IReadOnlyList<Split>
//   BestEfforts  -> an IReadOnlyList<BestEffort>
//   ZoneTimes    -> an IReadOnlyList<ZoneTime>
//
// Build this one near the end, once you have a few calculators working.
public sealed record ActivityAnalysis
{
}
