using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models.DTO;
using TodoList.Models.Entities;
using TodoList.Services;

namespace TodoList.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {

        private readonly ITodoItemService _todoItemService;

        private readonly IMapper _mapper;

        public TodoItemsController(ITodoItemService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoItem>>> GetAllTodoItems(int page)
        {   
            var items = await _todoItemService.GetAllTodoItems(page);
            return Ok(_mapper.Map<List<TodoItemDto>>(items));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TodoItem>>> GetSingleTodoItem(int id)
        {
            var item = await _todoItemService.GetSingleTodoItem(id);
            if (item is null)
            {
                return NotFound("Sorry, but this item doesn't exist.");
            }

            return Ok(_mapper.Map<TodoItemDto>(item));
        } 

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<List<TodoItem>>> AddTodoItem(TodoItemDto newItem)
        {
            var item = _mapper.Map<TodoItem>(newItem);
            await _todoItemService.AddTodoItem(item);
            return Created($"~api/items/{item.Id}", item);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TodoItem>>> DeleteTodoItem(int id)
        {
            var item = await _todoItemService.DeleteTodoItem(id);
            if (item is null)
            {
                return NotFound("Sorry, but this item doesn't exist.");
            }

            return Ok(_mapper.Map<List<TodoItemDto>>(item));
        }
    }
}
