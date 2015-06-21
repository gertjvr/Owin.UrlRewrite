using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class RealExampleTests
    {
        [Fact]
        public async Task Monkey()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { @"!\.\w+$ /index.html [L]" }));

            var request = OwinHelper.CreateRequest("http://test.com/notifications.json");
            var context = request.Context;

            await middleware.Invoke(context);

            request.Path.ShouldBe(new PathString("/notifications.json"));

        }
    }
}