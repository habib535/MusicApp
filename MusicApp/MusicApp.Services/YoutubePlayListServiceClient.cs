using Google.Apis.YouTube.v3.Data;
using MusicApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Services
{
    public class YoutubePlayListServiceClient
    {
        YoutubeServiceClient serviceClient;

        public YoutubePlayListServiceClient()
        {
            serviceClient = new YoutubeServiceClient();
        }

        public async Task<List<PlayList>> MyPlayLists()
        {
            var playListRequest = serviceClient.YouTubeService.Playlists.List("snippet,contentDetails");
            playListRequest.MaxResults = 50;

            //await EnsureDefaultChannel();
            playListRequest.Mine = true;

            var playListResponse = await playListRequest.ExecuteAsync();

            var playLists = new List<PlayList>();

            foreach (var playList in playListResponse.Items)
            {
                playLists.Add(
                    new PlayList
                    {
                        Id = playList.Id,
                        Description = playList.Snippet.ChannelTitle,
                        Title = playList.Snippet.Title,
                        PublishedAt = playList.Snippet.PublishedAt,
                        ItemsCount = playList.ContentDetails.ItemCount
                    });
            }

            return playLists;
        }

        public async Task CreatePlayList(string title, string description)
        {
            var newPlaylist = new Playlist();
            newPlaylist.Snippet = new PlaylistSnippet();
            newPlaylist.Snippet.Title = title;
            newPlaylist.Snippet.Description = description;
            newPlaylist.Status = new PlaylistStatus();
            newPlaylist.Status.PrivacyStatus = "private";
            await serviceClient.YouTubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();
        }

        public async Task<List<Song>> FetchSongsFromPlayList(PlayList playList)
        {
            var playListItemRequest = serviceClient.YouTubeService.PlaylistItems.List("snippet,contentDetails");
            playListItemRequest.PlaylistId = playList.Id;
            var playListResponse = await playListItemRequest.ExecuteAsync();

            var songs = new List<Song>();

            foreach (var playListItem in playListResponse.Items)
            {
                songs.Add(
                    new Song
                    {
                        Id = playListItem.Id,
                        Description = playListItem.Snippet.ChannelTitle,
                        Title = playListItem.Snippet.Title,
                        PublishedAt = playListItem.Snippet.PublishedAt,
                        ChannelId = playListItem.Snippet.ResourceId.ChannelId,
                        ContentType = ContentType.Music,
                        ETag = playListItem.Snippet.ResourceId.ChannelId,
                        Kind = playListItem.Snippet.ResourceId.ChannelId,
                        PlaylistId = playListItem.Snippet.ResourceId.ChannelId,
                        VideoId = playListItem.Snippet.ResourceId.VideoId,
                    });
            }

            return songs;
        }

        public async Task UpdatePlayList()
        {
            await Task.CompletedTask;
        }
        public async Task RemovePlayList()
        {
            await Task.CompletedTask;
        }

        public async Task<string> AddSongToPlayList(PlayList playList, Song song)
        {
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = playList.Id;
            newPlaylistItem.Snippet.ResourceId = new ResourceId
            {
                ChannelId = song.ChannelId,
                Kind = song.Kind,
                ETag = song.ETag,
                PlaylistId = song.PlaylistId,
                VideoId = song.VideoId
            };
            newPlaylistItem = await serviceClient.YouTubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();

            return newPlaylistItem.Id;
        }

        public async Task RemoveSongFromPlayList()
        {
            await Task.CompletedTask;
        }

        private async Task EnsureDefaultChannel()
        {
            var channelsListRequest = serviceClient.YouTubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;

            var channelsListResponse = await channelsListRequest.ExecuteAsync();
            var channel = channelsListResponse.Items.First();
            YoutubeServiceClient.DefaultChannelId = channel.Id;
        }
    }
}
