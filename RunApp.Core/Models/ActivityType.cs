namespace RunApp.Core.Models;

// WHAT IS THIS?
// A list of the kinds of exercise someone can do.
//
// WHAT DO I DO?
// Write each kind inside the { }, with a comma after each one.
// Example: Run, Ride, Walk, Hike
//
// TIP: Your GPX files say <type>running</type>. The parser will turn that word into Run.
public enum ActivityType
{
    Run,
    Walk,
    Climb,
    Hike,
    Ride

}
