namespace Terrajobst.GitHubEvents
{
    public sealed class GitHubEventOrganization
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string NodeId { get; set; }
        public string Url { get; set; }
        public string ReposUrl { get; set; }
        public string EventsUrl { get; set; }
        public string HooksUrl { get; set; }
        public string IssuesUrl { get; set; }
        public string MembersUrl { get; set; }
        public string PublicMembersUrl { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
    }
}
