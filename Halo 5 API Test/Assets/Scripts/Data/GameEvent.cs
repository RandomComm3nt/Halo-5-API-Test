using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
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

        public TimeSpan GetTimeSinceStart()
        {
            TimeSpan result;
            TimeSpan.TryParse(TimeSinceStart, out result);
            return result;
        }
    }
}
