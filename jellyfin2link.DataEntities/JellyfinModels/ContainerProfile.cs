using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    /// <summary>
    /// Defines the <see cref="ContainerProfile"/>.
    /// </summary>
    public class ContainerProfile
    {
        /// <summary>
        /// Gets or sets the <see cref="DlnaProfileType"/> which this container must meet.
        /// </summary>
        [XmlAttribute("type")]
        public DlnaProfileType Type { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="ProfileCondition"/> which this container will be applied to.
        /// </summary>
        public ProfileCondition[] Conditions { get; set; } = Array.Empty<ProfileCondition>();

        /// <summary>
        /// Gets or sets the container(s) which this container must meet.
        /// </summary>
        [XmlAttribute("container")]
        public string? Container { get; set; }

        /// <summary>
        /// Gets or sets the sub container(s) which this container must meet.
        /// </summary>
        [XmlAttribute("subcontainer")]
        public string? SubContainer { get; set; }

        /// <summary>
        /// Returns true if an item in <paramref name="container"/> appears in the <see cref="Container"/> property.
        /// </summary>
        /// <param name="container">The item to match.</param>
        /// <param name="useSubContainer">Consider subcontainers.</param>
        /// <returns>The result of the operation.</returns>
        public bool ContainsContainer(ReadOnlySpan<char> container, bool useSubContainer = false)
        {
            var containerToCheck = useSubContainer && string.Equals(Container, "hls", StringComparison.OrdinalIgnoreCase) ? SubContainer : Container;
            return ContainerHelper.ContainsContainer(containerToCheck, container);
        }
    }
}
