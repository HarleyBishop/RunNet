namespace RunApp.Core.Parsing;

// WHAT IS THIS?
// A special error that means "this file is broken, or it isn't a run file".
// The parser throws it so that whoever called the parser knows exactly what went wrong.

public class ActivityParseException : Exception
{
    public ActivityParseException(string message): base(message) {}
}
