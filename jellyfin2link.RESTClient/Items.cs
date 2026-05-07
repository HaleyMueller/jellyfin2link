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
    public class Items : RESTClient
    {
        public Items(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<DataEntities.APIResponse<QueryResult<BaseItemDto>>> GetItems(Requests.GetItemsRequest requestData)
        {
            var ret = new DataEntities.APIResponse<QueryResult<BaseItemDto>>("Successfully got items");

            try
            {
                var request = new RestRequest("Items?" + requestData.ToString());

                ret.Data = await base.GetAndHandleExceptionAsync<QueryResult<BaseItemDto>>(request);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't grab items");
                ret.AddExecption(ex);
            }

            return ret;
        }
    }
}
