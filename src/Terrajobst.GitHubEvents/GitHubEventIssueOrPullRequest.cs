using System;

namespace Terrajobst.GitHubEvents
{
    public abstract class GitHubEventIssueOrPullRequest
    {
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public GitHubEventLabel[] Labels { get; set; }
        public GitHubEventMilestone Milestone { get; set; }
        public GitHubEventUser Assignee { get; set; }
        public GitHubEventUser User { get; set; }
        public GitHubEventUser[] Assignees { get; set; }
        public int Comments { get; set; }
        public int Id { get; set; }
        public int Number { get; set; }
        public string ActiveLockReason { get; set; }
        public string AuthorAssociation { get; set; }
        public string Body { get; set; }
        public string CommentsUrl { get; set; }
        public string HtmlUrl { get; set; }
        public string NodeId { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
