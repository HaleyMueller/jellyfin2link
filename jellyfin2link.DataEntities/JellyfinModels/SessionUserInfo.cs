using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    /// <summary>
    /// Class SessionUserInfo.
    /// </summary>
    public class SessionUserInfo
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        //[JsonConverter(typeof(DashlessGuidConverter))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}
