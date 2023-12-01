using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using TodoList.Data;
using TodoList.Models.Entities;
using TodoList.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        using var dbContext = new ApplicationDbContext();

        dbContext.Database.EnsureCreated();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            option.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        builder.Services.AddScoped<ITodoItemService, TodoItemService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(o => o.SignIn.RequireConfirmedAccount = true)
            .AddDefaultTokenProviders();

        builder.Services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
        builder.Services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
        builder.Services.AddTransient<UserTable>();
        builder.Services.AddTransient<RoleTable>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("AppSettings:Token").Value!))
                };
            });


        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireUserRole",
                policy => policy.RequireRole("User"));

            options.AddPolicy("RequireAdminRole",
                policy => policy.RequireRole("Admin"));
        });


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