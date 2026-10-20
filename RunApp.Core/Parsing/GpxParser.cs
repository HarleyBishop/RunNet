using RunApp.Core.Models;

namespace RunApp.Core.Parsing;

// WHAT IS THIS?
// Reads a .gpx file and turns it into a ParsedActivity.
//
// WHAT DO I DO?
// Add a method: public ParsedActivity Parse(Stream stream)
// Inside it:
//   1. Load the file with XDocument.Load(stream). (Add "using System.Xml.Linq;" at the top.)
//   2. Every GPX tag has a hidden "surname" called a namespace: http://www.topografix.com/GPX/1/1
//      You MUST use it when searching for tags, or you'll find nothing. Look up "XNamespace".
//   3. Find every <trkpt> tag.
//   4. For each one, read lat and lon (attributes), <ele> and <time> (child tags),
//      and make a TrackPoint out of them.
//   5. Read the <name> and <type> tags.
//   6. Put it all into a ParsedActivity and return it.
//   7. If the file is broken (no points, a missing time), throw an ActivityParseException.
//
// TEST IT: open "example runs/Chill.gpx" and check you get lots of points, and the name is "Chill".
public class GpxParser
{
}
