using RunApp.Core.Models;

namespace RunApp.Core.Calculations;

// WHAT IS THIS?
// The boss. It doesn't do any maths itself. It asks every other calculator
// for its answer, then packs all the answers into one ActivityAnalysis.
//
// WHAT DO I DO?
// Add a method: public static ActivityAnalysis Analyze(ParsedActivity activity)
// Inside it:
//   1. Ask DistanceCalculator, MovingTimeCalculator, ElevationCalculator... for their answers.
//   2. Put the headline numbers into an ActivityStats.
//   3. Put everything into an ActivityAnalysis and return it.
//
// Build this LAST, once two or three calculators work.
public static class ActivityAnalyzer
{
}
