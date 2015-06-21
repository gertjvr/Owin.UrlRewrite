using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Shouldly;
using Xunit;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class QueryParamsTests
    {
        [Fact]
        public async Task ShouldKeepNestedQueryParameters()
        {
            var middleware = new UrlRewriterMiddleware(new VoidOwinMiddleware(), new UrlRewriterOptions(new[] { "/a /b [L]", "/a /c" }));

            var request = OwinHelper.CreateRequest("http://test.com/d?foo[0]=bar&foo[1]=baz&q");

            await middleware.Invoke(request.Context);

            request.QueryString.ShouldBe(new QueryString("foo[0]=bar&foo[1]=baz&q"));
        }
    }
}