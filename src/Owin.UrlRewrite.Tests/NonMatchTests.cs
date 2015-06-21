using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class NonMatchTests
    {
        [Fact]
        public async Task ShouldLeaveTheUrlUnrewrittenIfThereIsNoMatch()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [L]", "/a /c" }));

            var request = OwinHelper.CreateRequest("http://test.com/d");

            await middleware.Invoke(request.Context);

            request.Path.ShouldBe(new PathString("/d"));
        }

        [Fact]
        public async Task ShouldKeepTheQueryParameters()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [L]", "/a /c" }));

            var request = OwinHelper.CreateRequest("http://test.com/d?foo=bar&q");

            await middleware.Invoke(request.Context);

            request.QueryString.ShouldBe(new QueryString("foo=bar&q"));
        }
    }
}