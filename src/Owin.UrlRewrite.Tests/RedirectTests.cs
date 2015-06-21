using System.Net;
using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class RedirectTests
    {
        [Fact]
        public async Task ShouldSetDefaultStatusCodeTo301IfRewriteFlagIsSet()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [R]" }));

            var request = OwinHelper.CreateRequest("http://test.com/a");
            var context = request.Context;

            await middleware.Invoke(context);

            context.Response.StatusCode.ShouldBe((int)HttpStatusCode.Redirect);
            //context.Response.Received(1).Redirect(Arg.Is("http://test.com/b"));
        }

        [Fact]
        public async Task ShouldBeAbleToSetARewriteWithHostnameAndProtocol()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/(.*) http://localhost/$1 [R]" }));

            var request = OwinHelper.CreateRequest("http://localhost/a");
            var context = request.Context;

            await middleware.Invoke(context);

            context.Response.StatusCode.ShouldBe((int)HttpStatusCode.Redirect);
            //context.Response.Received(1).Redirect(Arg.Is("http://localhost/b"));
        }

        [Fact]
        public async Task ShouldSetCustomStatusCodeIfRewriteCustomFlagIsSet()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [R=307]" }));

            var request = OwinHelper.CreateRequest("http://test.com/a");
            var context = request.Context;

            await middleware.Invoke(context);

            context.Response.StatusCode.ShouldBe(307);
        }
    }
}