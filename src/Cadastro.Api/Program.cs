using Cadastro.Api.Configuration;
using Cadastro.Infra.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<CadastroContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadastroConStr"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapperConfig();

//builder.Services.AddControllers();
builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.


//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
app.UseApiConfig(app.Environment);

app.UseSwaggerConfig();

app.Run();
