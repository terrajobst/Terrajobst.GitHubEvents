namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventRef
{
    public string Label { get; set; }
    public string Ref { get; set; }
    public string Sha { get; set; }
    public GitHubEventUser User { get; set; }
    public GitHubEventRepository Repo { get; set; }
}
