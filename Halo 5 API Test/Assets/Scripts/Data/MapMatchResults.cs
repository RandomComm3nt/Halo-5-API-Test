
using System.Collections.Generic;
using Assets.Scripts.Data.Api;

namespace Assets.Scripts.Data
{
    public class MapMatchResults
    {
        public MapData map;
        private List<MatchResult> seasonMatches;

        public MapMatchResults()
        {
            seasonMatches = new List<MatchResult>();
        }

        public void AddMatch(MatchResult result)
        {
            seasonMatches.Add(result);
        }
    }
}
