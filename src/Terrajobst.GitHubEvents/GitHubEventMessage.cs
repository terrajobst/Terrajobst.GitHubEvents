using System.Text;

using Microsoft.Extensions.Primitives;

namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventMessage
{
    public static GitHubEventMessage FromGitHubActions()
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

    public GitHubEventMessage(string userAgent,
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

    public GitHubEventMessageKind Kind { get; }

    public string UserAgent { get; }

    public string Delivery { get; }

    public string Event { get; }

    public string HookId { get; }

    public string HookInstallationTargetId { get; }

    public string HookInstallationTargetType { get; }

    public IReadOnlyDictionary<string, StringValues> Headers { get; }

    public GitHubEventBody Body { get; }

    public string BodyJson { get; }

    public static GitHubEventMessage Parse(string message)
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

    public static GitHubEventMessage Parse(string @event, string body)
    {
        ArgumentNullException.ThrowIfNull(@event);
        ArgumentNullException.ThrowIfNull(body);

        var headers = new Dictionary<string, StringValues>()
        {
            {"X-GitHub-Event", @event }
        };

        return Parse(headers, body);
    }

    public static GitHubEventMessage Parse(IReadOnlyDictionary<string, StringValues> headers, string body)
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

        return new GitHubEventMessage(userAgent,
                                      delivery,
                                      @event,
                                      hookId,
                                      hookInstallationTargetId,
                                      hookInstallationTargetType,
                                      headers,
                                      parsedBody,
                                      body);
    }

    private static GitHubEventMessageKind GetKind(string @event, string action)
    {
        // TODO: Handle project events

        return (@event, action) switch
        {
            ("installation_repositories", "added")       => GitHubEventMessageKind.InstallationAdded,
            ("installation_repositories", "removed")     => GitHubEventMessageKind.InstallationRemoved,
            ("installation_repositories", "transferred") => GitHubEventMessageKind.InstallationTransferred,

            ("repository", "created")    => GitHubEventMessageKind.RepositoryCreated,
            ("repository", "deleted")    => GitHubEventMessageKind.RepositoryDeleted,
            ("repository", "renamed")    => GitHubEventMessageKind.RepositoryRenamed,
            ("repository", "archived")   => GitHubEventMessageKind.RepositoryArchived,
            ("repository", "unarchived") => GitHubEventMessageKind.RepositoryUnarchived,
            ("repository", "publicized") => GitHubEventMessageKind.RepositoryPublicized,
            ("repository", "privatized") => GitHubEventMessageKind.RepositoryPrivatized,

            ("label", "created") => GitHubEventMessageKind.LabelCreated,
            ("label", "edited")  => GitHubEventMessageKind.LabelEdited,
            ("label", "deleted") => GitHubEventMessageKind.LabelDeleted,

            ("milestone", "created") => GitHubEventMessageKind.MilestoneCreated,
            ("milestone", "edited")  => GitHubEventMessageKind.MilestoneEdited,
            ("milestone", "closed")  => GitHubEventMessageKind.MilestoneClosed,
            ("milestone", "opened")  => GitHubEventMessageKind.MilestoneOpened,
            ("milestone", "deleted") => GitHubEventMessageKind.MilestoneDeleted,

            ("issues", "opened")       => GitHubEventMessageKind.IssueOpened,
            ("issues", "reopened")     => GitHubEventMessageKind.IssueReopened,
            ("issues", "closed")       => GitHubEventMessageKind.IssueClosed,
            ("issues", "deleted")      => GitHubEventMessageKind.IssueDeleted,
            ("issues", "edited")       => GitHubEventMessageKind.IssueEdited,
            ("issues", "assigned")     => GitHubEventMessageKind.IssueAssigned,
            ("issues", "unassigned")   => GitHubEventMessageKind.IssueUnassigned,
            ("issues", "labeled")      => GitHubEventMessageKind.IssueLabeled,
            ("issues", "unlabeled")    => GitHubEventMessageKind.IssueUnlabeled,
            ("issues", "milestoned")   => GitHubEventMessageKind.IssueMilestoned,
            ("issues", "demilestoned") => GitHubEventMessageKind.IssueDemilestoned,
            ("issues", "locked")       => GitHubEventMessageKind.IssueLocked,
            ("issues", "unlocked")     => GitHubEventMessageKind.IssueUnlocked,
            ("issues", "transferred")  => GitHubEventMessageKind.IssueTransferred,

            ("issue_comment", "created") => GitHubEventMessageKind.CommentCreated,
            ("issue_comment", "edited")  => GitHubEventMessageKind.CommentEdited,
            ("issue_comment", "deleted") => GitHubEventMessageKind.CommentDeleted,

            ("pull_request", "opened")             => GitHubEventMessageKind.PullRequestOpened,
            ("pull_request", "reopened")           => GitHubEventMessageKind.PullRequestReopened,
            ("pull_request", "closed")             => GitHubEventMessageKind.PullRequestClosed,
            ("pull_request", "edited")             => GitHubEventMessageKind.PullRequestEdited,
            ("pull_request", "assigned")           => GitHubEventMessageKind.PullRequestAssigned,
            ("pull_request", "unassigned")         => GitHubEventMessageKind.PullRequestUnassigned,
            ("pull_request", "labeled")            => GitHubEventMessageKind.PullRequestLabeled,
            ("pull_request", "unlabeled")          => GitHubEventMessageKind.PullRequestUnlabeled,
            ("pull_request", "locked")             => GitHubEventMessageKind.PullRequestLocked,
            ("pull_request", "unlocked")           => GitHubEventMessageKind.PullRequestUnlocked,
            ("pull_request", "ready_for_review")   => GitHubEventMessageKind.PullRequestReadyForReview,
            ("pull_request", "converted_to_draft") => GitHubEventMessageKind.PullRequestConvertedToDraft,

            ("project", "created")  => GitHubEventMessageKind.ProjectCreated,
            ("project", "edited")   => GitHubEventMessageKind.ProjectEdited,
            ("project", "closed")   => GitHubEventMessageKind.ProjectClosed,
            ("project", "reopened") => GitHubEventMessageKind.ProjectReopened,
            ("project", "deleted")  => GitHubEventMessageKind.ProjectDeleted,

            ("project_column", "created") => GitHubEventMessageKind.ProjectColumnCreated,
            ("project_column", "edited")  => GitHubEventMessageKind.ProjectColumnEdited,
            ("project_column", "deleted") => GitHubEventMessageKind.ProjectColumnDeleted,

            ("project_card", "created") => GitHubEventMessageKind.ProjectCardCreated,
            ("project_card", "deleted") => GitHubEventMessageKind.ProjectCardDeleted,
            ("project_card", "moved")   => GitHubEventMessageKind.ProjectCardMoved,

            _ => GitHubEventMessageKind.Unknown,
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
        return Kind != GitHubEventMessageKind.Unknown
            ? $"{Kind}, {Body}, Delivery={Delivery}"
            : $"'{Event} {Body.Action}', {Body}, Delivery={Delivery}";
    }
}
