using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using TodoList.Data;
using TodoList.Models.Entities;
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
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(o => o.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
        builder.Services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
        builder.Services.AddTransient<UserTable>();
        builder.Services.AddTransient<RoleTable>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);


        builder.Services.AddAuthorization(options =>
            options.AddPolicy("RequireUserRole",
            policy => policy.RequireRole("User")));


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
