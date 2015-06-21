namespace GertJvR.Owin.UrlRewrite
{
    public class UrlRewriterOptions
    {
        public UrlRewriterOptions()
            : this(new string[0])
        {
        }

        public UrlRewriterOptions(string[] rewriteRules)
        {
            RewriteRules = rewriteRules;
        }

        public string[] RewriteRules { get; private set; }
    }
}