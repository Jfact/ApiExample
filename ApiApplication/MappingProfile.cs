using AutoMapper;
using EntitiesLibrary.Entities;
using EntitiesLibrary.Entities.DataTransfer;

namespace ApiApplication
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserEditDto, User>();
            CreateMap<UserCreateDto, User>();

        }
    }
}
