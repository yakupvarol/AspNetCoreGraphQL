using AspNetCoreGraphQL.DTO.User;
using AspNetCoreGraphQL.Entities;
using AutoMapper;

namespace AspNetCoreGraphQL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserListResponseDTO, User>().ReverseMap().ForMember(dest => dest.FullName, from => from.MapFrom(s => s.FirstName + " " + s.LastName));
            CreateMap<UserGroup, UserGroupMapperUserDTO>().ReverseMap();
            CreateMap<UserType, UserMapperUserTypeDTO>().ReverseMap();
        }
    }
}

