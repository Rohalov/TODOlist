using AutoMapper;
using TodoList.Models.DTO;
using TodoList.Models.Entities;

namespace TodoList.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<ApplicationRole, RoleDTO>();
            CreateMap<RoleDTO, ApplicationRole>();
        }
    }
}
