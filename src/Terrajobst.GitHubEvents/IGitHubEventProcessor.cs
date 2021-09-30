namespace Terrajobst.GitHubEvents;

public interface IGitHubEventProcessor
{
    void Process(GitHubEvent message);
}
