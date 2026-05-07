using jellyfin2link.DataEntities;
using jellyfin2link.DataEntities.JellyfinModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jellyfin2link.BusinessLogicLayer
{
    public class Users : BusinessBase
    {
        public Users(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public Task<APIResponse<List<UserDto>>> GetAllUsers()
        {
            jellyfin2link.RESTClient.User userRestClient = new RESTClient.User(ServiceProvider);

            return userRestClient.GetUsers();
        }
    }
}
