namespace BookmarksAPI.Services
{
    using DataWorkShop.Entities;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<User> GetByIdAsync(string id);

        Task<User> CreateAsync(User user, string password);
    }
}
