namespace Terrajobst.GitHubEvents;

[Flags]
public enum GitHubEventMessageKind
{
    Unknown = 0,

    // Event (lower 4 bits)

    EventInstallation  = 1,
    EventRepository    = 2,
    EventLabel         = 3,
    EventMilestone     = 4,
    EventIssue         = 5,
    EventPullRequest   = 6,
    EventComment       = 7,   
    EventProject       = 8,
    EventProjectColumn = 9,
    EventProjectCard   = 10,

    // Actions

    ActionAdded            = 1 << 4,
    ActionRemoved          = 2 << 4,
    ActionTransferred      = 3 << 4,
    ActionCreated          = 4 << 4,
    ActionDeleted          = 5 << 4,
    ActionRenamed          = 6 << 4,
    ActionArchived         = 7 << 4,
    ActionUnarchived       = 8 << 4,
    ActionPublicized       = 9 << 4,
    ActionPrivatized       = 10 << 4,
    ActionEdited           = 11 << 4,
    ActionClosed           = 12 << 4,
    ActionOpened           = 13 << 4,
    ActionReopened         = 14 << 4,
    ActionAssigned         = 15 << 4,
    ActionUnassigned       = 16 << 4,
    ActionLabeled          = 17 << 4,
    ActionUnlabeled        = 18 << 4,
    ActionMilestoned       = 19 << 4,
    ActionDemilestoned     = 20 << 4,
    ActionLocked           = 21 << 4,
    ActionUnlocked         = 22 << 4,
    ActionReadyForReview   = 23 << 4,
    ActionConvertedToDraft = 24 << 4,
    ActionMoved            = 25 << 4,

    // Compound

    InstallationAdded = EventInstallation | ActionAdded,
    InstallationRemoved = EventInstallation | ActionRemoved,
    InstallationTransferred = EventInstallation | ActionTransferred,

    RepositoryCreated = EventRepository | ActionCreated,
    RepositoryDeleted = EventRepository | ActionDeleted,
    RepositoryRenamed = EventRepository | ActionRenamed,
    RepositoryArchived = EventRepository | ActionArchived,
    RepositoryUnarchived = EventRepository | ActionUnarchived,
    RepositoryPublicized = EventRepository | ActionPublicized,
    RepositoryPrivatized = EventRepository | ActionPrivatized,

    LabelCreated = EventLabel | ActionCreated,
    LabelEdited = EventLabel | ActionEdited,
    LabelDeleted = EventLabel | ActionDeleted,

    MilestoneCreated = EventMilestone | ActionCreated,
    MilestoneEdited = EventMilestone | ActionEdited,
    MilestoneClosed = EventMilestone | ActionClosed,
    MilestoneOpened = EventMilestone | ActionOpened,
    MilestoneDeleted = EventMilestone | ActionDeleted,

    IssueOpened = EventIssue | ActionOpened,
    IssueReopened = EventIssue | ActionReopened,
    IssueClosed = EventIssue | ActionClosed,
    IssueDeleted = EventIssue | ActionDeleted,
    IssueEdited = EventIssue | ActionEdited,
    IssueAssigned = EventIssue | ActionAssigned,
    IssueUnassigned = EventIssue | ActionUnassigned,
    IssueLabeled = EventIssue | ActionLabeled,
    IssueUnlabeled = EventIssue | ActionUnlabeled,
    IssueMilestoned = EventIssue | ActionMilestoned,
    IssueDemilestoned = EventIssue | ActionDemilestoned,
    IssueLocked = EventIssue | ActionLocked,
    IssueUnlocked = EventIssue | ActionUnlocked,
    IssueTransferred = EventIssue | ActionTransferred,

    PullRequestOpened = EventPullRequest | ActionOpened,
    PullRequestReopened = EventPullRequest | ActionReopened,
    PullRequestClosed = EventPullRequest | ActionClosed,
    PullRequestEdited = EventPullRequest | ActionEdited,
    PullRequestAssigned = EventPullRequest | ActionAssigned,
    PullRequestUnassigned = EventPullRequest | ActionUnassigned,
    PullRequestLabeled = EventPullRequest | ActionLabeled,
    PullRequestUnlabeled = EventPullRequest | ActionUnlabeled,
    PullRequestLocked = EventPullRequest | ActionLocked,
    PullRequestUnlocked = EventPullRequest | ActionUnlocked,
    PullRequestReadyForReview = EventPullRequest | ActionReadyForReview,
    PullRequestConvertedToDraft = EventPullRequest | ActionConvertedToDraft,

    CommentCreated = EventComment | ActionCreated,
    CommentEdited = EventComment | ActionEdited,
    CommentDeleted = EventComment | ActionDeleted,

    ProjectCreated = EventProject | ActionCreated,
    ProjectEdited = EventProject | ActionEdited,
    ProjectClosed = EventProject | ActionClosed,
    ProjectReopened = EventProject | ActionReopened,
    ProjectDeleted = EventProject | ActionDeleted,

    ProjectColumnCreated = EventProjectColumn | ActionCreated,
    ProjectColumnEdited = EventProjectColumn | ActionEdited,
    ProjectColumnDeleted = EventProjectColumn | ActionDeleted,
    
    ProjectCardCreated = EventProjectCard | ActionCreated,
    ProjectCardDeleted = EventProjectCard | ActionDeleted,
    ProjectCardMoved = EventProjectCard | ActionMoved,
}
