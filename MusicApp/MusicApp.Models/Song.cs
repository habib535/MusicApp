using System;

namespace MusicApp.Models
{
    public class Song
    {
        public string __ { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishedAt { get; set; }
        public ContentType ContentType { get; set; }
        public string ChannelId { get; set; }
        public string Kind { get; set; }
        public string PlaylistId { get; set; }
        public string VideoId { get; set; }
        public string ETag { get; set; }
    }
}
