namespace Terrajobst.GitHubEvents;

public static class GitHubEventMessageKindExtensions
{
    private const GitHubEventMessageKind ActionMask = (GitHubEventMessageKind)0b1111;
    private const GitHubEventMessageKind EventMask = ~ActionMask;

    public static GitHubEventMessageKind GetEvent(this GitHubEventMessageKind kind)
    {
        return kind & ActionMask;
    }

    public static GitHubEventMessageKind GetAction(this GitHubEventMessageKind kind)
    {
        return kind & EventMask;
    }

    public static bool IsEvent(this GitHubEventMessageKind kind, GitHubEventMessageKind @event)
    {
        return kind.GetEvent() == @event;
    }

    public static bool IsAction(this GitHubEventMessageKind kind, GitHubEventMessageKind action)
    {
        return kind.GetAction() == action;
    }
}
