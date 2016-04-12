using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Api
{
    [Serializable]
    public class GameEvent
    {
        public List<PlayerData> Assistants;
        public PlayerData Killer;
        public PlayerData Victim;
        public int KillerStockId;
        public int DeathDisposition;
        public string TimeSinceStart;
        public Vector3 KillerWorldLocation;
        public Vector3 VictimWorldLocation;
        public int KillerAgent;
        public int VictimAgent;

        public TimeSpan GetTimeSinceStart()
        {
            TimeSpan result;
            TimeSpan.TryParse(TimeSinceStart, out result);
            return result;
        }
    }
}
