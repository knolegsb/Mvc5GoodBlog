using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc5GoodBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // GET: Login
            routes.MapRoute(
                name: "Login",
                url: "Account/{Action}",
                defaults: new { controller = "Account", action = "Login" },
                namespaces: new String[] { "Mvc5GoodBlog.Controllers" }
            );

            // GET: /{slug} Single
            routes.MapRoute(
                name: "Slug",
                url: "{slug}",
                defaults: new { controller = "Post", action = "Details" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") },
                namespaces: new String[] { "Mvc5GoodBlog.Controllers" }
            );

            routes.MapRoute(
                name: "Default_",
                url: "{controller}/{id}",
                defaults: new { controller = "Post", action = "Index" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") },
                namespaces: new String[] { "Mvc5GoodBlog.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional },
                namespaces: new String[] { "Mvc5GoodBlog.Controllers" }
            );
        }
    }
}
