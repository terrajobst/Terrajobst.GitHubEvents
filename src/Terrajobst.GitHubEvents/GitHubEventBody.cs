using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Terrajobst.GitHubEvents;

public sealed class GitHubEventBody
{
    public string Action { get; set; }
    public string After { get; set; }
    public string BaseRef { get; set; }
    public string Before { get; set; }
    public string Compare { get; set; }
    public bool Created { get; set; }
    public bool Deleted { get; set; }
    public bool Forced { get; set; }
    public string Ref { get; set; }
    public GitHubEventCommit[] Commits { get; set; }
    public GitHubEventCommit HeadCommit { get; set; }
    public GitHubEventAuthor Pusher { get; set; }
    public GitHubEventOrganization Organization { get; set; }
    public GitHubEventRepository Repository { get; set; }
    public GitHubEventIssue Issue { get; set; }
    public GitHubEventPullRequest PullRequest { get; set; }
    public GitHubEventComment Comment { get; set; }
    public GitHubEventProject Project { get; set; }
    public GitHubEventProjectColumn ProjectColumn { get; set; }
    public GitHubEventProjectCard ProjectCard { get; set; }
    public GitHubEventUser Assignee { get; set; }
    public GitHubEventMilestone Milestone { get; set; }
    public GitHubEventLabel Label { get; set; }
    public GitHubEventUser Sender { get; set; }
    public GitHubEventEnterprise Enterprise { get; set; }
    public GitHubEventInstallation Installation { get; set; }
    public GitHubEventChanges Changes { get; set; }

    public static GitHubEventBody Parse(string json)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Converters = {
                new GitHubDateTimeConverter()
            }
        };

        return JsonConvert.DeserializeObject<GitHubEventBody>(json, settings);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"Action={Action}");

        if (Organization is not null)
            sb.Append($", Org={Organization.Login}");

        if (Repository is not null)
            sb.Append($", Repo={Repository.Name}");

        if (Issue is not null)
            sb.Append($", Issue={Issue.Number}");

        if (PullRequest is not null)
            sb.Append($", PullRequest={PullRequest.Number}");

        if (Comment is not null)
            sb.Append($", Comment={Comment.Id}");

        if (Project is not null)
            sb.Append($", Project={Project.Name}");

        if (ProjectColumn is not null)
            sb.Append($", ProjectColumn={ProjectColumn.Name}");

        if (ProjectCard is not null)
            sb.Append($", ProjectCard={ProjectCard.ColumnId}");

        if (Assignee is not null)
            sb.Append($", Assignee={Assignee.Login}");

        if (Milestone is not null)
            sb.Append($", Milestone={Milestone.Title}");

        if (Label is not null)
            sb.Append($", Label={Label.Name}");

        if (Sender is not null)
            sb.Append($", Sender={Sender.Login}");

        if (Installation is not null)
            sb.Append($", Installation={Installation.Id}");

        if (Changes is not null)
        {
            var properties = new List<string>();

            if (Changes.NewIssue is not null)
                properties.Add("new_issue");

            if (Changes.NewRepository is not null)
                properties.Add("new_repository");

            if (Changes.AdditionalData is not null)
                properties.AddRange(Changes.AdditionalData.Keys);

            if (properties.Count > 0)
            {
                properties.Sort();
                var propertyList = string.Join(",", properties);
                sb.Append($", Changes={propertyList}");
            }
        }

        return sb.ToString();
    }
}
