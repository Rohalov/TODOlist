using TodoList.Data;
using TodoList.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        using var dbContext = new ApplicationDbContext();

        dbContext.Database.EnsureCreated();


        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ITodoItemService, TodoItemService>();
        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);


        var app = builder.Build();
        

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
