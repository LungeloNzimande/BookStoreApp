using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.User;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<ApiUser, UserRegisterDto>();
        }
    }
}
