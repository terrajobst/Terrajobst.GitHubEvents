using System;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Terrajobst.GitHubEvents.AspNetCore
{
    public static class GitHubEventsExtensions
    {
        public static void MapGitHubWebHook(this IEndpointRouteBuilder endpoints, string pattern = "/github-webhook", string secret = null)
        {
            endpoints.MapPost(pattern, async context =>
            {
                // Verify content type

                if (!VerifyContentType(context, MediaTypeNames.Application.Json))
                    return;

                // Get body

                var body = await GetBodyAsync(context);

                // Verify signature

                if (!await VerifySignatureAsync(context, secret, body))
                    return;

                // Process body

                try
                {
                    var service = context.RequestServices.GetRequiredService<GitHubEventProcessor>();
                    service.Process(context.Request.Headers, body);
                    context.Response.StatusCode = 200;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 500;
                }
            });
        }

        private static bool VerifyContentType(HttpContext context, string expectedContentType)
        {
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

        private static async Task<bool> VerifySignatureAsync(HttpContext context, string secret, string body)
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
                var keyBytes = Encoding.UTF8.GetBytes(secret);
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
}
