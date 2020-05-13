using CsvHelper.Configuration;
using MusicApp.Models;
using System.Globalization;

namespace MusicApp.Services.Mappers
{
    internal sealed class SongMapper : ClassMap<Song>
    {
        public SongMapper()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Ignore();
            Map(m => m.Id).Ignore();
            Map(m => m.ETag).Ignore();
            Map(m => m.PlaylistId).Ignore();
            Map(m => m.VideoId).Ignore();
            Map(m => m.ChannelId).Ignore();
            Map(m => m.ContentType).Ignore();
        }
    }
}
