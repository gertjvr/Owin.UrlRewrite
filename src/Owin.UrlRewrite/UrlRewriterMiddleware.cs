using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace GertJvR.Owin.UrlRewrite
{
    internal static class Constants
    {
        public const string NoCaseSyntax = "NC";
        public const string LastSyntax = "L";
        public const string ProxySyntax = "P";
        public const string RedirectSyntax = "R=?(\\d+)?";
        public const string ForbiddenSyntax = "F";
        public const string GoneSyntax = "G";
        public const string TypeSyntax = "T=([\\w|\\/]+,?)";
        public const string HostSyntax = "H=([^,]+)";
        public const string FlagSyntax = "\\[(.*)\\]$";
        public const string PartsSyntax = "\\s+|\\t+";
        public const string HttpsSyntax = "^https";
        public const string QuerySyntax = "\\?(.*)";
    }

    internal class UrlRewriterMiddleware : OwinMiddleware
    {
        private readonly UrlRewriterOptions _options;

        public UrlRewriterMiddleware(OwinMiddleware next, UrlRewriterOptions options)
            : base(next)
        {
            _options = options;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var request = context.Request;

            var protocol = (request.IsSecure || request.Headers["x-forwarded-proto"] == "https") ? "https" : "http";
            var callNext = true;

            var rewriteRules = _options.RewriteRules
                .Select(rewriteRule => new RewriteRule(rewriteRule));

            rewriteRules.Any(rewriteRule => 
            {
                if (rewriteRule.HostFlag)
                {
                    var input = request.Headers["Host"];
                    if (!Regex.IsMatch(input, rewriteRule.Host))
                    {
                        return false;
                    }
                }

                string location;
                if (Regex.IsMatch(rewriteRule.Replace, "/\\:\\/\\//"))
                {
                    location = request.Path.Value.Replace(rewriteRule.RegExp, rewriteRule.Replace);
                }
                else
                {
                    location = protocol + "://" + request.Headers["Host"] + request.Path.Value.Replace(rewriteRule.RegExp, rewriteRule.Replace);
                }

                var match = rewriteRule.Regex.IsMatch(request.Path.Value);

                // If not match
                if (!match)
                {
                    if (rewriteRule.Inverted)
                    {
                        request.Path = new PathString(rewriteRule.Replace);
                        return rewriteRule.LastFlag;
                    }

                    return false;
                }

                // Type
                if (rewriteRule.ContentTypeFlag)
                {
                    context.Response.Headers["Content-Type"] = rewriteRule.ContentType;
                }

                // Gone
                if (rewriteRule.GoneFlag)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Gone;
                    callNext = false;
                    return true;
                }

                // Forbidden
                if (rewriteRule.ForbiddenFlag)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    callNext = false;
                    return true;
                }

                // ProxyFlag
                if (rewriteRule.ProxyFlag)
                {
                    callNext = false;
                    return true;
                }

                // Redirect
                if (rewriteRule.RedirectFlag)
                {
                    context.Response.Redirect(location);
                    context.Response.StatusCode = rewriteRule.RedirectStatusCode;
                    callNext = false;
                    return true;
                }

                // Rewrite
                if (!rewriteRule.Inverted)
                {
                    if (rewriteRule.Replace != "-")
                    {
                        request.Path = new PathString(request.Path.Value.Replace(rewriteRule.RegExp, rewriteRule.Replace));
                    }
                    return rewriteRule.LastFlag;
                }

                // Add to query object
                var queryValue = Regex.Match(request.Uri.PathAndQuery, Constants.QuerySyntax);
                if (queryValue.Success)
                {
                    request.QueryString = new QueryString(queryValue.Groups[1].Value);
                }

                return false;
            });

            if (callNext)
            {
                await Next.Invoke(context);
            }
        }
    }
}