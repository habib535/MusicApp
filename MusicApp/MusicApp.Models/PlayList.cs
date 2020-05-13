using System;
using System.Collections.Generic;

namespace MusicApp.Models
{
    public class PlayList
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishedAt { get; set; }
        public long? ItemsCount { get; set; }
        public List<Song> Songs { get; set; }
    }
}
