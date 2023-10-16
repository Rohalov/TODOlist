using AutoMapper;
using TodoList.Models.DTO;
using TodoList.Models.Entities;

namespace TodoList.Profiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDto>();
            CreateMap<TodoItemDto, TodoItem>();
        }
    }
}
