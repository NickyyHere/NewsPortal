using AutoMapper;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.DTO.Read;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Mappings
{
    public class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<CreateAtricleDTO, Article>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Slug, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());
        }
    }
}
