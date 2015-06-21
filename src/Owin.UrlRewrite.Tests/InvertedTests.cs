using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class InvertedTests
    {
        [Fact]
        public async Task ShouldRewriteIfThePatternDoesNotMatch()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "!/a /b [L]", "/b /c" }));

            var request = OwinHelper.CreateRequest("http://test.com/b");

            await middleware.Invoke(request.Context);

            request.Path.ShouldBe(new PathString("/b"));
        }

        [Fact]
        public async Task ShouldNotRewriteIfThePatternMatch()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "!/a /b [L]", "/b /c" }));

            var request = OwinHelper.CreateRequest("http://test.com/b");

            await middleware.Invoke(request.Context);

            request.Path.ShouldNotBe(new PathString("/c"));
        }
    }
}