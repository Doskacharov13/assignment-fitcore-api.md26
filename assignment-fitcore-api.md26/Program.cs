using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace assignment_fitcore_api.md26
{
    /// <summary>
    /// Entry point of the FitCore API application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method configuring the application.
        /// </summary>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Register DbContext
            builder.Services.AddDbContext<FitCoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
}