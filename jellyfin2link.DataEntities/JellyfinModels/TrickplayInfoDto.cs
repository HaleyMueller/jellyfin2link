using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    /// <summary>
    /// The trickplay api model.
    /// </summary>
    public class TrickplayInfoDto
    {
        public TrickplayInfoDto() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrickplayInfoDto"/> class.
        /// </summary>
        /// <param name="info">The trickplay info.</param>
        public TrickplayInfoDto(TrickplayInfo info)
        {
                if (info == null)
                    throw new ArgumentNullException("info");

            Width = info.Width;
            Height = info.Height;
            TileWidth = info.TileWidth;
            TileHeight = info.TileHeight;
            ThumbnailCount = info.ThumbnailCount;
            Interval = info.Interval;
            Bandwidth = info.Bandwidth;
        }

        /// <summary>
        /// Gets the width of an individual thumbnail.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets the height of an individual thumbnail.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets the amount of thumbnails per row.
        /// </summary>
        public int TileWidth { get; set; }

        /// <summary>
        /// Gets the amount of thumbnails per column.
        /// </summary>
        public int TileHeight { get; set; }

        /// <summary>
        /// Gets the total amount of non-black thumbnails.
        /// </summary>
        public int ThumbnailCount { get; set; }

        /// <summary>
        /// Gets the interval in milliseconds between each trickplay thumbnail.
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Gets the peak bandwidth usage in bits per second.
        /// </summary>
        public int Bandwidth { get; set; }

    }
}
