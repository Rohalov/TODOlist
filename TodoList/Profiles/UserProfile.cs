using AutoMapper;

namespace TodoList.Provider
{
    public class UserProfile : Profile
    {
        protected UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}
