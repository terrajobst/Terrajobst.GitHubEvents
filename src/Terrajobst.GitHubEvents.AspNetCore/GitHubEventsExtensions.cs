using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Terrajobst.GitHubEvents.AspNetCore;

public static class GitHubEventsExtensions
{
    public static void MapGitHubWebHook(this IEndpointRouteBuilder endpoints, string pattern = "/github-webhook", string? secret = null)
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        ArgumentNullException.ThrowIfNull(pattern);

        endpoints.MapPost(pattern, async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<IGitHubEventProcessor>>();

            // Verify content type

            if (!VerifyContentType(context, MediaTypeNames.Application.Json))
            {
                logger.LogError("GitHub event doesn't have correct content type.");
                return;
            }

            // Get body

            var body = await GetBodyAsync(context);

            // Verify signature

            if (!await VerifySignatureAsync(context, secret, body))
            {
                logger.LogError("GitHub event failed signature validation.");
                return;
            }

            // Process body

            try
            {
                var headers = context.Request.Headers.ToDictionary(kv => kv.Key, kv => kv.Value);
                var message = GitHubEvent.Parse(headers, body);

                var service = context.RequestServices.GetRequiredService<IGitHubEventProcessor>();
                service.Process(message);

                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing GitHub event");
                context.Response.StatusCode = 500;
            }
        });
    }

    private static bool VerifyContentType(HttpContext context, string expectedContentType)
    {
        if (context.Request.ContentType is null)
            return false;

        var contentType = new ContentType(context.Request.ContentType);
        if (contentType.MediaType != expectedContentType)
        {
            context.Response.StatusCode = 400;
            return false;
        }

        return true;
    }

    private static async Task<string> GetBodyAsync(HttpContext context)
    {
        string body;
        using (var reader = new StreamReader(context.Request.Body))
            body = await reader.ReadToEndAsync();
        return body;
    }

    private static async Task<bool> VerifySignatureAsync(HttpContext context, string? secret, string body)
    {
        context.Request.Headers.TryGetValue("X-Hub-Signature-256", out var signatureSha256);

        var isSigned = signatureSha256.Count > 0;
        var expectedSignature = !string.IsNullOrEmpty(secret);

        if (!isSigned && !expectedSignature)
        {
            // Nothing to do.
            return true;
        }
        else if (!isSigned && expectedSignature)
        {
            context.Response.StatusCode = 400;
            return false;
        }
        else if (isSigned && !expectedSignature)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Payload includes a secret, so the web hook receiver must configure a secret");
            return false;
        }
        else // if (isSigned && expectedSignature)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret!);
            var bodyBytes = Encoding.UTF8.GetBytes(body);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hash = hmac.ComputeHash(bodyBytes);
                var hashHex = Convert.ToHexString(hash);
                var expectedHeader = $"sha256={hashHex.ToLower()}";
                if (signatureSha256.ToString() != expectedHeader)
                {
                    context.Response.StatusCode = 400;
                    return false;
                }
            }

            return true;
        }
    }
}
