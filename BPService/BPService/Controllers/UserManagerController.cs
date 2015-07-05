using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

namespace BPService.Controllers
{
    public class UserManagerController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/UserManager
        [AuthorizeLevel(Microsoft.WindowsAzure.Mobile.Service.Security.AuthorizationLevel.Anonymous)]
        public bool Get()
        {
            Services.Log.Info("Received ping from client.");
            return true;
        }

    }
}
