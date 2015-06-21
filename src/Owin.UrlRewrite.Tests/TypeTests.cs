using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class TypeTests
    {
        [Fact]
        public async Task ShouldSetContentTypeHeaderIfTypeFlagIsSet()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "!/ /b [T=image/jpeg]" }));

            var request = OwinHelper.CreateRequest("http://test.com/b");

            var context = request.Context;
            await middleware.Invoke(context);

            var header = context.Response.Headers["Content-Type"];
            header.ShouldBe("image/jpeg");
        }

        [Fact]
        public async Task ShouldNotDoAnythingTheTypeFlagIsNotSet()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [L]" }));

            var request = OwinHelper.CreateRequest("http://test.com/a");

            var context = request.Context;
            await middleware.Invoke(context);

            var header = context.Response.Headers["Content-Type"];
            header.ShouldBeNullOrEmpty();
        }
    }
}

