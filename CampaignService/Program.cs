using CampaignService_Repository;
using CampaignService_Repository.Interfaces;
using CampaignService_Repository.Models;
using CampaignService_Repository.Repositories;
using CampaignService_Service.Helpers;
using CampaignService_Service.Interfaces;
using CampaignService_Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:5055")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
#endregion

#region Database Context
var connString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);

dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(dataSource, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null);
    })
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    // Enable detailed errors for debugging
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});
#endregion

builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Dependency Injection (DI)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceProviders, ServiceProviders>();

// Repositories
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<ICampaignVehicleRepository, CampaignVehicleRepository>();
builder.Services.AddScoped<ICampaignAppointmentRepository, CampaignAppointmentRepository>();

// Services
builder.Services.AddScoped<ICampaignService, CampaignService_Service.Services.CampaignService>();
builder.Services.AddScoped<ICampaignVehicleService, CampaignVehicleService>();
builder.Services.AddScoped<ICampaignAppointmentService, CampaignAppointmentService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
#endregion

#region Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OEMEV Campaign Service API",
        Version = "v1",
        Description = "OEMEV Campaign Service API for managing recall campaigns and appointments"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });

    // Enable annotations if you have the Swashbuckle.Annotations package
    // c.EnableAnnotations();
});

// Remove or comment out this duplicate SwaggerGen configuration
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();