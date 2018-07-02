namespace DataWorkShop.Entities
{
    using System.Collections.Generic;
    using ApiInstructions.BaseEntities.Entities;

    public class Bookmark: BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryBookmark> CategoryBookmarks { get; } = new List<CategoryBookmark>();
    }
}
