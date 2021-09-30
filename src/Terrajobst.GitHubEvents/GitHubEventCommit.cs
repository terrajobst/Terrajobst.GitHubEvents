namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventCommit
{
    public GitHubEventAuthor Author { get; set; }
    public GitHubEventAuthor Committer { get; set; }
    public bool Distinct { get; set; }
    public string Id { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public string TreeId { get; set; }
    public string Url { get; set; }
}
