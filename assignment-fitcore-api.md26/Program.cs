using FitCore.API.GraphQL;
using FitCore.API.Hubs;
using FitCore.API.Middleware;
using FitCore.Application.Interfaces;
using FitCore.Application.Services;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FitCore.API.Hubs;
using FitCore.API.Services;
using FitCore.Application.Interfaces;

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

            builder.Services.AddScoped<ITrainerService, TrainerService>();

            builder.Services.AddScoped<IRoomService, RoomService>();

            builder.Services.AddScoped<IWorkoutClassService, WorkoutClassService>();

            builder.Services.AddScoped<IReservationService, ReservationService>();

            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddScoped<IVisitService, VisitService>();

            builder.Services.AddSignalR();

            builder.Services.AddScoped<INotificationService, NotificationService>();


            builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();
            var app = builder.Build();

            // Configure HTTP pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGraphQL();

            app.MapHub<NotificationHub>("/notificationHub");

            app.Run();
        }
    }
}