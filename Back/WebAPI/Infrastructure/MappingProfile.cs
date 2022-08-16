using AutoMapper;
using DAL.Models;
using DAL.ViewModels;

namespace WebAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();

            CreateMap<Role, RoleVM>();
            CreateMap<RoleVM, Role>();
        }
    }
}
