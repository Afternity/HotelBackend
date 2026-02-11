using FluentValidation;
using HotelBackend.Application.DependencyInjections;
using HotelBackend.Persistence.DependencyInjections;
using Microsoft.OpenApi.Models;
using HotelBackend.Identity.DependecyInjections;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithExposedHeaders("Content-Disposition");
        });
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelBackend", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT", 
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelBackend API v1");
        options.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseExceptionHandler("/error");
}

app.Map("/error", (HttpContext context, ILogger<Program> logger, IWebHostEnvironment env) =>
{
    var exception = context.Features
        .Get<IExceptionHandlerFeature>()?.Error;

    if (exception is ValidationException validationException)
    {
        return Results.ValidationProblem(
            validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()),
            statusCode: 400);
    }

    logger.LogError(exception, "Необработанное исключение");

    return Results.Problem(
        title: "Внутренняя ошибка сервера",
        detail: env.IsDevelopment() ? exception?.Message : null,
        statusCode: 500);
});

app.MapControllers();

app.Run();