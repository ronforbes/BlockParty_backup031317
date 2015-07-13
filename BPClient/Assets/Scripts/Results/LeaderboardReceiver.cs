using UnityEngine;
using System.Collections;
using Bitrave.Azure;
using System.Collections.Generic;
using System;

public class LeaderboardReceiver : MonoBehaviour {
    bool requestedLeaderboard = false;
	
	// Update is called once per frame
	void Update () {
	    if(GameClock.Instance.TimeRemaining.Seconds <= 5 && !requestedLeaderboard)
        {
            WWW leaderboardHttpRequest = new WWW("http://blockparty.azure-mobile.net/api/Leaderboard");
            //WWW leaderboardHttpRequest = new WWW("http://localhost:49753/api/Leaderboard");
            StartCoroutine(OnLeaderboardHttpRequest(leaderboardHttpRequest));
            
            requestedLeaderboard = true;
        }
	}

    IEnumerator OnLeaderboardHttpRequest(WWW httpRequest)
    {
        // Wait until the HTTP request has received a response
        yield return httpRequest;

        // Deserialize the response into a list of objects
        List<object> list = Facebook.MiniJSON.Json.Deserialize(httpRequest.text) as List<object>;

        int rank = 1;

        foreach(object dict in list)
        {
            Dictionary<string, object> d = dict as Dictionary<string, object>;

            LeaderboardEntry entry = new LeaderboardEntry();

            entry.Rank = rank;
            entry.Name = d["userName"] as string;           
            
            try
            {
                entry.Score = int.Parse(d["score"].ToString());
            }
            catch(Exception e)
            {
                e = null;
                entry.Score = 0;
            }

            Leaderboard.Instance.LeaderboardEntries.Add(entry);

            rank++;
        }
    }
}
