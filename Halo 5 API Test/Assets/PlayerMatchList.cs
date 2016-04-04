using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    [Serializable]
    public class PlayerMatchList
    {
        public int ResultCount;

        public List<PlayerMatchResult> Results;
    }
}
