namespace RunApp.Core.Models;

// WHAT IS THIS?
// A list of the kinds of file a run can come from.
//
// WHAT DO I DO?
// Add: Gpx, Tcx, Fit
// (You only need Gpx for a long time. The others are for later.)
public enum FileFormat
{
    Gpx,
    Tcx,
    Fit,
}
