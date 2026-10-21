using System.Xml.Linq;
using RunApp.Core.Models;

namespace RunApp.Core.Parsing;

// WHAT IS THIS?
// Reads a .gpx file and turns it into a ParsedActivity.

public class GpxParser
{
    public ParsedActivity Parse(Stream stream)
    {
        
        XDocument doc = XDocument.Load(stream);

        // The "surname" every GPX tag has. Use gpx + "tagname" whenever you look for a tag.
        XNamespace gpx = "http://www.topografix.com/GPX/1/1";

        List<TrackPoint> points = doc
            .Descendants(gpx + "trkpt")              // every <trkpt>, however deep it is
            .Select(pt => new TrackPoint             // for each one (called "pt"), make a TrackPoint
            {
                // lat/lon are attributes, so no namespace. Cast to double: throws if missing.
                Coordinate = new GeoCoordinate(
                    (double)pt.Attribute("lat")!,
                    (double)pt.Attribute("lon")!),

                
                Time = (DateTimeOffset)pt.Element(gpx + "time")!,

                ElevationMeters = (double?)pt.Element(gpx + "ele"),
            })
            .ToList();                  

        if (points.Count == 0)
        {
            throw new ActivityParseException("List of Track Points is empty");
        }


        string name = (string?)doc.Descendants(gpx + "name").FirstOrDefault() ?? "No Name";

    

        string? typeText = (string?)doc.Descendants(gpx + "type").FirstOrDefault();

        // Potentially edit so it doesnt default to run
        ActivityType type = typeText switch
        {
            "running" => ActivityType.Run,
            "walking" => ActivityType.Walk,
            _ => ActivityType.Run
        };



        var activity = new ParsedActivity{
            Name = name, 
            Type = type, 
            Format = FileFormat.Gpx, 
            Points = points};

        return activity;
    }
}
