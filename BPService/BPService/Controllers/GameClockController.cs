using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

namespace BPService.Controllers
{
    public class GameClockController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/GameClock
        [AuthorizeLevel(Microsoft.WindowsAzure.Mobile.Service.Security.AuthorizationLevel.Anonymous)]
        public GameClock Get()
        {
            return GameClock.Instance;
        }
    }
}
