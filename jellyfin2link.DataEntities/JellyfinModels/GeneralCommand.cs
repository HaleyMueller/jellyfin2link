using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public class GeneralCommand
    {
        public GeneralCommand()
            : this(new Dictionary<string, string>())
        {
        }

        [JsonConstructor]
        public GeneralCommand(Dictionary<string, string>? arguments)
        {
            Arguments = arguments ?? new Dictionary<string, string>();
        }

        public GeneralCommandType Name { get; set; }

        public Guid ControllingUserId { get; set; }

        public Dictionary<string, string> Arguments { get; }
    }
}
