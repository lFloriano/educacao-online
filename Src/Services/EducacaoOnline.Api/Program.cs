using EducacaoOnline.Api.Configurations;
using EducacaoOnline.Core.Data;
using EducacaoOnline.Core.Filters;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); });
builder.Services.AddAuthConfig(builder.Configuration);
builder.AddSwagger();
builder.AddDatabaseSelector();
builder.Services.AddIdentityConfig();

builder.Services.AddApiConfig(builder.Configuration);
builder.AddMediator();
builder.AddAutoMapper();

builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UsarSwagger();
    app.UseMigrationsAndSeed();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
