using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terrajobst.GitHubEvents
{
    public sealed class GitHubEventChanges
    {
        public GitHubEventIssue NewIssue { get; set; }
        public GitHubEventRepository NewRepository { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; }
    }
}
