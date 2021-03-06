﻿namespace BookmarksAPI.Models
{
    public class BookmarkViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
