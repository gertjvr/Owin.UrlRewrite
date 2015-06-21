using System.Net;
using System.Text.RegularExpressions;

namespace GertJvR.Owin.UrlRewrite
{
    internal class RewriteRule
    {
        public RewriteRule(string rewriteRule)
        {
            Raw = rewriteRule;

            var parts = rewriteRule.Replace(Constants.PartsSyntax, " ").Split(' ');
            var flags = string.Empty;

            if (Regex.IsMatch(rewriteRule, Constants.FlagSyntax))
            {
                flags = Regex.Match(rewriteRule, Constants.FlagSyntax).Groups[0].Value;
            }

            // Check inverted urls
            Inverted = parts[0].Substring(0, 1) == "!";
            if (Inverted)
            {
                parts[0] = parts[0].Substring(1);
            }

            LastFlag = Regex.IsMatch(flags, Constants.LastSyntax);
            ProxyFlag = Regex.IsMatch(flags, Constants.ProxySyntax);
            ForbiddenFlag = Regex.IsMatch(flags, Constants.ForbiddenSyntax);
            GoneFlag = Regex.IsMatch(flags, Constants.GoneSyntax);
            
            Regex = Regex.IsMatch(flags, Constants.NoCaseSyntax) 
                ? new Regex(parts[0], RegexOptions.IgnoreCase) 
                : new Regex(parts[0]);

            RegExp = parts[0];
            Replace = parts[1];

            var hostMatch = Regex.Match(flags, Constants.HostSyntax);
            if (hostMatch.Success)
            {
                HostFlag = true;
                Host = hostMatch.Groups[1].Value.Replace("]", string.Empty);
            }

            var redirectMatch = Regex.Match(flags, Constants.RedirectSyntax);
            if (redirectMatch.Success)
            {
                RedirectFlag = true;
                int statusCode;
                if (!int.TryParse(redirectMatch.Groups[1].Value, out statusCode))
                {
                    statusCode = (int) HttpStatusCode.Redirect;
                }
                RedirectStatusCode = statusCode;
            }

            var typeMatch = Regex.Match(flags, Constants.TypeSyntax);
            if (typeMatch.Success)
            {
                ContentTypeFlag = true;
                var contentType = typeMatch.Groups[1].Value;
                ContentType = string.IsNullOrEmpty(contentType) ? "text/plain" : contentType;
            }
        }

        public string Raw { get; protected set; }

        public bool Inverted { get; protected set; }
        
        public bool HostFlag { get; set; }
        public string Host { get; protected set; }
        
        public string Replace { get; protected set; }
        public string RegExp { get; protected set; }

        public bool ContentTypeFlag { get; protected set; }
        public string ContentType { get; protected set; }
        
        public bool LastFlag { get; protected set; }
        public bool GoneFlag { get; protected set; }
        public bool ForbiddenFlag { get; protected set; }
        public bool ProxyFlag { get; protected set; }
        
        public bool RedirectFlag { get; protected set; }
        public int RedirectStatusCode { get; protected set; }

        public Regex Regex { get; protected set; }
        
    }
}