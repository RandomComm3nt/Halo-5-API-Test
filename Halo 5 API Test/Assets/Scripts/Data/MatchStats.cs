using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class MatchStats
    {
        public List<GameEvent> GameEvents;
        public bool IsCompleteSetoOfEvents;
    }
}
