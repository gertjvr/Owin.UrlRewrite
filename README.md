### Owin UrlRewrite [![Build Status](http://teamcity.gertjvr.com/app/rest/builds/buildType:(id:OSS_OwinUrlRewrite_CI)/statusIcon)](http://teamcity.gertjvr.com/viewType.html?buildTypeId=OSS_OwinUrlRewrite_CI&guest=1)

An OWIN middleware that provides a way to modify incoming URL requests, dynamically, based on regular expression rules. This allows you to map arbitrary URLs onto your internal URL structure in any way you like, based on apache [mod_rewrite](http://httpd.apache.org/docs/current/rewrite)

#### Using

See example project:

```
private class StartUp
{
    public void Configuration(IAppBuilder app)
    {
        app.UrlRewriter("!\\.\\w+$ /index.html [L]");

        app.UseStaticFiles();
    }
}
```

#### Flags
##### Last [L]
If a path matches, any subsequent rewrite rules will be disregarded.

##### Proxy \[P\] (Not Implemented Yet)
Proxy your requests
```'^/test/proxy/(.*)$ http://example.org/$1 [P]'```

##### Redirect \[R\], \[R=3**\] (replace * with numbers)
Issue a redirect for request.

##### Nocase \[NC\]
Regex match will be case-insensitive.

##### Forbidden \[F\]
Gives a HTTP 403 forbidden response.

##### Gone \[G\]
Gives a HTTP 410 gone response.

##### Type \[T=*\] (replace * with mime-type)
Sets content-type to the specified one.

##### Host \[H\], \[H=*\] (replace * with a regular expression that match a hostname)
Match on host.

For more info about available flags, please go to the Apache page: http://httpd.apache.org/docs/current/rewrite/flags.html

####<a id="continuous-integration-from-teamcity">Continuous Integration from TeamCity</a>
Owin.UrlRewrite project is built & tested continuously by TeamCity (more details [here](http://www.mehdi-khalili.com/continuous-integration-delivery-github-teamcity)). That applies to pull requests too. Shortly after you submit a PR you can check the build and test status notification on your PR. I would appreciate if you could send me green PRs.

####<a id="author">Author</a>
Gert Jansen van Rensburg ([@gertjvr81](http://twitter.com/gertjvr81))
