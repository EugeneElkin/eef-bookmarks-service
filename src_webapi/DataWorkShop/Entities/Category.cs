namespace DataWorkShop.Entities
{    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using EEFApps.ApiInstructions.BaseEntities.Entities;

    public class Category : BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
    }
}
