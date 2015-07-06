using System;
using System.Diagnostics;
using System.Timers;
using System.Web.Http;
using System.Web.Routing;

namespace BPService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        GameClock clock;

        protected void Application_Start()
        {
            WebApiConfig.Register();

            clock = new GameClock();
        }
    }
}