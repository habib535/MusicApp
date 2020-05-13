using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MusicApp.Services
{
    public sealed class YoutubeServiceClient
    {
        public YouTubeService YouTubeService { get; set; }
        public static string DefaultChannelId { get; set; }

        public YoutubeServiceClient()
        {
            YouTubeService = Task.Run(async () => await AuthenticateUserServiceAsync()).Result;
        }

        private async Task<YouTubeService> AuthenticateUserServiceAsync()
        {
            var folder = System.IO.Path.GetFullPath(@"..\..\");
            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube },
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore(folder)
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Music App"
            });

            return youtubeService;
        }
    }
}
