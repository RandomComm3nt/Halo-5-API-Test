using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data.Api
{
    [Serializable]
    public class MatchResultList
    {
        public int ResultCount;

        public List<MatchResult> Results;
    }
}
