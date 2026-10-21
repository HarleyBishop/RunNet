
using RunApp.Core.Models;
using RunApp.Core.Parsing;

string path = "C:/Users/Harle/Coding Projects/Running net app/example runs/Chill.gpx";

using var stream = File.OpenRead(path);
GpxParser parser = new GpxParser();
ParsedActivity activity = parser.Parse(stream);

Console.WriteLine($"Name:{activity.Name}, Format:{activity.Format}, Points:{activity.Points.Count}, Type:{activity.Type}");
