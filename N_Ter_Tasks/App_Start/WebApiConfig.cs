using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace N_Ter_Tasks
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var httpControllerRouteHandler = typeof(HttpControllerRouteHandler).GetField("_instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            if (httpControllerRouteHandler != null)
            {
                httpControllerRouteHandler.SetValue(null, new Lazy<HttpControllerRouteHandler>(() => new SessionHttpControllerRouteHandler(), true));
            }

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );
        }
    }

    public class SessionControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionControllerHandler(RouteData routeData) : base(routeData) { }
    }

    public class SessionHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SessionControllerHandler(requestContext.RouteData);
        }
    }
}
