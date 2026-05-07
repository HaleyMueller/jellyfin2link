using jellyfin2link.DataEntities.JellyfinModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace jellyfin2link.RESTClient.Requests
{
    public class GetVideoStreamQuery
    {
        public Guid? ItemId { get; set; }

        public string? Container { get; set; }
        public bool? Static { get; set; }
        public string? Params { get; set; }
        public string? Tag { get; set; }
        public string? DeviceProfileId { get; set; }
        public string? PlaySessionId { get; set; }
        public string? SegmentContainer { get; set; }
        public int? SegmentLength { get; set; }
        public int? MinSegments { get; set; }
        public string? MediaSourceId { get; set; }
        public string? DeviceId { get; set; }
        public string? AudioCodec { get; set; }
        public bool? EnableAutoStreamCopy { get; set; }
        public bool? AllowVideoStreamCopy { get; set; }
        public bool? AllowAudioStreamCopy { get; set; }
        public bool? BreakOnNonKeyFrames { get; set; }
        public int? AudioSampleRate { get; set; }
        public int? MaxAudioBitDepth { get; set; }
        public int? AudioBitRate { get; set; }
        public int? AudioChannels { get; set; }
        public int? MaxAudioChannels { get; set; }
        public string? Profile { get; set; }
        public string? Level { get; set; }
        public float? Framerate { get; set; }
        public float? MaxFramerate { get; set; }
        public bool? CopyTimestamps { get; set; }
        public long? StartTimeTicks { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
        public int? VideoBitRate { get; set; }
        public int? SubtitleStreamIndex { get; set; }
        public SubtitleDeliveryMethod? SubtitleMethod { get; set; }
        public int? MaxRefFrames { get; set; }
        public int? MaxVideoBitDepth { get; set; }
        public bool? RequireAvc { get; set; }
        public bool? DeInterlace { get; set; }
        public bool? RequireNonAnamorphic { get; set; }
        public int? TranscodingMaxAudioChannels { get; set; }
        public int? CpuCoreLimit { get; set; }
        public string? LiveStreamId { get; set; }
        public bool? EnableMpegtsM2TsMode { get; set; }
        public string? VideoCodec { get; set; }
        public string? SubtitleCodec { get; set; }
        public string? TranscodeReasons { get; set; }
        public int? AudioStreamIndex { get; set; }
        public int? VideoStreamIndex { get; set; }
        public EncodingContext? Context { get; set; }
        public Dictionary<string, string>? StreamOptions { get; set; }
        public bool EnableAudioVbrEncoding { get; set; } = true;

        public override string ToString()
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var queryParts = properties.SelectMany(prop =>
            {
                var value = prop.GetValue(this);
                if (value == null) return Array.Empty<string>();

                // Handle Dictionary<string, string>
                if (value is IDictionary dict)
                {
                    var list = dict.Keys.Cast<object>()
                        .Select(k => $"{Uri.EscapeDataString(k?.ToString() ?? "")}={Uri.EscapeDataString(dict[k]?.ToString() ?? "")}")
                        .ToArray();
                    if (list.Length == 0) return Array.Empty<string>();
                    return list;
                }

                // Handle IEnumerable (arrays, lists), but not string
                if (value is IEnumerable enumerable && !(value is string))
                {
                    var list = enumerable.Cast<object>().ToArray();
                    if (list.Length == 0) return Array.Empty<string>();
                    return new[] { $"{prop.Name}={string.Join(",", list.Select(x => Uri.EscapeDataString(x?.ToString() ?? "")))}" };
                }

                // Format DateTime in ISO 8601
                if (value is DateTime dt)
                    return new[] { $"{prop.Name}={Uri.EscapeDataString(dt.ToString("O"))}" };

                // Boolean values as lowercase
                if (value is bool b)
                    return new[] { $"{prop.Name}={b.ToString().ToLower()}" };

                // Enum values
                if (value.GetType().IsEnum)
                    return new[] { $"{prop.Name}={Uri.EscapeDataString(value.ToString()!)}" };

                // Default: string or numeric
                return new[] { $"{prop.Name}={Uri.EscapeDataString(value.ToString()!)}" };
            });

            return string.Join("&", queryParts);
        }
    }
}
