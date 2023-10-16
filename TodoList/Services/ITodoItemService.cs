﻿using TodoList.Models.Entities;

namespace TodoList.Services
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetAllTodoItems();
        Task<TodoItem> GetSingleTodoItem(int id);
        Task<List<TodoItem>> AddTodoItem(TodoItem item);
        Task<List<TodoItem>> DeleteTodoItem(int id);
    }
}
