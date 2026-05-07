using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace jellyfin2link.BusinessLogicLayer
{
    public class BusinessBase
    {
        internal IServiceProvider ServiceProvider { get; private set; }
        internal IConfiguration Configuration { get; private set; }


        public string jellyfinServer => Configuration["PublicJellyfinServer"];
        public string localJellyfinServer => Configuration["LocalJellyfinServer"];
        public string apiKey => Configuration["JellyfinApiKey"];


        public BusinessBase(IServiceProvider serviceProvider) 
        {
            ServiceProvider = serviceProvider;
            Configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public void Dispose()
        {
            
        }
    }
}
