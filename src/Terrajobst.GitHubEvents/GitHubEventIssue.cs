using Newtonsoft.Json.Linq;

namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventIssue : GitHubEventIssueOrPullRequest
{
    public string RepositoryUrl { get; set; }
    public string LabelsUrl { get; set; }
    public string EventsUrl { get; set; }
    public JObject PerformedViaGithubApp { get; set; }
}
