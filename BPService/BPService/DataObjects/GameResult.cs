using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPService.DataObjects
{
    public class GameResult : EntityData
    {
        public string GameId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int Score { get; set; }
    }
}