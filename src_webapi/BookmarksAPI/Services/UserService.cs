namespace BookmarksAPI.Services
{
    using ApiInstructions.DataInstructions.Instructions;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private BookmarksDbContext context;

        public UserService(BookmarksDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var user = await new ReceivingInstruction<User, string>(this.context, id).Execute();
            return user;
        }
    }
}
