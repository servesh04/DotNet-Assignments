using WebAPIAssignment.Services;

namespace WebAPIAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add Controllers
            builder.Services.AddControllers();

            // 2. Register Swagger Generators
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 3. Register your Dependency Injection Service
            builder.Services.AddSingleton<ILeaveRequestService, LeaveRequestService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // 4. Enable Swagger JSON generation and Visual Web UI
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Leave Request API v1");
                    options.RoutePrefix = "swagger"; // Opens at yourURL/swagger
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}