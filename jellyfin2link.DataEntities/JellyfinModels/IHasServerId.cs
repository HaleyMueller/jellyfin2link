using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public interface IHasServerId
    {
        string ServerId { get; }
    }
}
