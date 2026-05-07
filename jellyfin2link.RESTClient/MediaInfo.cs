using jellyfin2link.DataEntities.JellyfinModels;
using jellyfin2link.RESTClient.Responses;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace jellyfin2link.RESTClient
{
    public class MediaInfo : RESTClient
    {
        public MediaInfo(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<DataEntities.APIResponse<PlaybackInfoResponse>> GetPlaybackInfo(string itemID, PlaybackInfoDto requestData)
        {
            var ret = new DataEntities.APIResponse<PlaybackInfoResponse>("Successfully got playback info");

            try
            {
                var request = new RestRequest($"Items/{itemID}/PlaybackInfo");
                request.AddJsonBody(requestData);

                ret.Data = await base.PostAndHandleExceptionAsync<PlaybackInfoResponse>(request);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't grab playback info");
                ret.AddExecption(ex);
            }

            return ret;
        }
    }
}
