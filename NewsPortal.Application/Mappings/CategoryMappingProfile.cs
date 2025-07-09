using AutoMapper;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.DTO.Read;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
