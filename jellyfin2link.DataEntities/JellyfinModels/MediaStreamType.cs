using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    /// <summary>
    /// Enum MediaStreamType.
    /// </summary>
    public enum MediaStreamType
    {
        /// <summary>
        /// The audio.
        /// </summary>
        Audio,

        /// <summary>
        /// The video.
        /// </summary>
        Video,

        /// <summary>
        /// The subtitle.
        /// </summary>
        Subtitle,

        /// <summary>
        /// The embedded image.
        /// </summary>
        EmbeddedImage,

        /// <summary>
        /// The data.
        /// </summary>
        Data,

        /// <summary>
        /// The lyric.
        /// </summary>
        Lyric
    }
}
