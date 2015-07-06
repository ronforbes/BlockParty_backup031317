using System;
using System.Diagnostics;
using System.Timers;
using System.Web.Http;
using System.Web.Routing;

namespace BPService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();

            GameClock.Instance.LogState();
        }
    }
}