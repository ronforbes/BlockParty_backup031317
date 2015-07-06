using System;
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
        Lobby
    }

    public ClockState State;
    public DateTime NextStateTime;

    TimeSpan gameDuration = TimeSpan.FromSeconds(10);
    TimeSpan lobbyDuration = TimeSpan.FromSeconds(10);

    Timer timer;
    const int timerUpdatesPerSecond = 1;

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
        State = ClockState.Default;
        NextStateTime = DateTime.UtcNow + gameDuration;

        timer = new Timer(1000.0f / timerUpdatesPerSecond);
        timer.Elapsed += TimerElapsed;
        timer.Start();
    }

    public void LogState()
    {
        Debug.Log("State=" + State.ToString());
    }

    void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        switch (State)
        {
            case ClockState.Game:
                if (DateTime.UtcNow >= NextStateTime)
                {
                    State = ClockState.Lobby;
                    NextStateTime = DateTime.UtcNow + lobbyDuration;
                    Debug.Log("Changing state to Lobby.");
                }
                break;

            case ClockState.Lobby:
                if (DateTime.UtcNow >= NextStateTime)
                {
                    State = ClockState.Game;
                    NextStateTime = DateTime.UtcNow + gameDuration;
                    Debug.Log("Changing state to Game.");
                }
                break;
        }
    }
}
