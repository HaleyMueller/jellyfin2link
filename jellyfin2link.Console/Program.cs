using jellyfin2link.BusinessLogicLayer;
using jellyfin2link.RESTClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main(string[] args)
    {
        // Setup DI container
        var services = new ServiceCollection();

        // Add logging (console provider)
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        // Build provider
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        string jellyfinUserGUID = configuration["JellyfinUserGUID"];

        //TESTING FOR DEBUG BELOW


        var input = "";

        var playstateBLL = new jellyfin2link.BusinessLogicLayer.Playstate(serviceProvider);
        var itemBLL = new jellyfin2link.BusinessLogicLayer.Items(serviceProvider);
        var mediaInfoBLL = new jellyfin2link.BusinessLogicLayer.MediaInfo(serviceProvider);
        var sessionBLL = new jellyfin2link.BusinessLogicLayer.Session(serviceProvider);

        var user = new Guid(jellyfinUserGUID);

        
        string itemID = "9e119ea6-b326-4c20-44e1-46873d786828";


        var request = new jellyfin2link.RESTClient.Requests.GetItemsRequest() { Recursive = false, Ids = new Guid[] { new Guid(itemID) }, UserId = user };
        var movieFolder = itemBLL.GetItems(request).Result;

        var playbackInfo = mediaInfoBLL.GetPlaybackInfo(itemID, new jellyfin2link.DataEntities.JellyfinModels.PlaybackInfoDto() { UserId = user }).Result;

        string mediaSourceId = playbackInfo.Data.MediaSources.FirstOrDefault().Id;
        string sessionID = "";
        string playsessionID = playbackInfo.Data.PlaySessionId;

        while (input != "exit")
        {
            input = Console.ReadLine();
            switch (input.Split(' ').First()) 
            {
                case "start":
                    var playStart = playstateBLL.ReportPlaybackStart(new jellyfin2link.DataEntities.JellyfinModels.PlaybackStartInfo()
                    {
                        PlaySessionId = playsessionID,
                        ItemId = new Guid(itemID),
                        PlayMethod = jellyfin2link.DataEntities.JellyfinModels.PlayMethod.DirectPlay,
                        PlaybackOrder = jellyfin2link.DataEntities.JellyfinModels.PlaybackOrder.Default,
                        MediaSourceId = mediaSourceId,

                    }).Result;

                    Console.WriteLine(playStart.HasError);

                    break;
                case "stop":
                    var _ = playstateBLL.ReportPlaybackStopped(new jellyfin2link.DataEntities.JellyfinModels.PlaybackStopInfo()
                    {
                        ItemId = new Guid(itemID),
                        MediaSourceId = mediaSourceId,
                        PlaySessionId = playsessionID,
                        //SessionId = sessionID
                    }).Result; 
                    Console.WriteLine(_.HasError);
                    break;
                case "progress":
                    var __ = playstateBLL.ReportPlaybackProgress(new jellyfin2link.DataEntities.JellyfinModels.PlaybackProgressInfo()
                    {
                        ItemId = new Guid(itemID),
                        MediaSourceId = mediaSourceId,
                        PlaySessionId = playsessionID,
                        PlayMethod = jellyfin2link.DataEntities.JellyfinModels.PlayMethod.DirectPlay,
                        //SessionId = sessionID,
                        PositionTicks = long.Parse(input.Split(' ').Last())
                    }).Result;
                    Console.WriteLine(__.HasError);
                    break;
                case "sessioncommand":
                    //var _t_ = sessionBLL.IssuePlaystatePause(input.Split(' ').Last(), playsessionID).Result;
                    //var _t_ = sessionBLL.IssuePlaystateCommandToClient(sessionID, input.Split(' ').Last(), 69000).Result;

                    var arguments = new Dictionary<string, string>
                        {
                            { "Command", "Pause" },
                        };

                    var _t_ = sessionBLL.SendFullGeneralCommand(sessionID, new jellyfin2link.DataEntities.JellyfinModels.GeneralCommand(arguments)
                    {
                        Name = jellyfin2link.DataEntities.JellyfinModels.GeneralCommandType.PlayState
                    }).Result;
                    
                    Console.WriteLine(_t_.HasError);
                    break;
                case "sessions":
                    var sessions = sessionBLL.GetSessions(new jellyfin2link.RESTClient.Requests.GetSessionsRequest()).Result;
                    Console.WriteLine(sessions.Data.Count);
                    break;
                case "sessionget":
                    var sessionsGet = sessionBLL.GetSessions(new jellyfin2link.RESTClient.Requests.GetSessionsRequest()).Result;

                    var activeSession = sessionsGet.Data.Where(x => x.IsActive && x.DeviceId == "72479166fa894409b7e8942c1f239270");

                    if (activeSession == null)
                    {
                        Console.WriteLine("No active sessions");
                    }
                    else
                    {
                        var currentSession = activeSession.FirstOrDefault();

                        sessionID = currentSession.Id;

                        Console.WriteLine($"{currentSession.IsActive} {currentSession.PlayState.IsPaused} {currentSession.PlayState.PositionTicks}");
                    }

                        
                    break;
            }
        }


        var userBLL = new Users(serviceProvider);
        var users = userBLL.GetAllUsers().Result;

        //var itemBLL = new jellyfin2link.BusinessLogicLayer.Items(serviceProvider);
        //var mediaInfoBLL = new jellyfin2link.BusinessLogicLayer.MediaInfo(serviceProvider);

        //var movieFolder = itemBLL.GetItems(new jellyfin2link.RESTClient.Requests.GetItemsRequest() { Recursive = false, ParentId = new Guid("f7ff0c0d-c2f4-f1b6-6ae1-382a87aa9ae1") }).Result;

        var movie = movieFolder.Data.Items.FirstOrDefault(x => x.Type == jellyfin2link.DataEntities.JellyfinModels.BaseItemKind.Movie && x.Name.Contains("Imitation Game"));

        var mediaInfo = mediaInfoBLL.GetPlaybackInfo(movie.Id.ToString(), new jellyfin2link.DataEntities.JellyfinModels.PlaybackInfoDto() {  }).Result;

        var subtitles = mediaInfo.Data.MediaSources.FirstOrDefault().MediaStreams.Where(x => x.Type == jellyfin2link.DataEntities.JellyfinModels.MediaStreamType.Subtitle && x.Language == "eng").ToList();

        var subtitleURL = "need to copy over";

        var directURL = mediaInfoBLL.GetUrl(mediaInfo.Data, new jellyfin2link.RESTClient.Requests.GetVideoStreamQuery() { Static = true });
        var transcodedURL = mediaInfoBLL.GetUrl(mediaInfo.Data, 
            new jellyfin2link.RESTClient.Requests.GetVideoStreamQuery() { 
                Static = false, 
                VideoCodec = "h264", 
                SubtitleMethod = jellyfin2link.DataEntities.JellyfinModels.SubtitleDeliveryMethod.Encode, 
                SubtitleStreamIndex = subtitles.FirstOrDefault().Index 
            });



    }
}