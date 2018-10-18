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

            CreateMap<Bookmark, BookmarkViewModel>()
                .ForSourceMember(sm => sm.CategoryId, opt => opt.Ignore());

            CreateMap<NewCategoryViewModel, Category>();

            CreateMap<NewBookmarkViewModel, Bookmark>();

            CreateMap<NewUserViewModel, User>()
                .ForSourceMember(sm => sm.Password, opt => opt.Ignore());
        }
    }
}
