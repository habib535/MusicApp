using CsvHelper.Configuration;
using MusicApp.Models;
using System.Globalization;

namespace MusicApp.Services.Mappers
{
    internal sealed class PlayListMapper : ClassMap<PlayList>
    {
        public PlayListMapper()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Ignore();
            Map(m => m.ItemsCount).Ignore();
            Map(m => m.Songs).Ignore();
        }
    }
}
