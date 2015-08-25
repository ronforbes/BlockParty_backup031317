using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    static GameClock instance;
    public static GameClock Instance
    {
        get
        {
            // Get the singleton instance of the Game Clock
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameClock>();

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    public enum ClockState
    {
        Default,
        Game,
        Results,
        Lobby
    }

    public ClockState State;
    public DateTime NextStateTime;
    public TimeSpan TimeRemaining;
    public string CurrentGameId;
    public string NextGameId;

    TimeSpan gameDuration = TimeSpan.FromSeconds(120);
    TimeSpan resultsDuration = TimeSpan.FromSeconds(15);
    TimeSpan lobbyDuration = TimeSpan.FromSeconds(15);

    bool syncedGameId;

    void Awake()
    {
        // If this is the first instance, make it the singleton. If a singleton already exists and another reference is found, destroy it
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }

    void Start()
    { 
        // Set the default state until the correct state is received from server Game Clock
        State = ClockState.Default;

        // Sync to the global Game Clock
        //WWW gameClockHttpRequest = new WWW("http://localhost:49753/api/GameClock");
        WWW gameClockHttpRequest = new WWW("http://blockparty.azure-mobile.net/api/GameClock");
        StartCoroutine(OnGameClockHttpRequest(gameClockHttpRequest));
    }

    IEnumerator OnGameClockHttpRequest(WWW httpRequest)
    {
        // Wait until the HTTP request has received a response
        yield return httpRequest;

        // Deserialize and set the Game Clock state
        Dictionary<string, object> clock = Facebook.MiniJSON.Json.Deserialize(httpRequest.text) as Dictionary<string, object>;
        if(clock != null && clock.ContainsKey("state"))
		{
			Debug.Log(clock["state"]);
			State = (GameClock.ClockState)Enum.Parse(typeof(GameClock.ClockState), clock["state"] as string, true);
		}

		if (clock != null && clock.ContainsKey ("nextStateTime")) {
			NextStateTime = DateTime.Parse (clock ["nextStateTime"] as string);
		}

		if (clock != null && clock.ContainsKey ("currentGameId")) {
			CurrentGameId = clock ["currentGameId"] as string;
		}
    }

    void Update()
    {
        TimeRemaining = NextStateTime - DateTime.UtcNow;

        // Update state based on the current one
        switch (State)
        {
            case ClockState.Game:
                if (DateTime.UtcNow >= NextStateTime)
                {
                    State = ClockState.Results;
                    NextStateTime = DateTime.UtcNow + resultsDuration;
                    
					if(Leaderboard.Instance != null)
					{
						Leaderboard.Instance.Reset();
					}

                    if (Application.loadedLevelName == "Game")
                    {
                        Application.LoadLevel("Results");
                    }
                }
                break;

            case ClockState.Results:
                if(DateTime.UtcNow >= NextStateTime)
                {
                    State = ClockState.Lobby;
                    NextStateTime = DateTime.UtcNow + lobbyDuration;                    

                    if(Application.loadedLevelName == "Results")
                    {
                        Application.LoadLevel("Lobby");
                    }
                }
                break;

            case ClockState.Lobby:
                if(DateTime.UtcNow >= NextStateTime - TimeSpan.FromSeconds(5))
                {
                    // Get the next Game ID
                    if (!syncedGameId)
                    {
                        syncedGameId = true;
                        //WWW nextGameIdHttpRequest = new WWW("http://localhost:49753/api/GameClock");
                        WWW nextGameIdHttpRequest = new WWW("http://blockparty.azure-mobile.net/api/GameClock");
                        StartCoroutine(OnNextGameIdHttpRequest(nextGameIdHttpRequest));
                    }
                }

                if (DateTime.UtcNow >= NextStateTime)
                {
                    State = ClockState.Game;
                    NextStateTime = DateTime.UtcNow + gameDuration;
                    CurrentGameId = NextGameId;
                    syncedGameId = false;

                    if (Application.loadedLevelName == "Lobby")
                    {
                        ScoreManager.Instance.Reset(); 

                        Application.LoadLevel("Game");
                    }
                }
                break;
        }
    }

    IEnumerator OnNextGameIdHttpRequest(WWW httpRequest)
    {
        // Wait until the HTTP request has received a response
        yield return httpRequest;

        // Deserialize and set the Game Clock state
        Dictionary<string, object> clock = Facebook.MiniJSON.Json.Deserialize(httpRequest.text) as Dictionary<string, object>;
        NextGameId = clock["nextGameId"] as string;
    }
}
