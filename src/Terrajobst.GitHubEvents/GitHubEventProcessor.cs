using System;
using System.Collections.Generic;

using Microsoft.Extensions.Primitives;

namespace Terrajobst.GitHubEvents
{
    public abstract class GitHubEventProcessor
    {
        public virtual void Process(IDictionary<string, StringValues> headers, string body)
        {
            if (headers is null)
                throw new ArgumentNullException(nameof(headers));

            if (body is null)
                throw new ArgumentNullException(nameof(body));

            var message = GitHubEventMessage.Parse(headers, body);
            ProcessMessage(message);
        }

        public virtual void ProcessMessage(GitHubEventMessage message)
        {
            if (message is null)
                throw new ArgumentNullException(nameof(message));

            switch (message.Headers.Event)
            {
                case "installation_repositories":
                    ProcessRepoInstallationMessage(message, message.Body.Installation, message.Body.Sender);
                    break;
                case "repository":
                    ProcessRepoMessage(message, message.Body.Repository);
                    break;
                case "label":
                    ProcessLabelMessage(message, message.Body.Repository, message.Body.Label);
                    break;
                case "milestone":
                    ProcessMilestoneMessage(message, message.Body.Repository, message.Body.Milestone);
                    break;
                case "issues":
                    ProcessIssueMessage(message, message.Body.Repository, message.Body.Issue);
                    break;
                case "issue_comment":
                    ProcessIssueCommentMessage(message, message.Body.Repository, message.Body.Issue, message.Body.Comment);
                    break;
                case "pull_request":
                    ProcessPullRequestMessage(message, message.Body.Repository, message.Body.PullRequest);
                    break;
            }
        }

        // Repo installations

        private void ProcessRepoInstallationMessage(GitHubEventMessage message, GitHubEventInstallation installation, GitHubEventUser sender)
        {
            switch (message.Body.Action)
            {
                case "added":
                    ProcessRepoInstallationMessage(message, installation, sender, GitHubEventRepoInstallationAction.Added);
                    break;
                case "removed":
                    ProcessRepoInstallationMessage(message, installation, sender, GitHubEventRepoInstallationAction.Removed);
                    break;
                case "transferred":
                    ProcessRepoInstallationMessage(message, installation, sender, GitHubEventRepoInstallationAction.Transferred);
                    break;
            }
        }

        protected virtual void ProcessRepoInstallationMessage(GitHubEventMessage message, GitHubEventInstallation installation, GitHubEventUser sender, GitHubEventRepoInstallationAction action)
        {
        }

        // Repos

        private void ProcessRepoMessage(GitHubEventMessage message, GitHubEventRepository repository)
        {
            switch (message.Body.Action)
            {
                case "created":
                    ProcessRepoMessage(message, repository, GitHubEventRepoAction.Created);
                    break;
                case "deleted":
                    ProcessRepoMessage(message, repository, GitHubEventRepoAction.Deleted);
                    break;
                case "archived":
                    ProcessRepoMessage(message, repository, GitHubEventRepoAction.Archived);
                    break;
                case "unarchived":
                    ProcessRepoMessage(message, repository, GitHubEventRepoAction.Unarchived);
                    break;
                case "publicized":
                    ProcessRepoMessage(message, repository, GitHubEventRepoAction.Publicized);
                    break;
                case "privatized":
                    ProcessRepoMessage(message, repository, GitHubEventRepoAction.Privatized);
                    break;
            }
        }

        protected virtual void ProcessRepoMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventRepoAction action)
        {
        }

        // Labels

        private void ProcessLabelMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventLabel label)
        {
            switch (message.Body.Action)
            {
                case "created":
                    ProcessLabelMessage(message, repository, label, GitHubEventLabelAction.Created);
                    break;
                case "edited":
                    ProcessLabelMessage(message, repository, label, GitHubEventLabelAction.Edited);
                    break;
                case "deleted":
                    ProcessLabelMessage(message, repository, label, GitHubEventLabelAction.Deleted);
                    break;
            }
        }

        protected virtual void ProcessLabelMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventLabel label, GitHubEventLabelAction action)
        {
        }

        // Milestones

        private void ProcessMilestoneMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventMilestone milestone)
        {
            switch (message.Body.Action)
            {
                case "created":
                    ProcessMilestoneMessage(message, repository, milestone, GitHubEventMilestoneAction.Created);
                    break;
                case "edited":
                    ProcessMilestoneMessage(message, repository, milestone, GitHubEventMilestoneAction.Edited);
                    break;
                case "closed":
                    ProcessMilestoneMessage(message, repository, milestone, GitHubEventMilestoneAction.Closed);
                    break;
                case "opened":
                    ProcessMilestoneMessage(message, repository, milestone, GitHubEventMilestoneAction.Opened);
                    break;
                case "deleted":
                    ProcessMilestoneMessage(message, repository, milestone, GitHubEventMilestoneAction.Deleted);
                    break;
            }
        }

        protected virtual void ProcessMilestoneMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventMilestone milestone, GitHubEventMilestoneAction action)
        {
        }

        // Issues

        private void ProcessIssueMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventIssue issue)
        {
            switch (message.Body.Action)
            {
                case "opened":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Opened);
                    break;
                case "reopened":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Reopened);
                    break;
                case "closed":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Closed);
                    break;
                case "deleted":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Deleted);
                    break;
                case "edited":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Edited);
                    break;
                case "assigned":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Assigned);
                    break;
                case "unassigned":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Unassigned);
                    break;
                case "labeled":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Labeled);
                    break;
                case "unlabeled":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Unlabeled);
                    break;
                case "milestoned":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Milestoned);
                    break;
                case "demilestoned":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Demilestoned);
                    break;
                case "locked":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Locked);
                    break;
                case "unlocked":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Unlocked);
                    break;
                case "transferred":
                    ProcessIssueMessage(message, repository, issue, GitHubEventIssueAction.Transferred);
                    break;
            }
        }

        protected virtual void ProcessIssueMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventIssue issue, GitHubEventIssueAction action)
        {
        }

        // Issue Comments

        private void ProcessIssueCommentMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventIssue issue, GitHubEventComment comment)
        {
            switch (message.Body.Action)
            {
                case "created":
                    ProcessIssueCommentMessage(message, repository, issue, comment, GitHubEventCommentAction.Created);
                    break;
                case "edited":
                    ProcessIssueCommentMessage(message, repository, issue, comment, GitHubEventCommentAction.Edited);
                    break;
                case "deleted":
                    ProcessIssueCommentMessage(message, repository, issue, comment, GitHubEventCommentAction.Deleted);
                    break;
            }
        }

        protected virtual void ProcessIssueCommentMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventIssue issue, GitHubEventComment comment, GitHubEventCommentAction action)
        {
        }

        // Pull Requests

        private void ProcessPullRequestMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventPullRequest pullRequest)
        {
            switch (message.Body.Action)
            {
                case "opened":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Opened);
                    break;
                case "reopened":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Reopened);
                    break;
                case "closed":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Closed);
                    break;
                case "edited":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Edited);
                    break;
                case "assigned":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Assigned);
                    break;
                case "unassigned":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Unassigned);
                    break;
                case "labeled":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Labeled);
                    break;
                case "unlabeled":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Unlabeled);
                    break;
                // For some reason, GitHub doesn't seem to send milestoned/demilestoned events for PRs, they come in as issue events.
                case "locked":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Locked);
                    break;
                case "unlocked":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.Unlocked);
                    break;
                case "ready_for_review":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.ReadyForReview);
                    break;
                case "converted_to_draft":
                    ProcessPullRequestMessage(message, repository, pullRequest, GitHubEventPullRequestAction.ConvertedToDraft);
                    break;
            }
        }

        protected virtual void ProcessPullRequestMessage(GitHubEventMessage message, GitHubEventRepository repository, GitHubEventPullRequest pullRequest, GitHubEventPullRequestAction action)
        {
        }
    }
}
