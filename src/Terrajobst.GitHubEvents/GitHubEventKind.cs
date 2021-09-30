namespace Terrajobst.GitHubEvents;

[Flags]
public enum GitHubEventKind
{
    Unknown = 0,

    // Event (lower 16 bits)

    EventPush             =  1,
    EventWorkflowDispatch =  2,
    EventSchedule         =  3,
    EventInstallation     =  4,
    EventRepository       =  5,
    EventLabel            =  6,
    EventMilestone        =  7,
    EventIssue            =  8,
    EventPullRequest      =  9,
    EventComment          = 10,
    EventProject          = 11,
    EventProjectColumn    = 12,
    EventProjectCard      = 13,

    // Actions (upper 16 bits)

    ActionAdded            = 1 << 16,
    ActionRemoved          = 2 << 16,
    ActionTransferred      = 3 << 16,
    ActionCreated          = 4 << 16,
    ActionDeleted          = 5 << 16,
    ActionRenamed          = 6 << 16,
    ActionArchived         = 7 << 16,
    ActionUnarchived       = 8 << 16,
    ActionPublicized       = 9 << 16,
    ActionPrivatized       = 10 << 16,
    ActionEdited           = 11 << 16,
    ActionClosed           = 12 << 16,
    ActionOpened           = 13 << 16,
    ActionReopened         = 14 << 16,
    ActionAssigned         = 15 << 16,
    ActionUnassigned       = 16 << 16,
    ActionLabeled          = 17 << 16,
    ActionUnlabeled        = 18 << 16,
    ActionMilestoned       = 19 << 16,
    ActionDemilestoned     = 20 << 16,
    ActionLocked           = 21 << 16,
    ActionUnlocked         = 22 << 16,
    ActionReadyForReview   = 23 << 16,
    ActionConvertedToDraft = 24 << 16,
    ActionMoved            = 25 << 16,

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
