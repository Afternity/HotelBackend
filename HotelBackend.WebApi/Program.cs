using HotelBackend.Application.DependencyInjections;
using HotelBackend.Persistence.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddDatabaseService(builder.Configuration);

var app = builder.Build();

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

app.Map("/error", (HttpContext context) =>
{
    var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
    return Results.Problem(
        title: "Произошла ошибка",
        detail: exception?.Error?.Message,
        statusCode: 500);
});

app.MapControllers();

app.Run();