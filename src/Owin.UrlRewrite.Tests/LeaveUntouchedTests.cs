using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class LeaveUntouchedTests
    {
        [Fact]
        public async Task ShouldUseTheCurrentRuleIfTheHostMatch()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "^/a - [L]" }));

            var request = OwinHelper.CreateRequest("http://test.com/a/foo/bar/woot");
            
            var context = request.Context;
            await middleware.Invoke(context);

            request.Path.ShouldBe(new PathString("/a/foo/bar/woot"));
        }
    }
}