using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public class ClientCapabilities
    {
        public ClientCapabilities()
        {
            PlayableMediaTypes = Array.Empty<MediaType>();
            SupportedCommands = Array.Empty<GeneralCommandType>();
            SupportsPersistentIdentifier = true;
        }

        public IReadOnlyList<MediaType> PlayableMediaTypes { get; set; }

        public IReadOnlyList<GeneralCommandType> SupportedCommands { get; set; }

        public bool SupportsMediaControl { get; set; }

        public bool SupportsPersistentIdentifier { get; set; }

        public DeviceProfile DeviceProfile { get; set; }

        public string AppStoreUrl { get; set; }

        public string IconUrl { get; set; }

        // TODO: Remove after 10.9
        [Obsolete("Unused")]
        [DefaultValue(false)]
        public bool? SupportsContentUploading { get; set; } = false;

        // TODO: Remove after 10.9
        [Obsolete("Unused")]
        [DefaultValue(false)]
        public bool? SupportsSync { get; set; } = false;
    }
}
