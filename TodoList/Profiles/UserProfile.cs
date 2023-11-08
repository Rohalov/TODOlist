using AutoMapper;
using TodoList.Models.DTO;
using TodoList.Models.Entities;

namespace TodoList.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}
