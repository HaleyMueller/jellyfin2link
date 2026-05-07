using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public enum SubtitleDeliveryMethod
    {
        /// <summary>
        /// Burn the subtitles in the video track.
        /// </summary>
        Encode = 0,

        /// <summary>
        /// Embed the subtitles in the file or stream.
        /// </summary>
        Embed = 1,

        /// <summary>
        /// Serve the subtitles as an external file.
        /// </summary>
        External = 2,

        /// <summary>
        /// Serve the subtitles as a separate HLS stream.
        /// </summary>
        Hls = 3,

        /// <summary>
        /// Drop the subtitle.
        /// </summary>
        Drop = 4
    }
}
