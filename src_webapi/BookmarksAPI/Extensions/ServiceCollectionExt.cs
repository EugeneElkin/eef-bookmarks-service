namespace BookmarksAPI.Extensions
{
    using AutoMapper;
    using BookmarksAPI.Services;
    using BookmarksAPI.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExt
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
        }

        public static void AddUserService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddTokenService(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
        }
    }
}
