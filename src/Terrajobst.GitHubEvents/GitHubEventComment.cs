using System;

using Newtonsoft.Json.Linq;

namespace Terrajobst.GitHubEvents
{
    public sealed class GitHubEventComment
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string IssueUrl { get; set; }
        public int Id { get; set; }
        public string NodeId { get; set; }
        public GitHubEventUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AuthorAssociation { get; set; }
        public string Body { get; set; }
        public JObject PerformedViaGithubApp { get; set; }
    }
}
