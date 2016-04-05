using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerMatchList
    {
        public int ResultCount;

        public List<PlayerMatchResult> Results;

        public List<MatchStats> matches;
    }
}
