using System.Text;
using RunApp.Core.Models;
using RunApp.Core.Parsing;

namespace RunApp.Core.Tests.Parsing;


// Test make small gpx as string
// use this to convert to stream var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
// Pass to new gpx parser for either pass or fail rememberto include namespace

// Checks to tick off: Successful infor for values and 2 points in trhe list, No name, no Points, No type/ Unknown type, no elevation for trackpoint, no time value in trackpoint
public class GpxParserTests
{

    // Happy path: name, type, 2 points with everything
private const string HappyPathGpx = """
    <?xml version="1.0" encoding="UTF-8"?>
    <gpx xmlns="http://www.topografix.com/GPX/1/1" version="1.1" creator="Test">
      <trk>
        <name>Test Run</name>
        <type>running</type>
        <trkseg>
          <trkpt lat="-27.4774190" lon="153.1257400">
            <ele>4.7</ele>
            <time>2026-04-12T05:58:10Z</time>
          </trkpt>
          <trkpt lat="-27.4775000" lon="153.1258000">
            <ele>5.1</ele>
            <time>2026-04-12T05:58:11Z</time>
          </trkpt>
        </trkseg>
      </trk>
    </gpx>
    """;

// No <name> tag → expect "No Name"
private const string NoNameGpx = """
    <?xml version="1.0" encoding="UTF-8"?>
    <gpx xmlns="http://www.topografix.com/GPX/1/1" version="1.1" creator="Test">
      <trk>
        <type>running</type>
        <trkseg>
          <trkpt lat="-27.4774190" lon="153.1257400">
            <ele>4.7</ele>
            <time>2026-04-12T05:58:10Z</time>
          </trkpt>
        </trkseg>
      </trk>
    </gpx>
    """;

// Type we don't know → expect your default
private const string UnknownTypeGpx = """
    <?xml version="1.0" encoding="UTF-8"?>
    <gpx xmlns="http://www.topografix.com/GPX/1/1" version="1.1" creator="Test">
      <trk>
        <name>Test Swim</name>
        <type>swimming</type>
        <trkseg>
          <trkpt lat="-27.4774190" lon="153.1257400">
            <ele>4.7</ele>
            <time>2026-04-12T05:58:10Z</time>
          </trkpt>
        </trkseg>
      </trk>
    </gpx>
    """;

// No trkpts at all → expect ActivityParseException
private const string NoPointsGpx = """
    <?xml version="1.0" encoding="UTF-8"?>
    <gpx xmlns="http://www.topografix.com/GPX/1/1" version="1.1" creator="Test">
      <trk>
        <name>Empty</name>
        <type>running</type>
        <trkseg>
        </trkseg>
      </trk>
    </gpx>
    """;

// Point with no <ele> → expect ElevationMeters == null
private const string NoElevationGpx = """
    <?xml version="1.0" encoding="UTF-8"?>
    <gpx xmlns="http://www.topografix.com/GPX/1/1" version="1.1" creator="Test">
      <trk>
        <name>Flat</name>
        <type>running</type>
        <trkseg>
          <trkpt lat="-27.4774190" lon="153.1257400">
            <time>2026-04-12T05:58:10Z</time>
          </trkpt>
        </trkseg>
      </trk>
    </gpx>
    """;

// Point with no <time> → you decide what should happen 
private const string NoTimeGpx = """
    <?xml version="1.0" encoding="UTF-8"?>
    <gpx xmlns="http://www.topografix.com/GPX/1/1" version="1.1" creator="Test">
      <trk>
        <name>Timeless</name>
        <type>running</type>
        <trkseg>
          <trkpt lat="-27.4774190" lon="153.1257400">
            <ele>4.7</ele>
          </trkpt>
        </trkseg>
      </trk>
    </gpx>
    """;






    [Fact]
    public void Successful_Parse()
    {

    var stream = new MemoryStream(Encoding.UTF8.GetBytes(HappyPathGpx));
    GpxParser parser = new GpxParser();
    ParsedActivity successfulActivity = parser.Parse(stream);

    Assert.Equal("Test Run", successfulActivity.Name);
    Assert.Equal(ActivityType.Run, successfulActivity.Type);
    Assert.Equal(FileFormat.Gpx, successfulActivity.Format);
    Assert.Equal( 2, successfulActivity.Points.Count);
    Assert.Equal( -27.4774190, successfulActivity.Points[0].Coordinate.Latitude);
    }


    [Fact]
    public void NoNameValue()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(NoNameGpx));
        GpxParser parser = new GpxParser();
        ParsedActivity noNameActivity = parser.Parse(stream);

        Assert.Equal("No Name", noNameActivity.Name);
    }


    [Fact]
    public void UnknownType()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(UnknownTypeGpx));
        GpxParser parser = new GpxParser();
        ParsedActivity unknownTypeActivity = parser.Parse(stream);

        Assert.Equal(ActivityType.Run, unknownTypeActivity.Type);
    }


    [Fact]
    public void NoPoints()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(NoPointsGpx));
        GpxParser parser = new GpxParser();

        // Because error will be cause when trying to parse the stream 
        Assert.Throws<ActivityParseException>(() => parser.Parse(stream));
    }


     [Fact]
    public void NoElevation()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(NoElevationGpx));
        GpxParser parser = new GpxParser();
        ParsedActivity noElevationActivity = parser.Parse(stream);

        Assert.Null(noElevationActivity.Points[0].ElevationMeters);
    }

    [Fact]
    public void NoTime()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(NoTimeGpx));
        GpxParser parser = new GpxParser();
        
        Assert.Throws<ActivityParseException>(() => parser.Parse(stream));
    }


}

