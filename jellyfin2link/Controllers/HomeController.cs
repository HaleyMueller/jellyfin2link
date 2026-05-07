using jellyfin2link.BusinessLogicLayer;
using jellyfin2link.DataEntities.JellyfinModels;
using jellyfin2link.Models;
using jellyfin2link.RESTClient;
using jellyfin2link.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace jellyfin2link.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider) { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Item(string itemID)
        {
            try
            {
                var ret = new ItemResponse();

                var user = new Guid(base.JellyfinUserGUID);

                var itemBLL = new BusinessLogicLayer.Items(ServiceProvider);
                var mediaInfoBLL = new BusinessLogicLayer.MediaInfo(ServiceProvider);
                var sessionBLL = new BusinessLogicLayer.Session(ServiceProvider);
                var playstateBLL = new BusinessLogicLayer.Playstate(ServiceProvider);
                var request = new jellyfin2link.RESTClient.Requests.GetItemsRequest() { Recursive = false, Ids = new Guid[] { new Guid(itemID) }, UserId = user };

                var movieFolder = itemBLL.GetItems(request).Result;
                ret.Item = movieFolder;

                //inject public url
                ret.PublicJellyfinURL = base.jellyfinServer;
                var images = new Dictionary<ImageType, string>();
                var r = ret.Item.Data.Items.FirstOrDefault();
                foreach (var i in ret.Item.Data.Items.FirstOrDefault().Images(jellyfinServer))
                {
                    var uri = new UriBuilder(i.Value);

                    var val = i.Value.Replace(base.localJellyfinServer, base.jellyfinServer);
                    images.Add(i.Key, val);
                }

                ret.Images = images;




                var playbackInfo = mediaInfoBLL.GetPlaybackInfo(itemID, new jellyfin2link.DataEntities.JellyfinModels.PlaybackInfoDto() { /*UserId = user*/ }).Result;

                //var playStart = playstateBLL.ReportPlaybackStart(new DataEntities.JellyfinModels.PlaybackStartInfo()
                //{
                //    PlaySessionId = playbackInfo.Data.PlaySessionId,
                //    ItemId = new Guid(itemID),
                //     PlayMethod = DataEntities.JellyfinModels.PlayMethod.DirectPlay,
                //      PlaybackOrder = DataEntities.JellyfinModels.PlaybackOrder.Default,
                //       MediaSourceId = playbackInfo.Data.MediaSources.FirstOrDefault().Id,
                        
                //}).Result;

                //var sessions = sessionBLL.GetSessions(new RESTClient.Requests.GetSessionsRequest()).Result;


                var resoniteDevice = new Device();
                resoniteDevice.MaxVideoBitRate = 1_000_000;
                resoniteDevice.MaxVideoBitDepth = 8;
                resoniteDevice.Name = "Resonite";
                resoniteDevice.SupportedContainers = new List<string>
                {
                    "3gp",
                    "aac", // adts
                    "ac3",
                    "ape",
                    "avi",
                    "flv",
                    "mov",
                    "m41",  // m4a / mov
                    "mkv", // matroska, mka
                    "mov",
                    "mpegts", // mp2, ts
                    "mp3",
                    "mp4",
                    "webm",
                    "aiff",
                    "asf",
                    "au",
                    "caf",
                    "nsv",
                    "nuv",
                    "ogg",
                    "mpeg", // ps
                    "wav",
                    "flac",
                    "tta",
                    "vc1"
                };

                resoniteDevice.SupportedVideoCodec = new List<string>
                {
                    "av1",      // aom
                    "h264",     // x264, x26410b
                    "hevc",     // x265
                    "vp9",      // vpx
                    "mpeg2video", // mpeg2, mpgv
                    "libtheora"   // theora
                };

                resoniteDevice.SupportedAudioCodec = new List<string>
                {
                    "ac3",    // AC-3
                    "flac",   // FLAC
                    "mp3",    // MP3
                    "eac3",   // Enhanced AC-3
                    "vorbis", // Vorbis
                    "dts",    // DTS
                    "opus",   // Opus
                    "alac"    // Apple Lossless
                };

                resoniteDevice.SupportedSubtitleCodec = new List<string>
                {
                    "aribsub",  // aribsub
                    "eia_608",  // cc
                    "cdg",      // cdg
                    "dvdsub",   // dvbsub
                    "kate",     // kate
                    "ass",      // libass
                    "scte_18",  // scte18
                    "scte_27",  // scte27
                    "stl",      // stl
                    "svcd",     // svcdsub
                    "t140",     // t140
                    "text",     // textst
                    "ttml",     // ttml
                    "webvtt",   // webvtt
                    "zvbi",     // zvbi
                    "srt"       // srt
                };



                var jellyfinDevice = new Device();
                jellyfinDevice.Name = "Jellyfin";
                jellyfinDevice.SupportedContainers = new List<string>
                {
                    "mp4",       // MP4
                    "mkv",  // MKV
                    "webm",      // WebM
                    "ogg"        // OGG
                };

                jellyfinDevice.SupportedVideoCodec = new List<string>
                {
                    "mpeg4",   // MPEG-4 Part 2 (SP, ASP)
                    "h264",    // H.264 (8-bit, 10-bit with pix_fmt)
                    "hevc",    // H.265 / HEVC (8-bit, 10-bit with pix_fmt)
                    "vp9",     // VP9
                    "av1"      // AV1
                };

                jellyfinDevice.SupportedAudioCodec = new List<string>
                {
                    "ac3",       // a52, ac3
                    "aac",       // aac
                    "adpcm_ms",  // adpcm (example, can vary)
                    "ape",       // ape
                    "pcm_s16be", // araw
                    "dts",       // dca
                    "flac",      // flac
                    "pcm_alaw",  // g711
                    "pcm_mulaw", // g711
                    "pcm_s16le", // lpcm, rawaudio, rawaud
                    "mp2",       // mp2, twolame
                    "mp3",       // mp3, mpg123
                    "opus",      // opus
                    "speex",     // speex
                    "tta",       // tta
                    "vorbis"     // vorbis
                };

                jellyfinDevice.SupportedSubtitleCodec = new List<string>
                {
                    "srt",      // SubRip Text (SRT)
                    "webvtt",   // WebVTT (VTT)
                    "ass",      // ASS/SSA
                    "dvdsub",   // VobSub
                    "mov_text", // MP4TT/TXTT
                    "pgssub",   // PGS Subtitles
                    "eia_608"   // CEA-608/708
                };

                var playbackInfoResonite = playbackInfo.Data;
                var itemInfo = movieFolder.Data.Items.FirstOrDefault();


                //Get best usable codecs for client
                var containerToUse = itemInfo.Container;

                var mediaSources = playbackInfoResonite.MediaSources.FirstOrDefault();

                if (mediaSources.MediaStreams == null || mediaSources.MediaStreams.Count <= 0)
                    throw new Exception($"No media streams were found for: {mediaSources.Path}");

                var audioSource = GetPreferredStream(mediaSources.MediaStreams, DataEntities.JellyfinModels.MediaStreamType.Audio, jellyfinDevice.SupportedAudioCodec);
                if (audioSource == null)
                    audioSource = mediaSources.MediaStreams.FirstOrDefault(x => x.Type == DataEntities.JellyfinModels.MediaStreamType.Audio);

                var videoSource = GetPreferredStream(mediaSources.MediaStreams, DataEntities.JellyfinModels.MediaStreamType.Video, jellyfinDevice.SupportedVideoCodec);
                if (videoSource == null)
                    videoSource = mediaSources.MediaStreams.FirstOrDefault(x => x.Type == DataEntities.JellyfinModels.MediaStreamType.Video);

                var subtitleSource = GetPreferredStream(mediaSources.MediaStreams, DataEntities.JellyfinModels.MediaStreamType.Subtitle, jellyfinDevice.SupportedSubtitleCodec, "eng");
                if (subtitleSource == null)
                    subtitleSource = mediaSources.MediaStreams.FirstOrDefault(x => x.Type == DataEntities.JellyfinModels.MediaStreamType.Subtitle);

                //TODO if transcoding need to pick audio, video, sub stream directly.
                //TODO all GetVideoStreamQuery options on screen
                //TODO pick device on the screen then it gives you the url for that device generated from above

                DataEntities.JellyfinModels.SubtitleDeliveryMethod? subtitleMethodWants = null;

                var directURL = mediaInfoBLL.GetUrl(playbackInfo.Data, new jellyfin2link.RESTClient.Requests.GetVideoStreamQuery() { Static = true });

                ret.ItemLinks.Add(new ItemLink() { URL = directURL, Name = "Direct URL", Container = itemInfo.Container});
                ret.ItemLinks.Add(GetItemLink(resoniteDevice, jellyfinDevice, itemInfo, playbackInfo.Data, audioSource, videoSource, subtitleSource, mediaInfoBLL, null, resoniteDevice.MaxVideoBitRate, "Perfect URL"));
                ret.ItemLinks.Add(GetItemLink(resoniteDevice, jellyfinDevice, itemInfo, playbackInfo.Data, audioSource, videoSource, subtitleSource, mediaInfoBLL, DataEntities.JellyfinModels.SubtitleDeliveryMethod.Encode, resoniteDevice.MaxVideoBitRate, "Perferct URL but Subtitles"));


                ret.PlaybackInfo = playbackInfo;

                return PartialView(ret);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "Couldn't get item");
            }

            return NotFound();
        }

        private DataEntities.JellyfinModels.MediaStream GetPreferredStream(IReadOnlyList<DataEntities.JellyfinModels.MediaStream> streams, DataEntities.JellyfinModels.MediaStreamType streamType, List<string> supportedCodecs, string? lang = null)
        {
            foreach (var codec in supportedCodecs)
            {
                var query = streams.FirstOrDefault(x => x.Type == streamType && x.Codec == codec);

                if (string.IsNullOrEmpty(lang) == false && query?.Language != lang)
                    continue;

                if (query != null)
                {
                    return query;
                }
            }

            return null;
        }

        public ItemLink GetItemLink(Device clientDevice, Device trancodingDevice, RESTClient.Responses.InjectedBaseItemDto item, DataEntities.JellyfinModels.PlaybackInfoResponse playbackInfo,
            DataEntities.JellyfinModels.MediaStream audioSource, DataEntities.JellyfinModels.MediaStream videoSource, DataEntities.JellyfinModels.MediaStream subtitleSource, 
            BusinessLogicLayer.MediaInfo mediaInfoBLL, DataEntities.JellyfinModels.SubtitleDeliveryMethod? subtitleMethodWants, int clientMaxVideoBitrate, string name = null)
        {
            var ret = new ItemLink();
            var codecsToUse = clientDevice.GetSupportedCodecs(trancodingDevice, item.Container, videoSource.Codec, audioSource.Codec, subtitleSource?.Codec);

            ret.Container = codecsToUse.Container;
            ret.Name = string.IsNullOrEmpty(name) ? clientDevice.Name : name;

            var query = new jellyfin2link.RESTClient.Requests.GetVideoStreamQuery() //todo copy all values from audio /video /subtitle sources to this link if no transcode then no selecting sources indexx || I HAVE NO IDEA
            {
                Static = false,
                VideoCodec = codecsToUse.VideoCodec,
                VideoStreamIndex = videoSource.Index,
                AudioCodec = codecsToUse.AudioCodec,
                AudioStreamIndex = audioSource.Index,
                Container = codecsToUse.Container,
                Width = videoSource.Width,
                Height = videoSource.Height,
                MaxVideoBitDepth = videoSource.BitDepth > clientDevice.MaxVideoBitDepth && clientDevice.MaxVideoBitDepth > 0 ? clientDevice.MaxVideoBitDepth : videoSource.BitDepth,
                MaxAudioBitDepth = audioSource.BitDepth > clientDevice.MaxAudioBitDepth && clientDevice.MaxAudioBitDepth > 0 ? clientDevice.MaxAudioBitDepth : audioSource.BitDepth,
                VideoBitRate = videoSource.BitRate > clientMaxVideoBitrate && clientMaxVideoBitrate > 0 ? clientMaxVideoBitrate : videoSource.BitRate //Needed or it will default to the lowest bitrate / resolution that is in jellyfin settings
            };

            if (query.MaxAudioBitDepth != audioSource.BitDepth || query.MaxVideoBitDepth != videoSource.BitDepth)
            {
                query.RequireAvc = true;
                query.VideoCodec = "h264"; //only way I know to force removal of HDR
                query.AllowVideoStreamCopy = false;
                //query.colo = false; shit there was something here but i forgor
            }

            if (subtitleMethodWants != null)
            {
                if (subtitleSource == null)
                    return new ItemLink() { Name = "ERROR NO SUBS" };

                query.SubtitleCodec = codecsToUse.SubtitleCodec;
                query.SubtitleMethod = subtitleMethodWants;
                query.SubtitleStreamIndex = subtitleSource.Index;
            }

            if (codecsToUse.Container == item.Container && codecsToUse.AudioCodec == audioSource.Codec && codecsToUse.VideoCodec == videoSource.Codec && query.VideoBitRate != videoSource.BitRate 
                && query.MaxAudioBitDepth == audioSource.BitDepth && query.MaxVideoBitDepth == videoSource.BitDepth
                && (subtitleMethodWants == null || (subtitleMethodWants == subtitleSource.DeliveryMethod))) //codecsToUse.SubtitleCodec == subtitleSource.Codec prob unnesscary
                query = new RESTClient.Requests.GetVideoStreamQuery() { Static = true };

            ret.URL = mediaInfoBLL.GetUrl(playbackInfo, query);

            return ret;
        }

        public class ItemLink
        {
            public string Name { get; set; }
            public string URL { get; set; }
            public string Container { get; set; }
        }

        public class Device
        {
            public string Name { get; set; }
            public int MaxVideoBitRate { get; set; }
            public int MaxVideoBitDepth { get; set; }
            public int MaxAudioBitDepth { get; set; }
            public List<string> SupportedContainers { get; set; } = new List<string>();
            public List<string> SupportedVideoCodec { get; set; } = new List<string>();
            public List<string> SupportedAudioCodec { get; set; } = new List<string>();
            public List<string> SupportedSubtitleCodec { get; set; } = new List<string>();

            public DeviceCodecMatch GetSupportedCodecs(Device transcodingDevice, string fileContainer, string fileVideoCodec, string fileAudioCodec, string fileSubtitleCodec)
            {
                var ret = new DeviceCodecMatch();

                ret.Container = GetMatchingCodec(SupportedContainers, transcodingDevice.SupportedContainers, fileContainer);
                ret.VideoCodec = GetMatchingCodec(SupportedVideoCodec, transcodingDevice.SupportedVideoCodec, fileVideoCodec);
                ret.AudioCodec = GetMatchingCodec(SupportedAudioCodec, transcodingDevice.SupportedAudioCodec, fileAudioCodec);
                ret.SubtitleCodec = GetMatchingCodec(SupportedSubtitleCodec, transcodingDevice.SupportedSubtitleCodec, fileSubtitleCodec);

                return ret;
            }

            private string? GetMatchingCodec(List<string> codecFromStreamingDevice, List<string> codecsFromHostingDevice, string codec)
            {
                if (codecFromStreamingDevice.Contains(codec) && codecsFromHostingDevice.Contains(codec))
                {
                    return codec;
                }
                else
                {

                    foreach (var container in codecFromStreamingDevice)
                    {
                        if (codecsFromHostingDevice.Contains(container))
                        {
                            return container;
                        }
                    }
                }

                return null;
            }

            public class DeviceCodecMatch
            {
                public string Container { get; set; }
                public string VideoCodec { get; set; }
                public string AudioCodec { get; set; }
                public string SubtitleCodec { get; set; }
            }
        }

        public class ItemResponse
        {
            public Dictionary<ImageType, string> Images { get; set; }
            public DataEntities.APIResponse<DataEntities.JellyfinModels.PlaybackInfoResponse> PlaybackInfo { get; set; }
            public DataEntities.APIResponse<DataEntities.JellyfinModels.QueryResult<RESTClient.Responses.InjectedBaseItemDto>> Item { get; set; }

            public List<ItemLink> ItemLinks { get; set; } = new List<ItemLink>();

            public string PublicJellyfinURL { get; set; }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
