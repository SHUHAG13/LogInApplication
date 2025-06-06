
using LoginApplication.API.Data;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("conString")));

            builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAngularOrigin", builder =>
                        builder.WithOrigins("http://localhost:4200")  // URL of the Angular frontend
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials());
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAngularOrigin"); // Enable CORS for the Angular frontend

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
