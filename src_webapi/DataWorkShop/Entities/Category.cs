namespace DataWorkShop.Entities
{    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using ApiInstructions.BaseEntities.Entities;

    public class Category : BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        public ICollection<Category> Categories { get; } = new List<Category>();
        public ICollection<Bookmark> Bookmarks { get; } = new List<Bookmark>();
    }
}
