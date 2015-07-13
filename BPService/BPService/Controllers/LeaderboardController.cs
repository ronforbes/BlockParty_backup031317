using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using BPService.Models;
using BPService.DataObjects;

namespace BPService.Controllers
{
    public class LeaderboardController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/Leaderboard
        [AuthorizeLevel(Microsoft.WindowsAzure.Mobile.Service.Security.AuthorizationLevel.Anonymous)]
        public IEnumerable<GameResult> Get()
        {
            MobileServiceContext context = new MobileServiceContext();
            IEnumerable<GameResult> leaderboard = context.GameResults.Where(r => r.GameId == GameClock.Instance.CurrentGameId.ToString()).OrderByDescending(r => r.Score);

            return leaderboard;
        }

    }
}
