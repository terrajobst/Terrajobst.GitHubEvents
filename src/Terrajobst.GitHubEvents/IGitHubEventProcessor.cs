namespace Terrajobst.GitHubEvents;

public interface IGitHubEventProcessor
{
    void Process(GitHubEventMessage message);
}
