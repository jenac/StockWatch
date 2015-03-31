using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StockWatch.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.MapSignalR();
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.IgnoreRoute("fonts", "fonts/{*pathInfo}");
            config.Routes.IgnoreRoute("Scripts", "Scripts/{*pathInfo}");
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);


            // Configure Static Contents, 
            // To layout with nuget package layout
            //Html     --WebRoot, we will copy the gulp output file here.
            var htmlFileSystem = new PhysicalFileSystem(@".\Html");
            var htmlOptions = new FileServerOptions()
            {
                EnableDirectoryBrowsing = true,
                FileSystem = htmlFileSystem,
            };
            appBuilder.UseFileServer(htmlOptions);

            var scriptsFileSystem = new PhysicalFileSystem(@".\Scripts");
            var scriptsOptions = new FileServerOptions()
            {
                EnableDirectoryBrowsing = true,
                FileSystem = scriptsFileSystem,
                RequestPath = new PathString("/Scripts")
            };
            appBuilder.UseFileServer(scriptsOptions);
        }
    }
}
