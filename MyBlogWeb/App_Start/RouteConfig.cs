using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBlogWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            //routes.MapRoute(
            //    name: "Timeline",
            //    url: "timeline",
            //    defaults: new { controller = "Timeline", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "MyBlog.Controllers" }
            //);
            //routes.MapRoute(
            //    name: "About",
            //    url: "about",
            //    defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "MyBlog.Controllers" }
            //);
            //routes.MapRoute(
            //    name: "Contact",
            //    url: "contact",
            //    defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "MyBlog.Controllers" }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlogWeb.Controllers" }
            );
        }
    }
}
