using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Services
{
    public class TodoItemService : ITodoItemService
    {
        private ApplicationDbContext _dbContext;

        public TodoItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TodoItem>> AddTodoItem(TodoItem item)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<List<TodoItem>> DeleteTodoItem(int id)
        {
            var item = await _dbContext.Items.FindAsync(id);
            if (item is null)
            {
                return null;
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Items.ToListAsync();
        }

        public async Task<List<TodoItem>> GetAllTodoItems(int page = 1)
        {
            int pageSize = 3;
            var items = await _dbContext.Items.ToListAsync();
            var itemsPerPage = items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();         

            return itemsPerPage;
        }

        public async Task<TodoItem> GetSingleTodoItem(int id)
        {
            var item = await _dbContext.Items.FindAsync(id);
            if (item is null)
            {
                return null;
            }

            return item;
        }
    }
}
