namespace DataWorkShop.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EEFApps.ApiInstructions.BaseEntities.Entities;

    public class Bookmark : BaseEntityWithUserContext<string, string>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Link { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
