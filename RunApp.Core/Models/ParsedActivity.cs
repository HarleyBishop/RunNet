namespace RunApp.Core.Models;

// WHAT IS THIS?
// Everything we read out of the file, with NOTHING worked out yet.
// Like tipping your school bag out onto the table: everything is there, but not sorted.
//
// WHAT DO I DO?
// Add these properties:
//   Name    -> a string (e.g. "Chill")
//   Type    -> an ActivityType (e.g. Run)
//   Format  -> a FileFormat (e.g. Gpx)
//   Points  -> an IReadOnlyList<TrackPoint> (all the dots, in order)
//
// TIP: IReadOnlyList means other code can look at the dots but can't add or remove any.
public sealed record ParsedActivity
{
    public required string Name {get; init; }    
    public required ActivityType Type {get; init; }
    public required  FileFormat Format {get; init;}
    public required IReadOnlyList<TrackPoint> Points {get; init; }
}



