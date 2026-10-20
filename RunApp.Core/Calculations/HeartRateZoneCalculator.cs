using RunApp.Core.Models;

namespace RunApp.Core.Calculations;

// WHAT IS THIS?
// Works out how long you spent in each heart-rate zone.
//
// WHAT DO I DO?
// 1. A method that makes 5 HrZones from someone's max heart rate:
//      Zone 1 = 50-60% of max, Zone 2 = 60-70%, Zone 3 = 70-80%, Zone 4 = 80-90%, Zone 5 = 90-100%
// 2. A method that, for each pair of neighbour dots, checks which zone the heart rate was in,
//    and adds the time between the dots to that zone. Return a list of ZoneTimes.
//
// Your GPX files have no heart rate yet, so leave this one until you have a file that does.
public static class HeartRateZoneCalculator
{
}
