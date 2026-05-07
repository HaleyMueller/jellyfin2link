using jellyfin2link.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace jellyfin2link.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : BaseController
    {
        public APIController(ILogger<APIController> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider) { }

        [HttpGet("GetItems")]
        public IActionResult GetItems([FromQuery]string? parentID, [FromQuery]string? format = "json")
        {
            var itemBLL = new Items(ServiceProvider);
            var request = new jellyfin2link.RESTClient.Requests.GetItemsRequest() { Recursive = false };

            if (!string.IsNullOrEmpty(parentID))
                request.ParentId = new Guid(parentID);

            var movieFolder = itemBLL.GetItems(request).Result;

            movieFolder.Data.Items = movieFolder.Data.Items.OrderBy(x => x.IndexNumber).ThenBy(x => x.Name).ToList();

            if (format == "json") 
                return Json(movieFolder);
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var data in movieFolder.Data.Items)
                {
                    sb.AppendLine($"{data.Id.ToString()}|{data.Images(jellyfinServer).FirstOrDefault(x => x.Key == DataEntities.JellyfinModels.ImageType.Primary).Value}|{data.Name}|{data.Type}|`");
                }

                return Content(sb.ToString());
            } 
        }

        [HttpGet("GetSessions")]
        public IActionResult GetSessions()
        {
            var sessionBLL = new Session(ServiceProvider);
            var sessions = sessionBLL.GetSessions(new RESTClient.Requests.GetSessionsRequest()).Result;

            return Json(sessions);
        }
    }
}
