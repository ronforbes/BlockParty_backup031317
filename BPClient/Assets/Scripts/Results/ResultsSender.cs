using UnityEngine;
using System.Collections;
using Bitrave.Azure;
using System.Text;
using System.Collections.Generic;

public class ResultsSender : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        // Create a game result to send to the service
        GameResult result = new GameResult
        {
            GameId = GameClock.Instance.CurrentGameId,
            UserId = UserManager.Instance.Id,
            UserName = UserManager.Instance.Name,
            Score = ScoreManager.Instance.Score
        };

        // Serialize the game result into JSON
        string serializedResult = result.ToJson();

        // Setup HTTP request headers
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        headers.Add("Content-Length", serializedResult.Length.ToString());
        headers.Add("X-ZUMO-APPLICATION", "tEtsHvgLHoRZKUATnELAkzCLWXARVl99");

        // Convert the serialized game result into a byte array
        byte[] body = Encoding.UTF8.GetBytes(serializedResult);

        //WWW httpRequest = new WWW("http://blockparty.azure-mobile.net/tables/GameResult", body, headers);
        WWW httpRequest = new WWW("http://localhost:49753/tables/GameResult", body, headers);
        StartCoroutine(OnHttpRequest(httpRequest));
    }

    IEnumerator OnHttpRequest(WWW httpRequest)
    {
        // Wait until the HTTP request has received a response
        yield return httpRequest;
    }
}
