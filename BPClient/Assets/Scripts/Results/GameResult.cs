using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GameResult
{
    public Guid? Id { get; set; }

    [JsonProperty(PropertyName = "gameId")]
    public string GameId { get; set; }

    [JsonProperty(PropertyName = "userId")]
    public string UserId { get; set; }

    [JsonProperty(PropertyName = "userName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "score")]
    public int Score { get; set; }

    public string ToJson()
    {
        string json = "{ \"gameId\": \"" + GameId + "\", \"userId\": \"" + UserId + "\", \"userName\": \"" + UserName + "\", \"score\": " + Score + " }";
        return json;
    }
}
