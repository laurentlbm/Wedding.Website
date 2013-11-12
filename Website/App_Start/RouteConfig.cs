using System.Web.Mvc;
using System.Web.Routing;
using LowercaseRoutesMVC4;

namespace Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLowercase(
                name: "Invitation",
                url: "invitations/{id}",
                defaults: new { controller = "Invitations", action = "Index" },
                constraints: new { id = new GuidConstraint() }
            );

            routes.MapRouteLowercase(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}