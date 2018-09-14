namespace DataWorkShop.Entities
{
    using ApiInstructions.BaseEntities.Entities.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public class CategoryBookmark : IVersionedEntity
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string BookmarkId { get; set; }
        public Bookmark Bookmark { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
