using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    /// <summary>
    /// An enum representing video ranges.
    /// </summary>
    public enum VideoRange
    {
        /// <summary>
        /// Unknown video range.
        /// </summary>
        Unknown,

        /// <summary>
        /// SDR video range.
        /// </summary>
        SDR,

        /// <summary>
        /// HDR video range.
        /// </summary>
        HDR
    }
}
