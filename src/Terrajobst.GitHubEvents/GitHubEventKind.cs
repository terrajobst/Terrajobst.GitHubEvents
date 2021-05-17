namespace Terrajobst.GitHubEvents
{
    public enum GitHubEventKind
    {
        Unknown,

        RepositoryCreated,
        RepositoryDeleted,
        RepositoryRenamed,
        RepositoryPublicized,
        RepositoryPrivatized,
        RepositoryArchived,
        RepositoryUnarchived,

        LabelCreated,
        LabelEdited,
        LabelDeleted,

        MilestoneCreated,
        MilestoneEdited,
        MilestoneOpened,
        MilestoneClosed,
        MilestoneDeleted,

        IssueOpened,
        IssueClosed,
        IssueReopened,
        IssueEdited,
        IssueDeleted,
        IssueAssigned,
        IssueUnassigned,
        IssueLabeled,
        IssueUnlabeled,
        IssueMilestoned,
        IssueDemilestoned,
        IssueLocked,
        IssueUnlocked,
        IssueTransferred,

        PullRequestOpened,
        PullRequestClosed,
        PullRequestReopened,
        PullRequestEdited,
        PullRequestConvertedToDraft,
        PullRequestReadyForReview,
        PullRequestAssigned,
        PullRequestUnassigned,
        PullRequestLabeled,
        PullRequestUnlabeled,
        PullRequestLocked,
        PullRequestUnlocked,

        CommentCreated,
        CommentEdited,
        CommentDeleted,
    }
}
