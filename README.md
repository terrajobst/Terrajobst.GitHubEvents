# Terrajobst.GitHubEvents

[![CI](https://github.com/terrajobst/Terrajobst.GitHubEvents/actions/workflows/CI.yml/badge.svg)](https://github.com/terrajobst/Terrajobst.GitHubEvents/actions/workflows/CI.yml)
[![nuget](https://img.shields.io/nuget/v/Terrajobst.GitHubEvents.AspNetCore.svg)](https://www.nuget.org/packages/Terrajobst.GitHubEvents.AspNetCore/)

These libraries help with handling GitHub events in .NET applications.

## Usage in GitHub Actions

If you're running inside of GitHub Actions, you can simply access the event data
that triggered the workflow by doing this:

```C#
GitHubEvent gitHubEvent = GitHubEvent.FromGitHubActions();
if (gitHubEvent is not null)
{
    // Triggered from inside GitHub Actions
}
```

## Usage in ASP.NET Core

Assuming your web hook lives in ASP.NET Core, simply do the following:

1. `dotnet add package Terrajobst.GitHubEvents.AspNetCore`
2. Add a class that implements from `IGitHubEventProcessor` to handle events
   from GitHub:

    ```C#
    public sealed class MyGitHubEventProcessor : IGitHubEventProcessor
    {
        private readonly TelemetryClient _telemetryClient;

        public MyGitHubEventProcessor(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void Process(GitHubEvent @event)
        {
            _telemetryClient.GetMetric("github_" + @event.Kind)
                            .TrackValue(1.0);
        }
    }
    ```

3. Modify your `ConfigureServices()` method to register an implementation for
   `IGitHubEventProcessor`:

    ```C#
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.AddSingleton<IGitHubEventProcessor, MyGitHubEventProcessor>();
        ...
    }
    ```
4. Modify your `Configure()` method to map the web hook end point:

    ```C#
    app.UseEndpoints(endpoints =>
    {
        ...
        endpoints.MapGitHubWebHook();
        ...
    });
    ```

`MapGitHubWebHook()` takes two optional parameters:

* `pattern`. Defaults to `/github-webhook`, the URL of the end point to use for
  GitHub.
* `secret`. The secret you have configured in GitHub, if you have set this up.

### CI Builds

    $ dotnet tool install Terrajobst.GitHubEvents.AspNetCore -g --add-source https://nuget.pkg.github.com/terrajobst/index.json
