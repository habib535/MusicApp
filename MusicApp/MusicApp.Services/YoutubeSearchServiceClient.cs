using MusicApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicApp.Services
{
    public class YoutubeSearchServiceClient
    {
        YoutubeServiceClient serviceClient;
        public YoutubeSearchServiceClient()
        {
            serviceClient = new YoutubeServiceClient();
        }

        public async Task<List<Song>> Search(string searchTerm)
        {
            var searchListRequest = serviceClient.YouTubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm; // search term.
            searchListRequest.MaxResults = 50;

            searchListRequest.VideoCategoryId = "10";   //music
            searchListRequest.Type = "video";   //mandatory as per api docs

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var searchItems = new List<Song>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        searchItems.Add(
                            new Song
                            {
                                Id = searchResult.Id.VideoId,
                                Title = searchResult.Snippet.Title,
                                Description = searchResult.Snippet.Description,
                                PublishedAt = searchResult.Snippet.PublishedAt,
                                ContentType = ContentType.Music,
                                //resource details
                                ChannelId = searchResult.Id.ChannelId,
                                Kind = searchResult.Id.Kind,
                                PlaylistId = searchResult.Id.PlaylistId,
                                VideoId = searchResult.Id.VideoId,
                                ETag = searchResult.Id.ETag,
                            });
                        break;
                }
            }

            return searchItems;
        }
    }
}
