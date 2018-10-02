namespace DataWorkShop.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using EEFApps.ApiInstructions.BaseEntities.Entities;

    public class Bookmark : BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
