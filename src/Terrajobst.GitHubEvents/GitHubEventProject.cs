namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventProject
{
    public string OwnerUrl { get; set; }
    public string Url { get; set; }
    public string HtmlUrl { get; set; }
    public string ColumnsUrl { get; set; }
    public long Id { get; set; }
    public string NodeId { get; set; }
    public string Name { get; set; }
    public string Body { get; set; }
    public int Number { get; set; }
    public string State { get; set; }
    public GitHubEventUser Creator { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CpdatedAt { get; set; }
}
