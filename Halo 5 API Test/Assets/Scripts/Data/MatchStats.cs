using System;
using System.Collections.Generic;
using Assets.Scripts.Data.Api;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class MatchStats
    {
        public MatchResult matchResult;

        public List<GameEvent> GameEvents;
        public bool IsCompleteSetoOfEvents;

        public List<GameEvent> playerKills;
        public List<GameEvent> playerDeaths;
        public List<GameEvent> playerAssists;

        public void FindPlayerEvents()
        {
            playerKills = new List<GameEvent>();
            playerDeaths = new List<GameEvent>();
            playerAssists = new List<GameEvent>();
            for (int i = 0; i < GameEvents.Count; i++)
            {
                if (GameEvents[i].Killer != null && GameEvents[i].Killer.Gamertag == "RandomComm3nt")
                    playerKills.Add(GameEvents[i]);
                if (GameEvents[i].Victim != null && GameEvents[i].Victim.Gamertag == "RandomComm3nt")
                    playerDeaths.Add(GameEvents[i]);
                for (int j = 0; j < GameEvents[i].Assistants.Count; j++)
                {
                    if (GameEvents[i].Assistants[j] != null && GameEvents[i].Assistants[j].Gamertag == "RandomComm3nt")
                        playerAssists.Add(GameEvents[i]);
                }
            }
        }
    }
}
