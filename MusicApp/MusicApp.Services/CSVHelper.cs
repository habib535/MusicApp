using CsvHelper;
using MusicApp.Models;
using MusicApp.Services.Mappers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MusicApp.Services
{
    public class CSVHelper
    {
        public void Export(IList<PlayList> playLists)
        {
            using (var writer = new StreamWriter(@"..\..\playlists.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<PlayListMapper>();
                csv.Configuration.RegisterClassMap<SongMapper>();
                foreach (var playList in playLists)
                {
                    csv.WriteHeader<PlayList>();
                    csv.NextRecord();
                    csv.WriteRecord(playList);
                    csv.NextRecord();
                    csv.WriteHeader<Song>();
                    csv.NextRecord();
                    foreach (var song in playList.Songs)
                    {
                        csv.WriteRecord(song);
                        csv.NextRecord();
                    }
                }
            }
        }
    }
}
