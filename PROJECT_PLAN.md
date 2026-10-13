# Running App: Project Plan

A Strava-style running app built in C# / .NET as a learning project.

---

## 1. .NET vs .NET Framework

Use **.NET**, not **".NET Framework"**. .NET Framework is the old version and only runs on Windows. Modern .NET (currently **.NET 10**, the long-term support release) runs on Windows, Mac, Linux, Android and iOS.

**Path to mobile:** build the backend as a web API and the frontend in **Blazor** (C# web UI). Later, wrap the same Blazor components in **.NET MAUI Blazor Hybrid** to get real Windows, Android and iOS apps. You write the UI once, and the mobile app also gets phone features like GPS.

## 2. Architecture

```
RunApp.sln
├── RunApp.Core          ← class library: models, file parsers, all the running maths
├── RunApp.Core.Tests    ← xUnit tests for the parsers and maths
├── RunApp.Api           ← ASP.NET Core Web API + EF Core + database
├── RunApp.Web           ← Blazor web app (works in any browser, including phones)
└── RunApp.Mobile        ← (later) MAUI Blazor Hybrid app, reuses the Web components
```

- **Core** has no UI or database code. It's pure C#, which makes it the best place to learn the language, and it's easy to test.
- You need an **API and a server database** anyway, because leaderboards and comparing against other runners only work if everyone's data is in one place.

## 3. Libraries and tools

| Piece | Use | Notes |
|---|---|---|
| IDE | **Visual Studio 2022+ Community** (free) | Install the "ASP.NET and web" and ".NET MAUI" workloads |
| Backend | **ASP.NET Core Web API** | Use **Scalar** or Swagger to test endpoints in the browser |
| Database access | **Entity Framework Core** | Start with **SQLite** (just a file, no setup), move to **PostgreSQL** later |
| Login | **ASP.NET Core Identity** + JWT tokens | Built in, no need for a third-party service |
| Frontend | **Blazor** + **MudBlazor** (UI components) | MudBlazor gives you tables, cards and forms for free |
| Maps (web) | **Leaflet.js** + **OpenStreetMap** tiles, called through Blazor JS interop | Free with no API key |
| Maps (mobile) | **Mapsui** or `Microsoft.Maui.Controls.Maps` | Both can draw a polyline from lat/long points |
| Charts | **Blazor-ApexCharts** or **LiveCharts2** | LiveCharts2 works in Blazor *and* MAUI |
| GPX/TCX parsing | **LINQ to XML (`XDocument`)**, written by you | Great learning exercise, and no library needed |
| FIT parsing | **Garmin FIT SDK** (`Garmin.FIT.Sdk` NuGet package) | FIT is a binary format, so use the official SDK |
| Testing | **xUnit** | Test every calculation |
| GPS recording | MAUI `Geolocation` API | Background tracking needs some Android/iOS-specific code (the hardest part) |
| Hosting | Azure App Service (free tier), Railway, or Fly.io | Later on |

## 4. How the data upload works

Runners get files from their watch, phone, or Strava ("Download your data" in Strava settings, or the export button on each activity).

- **GPX**: XML. A list of track points, each with **lat, lon, elevation, time**, and sometimes heart rate/cadence in extension tags. **Start here.**
- **TCX**: Garmin's XML format. Adds laps, heart rate, cadence and distance.
- **FIT**: binary, the native format of Garmin, Coros and similar watches. Has the most data. Add it later.

Example GPX track point:

```xml
<trkpt lat="51.5007" lon="-0.1246">
  <ele>12.4</ele>
  <time>2026-10-01T07:31:05Z</time>
  <extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>148</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions>
</trkpt>
```

**The flow:** the user uploads a file → the API receives it (`IFormFile`) → Core parses it into a list of `TrackPoint`s → Core calculates the stats → you save one Activity summary row plus the points → the frontend fetches them and draws the map and charts.

### Metrics to calculate

- **Distance**: the **Haversine formula** between consecutive lat/lon points, summed up
- **Moving time**: total time minus the gaps where speed was near zero
- **Pace**: overall, plus **per-km splits**
- **Elevation gain**: sum the climbs only, with smoothing, because GPS elevation is noisy
- **Heart-rate zones**: time spent in each zone, based on the user's max HR
- **Best efforts**: your fastest 1 km, 5 km, 10 km *inside* any run (a sliding-window / two-pointer problem)
- **PBs**: the best "best effort" across all runs

### Basic data model

- `User`: id, name, email, max HR, units preference
- `Activity`: id, userId, start time, distance, duration, moving time, elevation gain, average HR, original file
- `TrackPoint`: activityId, sequence, lat, lon, elevation, time, HR, cadence
- `BestEffort`: activityId, userId, distance (1k/5k/…), time. Leaderboards and PBs are just queries over this table.

## 5. Features and pages

### Version 1 (single user, web)
1. **Upload**: drag-and-drop a GPX file
2. **Activities list**: table of runs, sortable and filterable by date and distance
3. **Activity detail**: map with the route, a stats summary, a splits table, and pace/elevation/HR charts
4. **Trends**: weekly/monthly distance, how your average pace changes over time, a consistency calendar
5. **Personal records**: best 1k/5k/10k/half/marathon, with links to the runs

### Version 2 (multi-user)
6. **Login/Register**
7. **Dashboard/feed**: your recent runs and this week's summary (later, runs from people you follow)
8. **Compare runs**: two runs side by side, with both routes on the map and pace charts overlaid
9. **Leaderboards**: best efforts by distance, filterable by age group or gender
10. **Profile/settings**: units, max HR, HR zones, **privacy zones** (hide the start and end of routes near your home)

### Version 3 (mobile)
11. **Record a run**: live GPS, timer, current pace. Saves a GPX locally and uploads it when you finish.
12. Extras: TCX/FIT support, importing from the Strava API (OAuth), segments, a personal heatmap of all your routes

**What's hard:** background GPS recording on phones, and Strava-style **segment matching**. Everything else is ordinary, very achievable work.

## 6. Step-by-step plan

- [ ] **Step 0: Setup.** Install Visual Studio and the .NET 10 SDK, create a GitHub repo, and commit often. Export a few of your own GPX files to use as test data.
- [ ] **Step 1: Console app parser (in `RunApp.Core`).** Load a GPX file with `XDocument`, turn it into a `List<TrackPoint>`, and print the total distance, time and pace. *You'll learn:* classes/records, LINQ, `DateTime`/`TimeSpan`, XML namespaces.
- [ ] **Step 2: The metrics.** Splits, moving time, elevation gain, best efforts. Write **xUnit tests** for each one using small fake GPX files with known values.
- [ ] **Step 3: The API.** ASP.NET Core Web API with EF Core + SQLite. Endpoints: `POST /activities` (upload), `GET /activities`, `GET /activities/{id}`. Test with Scalar/Swagger. *You'll learn:* dependency injection, controllers or minimal APIs, migrations, DTOs vs entities, async/await.
- [ ] **Step 4: Blazor frontend.** Upload, Activities list and Activity detail pages (stats only for now) using MudBlazor. *You'll learn:* components, `HttpClient`, routing, parameters and state.
- [ ] **Step 5: The map.** Add Leaflet through JS interop. Pass the lat/lon list from C# to JavaScript and draw a polyline.
- [ ] **Step 6: The charts.** Pace, elevation and HR against distance on the detail page.
- [ ] **Step 7: Trends and PBs pages.** Mostly LINQ/EF queries that group by week or month.
- [ ] **Step 8: Logins and multiple users.** ASP.NET Core Identity, protect the endpoints, and make every query filter by the current user.
- [ ] **Step 9: Social features.** Leaderboards, comparing runs, privacy zones.
- [ ] **Step 10: Deploy** the API and web app, and switch to PostgreSQL.
- [ ] **Step 11: Mobile.** MAUI Blazor Hybrid project, move Blazor components into a shared Razor Class Library, build the Record page with `Geolocation`. Start with tracking while the screen is on, and add background tracking afterwards.

## 7. How to build it without AI writing the code

- **Build thin "vertical slices".** Get one feature working end to end (upload → save → display) before polishing anything.
- **Learn from Microsoft Learn first.** Do its tutorials "Create a web API with ASP.NET Core", "EF Core getting started" and "Build your first Blazor app" in a scratch project, then apply them here.
- **Book:** Mark Price's *C# and .NET – Modern Cross-Platform Development* (latest edition).
- **Write tests first for Core.** "A 1 km straight line at 5:00 pace should give a 5:00 split." The tests become your spec.
- **Work out the maths on paper first.** Write Haversine, splits and best efforts as plain-English steps before writing any C#.
- **Read the errors properly.** EF Core and DI errors are wordy but usually tell you exactly what's wrong.
- **Use AI as a tutor, not an author.** Ask it to explain concepts, debug your thinking, or review your code without rewriting it.
- **Keep a short dev log** (a markdown file in the repo) of what you learned and what you got stuck on.
