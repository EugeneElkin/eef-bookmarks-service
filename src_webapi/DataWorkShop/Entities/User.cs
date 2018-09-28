namespace DataWorkShop.Entities
{
    using DataWorkShop.Entities.Structures;
    using EEFApps.ApiInstructions.BaseEntities.Entities;

    public class User: BaseEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserStatusType Status { get; set; }
    }
}
