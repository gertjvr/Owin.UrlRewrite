using Owin;

namespace GertJvR.Owin.UrlRewrite
{
    public static class UrlRewriterExtensions
    {
        public static IAppBuilder UrlRewriter(this IAppBuilder builder, UrlRewriterOptions options)
        {
            return builder.Use(typeof(UrlRewriterMiddleware), options);
        }

        public static IAppBuilder UrlRewriter(this IAppBuilder builder, string[] rules)
        {
            return builder.Use(typeof(UrlRewriterMiddleware), new UrlRewriterOptions(rules));
        }

        public static IAppBuilder UrlRewriter(this IAppBuilder builder, string rule)
        {
            return builder.Use(typeof(UrlRewriterMiddleware), new UrlRewriterOptions(new[] { rule }));
        }
    }
}
