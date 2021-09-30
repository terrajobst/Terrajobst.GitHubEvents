using System.Text;

using Microsoft.Extensions.Primitives;

namespace Terrajobst.GitHubEvents;

public sealed class GitHubEvent
{
    public static GitHubEvent FromGitHubActions()
    {
        var eventName = Environment.GetEnvironmentVariable("GITHUB_EVENT_NAME");
        var eventPath = Environment.GetEnvironmentVariable("GITHUB_EVENT_PATH");

        if (!string.IsNullOrEmpty(eventName) && !string.IsNullOrEmpty(eventPath))
        {
            var eventBodyJson = File.ReadAllText(eventPath);
            var eventBody = GitHubEventBody.Parse(eventBodyJson);
            var eventMessage = Parse(eventName, eventBodyJson);
            return eventMessage;
        }

        return null;
    }

    public GitHubEvent(string userAgent,
                       string delivery,
                       string @event,
                       string hookId,
                       string hookInstallationTargetId,
                       string hookInstallationTargetType,
                       IReadOnlyDictionary<string, StringValues> headers,
                       GitHubEventBody body,
                       string bodyJson)
    {
        Kind = GetKind(@event, body.Action);
        UserAgent = userAgent;
        Delivery = delivery;
        Event = @event;
        HookId = hookId;
        HookInstallationTargetId = hookInstallationTargetId;
        HookInstallationTargetType = hookInstallationTargetType;
        Headers = headers;
        Body = body;
        BodyJson = bodyJson;
    }

    public GitHubEventKind Kind { get; }

    public string UserAgent { get; }

    public string Delivery { get; }

    public string Event { get; }

    public string HookId { get; }

    public string HookInstallationTargetId { get; }

    public string HookInstallationTargetType { get; }

    public IReadOnlyDictionary<string, StringValues> Headers { get; }

    public GitHubEventBody Body { get; }

    public string BodyJson { get; }

    public static GitHubEvent Parse(string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        // TODO: This isn't robust for headers that span lines.

        var headers = new Dictionary<string, StringValues>();
        var body = string.Empty;

        var stringReader = new StringReader(message);

        while (stringReader.ReadLine() is string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                body = stringReader.ReadToEnd();
            }
            else
            {
                var colon = line.IndexOf(':');
                if (colon >= 0)
                {
                    var key = line.Substring(0, colon).Trim();
                    var value = line.Substring(colon + 1).Trim();
                    if (!headers.TryGetValue(key, out var values))
                    {
                        headers.Add(key, value);
                    }
                    else
                    {
                        headers[key] = new StringValues(values.Append(value).ToArray());
                    }
                }
            }
        }

        return Parse(headers, body);
    }

    public static GitHubEvent Parse(string @event, string body)
    {
        ArgumentNullException.ThrowIfNull(@event);
        ArgumentNullException.ThrowIfNull(body);

        var headers = new Dictionary<string, StringValues>()
        {
            {"X-GitHub-Event", @event }
        };

        return Parse(headers, body);
    }

    public static GitHubEvent Parse(IReadOnlyDictionary<string, StringValues> headers, string body)
    {
        ArgumentNullException.ThrowIfNull(headers);
        ArgumentNullException.ThrowIfNull(body);

        headers.TryGetValue("User-Agent", out var userAgent);
        headers.TryGetValue("X-GitHub-Delivery", out var delivery);
        headers.TryGetValue("X-GitHub-Event", out var @event);
        headers.TryGetValue("X-GitHub-Hook-ID", out var hookId);
        headers.TryGetValue("X-GitHub-Hook-Installation-Target-ID", out var hookInstallationTargetId);
        headers.TryGetValue("X-GitHub-Hook-Installation-Target-Type", out var hookInstallationTargetType);

        var parsedBody = GitHubEventBody.Parse(body);

        return new GitHubEvent(userAgent,
                               delivery,
                               @event,
                               hookId,
                               hookInstallationTargetId,
                               hookInstallationTargetType,
                               headers,
                               parsedBody,
                               body);
    }

    private static GitHubEventKind GetKind(string @event, string action)
    {
        return (@event, action) switch
        {
            ("push", _)               => GitHubEventKind.EventPush,
            ("workflow_dispatch", _)  => GitHubEventKind.EventWorkflowDispatch,
            ("schedule", _)           => GitHubEventKind.EventSchedule,

            ("installation_repositories", "added")       => GitHubEventKind.InstallationAdded,
            ("installation_repositories", "removed")     => GitHubEventKind.InstallationRemoved,
            ("installation_repositories", "transferred") => GitHubEventKind.InstallationTransferred,

            ("repository", "created")    => GitHubEventKind.RepositoryCreated,
            ("repository", "deleted")    => GitHubEventKind.RepositoryDeleted,
            ("repository", "renamed")    => GitHubEventKind.RepositoryRenamed,
            ("repository", "archived")   => GitHubEventKind.RepositoryArchived,
            ("repository", "unarchived") => GitHubEventKind.RepositoryUnarchived,
            ("repository", "publicized") => GitHubEventKind.RepositoryPublicized,
            ("repository", "privatized") => GitHubEventKind.RepositoryPrivatized,

            ("label", "created") => GitHubEventKind.LabelCreated,
            ("label", "edited")  => GitHubEventKind.LabelEdited,
            ("label", "deleted") => GitHubEventKind.LabelDeleted,

            ("milestone", "created") => GitHubEventKind.MilestoneCreated,
            ("milestone", "edited")  => GitHubEventKind.MilestoneEdited,
            ("milestone", "closed")  => GitHubEventKind.MilestoneClosed,
            ("milestone", "opened")  => GitHubEventKind.MilestoneOpened,
            ("milestone", "deleted") => GitHubEventKind.MilestoneDeleted,

            ("issues", "opened")       => GitHubEventKind.IssueOpened,
            ("issues", "reopened")     => GitHubEventKind.IssueReopened,
            ("issues", "closed")       => GitHubEventKind.IssueClosed,
            ("issues", "deleted")      => GitHubEventKind.IssueDeleted,
            ("issues", "edited")       => GitHubEventKind.IssueEdited,
            ("issues", "assigned")     => GitHubEventKind.IssueAssigned,
            ("issues", "unassigned")   => GitHubEventKind.IssueUnassigned,
            ("issues", "labeled")      => GitHubEventKind.IssueLabeled,
            ("issues", "unlabeled")    => GitHubEventKind.IssueUnlabeled,
            ("issues", "milestoned")   => GitHubEventKind.IssueMilestoned,
            ("issues", "demilestoned") => GitHubEventKind.IssueDemilestoned,
            ("issues", "locked")       => GitHubEventKind.IssueLocked,
            ("issues", "unlocked")     => GitHubEventKind.IssueUnlocked,
            ("issues", "transferred")  => GitHubEventKind.IssueTransferred,

            ("issue_comment", "created") => GitHubEventKind.CommentCreated,
            ("issue_comment", "edited")  => GitHubEventKind.CommentEdited,
            ("issue_comment", "deleted") => GitHubEventKind.CommentDeleted,

            ("pull_request", "opened")             => GitHubEventKind.PullRequestOpened,
            ("pull_request", "reopened")           => GitHubEventKind.PullRequestReopened,
            ("pull_request", "closed")             => GitHubEventKind.PullRequestClosed,
            ("pull_request", "edited")             => GitHubEventKind.PullRequestEdited,
            ("pull_request", "assigned")           => GitHubEventKind.PullRequestAssigned,
            ("pull_request", "unassigned")         => GitHubEventKind.PullRequestUnassigned,
            ("pull_request", "labeled")            => GitHubEventKind.PullRequestLabeled,
            ("pull_request", "unlabeled")          => GitHubEventKind.PullRequestUnlabeled,
            ("pull_request", "locked")             => GitHubEventKind.PullRequestLocked,
            ("pull_request", "unlocked")           => GitHubEventKind.PullRequestUnlocked,
            ("pull_request", "ready_for_review")   => GitHubEventKind.PullRequestReadyForReview,
            ("pull_request", "converted_to_draft") => GitHubEventKind.PullRequestConvertedToDraft,

            ("project", "created")  => GitHubEventKind.ProjectCreated,
            ("project", "edited")   => GitHubEventKind.ProjectEdited,
            ("project", "closed")   => GitHubEventKind.ProjectClosed,
            ("project", "reopened") => GitHubEventKind.ProjectReopened,
            ("project", "deleted")  => GitHubEventKind.ProjectDeleted,

            ("project_column", "created") => GitHubEventKind.ProjectColumnCreated,
            ("project_column", "edited")  => GitHubEventKind.ProjectColumnEdited,
            ("project_column", "deleted") => GitHubEventKind.ProjectColumnDeleted,

            ("project_card", "created") => GitHubEventKind.ProjectCardCreated,
            ("project_card", "deleted") => GitHubEventKind.ProjectCardDeleted,
            ("project_card", "moved")   => GitHubEventKind.ProjectCardMoved,

            _ => GitHubEventKind.Unknown,
        };
    }

    public string FormatMessage()
    {
        var sb = new StringBuilder();

        foreach (var (key, value) in Headers)
        {
            sb.Append(key);
            sb.Append(':');
            sb.Append(' ');
            sb.Append(value);
            sb.AppendLine();
        }

        sb.AppendLine();
        sb.Append(BodyJson);

        return sb.ToString();
    }

    public override string ToString()
    {
        return Kind != GitHubEventKind.Unknown
            ? $"{Kind}, {Body}, Delivery={Delivery}"
            : $"'{Event} {Body.Action}', {Body}, Delivery={Delivery}";
    }
}
