
namespace DataWorkShop.Entities
{
    using System.ComponentModel.DataAnnotations;
    using ApiInstructions.BaseEntities.Entities.Interfaces;

    public class CategoryCategory : IVersionedEntity
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string ChildCategoryId { get; set; }
        public Category ChildCategory { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
