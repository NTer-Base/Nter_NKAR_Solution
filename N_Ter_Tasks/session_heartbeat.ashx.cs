using System;
using System.Web;

namespace N_Ter_Tasks
{
    /// <summary>
    /// Summary description for session_heartbeat
    /// </summary>
    public class session_heartbeat : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            context.Response.Cache.SetNoStore();
            context.Response.Cache.SetNoServerCaching();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}