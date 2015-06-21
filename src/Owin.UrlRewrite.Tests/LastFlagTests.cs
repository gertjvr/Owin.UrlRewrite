using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class LastFlagTests
    {
        [Fact]
        public async Task ShouldNotGoToNextRewriteRuleIfTheCurrentMatches()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [L]", "/a /c" }));

            var request = OwinHelper.CreateRequest("http://test.com/a");

            await middleware.Invoke(request.Context);

            request.Path.ShouldBe(new PathString("/b"));
        }

        [Fact]
        public async Task ShouldGoToTheNextRewriteRuleIfTheCurrentDoesNottMatches()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [L]", "/b /c" }));

            var request = OwinHelper.CreateRequest("http://test.com/b");

            await middleware.Invoke(request.Context);

            request.Path.ShouldBe(new PathString("/c"));
        }
    }
}