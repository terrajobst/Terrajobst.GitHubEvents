using System;

using Newtonsoft.Json.Linq;

namespace Terrajobst.GitHubEvents
{
    public sealed class GitHubEventPullRequest : GitHubEventIssueOrPullRequest
    {
        public string DiffUrl { get; set; }
        public string PatchUrl { get; set; }
        public string IssueUrl { get; set; }
        public DateTime? MergedAt { get; set; }
        public string MergeCommitSha { get; set; }
        public JObject[] RequestedReviewers { get; set; }
        public JObject[] RequestedTeams { get; set; }
        public bool Draft { get; set; }
        public string CommitsUrl { get; set; }
        public string ReviewCommentsUrl { get; set; }
        public string ReviewCommentUrl { get; set; }
        public string StatusesUrl { get; set; }
        public GitHubEventRef Head { get; set; }
        public GitHubEventRef Base { get; set; }
        public JObject AutoMerge { get; set; }
        public bool Merged { get; set; }
        public bool? Mergeable { get; set; }
        public bool? Rebaseable { get; set; }
        public string MergeableState { get; set; }
        public GitHubEventUser MergedBy { get; set; }
        public int ReviewComments { get; set; }
        public bool MaintainerCanModify { get; set; }
        public int Commits { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int ChangedFiles { get; set; }
    }
}
