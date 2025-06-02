
using System.Text.Json.Serialization;
using System.Text.Json;
using Serilog;
using ChatMessageWebApi.Middlewares;
using ChatMessageWebApi.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ChatMessageWebApi.Mappings;
using ChatMessageWebApi.Repositories.Interfaces;
using ChatMessageWebApi.Repositories;
using ChatMessageWebApi.Services.Interfaces;
using ChatMessageWebApi.Services;

namespace ChatMessageWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string serviceName = "chat-message-api";
            const string corsPolicy = "myAllowSpecificOrigins";
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            Console.WriteLine(JsonSerializer.Serialize($"DbConnectionString: {builder.Configuration.GetConnectionString("DbConnectionString")}, ENVIRONMENT: {builder.Configuration.GetValue<string>("ENVIRONMENT")}"));
            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
               .WriteTo.Seq(builder.Configuration["Seq:Endpoint"])
                );

            builder.Logging.ClearProviders();

            builder.Services
                    .AddCors(options =>
                    {
                        options.AddPolicy(corsPolicy,
                            builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            );
                    });

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = serviceName,
                    Version = "V1"
                });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
            }
              );

            builder.Services.
                    AddOpenTelemetry()
                    .ConfigureResource(resource => resource.AddService(serviceName))
                    .WithTracing(tracing =>
                    {
                        tracing.AddAspNetCoreInstrumentation()
                                .AddHttpClientInstrumentation()
                                .AddSqlClientInstrumentation(o => o.SetDbStatementForText = true);

                        tracing.AddOtlpExporter();
                    });

            //builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IRepostRepository, RepostRepository>();
            //builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IRepostService, RepostService>();
            builder.Services.AddLogging();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHealthChecks();

            WebApplication app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(corsPolicy);
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
