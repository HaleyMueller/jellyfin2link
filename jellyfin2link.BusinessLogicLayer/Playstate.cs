using jellyfin2link.DataEntities;
using jellyfin2link.DataEntities.JellyfinModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jellyfin2link.BusinessLogicLayer
{
    public class Playstate : BusinessBase
    {
        public Playstate(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public Task<APIResponse<bool>> ReportPlaybackStart(PlaybackStartInfo requestData)
        {
            jellyfin2link.RESTClient.Playstate userRestClient = new RESTClient.Playstate(ServiceProvider);

            return userRestClient.ReportPlaybackStart(requestData);
        }

        public Task<APIResponse<bool>> ReportPlaybackStopped(PlaybackStopInfo requestData)
        {
            jellyfin2link.RESTClient.Playstate userRestClient = new RESTClient.Playstate(ServiceProvider);

            return userRestClient.ReportPlaybackStopped(requestData);
        }

        public Task<APIResponse<bool>> ReportPlaybackProgress(PlaybackProgressInfo requestData)
        {
            jellyfin2link.RESTClient.Playstate userRestClient = new RESTClient.Playstate(ServiceProvider);

            return userRestClient.ReportPlaybackProgress(requestData);
        }
    }
}
