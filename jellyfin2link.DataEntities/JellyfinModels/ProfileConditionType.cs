using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public enum ProfileConditionType
    {
        Equals = 0,
        NotEquals = 1,
        LessThanEqual = 2,
        GreaterThanEqual = 3,
        EqualsAny = 4
    }
}
