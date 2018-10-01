namespace BookmarksAPI.Services.Interfaces
{
    using BookmarksAPI.Services.Structures;
    using DataWorkShop.Entities;

    public interface ITokenService
    {
        JsonWebToken CreateAccessToken(User user);
        JsonWebToken UpdateAccessToken(string refreshToken);
    }
}
