namespace Terrajobst.GitHubEvents;

public static class GitHubEventKindExtensions
{
    private const GitHubEventKind ActionMask = (GitHubEventKind)(2 << 15 - 1);
    private const GitHubEventKind EventMask = ~ActionMask;

    public static GitHubEventKind GetEvent(this GitHubEventKind kind)
    {
        return kind & ActionMask;
    }

    public static GitHubEventKind GetAction(this GitHubEventKind kind)
    {
        return kind & EventMask;
    }

    public static bool IsEvent(this GitHubEventKind kind, GitHubEventKind @event)
    {
        return kind.GetEvent() == @event;
    }

    public static bool IsAction(this GitHubEventKind kind, GitHubEventKind action)
    {
        return kind.GetAction() == action;
    }
}
