using RunApp.Core.Models;

namespace RunApp.Core.Parsing;

// WHAT IS THIS?
// A promise. Any class that says "I am an IActivityParser" promises it can
// turn a file into a ParsedActivity. GpxParser will make that promise, and later TcxParser too.
//
// WHAT DO I DO?
// Leave it empty for now. When you add your SECOND parser (TCX), put this one line inside:
//   ParsedActivity Parse(Stream stream);
// and then write ": IActivityParser" after "GpxParser" so it keeps the promise.
public interface IActivityParser
{
}
