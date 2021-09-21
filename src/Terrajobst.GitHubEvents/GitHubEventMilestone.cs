namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventMilestone
{
    public string Url { get; set; }
    public string HtmlUrl { get; set; }
    public string LabelsUrl { get; set; }
    public int Id { get; set; }
    public string NodeId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public GitHubEventUser Creator { get; set; }
    public int OpenIssues { get; set; }
    public int ClosedIssues { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DueOn { get; set; }
    public DateTime? ClosedAt { get; set; }
}
