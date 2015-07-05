using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Web;

namespace BPService
{
    public class GameClock
    {
        enum ClockState
        {
            Game,
            Lobby
        }

        ClockState clockState = ClockState.Game;

        TimeSpan gameLength = TimeSpan.FromSeconds(10);
        DateTime gameDuration;

        TimeSpan lobbyLength = TimeSpan.FromSeconds(10);
        DateTime lobbyDuration;

        Timer timer;
        const int timerUpdatesPerSecond = 1;

        public GameClock()
        {
            gameDuration = DateTime.UtcNow + gameLength;

            timer = new Timer(1000.0f / timerUpdatesPerSecond);
            timer.Elapsed += Update;
            timer.Start();
        }

        void Update(object sender, ElapsedEventArgs e)
        {
            switch (clockState)
            {
                case ClockState.Game:
                    if (DateTime.UtcNow >= gameDuration)
                    {
                        clockState = ClockState.Lobby;
                        lobbyDuration = DateTime.UtcNow + lobbyLength;
                        Debug.WriteLine("Changing state to Lobby.");
                    }
                    break;

                case ClockState.Lobby:
                    if (DateTime.UtcNow >= lobbyDuration)
                    {
                        clockState = ClockState.Game;
                        gameDuration = DateTime.UtcNow + gameLength;
                        Debug.WriteLine("Changing state to Game.");
                    }
                    break;
            }

        }
    }
}