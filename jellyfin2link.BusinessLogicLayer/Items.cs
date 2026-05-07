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
    public class Items : BusinessBase
    {
        public Items(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<APIResponse<QueryResult<InjectedBaseItemDto>>> GetItems(RESTClient.Requests.GetItemsRequest request)
        {
            RESTClient.Items itemRestClient = new RESTClient.Items(ServiceProvider);

            var data = await itemRestClient.GetItems(request);

            var items = new List<InjectedBaseItemDto>();
            foreach (var item in data.Data.Items)
            {
                items.Add(new InjectedBaseItemDto(item));
            }

            var queryResult = new QueryResult<InjectedBaseItemDto>(items);

            var ret = new APIResponse<QueryResult<InjectedBaseItemDto>>("Success");
            ret.Data = queryResult;


            return ret;
        }
    }
}
