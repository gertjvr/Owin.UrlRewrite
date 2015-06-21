using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class HostnameTests
    {
        [Fact]
        public async Task ShouldUseTheCurrentRuleIfTheHostMatch()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [H=(.+)\\.webview\\..*]" }));

            var request = OwinHelper.CreateRequest("http://ios.webview.test.com/a");
            var context = request.Context;

            await middleware.Invoke(context);

            request.Path.ShouldBe(new PathString("/b"));
        }

        [Fact]
        public async Task ShouldBeAbleToParseWithOtherFlags()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [H=(.+)\\.webview\\..*, L]" }));

            var request = OwinHelper.CreateRequest("http://ios.webview.test.com/a");
            var context = request.Context;

            await middleware.Invoke(context);

            request.Path.ShouldBe(new PathString("/b"));
        }
    }
}