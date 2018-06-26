namespace DataWorkShop.Entities
{
    using ApiInstructions.BaseEntities.Entities;
    using System.Collections.Generic;

    public class Category: BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
