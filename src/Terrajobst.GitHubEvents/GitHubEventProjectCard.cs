namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventProjectCard
{
    public string Url { get; set; }
    public string ProjectUrl { get; set; }
    public string ColumnUrl { get; set; }
    public int ColumnId { get; set; }
    public int Id { get; set; }
    public string NodeId { get; set; }
    public object Note { get; set; }
    public bool Archived { get; set; }
    public GitHubEventUser Creator { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ContentUrl { get; set; }
}
