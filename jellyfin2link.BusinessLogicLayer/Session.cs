using jellyfin2link.DataEntities;
using jellyfin2link.DataEntities.JellyfinModels;
using jellyfin2link.RESTClient.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static jellyfin2link.RESTClient.Items;

namespace jellyfin2link.BusinessLogicLayer
{
    public class Session : BusinessBase
    {
        public Session(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<APIResponse<List<SessionInfoDto>>> GetSessions(RESTClient.Requests.GetSessionsRequest request)
        {
            RESTClient.Session itemRestClient = new RESTClient.Session(ServiceProvider);

            var response = await itemRestClient.GetSessions(request);

            return response;
        }

        public async Task<APIResponse<bool>> IssuePlaystateCommandToClient(string sessionID, string command, long? seekPositionTicks)
        {
            RESTClient.Session itemRestClient = new RESTClient.Session(ServiceProvider);

            var response = await itemRestClient.IssuePlaystateCommandToClient(sessionID, command, seekPositionTicks);

            return response;
        }
        
        public async Task<APIResponse<bool>> SendFullGeneralCommand(string sessionID, GeneralCommand requestData)
        {
            RESTClient.Session itemRestClient = new RESTClient.Session(ServiceProvider);

            var response = await itemRestClient.SendFullGeneralCommand(sessionID, requestData);

            return response;
        }
    }
}
