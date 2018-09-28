namespace BookmarksAPI.Services.Interfaces
{
    using DataWorkShop.Entities;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<User> GetByIdAsync(string id);

        Task<User> CreateAsync(User user, string password);

        Task<User> AuthenticateAsync(string userName, string password);
    }
}
