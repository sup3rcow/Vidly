using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //novi naci definrianja custom rute, koristi ovo jer nemas zahardkodiranih naziva actiona i controllera
            //sada definiras route direktno na kontroleru
            routes.MapMvcAttributeRoutes();

            /*
            //definiranje custom route... url mora poceti sa ../movies/released
            routes.MapRoute(
                name: "MoviesByReleaseDate",
                url: "movies/released/{year}/{month}",
                defaults: new {controller = "Movies", action = "ByReleaseDate" },
                constraints: new { year = @"\d{4}", month = @"09|10"}//@ korstis zbog \, mozes i bez @ ali onda moras pisati \\,
                //   \d oznacava digit, {4} duljina broja,... @"09|10" kazes da mjesec moze biti 9 ili 10
                );

            */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
