﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Web;

namespace BPService
{
    public class GameClock
    {
        static GameClock instance;
        public static GameClock Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameClock();
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

        public GameClock()
        {
            State = ClockState.Game;
            NextStateTime = DateTime.UtcNow + gameDuration;

            timer = new Timer(1000.0f / timerUpdatesPerSecond);
            timer.Elapsed += Update;
            timer.Start();
        }

        public void LogState()
        {
            Debug.WriteLine("State=" + State.ToString());
        }

        void Update(object sender, ElapsedEventArgs e)
        {
            switch (State)
            {
                case ClockState.Game:
                    if (DateTime.UtcNow >= NextStateTime)
                    {
                        State = ClockState.Lobby;
                        NextStateTime = DateTime.UtcNow + lobbyDuration;
                        Debug.WriteLine("Changing state to Lobby.");
                    }
                    break;

                case ClockState.Lobby:
                    if (DateTime.UtcNow >= NextStateTime)
                    {
                        State = ClockState.Game;
                        NextStateTime = DateTime.UtcNow + gameDuration;
                        Debug.WriteLine("Changing state to Game.");
                    }
                    break;
            }

        }
    }
}