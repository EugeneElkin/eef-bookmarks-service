namespace BookmarksAPI.Extensions
{
    using AutoMapper;
    using BookmarksAPI.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExt
    {
        public static void AddAutoMapper(this IServiceCollection service)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
        }
    }
}
