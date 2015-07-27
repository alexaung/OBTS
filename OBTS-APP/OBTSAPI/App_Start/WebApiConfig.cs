using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using OBTSAPI.Formatter;

namespace OBTSAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           config.Routes.MapHttpRoute(
           name: "CountriesApi",
           routeTemplate: "api/country/{Id}/{action}", defaults: new { Id = "Id", Action = "regions", Controller = "Countries" }
            );

           config.Routes.MapHttpRoute(
          name: "RegionApi",
          routeTemplate: "api/region/{Id}/{action}", defaults: new { Id = "Id", Action = "cities", Controller = "Regions" }
           );



           config.Routes.MapHttpRoute(
          name: "CountriesApiRoute1.1",
          routeTemplate: "api/{controller}/{action}/{Id}", defaults: new { Id = "Id", Action = "country", Controller = "Countries" }
           );

           config.Routes.MapHttpRoute(
          name: "RegionApiRoute1.1",
          routeTemplate: "api/{controller}/{action}/{Id}", defaults: new { Id = "Id", Action = "region", Controller = "Regions" }
           );

          //config.Routes.MapHttpRoute(
          // name: "CodeTableRoute",
          // routeTemplate: "api/{controller}/{action}/{value}"
          //  );

           //clear default formatters 
           config.Formatters.Clear();
           config.Formatters.Add(new JsonMediaTypeFormatter());

           //set formatters only json 
           var jsonFormatter = new JsonMediaTypeFormatter();
           //optional: set serializer settings here
           config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
    }
}
