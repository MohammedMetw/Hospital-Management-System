using System.Text;
using FluentValidation;
using Hangfire;
using Hospital.API.Middleware;
using Hospital.Application.Common.Behaviors;
using Hospital.Application.Common.Setting;
using Hospital.Application.Features.Doctor.Command;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;
using Hospital.Infrastructure.Repositories;
using Hospital.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Hospital.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- Add services to the container ---
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // --- Database Configuration ---
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // --- Identity ---
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                 .AddDefaultTokenProviders(); ;

            // --- MediatR and Validation Configuration ---
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateDoctorCommand).Assembly));
            builder.Services.AddValidatorsFromAssembly(typeof(CreateDoctorCommandValidator).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // -- Handgire Configuration (background jobs)---
            builder.Services.AddHangfire(config => config
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                 .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHttpClient<IAIChatService, AIChatService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["AiSettings:OpenRouterBaseUrl"]);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", builder.Configuration["AiSettings:OpenRouterApiKey"]);
                client.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost");
            });
            builder.Services.AddHangfireServer();
            builder.Services.AddHttpContextAccessor();
           
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            // --- Repositories and Services ---
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<INurseRepository, NurseRepository>();
            builder.Services.AddScoped<IPharmacistRepository, PharmacistRepository>();
            builder.Services.AddScoped<IAccountantRepository, AccountantRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IMedicineInventoryRepository, MedicineInventoryRepository>();
            builder.Services.AddScoped<IStockAdjustmentRepository, StockAdjustmentRepository>();
            builder.Services.AddScoped<IDispenseLogRepository, DispenseLogRepository>();
            builder.Services.AddScoped<IPrescriptionRepository,PrescriptionRepository >();
            builder.Services.AddScoped<IPrescribedMedicineRepository,PrescribedMedicineRepository>();
            builder.Services.AddScoped<IEmailService , EmailService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserContextService, UserContextService>();
            builder.Services.AddScoped<IAIChatService, AIChatService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            // --- JWT Authentication ---
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Hospital Management System",
                    Description = "API.Net"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });



            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();

            // --- Build the app ---
            var app = builder.Build();

           
            // Development environment configuration
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Swagger middleware - only in development
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital Management System API v1");
                    c.RoutePrefix = "swagger"; // Swagger will be available at /swagger
                    c.DocumentTitle = "API.Net";
                });
            }


            // --- Seed Roles ---
            using (var scope = app.Services.CreateScope())
            {
                await RolesSeeder.SeedRolesAsync(scope.ServiceProvider);
            }

            app.UseHangfireDashboard();
            // app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
