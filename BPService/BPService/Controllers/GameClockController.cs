﻿using System;
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
        public GameClock Get()
        {
            Services.Log.Info("Received game clock request from client.");
            return GameClock.Instance;
        }

    }
}