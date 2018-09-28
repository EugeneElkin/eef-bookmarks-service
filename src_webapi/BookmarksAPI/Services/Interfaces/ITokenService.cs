namespace BookmarksAPI.Services.Interfaces
{
    using DataWorkShop.Entities;

    public interface ITokenService
    {
        string CreateAccessToken(User user);
    }
}
