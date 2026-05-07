using jellyfin2link.DataEntities.JellyfinModels;
using jellyfin2link.RESTClient.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class Session : RESTClient
    {
        public Session(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<DataEntities.APIResponse<List<SessionInfoDto>>> GetSessions(Requests.GetSessionsRequest requestData)
        {
            var ret = new DataEntities.APIResponse<List<SessionInfoDto>>("Successfully got sessions");

            try
            {
                var request = new RestRequest("Sessions" + requestData.ToString());

                ret.Data = await base.GetAndHandleExceptionAsync<List<SessionInfoDto>>(request);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't grab sessions");
                ret.AddExecption(ex);
            }

            return ret;
        }

        public async Task<DataEntities.APIResponse<bool>> IssuePlaystateCommandToClient(string sessionID, string command, long? seekPositionTicks)
        {
            var ret = new DataEntities.APIResponse<bool>("Successfully got sessions");

            try
            {
                var request = new RestRequest($"Sessions/{sessionID}/Command/{command}");
                await base.PostAndHandleExceptionAsync<object>(request);
                ret.Data = true;
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't grab sessions");
                ret.AddExecption(ex);
            }

            return ret;
        }

        public class ass
        {
            public string PlaySessionId { get; set; }
        }


        public async Task<DataEntities.APIResponse<bool>> SendFullGeneralCommand(string sessionID, GeneralCommand requestData)
        {
            var ret = new DataEntities.APIResponse<bool>("Successfully got sessions");

            try
            {
                var request = new RestRequest($"Sessions/{sessionID}/Command");
                request.AddJsonBody(requestData);
                await base.PostAndHandleExceptionAsync<object>(request);
                ret.Data = true;
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't grab sessions");
                ret.AddExecption(ex);
            }

            return ret;
        }
    }
}
