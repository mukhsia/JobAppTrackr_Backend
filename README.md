# JobAppTrackr_Backend
C# Web API backend for JobAppTrackr using starter base code of a _Car Make Model_ backend. My Capstone Project for DevCodeCamp.

Uses [Entity Framework Core](https://github.com/dotnet/efcore) with the related MySQL packages and [ASP.NET Core Authentication & Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-7.0).

The Backend should be used with the [JobAppTrackr_Frontend](https://github.com/mukhsia/JobAppTrackr_Frontend).

Requires installing .NET SDK and Visual Studio 2022 or later.

Might require setting up a MySQL database.

# MySQL Pre-Setup
0. Install [MySQL server](https://dev.mysql.com/downloads/mysql/8.0.html), keep note of the port, username, and password
1. Install [MySQLWorkbench](https://dev.mysql.com/downloads/workbench/)
2. Create a new connection to the local database server, keep note of the database name.

# Setup

1. Open `FullStackAuth_WebAPI.sln` with Visual Studio (2022)
2. Open `appsettings.json`
3. Modify the `DefaultConnection` values for the `Port`, `Database`, `User`, and `Password` if needed.
4. Open the Package Manager Console in Tools -> NuGet Package Manager -> Package Manager Console.
5. In the Package Manager Console run `Update-Database` (might require running `Add-Migration init` beforehand).
6. Run the project (https).

# Stretch Goal/ Potential TODOs

1. Recheck error handling and error messages.
2. Remove irrelevant legacy files/ code related to _Car Make Model_