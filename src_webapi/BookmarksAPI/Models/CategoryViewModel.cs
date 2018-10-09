
using System.Collections.Generic;

namespace BookmarksAPI.Models
{
    public class CategoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookmarkViewModel> Bookmarks { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
