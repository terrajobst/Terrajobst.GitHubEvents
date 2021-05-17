using System;
using System.Collections.Generic;

using Microsoft.Extensions.Primitives;

namespace Terrajobst.GitHubEvents
{
    public sealed class GitHubEventMessage
    {
        public GitHubEventHeaders Headers { get; set; }
        public GitHubEventBody Body { get; set; }

        public static GitHubEventMessage Parse(IDictionary<string, StringValues> headers, string body)
        {
            if (headers is null)
                throw new ArgumentNullException(nameof(headers));

            if (body is null)
                throw new ArgumentNullException(nameof(body));

            return new GitHubEventMessage
            {
                Headers = GitHubEventHeaders.Parse(headers),
                Body = GitHubEventBody.Parse(body)
            };
        }

        public override string ToString()
        {
            return $"{Headers?.Event}, {Body}";
        }
    }
}
