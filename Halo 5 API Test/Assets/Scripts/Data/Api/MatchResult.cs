using System;

namespace Assets.Scripts.Data.Api
{
    [Serializable]
    public class MatchResult
    {
        public Guid MapId;
        public Guid HopperId;
        public Guid GameBaseVariantId;
        public MatchIdWrapper Id;
        public DateWrapper MatchCompletedDate;
        public string SeasonId;
    }
}
