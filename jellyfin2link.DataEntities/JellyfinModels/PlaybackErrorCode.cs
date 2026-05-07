using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public enum PlaybackErrorCode
    {
        NotAllowed = 0,
        NoCompatibleStream = 1,
        RateLimitExceeded = 2
    }
}
