using SIADG.Api.Exceptions;
using SIADG.Api.Security;
using SIADG.Application.Extensions;
using SIADG.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceContextService(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ForbiddenMiddleware>();
app.MapControllers();

app.Run();
