namespace DataWorkShop.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using ApiInstructions.BaseEntities.Entities;

    public class Bookmark : BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
