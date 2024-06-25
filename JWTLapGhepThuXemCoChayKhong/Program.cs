using JWTLapGhepThuXemCoChayKhong.Authorization;
using JWTLapGhepThuXemCoChayKhong.Helpers;
using JWTLapGhepThuXemCoChayKhong.Services;

namespace JWTLapGhepThuXemCoChayKhong
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
          
            //builder.Services.AddEndpointsApiExplorer();

            var services = builder.Services;
            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings object
            services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();





            var app = builder.Build();


            // configure HTTP request pipeline
            {
                // global cors policy
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                // custom jwt auth middleware
                app.UseMiddleware<JwtMiddleware>();

                app.MapControllers();
            }

            app.Run("http://localhost:4000");
        }
    }
}
