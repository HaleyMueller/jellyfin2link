using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace jellyfin2link.Web.Controllers
{
    public class BaseController : Controller
    {
        internal ILogger Logger { get; private set; }
        internal IServiceProvider ServiceProvider { get; private set; }
        internal IConfiguration Configuration { get; private set; }

        public string jellyfinServer => Configuration["PublicJellyfinServer"];
        public string localJellyfinServer => Configuration["LocalJellyfinServer"];
        public string apiKey => Configuration["JellyfinApiKey"];
        public string JellyfinUserGUID => Configuration["JellyfinUserGUID"];

        public BaseController(ILogger logger, IServiceProvider serviceProvider)
        {
            Logger = logger;
            ServiceProvider = serviceProvider;
            Configuration = ServiceProvider.GetService<IConfiguration>();
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
