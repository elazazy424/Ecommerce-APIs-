using Ecommerce.Api.Middleware;
using Ecommerce.BLL.Interfaces;
using Ecommerce.BLL.Repository;
using Ecommerce.BLL.Services;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Ecommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = new CustomPropertyNamingPolicy();
             });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            var jwt = builder.Configuration.GetSection("Token");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["key"])),
                        ValidIssuer = jwt["Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(options);
            });
            
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            //34an y3ml map ll static files (images)
            app.UseStaticFiles();

            //use cors
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var logger = services.GetRequiredService<ILogger<Program>>();
            try
            {
                await context.Database.MigrateAsync();
                await identityContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context);
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during migration");
            }

            app.Run();
        }
    }
}
