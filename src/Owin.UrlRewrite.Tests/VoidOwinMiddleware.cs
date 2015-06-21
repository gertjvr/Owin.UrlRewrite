using System.Threading.Tasks;
using Microsoft.Owin;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public class VoidOwinMiddleware : OwinMiddleware
    {
        public VoidOwinMiddleware()
            : base(null)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
        }
    }
}