using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public enum SyncPlayUserAccessType
    {
        /// <summary>
        /// User can create groups and join them.
        /// </summary>
        CreateAndJoinGroups = 0,

        /// <summary>
        /// User can only join already existing groups.
        /// </summary>
        JoinGroups = 1,

        /// <summary>
        /// SyncPlay is disabled for the user.
        /// </summary>
        None = 2
    }
}
