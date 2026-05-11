using FitCore.Application.Interfaces;
using FitCore.Application.Services;
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

            // Add controllers
            builder.Services.AddControllers();

            // Register DbContext
            builder.Services.AddDbContext<FitCoreDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register services
            builder.Services.AddScoped<IClientService, ClientService>();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IMembershipPlanService, MembershipPlanService>();

            builder.Services.AddScoped<IMembershipService, MembershipService>();

            var app = builder.Build();

            // Configure HTTP pipeline
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