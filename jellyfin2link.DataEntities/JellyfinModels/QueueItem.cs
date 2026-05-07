using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public class QueueItem
    {
        //[JsonConverter(typeof(DashlessGuidConverter))]
        public Guid Id { get; set; }

        public string PlaylistItemId { get; set; }
    }

}
