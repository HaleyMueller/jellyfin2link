using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    public class ProfileCondition
    {
        public ProfileCondition()
        {
            IsRequired = true;
        }

        public ProfileCondition(ProfileConditionType condition, ProfileConditionValue property, string value)
            : this(condition, property, value, false)
        {
        }

        public ProfileCondition(ProfileConditionType condition, ProfileConditionValue property, string value, bool isRequired)
        {
            Condition = condition;
            Property = property;
            Value = value;
            IsRequired = isRequired;
        }

        [XmlAttribute("condition")]
        public ProfileConditionType Condition { get; set; }

        [XmlAttribute("property")]
        public ProfileConditionValue Property { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("isRequired")]
        public bool IsRequired { get; set; }
    }
}
