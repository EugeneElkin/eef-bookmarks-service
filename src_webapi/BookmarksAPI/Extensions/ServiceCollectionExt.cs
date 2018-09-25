namespace BookmarksAPI.Extensions
{
    using AutoMapper;
    using BookmarksAPI.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExt
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
        }

        public static void AddUserService(this IServiceCollection services)
        {
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }
    }
}
