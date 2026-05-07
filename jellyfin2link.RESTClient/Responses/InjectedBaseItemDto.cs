using jellyfin2link.DataEntities.JellyfinModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace jellyfin2link.RESTClient.Responses
{
    [DebuggerDisplay("{Name} | {Type}")]
    public class InjectedBaseItemDto : BaseItemDto
    {
        public InjectedBaseItemDto(BaseItemDto baseItem)
        {
            if (baseItem == null) throw new ArgumentNullException(nameof(baseItem));

            // Copy all public properties
            foreach (var prop in typeof(BaseItemDto).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    prop.SetValue(this, prop.GetValue(baseItem));
                }
            }

            // Or copy fields if needed:
            foreach (var field in typeof(BaseItemDto).GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                field.SetValue(this, field.GetValue(baseItem));
            }
        }

        public Dictionary<ImageType, string> Images(string publicURL)
        {
            var ret = new Dictionary<ImageType, string>();
            foreach (var imageTag in ImageTags)
            {
                var urll = $"{publicURL}/Items/{Id}/Images/{imageTag.Key}?fillHeight=240&fillWidth=426&quality=96&tag={imageTag.Value}";
                ret.Add(imageTag.Key, urll);
            }

            return ret;
        }
    }
}
