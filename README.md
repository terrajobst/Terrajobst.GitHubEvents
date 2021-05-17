# Terrajobst.GitHubEvents

[![CI](https://github.com/terrajobst/Terrajobst.GitHubEvents/actions/workflows/CI.yml/badge.svg)](https://github.com/terrajobst/Terrajobst.GitHubEvents/actions/workflows/CI.yml)
[![nuget](https://img.shields.io/nuget/v/Terrajobst.GitHubEvents.AspNetCore.svg)](https://www.nuget.org/packages/Terrajobst.GitHubEvents.AspNetCore/)

These libraries help with handling GitHub events in .NET applications.

## Usage in ASP.NET Core

Assuming your web hook lives in ASP.NET Core, simply do the following:

1. `dotnet add package Terrajobst.GitHubEvents.AspNetCore`
2. Modify your `Startup.cs` to add the web hook end point

    ```C#
    app.UseEndpoints(endpoints =>
    {
        ...
        endpoints.MapGitHubWebHook();
        ...
    });
    ```

Method takes two optional parameters:

* `pattern`. Defaults to `/github-webhook`, the URL of the end point to use for
  GitHub.
* `secret`. The secret you have configured in GitHub, if you have set this up.

### CI Builds

    $ dotnet tool install Terrajobst.GitHubEvents.AspNetCore -g --add-source https://nuget.pkg.github.com/terrajobst/index.json
