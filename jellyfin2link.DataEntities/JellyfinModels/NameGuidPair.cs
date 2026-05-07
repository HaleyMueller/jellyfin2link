using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public class NameGuidPair
    {
        public string Name { get; set; }

        //[JsonConverter(typeof(DashlessGuidConverter))]
        public Guid Id { get; set; }
    }
}
