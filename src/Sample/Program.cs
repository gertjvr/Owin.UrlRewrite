using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GertJvR.Owin.UrlRewrite;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var app = WebApp.Start("http://localhost:8080", StartUp))
            {
                Console.ReadKey();
            }

        }

        private static void StartUp(IAppBuilder app)
        {
            app.Use<LoggingMiddleware>();

            app.UrlRewriter("!\\.\\w+$ /index.html [L]");

            app.UseStaticFiles(new StaticFileOptions {
                FileSystem = new PhysicalFileSystem("app")
            });
        }
    }

    public class LoggingMiddleware : OwinMiddleware
    {
        public LoggingMiddleware(OwinMiddleware next) 
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            Console.WriteLine("[{0}] Begin Request: {1}", DateTimeOffset.UtcNow, context.Request.Path);
            await Next.Invoke(context);
            Console.WriteLine("[{0}] End Request: {1}", DateTimeOffset.UtcNow, context.Request.Path);
        }
    }
}
