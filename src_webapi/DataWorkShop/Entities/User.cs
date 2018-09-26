namespace DataWorkShop.Entities
{
    using EEFApps.ApiInstructions.BaseEntities.Entities;

    public class User: BaseEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
