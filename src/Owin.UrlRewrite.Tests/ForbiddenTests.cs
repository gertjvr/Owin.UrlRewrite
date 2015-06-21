using System.Net;
using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class ForbiddenTests
    {
        [Fact]
        public async Task ShouldSetStatusCodeTo403()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a [F]" }));
            var request = OwinHelper.CreateRequest("http://test.com/a");

            var context = request.Context;
            await middleware.Invoke(context);

            context.Response.StatusCode.ShouldBe((int)HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task ShouldNotDoAnythingIfGoneFlagIsNotSet()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a [F]" }));

            var request = OwinHelper.CreateRequest("http://test.com/d");
            var context = request.Context;

            await middleware.Invoke(context);

            context.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        }
    }
}
