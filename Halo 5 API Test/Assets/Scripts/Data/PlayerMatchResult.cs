using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerMatchResult
    {
        public Guid MapId;
        public Guid HopperId;
        public Guid GameBaseVariantId;
        public MatchIdWrapper Id;
        public DateWrapper MatchCompletedDate;
    }
}
