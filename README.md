# Gift of the Givers — ASP.NET Core application

This is a .NET 8 MVC web application that uses ASP.NET Core Identity and SQLite. It implements the two required roles:

- **Donor:** registration/login and optional anonymous donations.
- **Employee:** login, viewing volunteer sign-ups, and posting project updates.

## Run and deploy

In Visual Studio 2022 (version 17.8 or newer), extract the ZIP and open **GiftoftheGivers.sln** — do not open the ZIP or try to run an individual file. Install the **ASP.NET and web development** workload and .NET 8 SDK if prompted, then press F5.

The executable is generated automatically when Visual Studio builds the project; you should not need to supply an `.exe` file. The application does not hard-code a local address; a host provides its public address through the `ASPNETCORE_URLS` environment setting. Publish it with `dotnet publish -c Release -o publish` and deploy the `publish` folder to an ASP.NET Core-capable host.
