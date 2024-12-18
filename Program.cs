using MenuApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging; // Import required namespace

namespace MenuApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddConsole(); // Add console logging
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure dependency injection for DbContext
            builder.Services.AddDbContext<MenuDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));






            // Add HttpLogging service with specific configuration
            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders |
                                        HttpLoggingFields.ResponsePropertiesAndHeaders |
                                        HttpLoggingFields.RequestBody |
                                        HttpLoggingFields.ResponseBody;
                options.RequestBodyLogLimit = 4096; // Limit request body logging to 4 KB
                options.ResponseBodyLogLimit = 4096; // Limit response body logging to 4 KB
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontendOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "http://192.168.1.2:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Enable logging
            app.UseHttpLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowFrontendOrigins"); // Apply the policy by name
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
