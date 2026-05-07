using jellyfin2link.DataEntities;
using jellyfin2link.DataEntities.JellyfinModels;
using jellyfin2link.RESTClient;
using jellyfin2link.RESTClient.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static jellyfin2link.RESTClient.Items;

namespace jellyfin2link.BusinessLogicLayer
{
    public class MediaInfo : BusinessBase
    {
        public MediaInfo(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public Task<APIResponse<PlaybackInfoResponse>> GetPlaybackInfo(string itemID, PlaybackInfoDto requestData)
        {
            RESTClient.MediaInfo restClient = new RESTClient.MediaInfo(ServiceProvider);

            return restClient.GetPlaybackInfo(itemID, requestData);
        }

        //TODO have a url maker class for direct /transcodes + options
        public string GetUrl(PlaybackInfoResponse playbackInfo, GetVideoStreamQuery options)
        {
            var directURL = $"{jellyfinServer}/Videos/{playbackInfo.MediaSources.FirstOrDefault().Id.ToString()}/stream?playSessionId={playbackInfo.PlaySessionId}&api_key={apiKey}&{options.ToString()}";

            return directURL;
        }
    }
}
