using jellyfin2link.DataEntities.JellyfinModels;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jellyfin2link.RESTClient
{
    public class Playstate : RESTClient
    {
        public Playstate(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<DataEntities.APIResponse<bool>> ReportPlaybackStart(PlaybackStartInfo requestData)
        {
            var ret = new DataEntities.APIResponse<bool>("Successfully sent playback start");

            try
            {
                var request = new RestRequest("Sessions/Playing");
                request.AddJsonBody(requestData);

                await base.PostAndHandleExceptionAsync<object>(request);

                ret.Data = true;
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't send playback start");
                ret.AddExecption(ex);
            }

            return ret;
        }

        public async Task<DataEntities.APIResponse<bool>> ReportPlaybackProgress(PlaybackProgressInfo requestData)
        {
            var ret = new DataEntities.APIResponse<bool>("Successfully sent playback progress");

            try
            {
                var request = new RestRequest("Sessions/Playing/Progress");
                request.AddJsonBody(requestData);

                await base.PostAndHandleExceptionAsync<object>(request);

                ret.Data = true;
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't send playback progress");
                ret.AddExecption(ex);
            }

            return ret;
        }

        public async Task<DataEntities.APIResponse<bool>> ReportPlaybackStopped(PlaybackStopInfo requestData)
        {
            var ret = new DataEntities.APIResponse<bool>("Successfully sent playback stopped");

            try
            {
                var request = new RestRequest("Sessions/Playing/Stopped");
                request.AddJsonBody(requestData);

                await base.PostAndHandleExceptionAsync<object>(request);

                ret.Data = true;
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't send playback stopped");
                ret.AddExecption(ex);
            }

            return ret;
        }
    }
}
