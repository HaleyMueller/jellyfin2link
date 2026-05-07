using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    /// <summary>
    /// Media streaming protocol.
    /// Lowercase for backwards compatibility.
    /// </summary>
    [DefaultValue(http)]
    public enum MediaStreamProtocol
    {
        /// <summary>
        /// HTTP.
        /// </summary>
        http = 0,

        /// <summary>
        /// HTTP Live Streaming.
        /// </summary>
        hls = 1
    }
}
