using jellyfin2link.DataEntities.JellyfinModels;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jellyfin2link.RESTClient
{
    public class User : RESTClient
    {
        public User(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<DataEntities.APIResponse<List<UserDto>>> GetUsers()
        {
            var ret = new DataEntities.APIResponse<List<UserDto>>("Successfully got users");

            try
            {
                var request = new RestRequest("Users");

                ret.Data = await base.GetAndHandleExceptionAsync<List<UserDto>>(request);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex, $"Can't grab users");
                ret.AddExecption(ex);
            }

            return ret;
        }
    }
}
