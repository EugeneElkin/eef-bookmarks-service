namespace BookmarksAPI.Services
{
    using AutoMapper;
    using BookmarksAPI.Models;
    using DataWorkShop.Entities;

    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryViewModel>()
                .ForSourceMember(sm => sm.RowVersion, opt => opt.Ignore())
                .ForSourceMember(sm => sm.Parent, opt => opt.Ignore());
        }
    }
}
