namespace DataWorkShop.Entities
{
    using System;
    using ApiInstructions.BaseEntities.Entities;

    public class Bookmark: BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
