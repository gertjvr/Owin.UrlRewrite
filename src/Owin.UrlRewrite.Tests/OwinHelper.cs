using System;
using Microsoft.Owin;

namespace GertJvr.Owin.UrlRewrite.Tests
{
    public static class OwinHelper
    {
        public static IOwinRequest CreateRequest(string url)
        {
            var uriBuilder = new UriBuilder(url);
            var request = new OwinRequest();
            request.Host = HostString.FromUriComponent(uriBuilder.Uri);
            request.Path = PathString.FromUriComponent(uriBuilder.Uri);
            request.PathBase = PathString.Empty;
            request.Scheme = uriBuilder.Scheme;
            request.QueryString = QueryString.FromUriComponent(uriBuilder.Uri);

            return request;
        }
    }
}